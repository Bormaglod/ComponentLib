//-----------------------------------------------------------------------
// <copyright file="BaseObject.cs" company="Sergey Teplyashin">
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
// <date>23.04.2012</date>
// <time>11:46</time>
// <summary>Defines the BaseObject class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    #region Using directives
    
    using System;
    using System.Reflection;
    
    #endregion
    
    /// <summary>
    /// Базовый класс для всех объектов хранящихся в коллекциях базы данных. При создании объекта
    /// IsNew всегда равно true. При добавлении объекта в коллекцию, IsNew присваивается false.
    /// </summary>
    public class BaseObject
    {
        private bool isNew;
        
        public BaseObject()
        {
            this.isNew = true;
        }
        
        /// <summary>
        /// Gets the IsNew flag.
        /// </summary>
        /// <value>Флаг определяющий наличие объекта в коллекции базы данных. Если IsNew равно true,
        /// то объект только что создан, false - объект уже есть в коллекции и записан на диске.</value>
        public bool IsNew
        {
            get { return this.isNew; }
            internal set { this.isNew = value; }
        }
    }
}
