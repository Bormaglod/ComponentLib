//-----------------------------------------------------------------------
// <copyright file="TypeExtension.cs" company="Sergey Teplyashin">
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
// <date>15.01.2015</date>
// <time>13:38</time>
// <summary>Defines the TypeExtension class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    public static class TypeExtension
    {
        public static IDictionary<PropertyInfo, TAttr> GetProperties<TAttr>(this Type value) where TAttr: Attribute
        {
            Dictionary<PropertyInfo, TAttr> fields = new Dictionary<PropertyInfo, TAttr>();
            foreach (PropertyInfo m in value.GetProperties())
            {
                TAttr a = Attribute.GetCustomAttribute(m, typeof(TAttr)) as TAttr;
                if (a != null)
                {
                    fields.Add(m, a);
                }
            }
            
            return fields;
        }
        
        public static bool Inherits(this Type type, Type @from)
        {
            if (type == @from)
            {
                return true;
            }
            else if (type == null)
            {
                return false;
            }
            else
            {
                return Inherits(type.BaseType, @from);
            }
        }
    }
}
