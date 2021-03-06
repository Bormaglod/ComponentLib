﻿//-----------------------------------------------------------------------
// <copyright file="StringCollectionConverter.cs" company="Sergey Teplyashin">
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
// <date>09.02.2011</date>
// <time>10:51</time>
// <summary>Defines the StringCollectionConverter class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    using System;
    using System.ComponentModel;
    
    public class StringCollectionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            
        }
        
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof(string) ? Strings.List : base.ConvertTo(context, culture, value, destinationType);
            
        }
    }
}
