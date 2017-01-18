//-----------------------------------------------------------------------
// <copyright file="History.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2016 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>13.04.2011</date>
// <time>14:14</time>
// <summary>Defines the History class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// <para>Класс предназначен для организации переходов по объектам. Передвижение возможно в две стороны
    /// списка объектов.</para>
    /// <para>При добавлении объекта в любое место списка, отличное от конца, приводит
    /// перемещению метки конца списка на добавляемый объект.</para>
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке.</typeparam>
    public class History<T>
    {
        /// <summary>
        /// Текущая позиция используемая в переходах по объектам.
        /// </summary>
        int current;
        
        /// <summary>
        /// Список объектов.
        /// </summary>
        readonly List<T> objects;
        
        /// <summary>
        /// Initializes a new instance of the History class.
        /// </summary>
        public History()
        {
            this.objects = new List<T>();
            this.current = -1;
        }
        
        /// <summary>
        /// Gets the count objects in list history.
        /// </summary>
        /// <value>Количество объектов в списке.</value>
        public int Count
        {
            get { return objects.Count; }
        }
        
        /// <summary>
        /// Gets the last added object.
        /// </summary>
        /// <value>Последний объект из списка.</value>
        public T Last
        {
            get { return objects[objects.Count - 1]; }
        }
        
        /// <summary>
        /// Gets the current object.
        /// </summary>
        /// <value>Текущий объект списка.</value>
        public T Current
        {
            get { return objects[current]; }
        }
        
        /// <summary>
        /// Gets the IsFirst flag.
        /// </summary>
        /// <value>true, если текущий объект, является первым в списке.</value>
        public bool IsFirst
        {
            get { return current < 1; }
        }
        
        /// <summary>
        /// Gets the IsLast flag.
        /// </summary>
        /// <value>true, если текущий объект, является последним в списке или список пуст.</value>
        public bool IsLast
        {
            get { return current == -1 || current == objects.Count - 1; }
        }
        
        /// <summary>
        /// <para>Метод добавляет объект в список. Объект вставляется после объекта Current.
        /// Если это не последний объект в списке,</para>
        /// <para>то все объекты после Current будут предварительно удалены.
        /// Если при этом добавляемый объект</para>
        /// <para>поддерживает интерфейс IComparable и метод
        /// CompareTo() этого объекта возвращает 0, то происходит замена</para>
        /// <para>объекта Current на добавляемый без изменения списка.</para>
        /// </summary>
        /// <param name="obj">Добавляемый объект.</param>
        public void Add(T obj)
        {
            IComparable<T> cmpObj = obj as IComparable<T>;
            if (cmpObj != null)
            {
                if (current != -1 && cmpObj.CompareTo(Current) == 0)
                {
                    objects[current] = obj;
                }
                else
                {
                    AddItem(obj);
                }
            }
            else
            {
                AddItem(obj);
            }
        }
        
        /// <summary>
        /// Метод возвращает объект предшествующий Current и устанавливает Current на возвращаемый объект.
        /// </summary>
        /// <returns>Объект предшествующий Current.</returns>
        public T Back()
        {
            return objects[--current];
        }
        
        /// <summary>
        /// Метод возвращает объект следующий за Current и устанавливает Current на возвращаемый объект.
        /// </summary>
        /// <returns>Объект следующий за Current.</returns>
        public T Forward()
        {
            return objects[++current];
        }
        
        public void Clear()
        {
            objects.Clear();
            current = -1;
        }
        
        /// <summary>
        /// <para>Метод добавляет объект в конец списка, предварительно удалив все объекты</para>
        /// <para>следующие за Current. Current будет установлен на добавляемый объект.</para>
        /// </summary>
        /// <param name="obj">Добавляемый объект.</param>
        void AddItem(T obj)
        {
            while (objects.Count - 1 != current)
            {
                objects.RemoveAt(current + 1);
            }
            
            objects.Add(obj);
            current = objects.Count - 1;
        }
    }
}
