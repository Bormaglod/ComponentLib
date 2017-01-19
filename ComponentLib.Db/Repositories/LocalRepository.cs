//-----------------------------------------------------------------------
// <copyright file="LocalRepository.cs" company="Тепляшин Сергей Васильевич">
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
// <date>06.08.2014</date>
// <time>13:10</time>
// <summary>Defines the LocalRepository class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public abstract class LocalRepository<T> : Repository<T> where T: Entity, new()
    {
        readonly List<T> list;
        
        public LocalRepository(RepoCollections repoCollections, int id, string repoName) : 
            base(repoCollections, id, repoName)
        {
            list = new List<T>();
        }
        
        /// <summary>
        /// Метод очищает коллекцию, удаляя все данные содержащиеся в ней.
        /// </summary>
        protected override void Clear()
        {
            list.Clear();
        }
        
        /// <summary>
        /// Метод возвращает список всех элементов данной коллекции без учета фильтров.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<T> GetItemsWithoutFilters() => list;
        
        protected sealed override void AddInternal(object addedItem)
        {
            list.Add((T)addedItem);
        }
        
        protected sealed override void UpdateInternal(object updatedItem)
        {
        }
        
        protected sealed override void RemoveInternal(object removedItem)
        {
            list.Remove((T)removedItem);
        }
    }
}
