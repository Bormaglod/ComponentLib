//-----------------------------------------------------------------------
// <copyright file="AbstractBaseCollection.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
// <license>
//     This program is free software; you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation; either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <date>10.08.2011</date>
// <time>12:41</time>
// <summary>Defines the AbstractBaseCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    #region Using directives
    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Db4objects.Db4o.Linq;
    
    #endregion
    
    /// <summary>
    /// Description of AbstractBaseCollection.
    /// </summary>
    public abstract class AbstractBaseCollection<T> : IBaseCollection<T>, ITransaction, IExtCollection where T: BaseObject
    {
        private IDatabase database;
        private Dictionary<T, ObjectAction> transactCollection;
        private List<PropertyCollection<T>> objUpdated;
        private int transaction;
        private int updated;
        
        protected AbstractBaseCollection(IDatabase data)
        {
            this.updated = 0;
            this.database = data;
            this.transactCollection = new Dictionary<T, ObjectAction>();
            this.objUpdated = new List<PropertyCollection<T>>();
        }
        
        public event EventHandler<ObjectEventArgs<T>> ObjectModified;
        
        public IDatabase Database
        {
            get { return this.database; }
        }
        
        #region ITransaction implemented
        
        public void StartTransaction()
        {
            this.transaction++;
        }
        
        public void Commit()
        {
            this.transaction--;
            if (this.transaction == 0)
            {
                this.transactCollection.Clear();
                this.objUpdated.Clear();
            }
        }
        
        public void Rollback()
        {
            this.transaction--;
            if (this.transaction == 0)
            {
                foreach (T obj in this.transactCollection.Keys)
                {
                    switch (this.transactCollection[obj])
                    {
                        case ObjectAction.Add:
                            this.Remove(obj);
                            break;
                        case ObjectAction.Delete:
                            this.Update(obj);
                            break;
                        case ObjectAction.Modify:
                            this.RestoreObject(obj);
                            this.Update(obj);
                            break;
                    }
                }
                
                this.transactCollection.Clear();
                this.objUpdated.Clear();
            }
        }
        
        #endregion
        
        #region IBaseCollection<T> implemented
        
        /// <summary>
        /// Возвращает список объектов коллекции.
        /// </summary>
        public IEnumerable<T> Collection
        {
            get { return this.GetCollection(); }
        }
        
        /// <summary>
        /// Метод создает новый элемент коллекции и добавляет его в нее.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        public T Create()
        {
            Type t = typeof(T);
            ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
            T obj = ci.Invoke(null) as T;
            this.CreatePropertyObject(obj);
            Add(obj);
            return obj;
        }
        
        /// <summary>
        /// Метод добавляет объект в коллекцию. Добавляемый объект должен отсутсвовать в коллекции.
        /// </summary>
        /// <param name="addedElement">Добавляемый объект.</param>
        /// <seealso cref="BaseObject.IsNew"></seealso>
        public virtual void Add(T addedElement)
        {
            this.CheckObject(addedElement);
            
            if (!addedElement.IsNew)
            {
                throw new ObjectExistException(Strings.ObjectExist, addedElement);
            }
            
            addedElement.IsNew = false;
            if (this.transaction > 0)
            {
                this.transactCollection.Add(addedElement, ObjectAction.Add);
            }
            
            this.database.Data.Store(addedElement);
            this.OnObjectModified(addedElement, ObjectAction.Add);
        }
        
        /// <summary>
        /// Метод обновляет данные объекта в коллекции. Обновляемый объект должен присутствовать в коллекции.
        /// </summary>
        /// <param name="updatedElement">Обновляемый объект.</param>
        /// <seealso cref="BaseObject.IsNew"></seealso>
        public virtual void Update(T updatedElement)
        {
            this.CheckObject(updatedElement);
            
            if (updatedElement.IsNew)
            {
                throw new ObjectExistException(Strings.ObjectNotExist, updatedElement);
            }
            
            if (this.transaction > 0 && !this.transactCollection.ContainsKey(updatedElement))
            {
                this.transactCollection.Add(updatedElement, ObjectAction.Modify);
            }
            
            this.CascadeUpdate(updatedElement, ObjectAction.Modify);
            this.OnObjectModified(updatedElement, ObjectAction.Modify);
        }
        
        /// <summary>
        /// Метод удаляет объект из коллекции. Удаляемый объект должен присутствовать в коллекции.
        /// </summary>
        /// <param name="removedElement">Удаляемый объект.</param>
        /// <seealso cref="BaseObject.IsNew"></seealso>
        public virtual void Remove(T removedElement)
        {
            this.CheckObject(removedElement);

            if (removedElement.IsNew)
            {
                throw new ObjectExistException(Strings.ObjectNotExist, removedElement);
            }
            
            // Проверим возможность удаления объекта.
            foreach (IExtCollection collection in this.database.Collections)
            {
                if (!ReferenceEquals(this, collection))
                {
                    if (!collection.CanObjectRemoving(removedElement))
                    {
                        throw new ObjectExistException(Strings.ObjectUsed, removedElement);
                    }
                }
            }
            
            if (this.transaction > 0)
            {
                this.transactCollection.Add(removedElement, ObjectAction.Delete);
            }
            
            // Сообщим всем коллекциям базы данных об удалении элемента.
            foreach (IExtCollection collection in this.database.Collections)
            {
                if (!ReferenceEquals(this, collection))
                {
                    collection.ObjectRemoving(removedElement);
                }
            }
            
            this.CascadeUpdate(removedElement, ObjectAction.Delete);
            this.OnObjectModified(removedElement, ObjectAction.Delete);
        }
        
        #endregion
        
        #region IExtCollection implemented
        
        public long Count
        {
            get { return this.Collection.Count(); }
        }
        
        IEnumerable IExtCollection.Collection
        {
            get { return this.Collection; }
        }
        
        public bool IsEmpty
        {
            get { return this.Collection.FirstOrDefault() == null; }
        }
        
        public Type ContentsType
        {
            get { return typeof(T); }
        }
        
        /// <summary>
        /// Метод очищает коллекцию, удаляя все данные содержащиеся в ней.
        /// </summary>
        public virtual void Clear()
        {
            foreach (T obj in this)
            {
                this.Remove(obj);
            }
        }
        
        /// <summary>
        /// Метод устанавливает режим начала обновления коллекции. Каждый BeginUpdate должен быть
        /// в паре с EndUpdate.
        /// </summary>
        public void BeginUpdate()
        {
            this.updated++;
        }
        
        /// <summary>
        /// Метод устанавливает режим окончания обновления коллекции.
        /// </summary>
        public void EndUpdate()
        {
            if (this.updated > 0)
            {
                this.updated--;
            }
        }
        
        /// <summary>
        /// <para>Метод вызывается при удалении объекта из какой-либо коллекции IDatabase и предназначен
        /// для проверки возможности удаления этого объекта. Для запрещения удаления объекта
        /// коллекция должна перекрыть этот метод и вернуть false.</para>
        /// <para>Метод вызывается до удаления рбъекта и до вызова ObjectRemoving.</para>
        /// </summary>
        /// <param name="removingObject">Удаляемый объект.</param>
        public virtual bool CanObjectRemoving(object removingObject)
        {
            return true;
        }
        
        /// <summary>
        /// Метод вызывается при удалении объекта из какой-либо коллекции IDatabase. Вызов этого метода
        /// происходит после вызова CanObjectRemoving (причем он должен вернуть true для всех коллекций).
        /// В этом методе можно предусмотреть дополнительную обработку при удаление объектов из других
        /// коллекций.
        /// </summary>
        /// <param name="removingObject">Удаляемый объект.</param>
        public virtual void ObjectRemoving(object removingObject) {}
        
        #endregion
        
        protected virtual IEnumerable<T> GetCollection()
        {
            return from T c in this.database.Data select c;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return this.Collection.GetEnumerator();
        }
        
        public void StoreObject(T obj)
        {
            bool objExist = this.objUpdated.FirstOrDefault(u => u.Object == obj) != null;
            if (this.transaction > 0 && !obj.IsNew && !objExist)
            {
                PropertyCollection<T> pc = new PropertyCollection<T>(obj);
                
                Type content = obj.GetType();
                FieldInfo[] fi = content.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo prop in fi)
                {
                    object val = prop.GetValue(obj);
                    pc.AddProperty(prop.Name, val);
                }
                
                this.objUpdated.Add(pc);
            }
        }
        
        /// <summary>
        /// <para>Метод вызывается при создании элемента коллекции с помощью метода Create до добавления</para>
        /// <para>элемента в коллекцию.</para>
        /// <para>В этом методе можно заполнить необходимые свойства вновь созданного элемента до записи</para>
        /// <para>в коллекцию.</para>
        /// </summary>
        /// <param name="obj">Создаваемый объект подлежащий изменению.</param>
        protected virtual void CreatePropertyObject(T obj)
        {
        }
        
        private void OnObjectModified(T obj, ObjectAction action)
        {
            if (this.ObjectModified != null && this.updated == 0)
            {
                this.ObjectModified(this, new ObjectEventArgs<T>(obj, action, this.database.State));
            }
        }
        
        /// <summary>
        /// Проверка объекта obj. Если объект не существует (obj = null) или база данных не открыта,
        /// то генерируется исключение.
        /// </summary>
        /// <param name="obj">Проверяемый объект.</param>
        private void CheckObject(T obj)
        {
            if (obj == default(T))
            {
                throw new NullReferenceException(Strings.ObjectIsNull);
            }
            
            if (this.database.State == DatabaseState.Closed)
            {
                throw new DatabaseClosedException(Strings.DatabaseClose);
            }
        }
        
        private void UpdateObject(object obj, ObjectAction action)
        {
            switch (action)
            {
                case ObjectAction.Delete:
                    this.database.Data.Delete(obj);
                    break;
                case ObjectAction.Modify:
                    this.database.Data.Store(obj);
                    break;
                default:
                    throw new Exception("Invalid value for ObjectAction");
            }
        }
        
        private void RestoreObject(T obj)
        {
            PropertyCollection<T> pc = this.objUpdated.FirstOrDefault(o => o.Object == obj);
            if (pc != null)
            {
                Type content = obj.GetType();
                FieldInfo[] fi = content.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo prop in fi)
                {
                    object val = pc.GetPropertyValue(prop.Name);
                    prop.SetValue(obj, val);
                }
            }
        }
        
        private void CascadeUpdate(object obj, ObjectAction action)
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            
            Type content = obj.GetType();
            while (content != null)
            {
                FieldInfo[] fi = content.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                fields.AddRange(fi);
                content = content.BaseType;
            }
            
            foreach (FieldInfo prop in fields)
            {
                Attribute attr = Attribute.GetCustomAttribute(prop, typeof(CascadeAttribute));
                if (attr == null)
                {
                    continue;
                }
                
                object val = prop.GetValue(obj);
                if (val != null)
                {
                    if (val.GetType().GetInterface("IDictionary") != null)
                    {
                        if (((CascadeAttribute)attr).WithObjects)
                        {
                            foreach (var key in ((IDictionary)val).Keys)
                            {
                                object o = ((IDictionary)val)[key];
                                if (Attribute.GetCustomAttribute(o.GetType(), typeof(CascadeAttribute)) != null)
                                {
                                    this.CascadeUpdate(o, action);
                                    this.UpdateObject(o, action);
                                }
                            }
                        }
                    }
                    else if (val.GetType().GetInterface("IEnumerable") != null)
                    {
                        if (((CascadeAttribute)attr).WithObjects)
                        {
                            foreach (object o in (IEnumerable)val)
                            {
                                if (Attribute.GetCustomAttribute(o.GetType(), typeof(CascadeAttribute)) != null)
                                {
                                    this.CascadeUpdate(o, action);
                                    this.UpdateObject(o, action);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.CascadeUpdate(val, action);
                    }
                    
                    this.UpdateObject(val, action);
                }
            }
            
            this.UpdateObject(obj, action);
        }
    }
}
