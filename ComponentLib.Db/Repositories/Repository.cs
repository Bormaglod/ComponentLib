// <copyright file="Repository.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergio.teplyashin@gmail.com</email>
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
// <date>18.06.2014</date>
// <time>14:37</time>
// <summary>Defines the Repository class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    #if DEBUG
    using System.Diagnostics;
    #endif
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using Core.Xml;
    using DataModel;
    using Exceptions;
    
    public abstract class Repository<T>: IRepository<T> where T: Entity, new()
    {
        string name;
        int identifier;
        RepoCollections owner;
        IList<T> cache;
        List<IFilter<T>> filters;
        
        protected Repository(RepoCollections repoCollections, int id, string repoName)
        {
            owner = repoCollections;
            name = repoName;
            identifier = id;
            cache = new List<T>();
            filters = new List<IFilter<T>>();
        }
        
        public RepoCollections Owner
        {
            get { return owner; }
        }
        
        #region IPropertyRepository implemented
        
        /// <summary>
        /// Свойство возвращает true, если коллекция (с учетом фильтра) не содержит ни одного объекта.
        /// </summary>
        public bool IsEmpty
        {
            get { return Items.FirstOrDefault() == null; }
        }
        
        /// <summary>
        /// Свойство возвращает тип данных хранящихся в коллекции.
        /// </summary>
        public Type ContentsType
        {
            get { return typeof(T); }
        }
        
        /// <summary>
        /// Свойство возвращает количество объектов в коллекции.
        /// </summary>
        public long Count
        {
            get { return Items.Count(); }
        }
        
        /// <summary>
        /// Возвращает уникальный идентификатор коллекции.
        /// </summary>
        public int Identifier
        {
            get { return identifier; }
        }
        
        /// <summary>
        /// Возвращает наименование коллекции.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        
        public bool ReadOnly
        {
            get { return GetReadOnly(); }
        }
        
        IEnumerable IPropertyRepository.Objects
        {
            get { return Items; }
        }
        
        public IEnumerable<IPropertyRepository> Dependences
        {
            get
            {
                List<IPropertyRepository> dependences = new List<IPropertyRepository>();
                foreach (PropertyInfo p in ContentsType.GetProperties())
                {
                    if (p.PropertyType.Inherits(typeof(Entity)))
                    {
                        IPropertyRepository repo = owner.Get(p.PropertyType);
                        if (!dependences.Contains(repo))
                        {
                            dependences.Add(repo);
                        }
                    }
                }
                
                return dependences;
            }
        }

        #endregion

        #region IRepository<T> implemented

        /// <summary>
        /// Возвращает список объектов коллекции. Список может быть ограничен с помощью установленного
        /// фильтра. Фильтр устанавливается методом <see cref="AddFilter"/>.
        /// </summary>
        public IEnumerable<T> Items
        {
            get { return GetItems(); }
        }
        
        public IEnumerable<T> ItemsWithoutFilters
        {
            get { return GetItemsWithoutFilters(); }
        }
        
        public IEnumerable<IFilter<T>> Filters
        {
            get { return filters; }
        }
        
        public int AddFilter(IFilter<T> filter)
        {
            filters.Add(filter);
            return filters.Count - 1;
        }
        
        public void DeleteFilter(int index)
        {
            filters.RemoveAt(index);
        }
        
        public void DeleteFilter(IFilter<T> filter)
        {
            filters.Remove(filter);
        }

        public IFilter<T> GetFilter(int index)
        {
            return filters[index];
        }

        public IFilter<T> GetFilter(Guid idFilter)
        {
            return filters.SingleOrDefault(x => x.Id == idFilter);
        }

        /// <summary>
        /// Свойство возвращает объект, имеющий указанный ключ.
        /// </summary>
        public T this[string key]
        {
            get
            {
                return string.IsNullOrEmpty(key) ? null : ItemsWithoutFilters.FirstOrDefault(item => item.Key() == key);
            }
        }
        
        /// <summary>
        /// Метод создает новый элемент коллекции, но не записывает его в БД.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        public T Create()
        {
            T item = new T();
            OnAfterCreateItem(item);
            return item;
        }
        
        public bool ContainsKey(T item)
        {
            return ItemsWithoutFilters.FirstOrDefault(i => !i.Equals(item) && i.Key() == item.Key()) != null;
        }
        
        /// <summary>
        /// Метод создает новый элемент коллекции копируя данные из entity (если entity не null)
        /// и добавляет его в нее.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        /// <param name="entity">Копируемый объект.</param>
        public T Create(T entity)
        {
            T item = Create();
            if (entity != null)
            {
                entity.CopyTo(item);
            }
            
            return item;
        }
        
        /// <summary>
        /// Метод добавляет объект в коллекцию. Добавляемый объект должен отсутствовать в коллекции.
        /// Если ключ объекта не null, то в коллекции должен отсутствовать объект с таким же ключем.
        /// </summary>
        /// <param name="addedItem">Добавляемый объект.</param>
        /// <seealso cref="Entity.IsNew"></seealso>
        public virtual void Add(T addedItem)
        {
            CheckObject(addedItem);
            
            if (!addedItem.IsNew)
            {
                throw new RepositoryException(string.Format(Strings.ObjectExist, addedItem.ToString()), RepoErrors.EntityExist, addedItem);
            }
            
            CheckConstraints(addedItem);
            addedItem.IsNew = false;
            CascadeUpdate(addedItem, ObjectAction.Add);
            OnEntityModified(addedItem, ObjectAction.Add);
        }
        
        /// <summary>
        /// Метод обновляет данные объекта в коллекции. Обновляемый объект должен присутствовать в коллекции.
        /// Если ключ объекта не null, то в коллекции должен отсутствовать объект с таким же ключем.
        /// </summary>
        /// <param name="updatedItem">Обновляемый объект.</param>
        /// <seealso cref="Entity.IsNew"></seealso>
        public virtual void Update(T updatedItem)
        {
            CheckObject(updatedItem);
            
            if (updatedItem.IsNew)
            {
                throw new RepositoryException(string.Format(Strings.ObjectNotExist, updatedItem.ToString()), RepoErrors.EntityNotExist, updatedItem);
            }
            
            CheckConstraints(updatedItem);
            CascadeUpdate(updatedItem, ObjectAction.Modify);
            OnEntityModified(updatedItem, ObjectAction.Modify);
        }
        
        public virtual void AddOrUpdate(T item)
        {
            if (item.IsNew)
            {
                Add(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>
        /// Метод удаляет объект из коллекции. Удаляемый объект должен присутствовать в коллекции.
        /// </summary>
        /// <param name="removedItem">Удаляемый объект.</param>
        /// <seealso cref="Entity.IsNew"></seealso>
        public virtual void Remove(T removedItem)
        {
            CheckObject(removedItem);

            if (removedItem.IsNew)
            {
                throw new RepositoryException(string.Format(Strings.ObjectNotExist, removedItem.ToString()), RepoErrors.EntityNotExist, removedItem);
            }
            
            // Проверим возможность удаления объекта.
            foreach (IBaseRepository collection in Owner.Collections.OfType<IBaseRepository>())
            {
                if (!ReferenceEquals(this, collection))
                {
                    if (!collection.CanObjectRemoving(removedItem))
                    {
                        throw new RepositoryException(string.Format(Strings.ObjectUsed, removedItem.ToString(), collection.Name), RepoErrors.EntityUsed, removedItem);
                    }
                }
            }
            
            // Сообщим всем коллекциям базы данных об удалении элемента.
            foreach (IBaseRepository collection in Owner.Collections.OfType<IBaseRepository>())
            {
                if (!ReferenceEquals(this, collection))
                {
                    collection.ObjectRemoving(removedItem);
                }
            }
            
            CascadeUpdate(removedItem, ObjectAction.Delete);
            OnEntityModified(removedItem, ObjectAction.Delete);
            
            // Сообщим всем коллекциям базы данных об удалении элемента.
            foreach (IBaseRepository collection in Owner.Collections.OfType<IBaseRepository>())
            {
                if (!ReferenceEquals(this, collection))
                {
                    collection.ObjectRemoved(removedItem);
                }
            }
        }
        
        #endregion
        
        #region IBaseRepository implemented
        
        public event EventHandler<EntityEventArgs> EntityModified;
        
        /// <summary>
        /// <para>Метод вызывается при удалении объекта из какой-либо коллекции и предназначен
        /// для проверки возможности удаления этого объекта. Для запрещения удаления объекта
        /// коллекция должна перекрыть этот метод и вернуть false.</para>
        /// <para>Метод вызывается до удаления объекта и до вызова ObjectRemoving.</para>
        /// </summary>
        /// <param name="removingItem">Удаляемый объект.</param>
        public virtual bool CanObjectRemoving(Entity removingItem)
        {
            return true;
        }
        
        /// <summary>
        /// Метод вызывается при удалении объекта из какой-либо коллекции. Вызов этого метода
        /// происходит после вызова CanObjectRemoving (причем он должен вернуть true для всех коллекций).
        /// В этом методе можно предусмотреть дополнительную обработку при удаление объектов из других
        /// коллекций.
        /// </summary>
        /// <param name="removingItem">Удаляемый объект.</param>
        public virtual void ObjectRemoving(Entity removingItem)
        {
            RemoveDependence(removingItem);
        }
        
        /// <summary>
        /// Метод вызывается после удаления объекта из какой-либо коллекции.
        /// </summary>
        /// <param name="removedItem">Удаленный объект.</param>
        public virtual void ObjectRemoved(Entity removedItem) {}
        
        /// <summary>
        /// Метод возвращает true, если объект item можно добавить в данную коллекцию.
        /// </summary>
        /// <param name="item">Проверяемый объект.</param>
        /// <returns>true, если объект item можно добавить в коллекцию.</returns>
        public virtual bool Belonge(Entity item)
        {
            return item.GetType() == ContentsType;
        }
        
        public void Execute(Entity item, ObjectAction action)
        {
            switch (action)
            {
                case ObjectAction.Add:
                    Add((T)item);
                    break;
                case ObjectAction.Delete:
                    Remove((T)item);
                    break;
                case ObjectAction.Modify:
                    Update((T)item);
                    break;
            }
        }
        
        public void ClearAllData()
        {
            Clear();
        }
        
        public void SaveToXml(XmlDocument document)
        {
            if (document.DocumentElement == null)
            {
                document.CreateItem(null, "Repositories");
            }
            
            XmlElement r = document.CreateItem(document.DocumentElement, "Repository");
            document.CreateItem(r, "Id", Identifier.ToString());
            if (!string.IsNullOrEmpty(Name))
            {
                document.CreateItem(r, "Name", Name);
            }
            
            XmlElement items = document.CreateItem(r, "Items");
            foreach (Entity e in Items)
            {
                XmlElement item = document.CreateItem(items, e.GetType().Name);
                //document.CreateItem(item, "Key", e.Key());
                foreach (PropertyInfo p in e.GetType().GetProperties())
                {
                    if (p.CanWrite && p.GetSetMethod() != null)
                    {
                        object prop = p.GetValue(e, null);
                        if (prop == null)
                        {
                            continue;
                        }
                        
                        Entity propEntity = prop as Entity;
                        if (propEntity == null)
                        {
                            document.CreateItem(item, p.Name, prop.ToString());
                        }
                        else
                        {
                            XmlElement entityItem = document.CreateItem(item, p.Name, "From", prop.GetType().Name);
                            entityItem.AppendChild(document.CreateTextNode(propEntity.Key()));
                        }
                    }
                }
            }
        }
        
        public void LoadFromXml(XmlDocument document)
        {
            XmlNodeList nodes = document.SelectNodes(string.Format("/Repositories/Repository/Items/{0}", ContentsType.Name));
            foreach (XmlNode node in nodes)
            {
                T item = Create();
                PropertyInfo[] pi = ContentsType.GetProperties();
                foreach (XmlNode propName in node.ChildNodes)
                {
                    PropertyInfo p = pi.FirstOrDefault(x => x.Name == propName.Name);
                    if (p != null)
                    {
                        string refType = propName.AttributeValueOrDefault("From");
                        if (string.IsNullOrEmpty(refType))
                        {
                            
                            object value;
                            if (p.PropertyType.IsEnum)
                            {
                                value = Enum.Parse(p.PropertyType, propName.InnerText);
                            }
                            else
                            {
                                value = Convert.ChangeType(propName.InnerText, p.PropertyType);
                            }
                            
                            p.SetValue(item, value, null);
                        }
                        else
                        {
                            IPropertyRepository repo = owner.Get(refType);
                            Entity e = repo.Objects.OfType<Entity>().FirstOrDefault(x => x.Key() == propName.InnerText);
                            p.SetValue(item, e, null);
                        }
                    }
                }
                
                Add(item);
            }
        }
        
        #endregion
        
        public virtual void CheckConstraints(T item)
        {
            CheckKey(item);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        
        public override string ToString()
        {
            return Name;
        }
        
        /// <summary>
        /// Метод очищает коллекцию, удаляя все данные содержащиеся в ней.
        /// </summary>
        protected abstract void Clear();
        
        /// <summary>
        /// Метод возвращает коллекцию объектов с учётом установленного фильтра.
        /// </summary>
        /// <returns>Коллекцию объектов с учётом установленного фильтра.</returns>
        protected IEnumerable<T> GetItems()
        {
            IEnumerable<IFilter<T>> f = filters.Where(x => x.Enabled && !x.Empty);
            return ItemsWithoutFilters.Where(x => {
                                    bool c = true;
                                    foreach (var flt in f)
                                    {
                                        c = c && flt.Contains(x);
                                        if (!c)
                                            break;
                                    }
                                    
                                    return c;
                                });
        }
        
        /// <summary>
        /// Метод должен возвращать коллекцию всех объектов без учета фильтров.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<T> GetItemsWithoutFilters();
        
        /// <summary>
        /// <para>Метод вызывается при создании элемента коллекции с помощью метода Create до добавления</para>
        /// <para>элемента в коллекцию.</para>
        /// <para>В этом методе можно заполнить необходимые свойства вновь созданного элемента до записи</para>
        /// <para>в коллекцию.</para>
        /// </summary>
        /// <param name="item">Создаваемый объект подлежащий изменению.</param>
        protected virtual void OnAfterCreateItem(T item) { }
        
        protected abstract void AddInternal(object addedItem);
        
        protected abstract void UpdateInternal(object updatedItem);
        
        protected abstract void RemoveInternal(object removedItem);
        
        protected abstract bool GetReadOnly();
        
        /// <summary>
        /// Метод вызывается при удалении объекта из какой-либо коллекции и предназначен
        /// обновления зависимых объектов (объектов помеченых атрибутом DependenceAttribute).
        /// </summary>
        /// <param name="removingItem">Удаляемый объект.</param>
        void RemoveDependence(Entity removingItem)
        {
            IDictionary<PropertyInfo, DependenceAttribute> properties = ContentsType.GetProperties<DependenceAttribute>();
            
            if (properties.Count == 0)
            {
                return;
            }
            
            foreach (T item in ItemsWithoutFilters)
            {
                foreach (PropertyInfo p in properties.Keys)
                {
                    object propValue = p.GetValue(item, null);
                    if (propValue == null)
                    {
                        continue;
                    }
                    
                    if (propValue.GetType().GetInterface("IList") != null)
                    {
                        IList list = (IList)propValue;
                        Stack stack = new Stack();
                        foreach (object obj in list)
                        {
                            if (ReferenceEquals(obj, removingItem))
                            {
                                if (properties[p].Action == DependenceAction.Nothing)
                                {
                                    throw new RepositoryException(string.Format(Strings.DeleteEntityError, removingItem.ToString(), item.ToString()), RepoErrors.EntityDependenceError, removingItem);
                                }
                                else
                                {
                                    stack.Push(obj);
                                }
                            }
                            else
                            {
                                Entity entity = obj as Entity;
                                if (entity != null)
                                {
                                    if (entity.ContainsObject(removingItem))
                                    {
                                        if (properties[p].Action == DependenceAction.Nothing)
                                        {
                                            throw new RepositoryException(string.Format(Strings.DeleteEntityError, removingItem.ToString(), item.ToString()), RepoErrors.EntityDependenceError, removingItem);
                                        }
                                        else
                                        {
                                            stack.Push(obj);
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (stack.Count > 0)
                        {
                            while (stack.Count > 0)
                            {
                                list.Remove(stack.Pop());
                            }
                            
                            UpdateObjectInternal(item, ObjectAction.Modify);
                        }
                    }
                    else
                    {
                        if (ReferenceEquals(propValue, removingItem))
                        {
                            switch (properties[p].Action)
                            {
                                case DependenceAction.SetNull:
                                    p.SetValue(item, null, null);
                                    owner.Get<T>().Update(item);
                                    break;
                                case DependenceAction.Delete:
                                    owner.Get<T>().Remove(item);
                                    break;
                                case DependenceAction.Nothing:
                                    throw new RepositoryException(string.Format(Strings.DeleteEntityError, removingItem.ToString(), item.ToString()), RepoErrors.EntityDependenceError, removingItem);
                                default:
                                    throw new Exception("Invalid value for DependenceAction");
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Проверка объекта obj. Если объект не существует (obj = null) или база данных не открыта,
        /// то генерируется исключение.
        /// </summary>
        /// <param name="item">Проверяемый объект.</param>
        void CheckObject(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException(Strings.ObjectIsNull);
            }
            
            if (Owner.State == DatabaseState.Closed)
            {
                throw new DatabaseClosedException(Strings.DatabaseClose);
            }
        }
        
        /// <summary>
        /// Проверяет наличие в базе объекта с таким же ключём, и генерирует исключение, если такой
        /// объект найден.
        /// </summary>
        /// <param name="item">Проверяемый объект.</param>
        void CheckKey(T item)
        {
            if (string.IsNullOrEmpty(item.Key()))
            {
                throw new RepositoryException(string.Format(Strings.KeyIsEmpty, item), RepoErrors.KeyEmpty, item);
            }
            
            if (ContainsKey(item))
            {
                throw new RepositoryException(string.Format(Strings.KeyExist, item.Key()), RepoErrors.KeyExist, item);
            }
        }
        
        void UpdateObjectInternal(object obj, ObjectAction action)
        {
            switch (action)
            {
                case ObjectAction.Add:
                    AddInternal(obj);
                    break;
                case ObjectAction.Delete:
                    RemoveInternal(obj);
                    break;
                case ObjectAction.Modify:
                    UpdateInternal(obj);
                    break;
                default:
                    throw new Exception("Invalid value for ObjectAction");
            }
        }
        
        void CascadeUpdate(Entity item, bool delete)
        {
            if (item == null)
            {
                return;
            }
            
            IBaseRepository repo = (IBaseRepository)Owner.GetCollectionBelonge(item);
            if (repo == null)
            {
                return;
            }
            
            if (item.IsNew)
            {
                repo.Execute(item, ObjectAction.Add);
            }
            else if (delete)
            {
                repo.Execute(item, ObjectAction.Delete);
            }
            else
            {
                repo.Execute(item, ObjectAction.Modify);
            }
        }
        
        void CascadeUpdate(Entity item, ObjectAction action)
        {
            IDictionary<PropertyInfo, CascadeAttribute> properties = item.GetType().GetProperties<CascadeAttribute>();
            
            foreach (PropertyInfo prop in properties.Keys)
            {
                object propValue = prop.GetValue(item, null);
                if (propValue == null)
                {
                    continue;
                }
                
                bool delete = properties[prop].OnDelete && action == ObjectAction.Delete;
                
                if (propValue.GetType().GetInterface("IDictionary") != null)
                {
                    IDictionary dictionary = (IDictionary)propValue;
                    if (properties[prop].OnInside)
                    {
                        foreach (var key in dictionary.Keys)
                        {
                            Entity dictionaryKey = dictionary[key] as Entity;
                            CascadeUpdate(dictionaryKey, delete);
                        }
                    }
                    
                    UpdateObjectInternal(propValue, action);
                    continue;
                }
                
                if (propValue.GetType().GetInterface("IEnumerable") != null)
                {
                    if (properties[prop].OnInside)
                    {
                        foreach (var i in (IEnumerable)propValue)
                        {
                            CascadeUpdate(i as Entity, delete);
                        }
                    }
                    
                    UpdateObjectInternal(propValue, action);
                    continue;
                }
                
                CascadeUpdate(propValue as Entity, delete);
            }
            
            UpdateObjectInternal(item, action);
        }
        
        void OnEntityModified(T entity, ObjectAction action)
        {
            EntityModified?.Invoke(this, new EntityEventArgs(entity, action, Owner.State));
        }
    }
}
