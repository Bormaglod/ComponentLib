//-----------------------------------------------------------------------
// <copyright file="BDCollection.cs" company="Тепляшин Сергей Васильевич">
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
// <date>19.06.2014</date>
// <time>10:38</time>
// <summary>Defines the BDCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Db4o
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Db4objects.Db4o;
    using Db4objects.Db4o.Linq;
    using Repositories;
    
    public class DbRepository<T> : Repository<T> where T: Entity, new()
    {
        public DbRepository(RepoCollections repoCollections, int id, string repoName) : 
            base(repoCollections, id, repoName)
        {
        }
        
        /// <summary>
        /// Метод очищает коллекцию, удаляя все данные содержащиеся в ней.
        /// </summary>
        protected override void Clear()
        {
            foreach (T item in this)
            {
                Remove(item);
            }
        }
        
        public void Refresh(T item)
        {
            ((Db4objects.Db4o.Ext.IExtObjectContainer)Container).Refresh(item, 1);
        }
        
        protected IObjectContainer Container => ((RepoDbCollections)Owner).Container;
        
        /// <summary>
        /// Метод возвращает список всех элементов данной коллекции без учета фильтров.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<T> GetItemsWithoutFilters() => from T c in Container select c;
        
        protected override void AddInternal(object addedItem)
        {
            Container.Store(addedItem);
        }
        
        protected override void UpdateInternal(object updatedItem)
        {
            // Сохранение в БД будет сделано в Session после Commit
            if (((RepoDbCollections)Owner).Session == null)
            {
                Container.Store(updatedItem);
            }
        }
        
        protected override void RemoveInternal(object removedItem)
        {
            foreach (PropertyInfo pi in removedItem.GetType().GetProperties().Where(p => p.PropertyType == typeof(DbImage)))
            {
                DbImage image = (DbImage)pi.GetValue(removedItem, null);
                image.DeleteFile();
            }
            
            Container.Delete(removedItem);
        }
        
        protected override bool GetReadOnly() => false;
    }
}
