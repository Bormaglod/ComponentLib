//-----------------------------------------------------------------------
// <copyright file="ObjectAccessCollection.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2017 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>12.03.2012</date>
// <time>10:45</time>
// <summary>Defines the ObjectAccessCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    
    /// <summary>
    /// <para>Класс предназначен для создания коллекции содержащей элементы в количестве не более</para>
    /// <para>указанного. Эта коллекци хранит объекты которые наследуются от IAccess и только их.</para>
    /// <para>Это требование необходимо, поскольку все объекты сортируются по дате доступа (свойство</para>
    /// <para>AccessTime интерфейса IAccess).</para>
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранящихся в коллекции.</typeparam>
    public class ObjectAccessCollection<T> : ICollection<T>, IList where T : IAccess, new()
    {
        /// <summary>
        /// Массив объектов, содержащихся в коллекции.
        /// </summary>
        T[] objects;
        
        /// <summary>
        /// Количество хранящихся в коллекции объектов.
        /// </summary>
        int count;
        
        /// <summary>
        /// Initializes a new instance of the ObjectAccessCollection class.
        /// </summary>
        public ObjectAccessCollection() : this(6)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ObjectAccessCollection class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public ObjectAccessCollection(int capacity)
        {
            objects = new T[capacity];
            count = 0;
        }
        
        public ObjectAccessCollection(string fileName, string header)
        {
            T[] items = GetItemsFromXml(fileName, header).ToArray();
            count = items.Length;
            objects = new T[count < 6 ? 6 : count];
            items.CopyTo(objects, 0);
        }
        
        /// <summary>
        /// <para>Gets the object with lasted date/time access property.</para>
        /// <para>Свойство возвращает элемент (файл) коллекции имеющий самую позднюю дату/время доступа.</para>
        /// </summary>
        public T Last => Count == 0 ? default(T) : objects.Max(f => f);
        
        public T this[int index]
        {
            set
            {
                objects[index] = value;
            }
            
            get
            {
                return objects[index];
            }
        }
        
        #region ICollection<T> interface implemented
        
        /// <summary>
        /// Gets the number of elements contained in the ObjectAccessCollection.
        /// </summary>
        public int Count => count;
        
        /// <summary>
        /// Gets a value indicating whether the ObjectAccessCollection is read-only.
        /// </summary>
        public bool IsReadOnly => false;
        
        /// <summary>
        /// Метод добавляет элемент в коллекцию. Если количество элементов в коллекции до добавления равно максимальному
        /// (указывается в конструкторе параметром capacity), то элемент с более старой датой удаляется.
        /// </summary>
        /// <param name="item">Добавляемый элемент в коллекцию.</param>
        public void Add(T item)
        {
            if (Count == objects.Length)
            {
                T removed = objects.Min(f => f);
                Remove(removed);
            }
            
            objects[count] = item;
            count += 1;
        }
        
        /// <summary>
        /// Removes the first occurrence of a specific object from the ObjectAccessCollection.
        /// </summary>
        /// <param name="item">The object to remove from the ObjectAccessCollection.</param>
        /// <returns>true if item was successfully removed from the ObjectAccessCollection; otherwise, false. This method also returns false if item is not found in the original ObjectAccessCollection.</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                for (int i = index + 1; i < Count; i++)
                {
                    objects[i - 1] = objects[i];
                }
                
                count--;
                return true;
            }
            
            return false;
        }
        
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= objects.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            
            Remove(this[index]);
        }
        
        public int IndexOf(T item)
        {
            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                if (ReferenceEquals(objects[i], item))
                {
                    index = i;
                    break;
                }
            }
            
            return index;
        }

        /// <summary>
        /// Removes all items from the ObjectAccessCollection.
        /// </summary>
        public void Clear()
        {
            count = 0;
        }
        
        /// <summary>
        /// Determines whether the ObjectAccessCollection contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the ObjectAccessCollection.</param>
        /// <returns>true if item is found in the ObjectAccessCollection; otherwise, false.</returns>
        public bool Contains(T item) => objects.Contains(item);

        /// <summary>
        /// Copies the elements of the ObjectAccessCollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ObjectAccessCollection. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException("array");
            }
            
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }
            
            if (array.Length - arrayIndex < Count || array.Rank > 1)
            {
                throw new ArgumentException("An invalid argument was specified.");
            }
            
            foreach (T element in objects)
            {
                array[arrayIndex++] = element;
            }
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A IEnumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> items = from item in objects
                                    where !ReferenceEquals(item, null)
                                    orderby item.AccessTime descending
                                    select item;
            
            return items.GetEnumerator();
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        #endregion
        
        #region IList interface implemented
        
        bool ICollection.IsSynchronized => false;
        
        object ICollection.SyncRoot => null;
        
        bool IList.IsFixedSize => false;
        
        void ICollection.CopyTo(Array array, int index)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException("array");
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            
            if (array.Length - index < Count || array.Rank > 1)
            {
                throw new ArgumentException("An invalid argument was specified.");
            }
            
            foreach (T element in objects)
            {
                array.SetValue(element, index++);
            }
        }
        
        object IList.this[int index]
        {
            set
            {
                this[index] = (T)value;
            }
            
            get
            {
                return this[index];
            }
        }
        
        int IList.Add(object item)
        {
            Add((T)item);
            return Count - 1;
        }
        
        void IList.Remove(object item)
        {
            Remove((T)item);
        }
        
        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }
        
        int IList.IndexOf(object item) => IndexOf((T)item);
        
        bool IList.Contains(object item) => Contains((T)item);
        
        void IList.Insert(int index, object item)
        {
            Add((T)item);
        }
        
        #endregion
        
        /// <summary>
        /// Метод записывает данные коллекции в файл XML.
        /// </summary>
        /// <param name="fileName">Имя записываемого файла.</param>
        /// <param name="header"></param>
        public void SaveToXml(string fileName, string header)
        {
            Type elementType = typeof(T);
            if (elementType.GetInterface("IXmlData") != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty));
                XmlElement root = doc.CreateElement(header);
                doc.AppendChild(root);
            
                foreach (T element in this)
                {
                    root.AppendChild(((IXmlData)element).CreateXmlElement(doc));
                }
                
                doc.Save(fileName);
            }
        }
        
        /// <summary>
        /// Метод загружает данные коллекции из файла XML.
        /// </summary>
        /// <param name="fileName">Имя загружаемого файла.</param>
        /// <param name="header"></param>
        public void LoadFromXml(string fileName, string header)
        {
            Clear();
            foreach (T element in GetItemsFromXml(fileName, header))
            {
                Add(element);
            }
        }
        
        IEnumerable<T> GetItemsFromXml(string fileName, string header)
        {
            List<T> array = new List<T>();
            Type elementType = typeof(T);
            if (elementType.GetInterface("IXmlData") != null)
            {
                if (System.IO.File.Exists(fileName))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);
                    
                    XmlNodeList nodes = doc.SelectNodes(string.Format("/{0}/{1}", header, elementType.Name));
                    foreach (XmlNode node in nodes)
                    {
                    	T obj = new T();
                        ((IXmlData)obj).LoadFromXml(node);
                        array.Add(obj);
                    }
                }
            }
            
            return array;
        }
    }
}
