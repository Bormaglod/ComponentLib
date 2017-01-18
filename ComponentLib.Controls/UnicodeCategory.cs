//-----------------------------------------------------------------------
// <copyright file="UnicodeCategory.cs" company="Sergey Teplyashin">
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
// <time>8:27</time>
// <summary>Defines the UnicodeCategory class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    
    public class UnicodeCategory
    {
        UnicodeType type;
        readonly string name;
        int first;
        int last;
        
        public UnicodeCategory(UnicodeType unicodeType, string categoryName, int firstCode, int lastCode)
        {
            type = unicodeType;
            name = categoryName;
            first = firstCode;
            last = lastCode;
        }
        
        public UnicodeCategory(UnicodeCategory category)
        {
            type = category.UnicodeType;
            name = category.Name;
            first = category.FirstCode;
            last = category.LastCode;
        }
        
        public UnicodeType UnicodeType
        {
            get { return type; }
        }
        
        public string Name
        {
            get { return name; }
        }
        
        public int FirstCode
        {
            get { return first; }
        }
        
        public int LastCode
        {
            get { return last; }
        }
        
        public int CountSymbols
        {
            get { return last - first + 1; }
        }
        
        public override string ToString()
        {
            return name;
        }
    }
}
