//-----------------------------------------------------------------------
// <copyright file="IRepositiry.cs" company="Тепляшин Сергей Васильевич">
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
// <date>15.07.2014</date>
// <time>15:08</time>
// <summary>Defines the IRepositiry class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public interface IRepository<T> : IBaseRepository where T: Entity
    {
        /// <summary>
        /// Свойство возвращает список объектов, хранящихся в коллекции (с учетом фильтра).
        /// </summary>
        IEnumerable<T> Items { get; }
        
        /// <summary>
        /// Свойство возвращает список объектов, хранящихся в коллекции (без учета фильтра).
        /// </summary>
        IEnumerable<T> ItemsWithoutFilters { get; }
        
        IEnumerable<IFilter<T>> Filters { get; }
        
        /// <summary>
        /// Свойство возвращает объект, имеющий указанный ключ.
        /// </summary>
        T this[string key] { get; }
        
        int AddFilter(IFilter<T> filter);
        
        void DeleteFilter(int index);

        void DeleteFilter(IFilter<T> filter);

        IFilter<T> GetFilter(int index);

        IFilter<T> GetFilter(Guid idFilter);

        /// <summary>
        /// Метод создает новый элемент коллекции.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        T Create();
        
        /// <summary>
        /// Метод создает новый элемент коллекции копируя данные из entity (если entity не null)
        /// и добавляет его в нее.
        /// </summary>
        /// <returns>Новый элемент коллекции.</returns>
        /// <param name="entity">Копируемый объект.</param>
        T Create(T entity);
        
        /// <summary>
        /// Функция возвращает true, если в коллекции содержится объект item или есть объект с такаим же ключом.
        /// </summary>
        /// <param name="item">Проверяемый объект.</param>
        /// <returns>true, если в коллекции содержится объект item или есть объект с такаим же ключом.</returns>
        bool ContainsKey(T item);
        
        /// <summary>
        /// Метод добавляет объект в коллекцию. Добавляемый объект должен отсутствовать в коллекции.
        /// Если ключ объекта не null, то в коллекции должен отсутствовать объект с таким же ключем.
        /// </summary>
        /// <param name="addedItem">Добавляемый объект.</param>
        void Add(T addedItem);
        
        /// <summary>
        /// Метод обновляет данные объекта в коллекции. Обновляемый объект должен присутствовать в коллекции.
        /// Если ключ объекта не null, то в коллекции должен отсутствовать объект с таким же ключем.
        /// </summary>
        /// <param name="updatedItem">Обновляемый объект.</param>
        void Update(T updatedItem);
        
        void AddOrUpdate(T item);
        
        /// <summary>
        /// Метод удаляет объект из коллекции. Удаляемый объект должен присутствовать в коллекции.
        /// </summary>
        /// <param name="removedItem">Удаляемый объект.</param>
        void Remove(T removedItem);
    }
}
