//-----------------------------------------------------------------------
// <copyright file="IFilterGeneric.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
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
// <date>18.01.2012</date>
// <time>13:08</time>
// <summary>Defines the IFilter interface.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    
    public interface IFilter<T> : IFilter where T: Entity
    {
        /// <summary>
        /// Метод возвращает true, если параметр entity входит в диапазон установленный полями фильтра.
        /// </summary>
        /// <param name="entity">Параметр для проверки вхождения в диапазон установленного фильтра.</param>
        /// <returns>true, если параметр t входит в диапазон установленный полями фильтра.</returns>
        bool Contains(T entity);
    }
}
