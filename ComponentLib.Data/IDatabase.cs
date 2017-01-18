//-----------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
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
// <date>20.03.2012</date>
// <time>14:04</time>
// <summary>Defines the IDatabase interface.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    using System;
    using System.Collections.Generic;
    using Db4objects.Db4o;
    
    /// <summary>
    /// Description of IDatabase.
    /// </summary>
    public interface IDatabase
    {
        IObjectContainer Data { get; }
        
        /// <summary>
        /// Gets the opened flag.
        /// </summary>
        /// <value>true, если база данных открыта.</value>
        bool Opened { get; }
        
        DatabaseState State { get; }
        
        /// <summary>
        /// Gets the collection list stored in IDatabase.
        /// </summary>
        /// <value>Список коллекций, хранящихся в IDatabase.</value>
        IEnumerable<IExtCollection> Collections { get; }
        
        /// <summary>
        /// Gets the true if database is empty, false otherwise.
        /// </summary>
        bool IsEmpty { get; }
        
        IBaseCollection<T> GetCollection<T>();
    }
}
