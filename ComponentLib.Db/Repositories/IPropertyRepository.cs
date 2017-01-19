//-----------------------------------------------------------------------
// <copyright file="IPropertyRepository.cs" company="Тепляшин Сергей Васильевич">
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
// <date>24.07.2015</date>
// <time>9:07</time>
// <summary>Defines the IPropertyRepository class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    public interface IPropertyRepository
    {
        /// <summary>
        /// Свойство возвращает true, если коллекция (с учетом фильтра) не содержит ни одного объекта.
        /// </summary>
        bool IsEmpty { get; }
        
        /// <summary>
        /// Свойство возвращает тип данных хранящихся в коллекции.
        /// </summary>
        Type ContentsType { get; }
        
        /// <summary>
        /// Свойство возвращает количество объектов в коллекции.
        /// </summary>
        long Count { get; }
        
        /// <summary>
        /// Возвращает уникальный идентификатор коллекции.
        /// </summary>
        int Identifier { get; }
        
        /// <summary>
        /// Возвращает наименование коллекции.
        /// </summary>
        string Name { get; }
        
        bool ReadOnly { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IEnumerable Objects { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IPropertyRepository> Dependences { get; }
    }
}
