//-----------------------------------------------------------------------
// <copyright file="UnicodeType.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
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
// <date>26.10.2011</date>
// <time>8:25</time>
// <summary>Defines the UnicodeType class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    
    public class UnicodeType
    {
        UnicodeIndex id;
        readonly string name;
        
        public UnicodeType(UnicodeIndex identifier, string unicodeName)
        {
            id = identifier;
            name = unicodeName;
        }
        
        public UnicodeType(UnicodeType unicodeType)
        {
            id = unicodeType.Id;
            name = unicodeType.Name;
        }
        
        /// <summary>
        /// Идентификатор набора символов unicode.
        /// </summary>
        public UnicodeIndex Id
        {
            get { return id; }
        }
        
        /// <summary>
        /// Наименование набора символов unicode. 
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        
        public override string ToString()
        {
            return name;
        }
    }
}
