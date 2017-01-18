//-----------------------------------------------------------------------
// <copyright file="IBaseCollection.cs" company="Sergey Teplyashin">
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
// <date>29.07.2011</date>
// <time>12:47</time>
// <summary>Defines the IBaseCollection interface.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    /// <summary>
    /// Интерфейс представляющий список объектов
    /// </summary>
    public interface IBaseCollection<T>
    {
        /// <summary>
        /// Возвращает список объектов коллекции.
        /// </summary>
        IEnumerable<T> Collection { get; }
        
        /// <summary>
        /// Метод создает новый элемент коллекции и добавляет его в нее.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        T Create();
        
        /// <summary>
        /// Метод добавляет объект в коллекцию.
        /// </summary>
        /// <param name="addedElement">Добавляемый объект.</param>
        void Add(T addedElement);
        
        /// <summary>
        /// Метод обновляет данный об объекте в коллекции.
        /// </summary>
        /// <param name="updatedElement">Обновляемый объект.</param>
        void Update(T updatedElement);
        
        /// <summary>
        /// Метод удаляет объект из коллекции.
        /// </summary>
        /// <param name="removedElement">Удаляемый объект.</param>
        void Remove(T removedElement);
    }
}
