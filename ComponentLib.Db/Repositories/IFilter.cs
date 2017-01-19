//-----------------------------------------------------------------------
// <copyright file="IFilter.cs" company="Тепляшин Сергей Васильевич">
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
// <date>14.11.2016</date>
// <time>15:09</time>
// <summary>Defines the IFilter class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    
    public interface IFilter
    {
        event EventHandler<EventArgs> EnableChanged;
        
        event EventHandler<EventArgs> DataChanged;

        /// <summary>
        /// Свойство возвращает или устанавливает флаг для активизации фильтра.
        /// </summary>
        bool Enabled { get; set; }
        
        /// <summary>
        /// Свойство возвращает true, если поля фильтра не установлены.
        /// </summary>
        bool Empty { get; }
        
        /// <summary>
        /// Уникальный идентификатор фильтра.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Наименование фильтра.
        /// </summary>
        string Name { get; }
        
        object Data { get; }
        
        /// <summary>
        /// Метод очищает все поля фильтра.
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Активизирует фильтр, передавая ему значения полей в объекте data.
        /// </summary>
        /// <param name="data"></param>
        void EnableFilter(object data);
    }
}
