//-----------------------------------------------------------------------
// <copyright file="UnicodeCategoryConverter.cs" company="Sergey Teplyashin">
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
// <time>10:06</time>
// <summary>Defines the UnicodeCategoryConverter class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Linq;
    using System.Reflection;
    
    public class UnicodeCategoryConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            UnicodeView uv = ((UnicodeView)context.Instance);
            UnicodeCollection ul = uv.Collection;
            return new TypeConverter.StandardValuesCollection(ul.GetCategories(uv.UnicodeType).ToList());
        }
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }
        
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) && (value is UnicodeCategory))
            {
                ConstructorInfo ci = typeof(UnicodeCategory).GetConstructor(
                    new [] {
                        typeof(UnicodeType), 
                        typeof(string),
                        typeof(Int32),
                        typeof(Int32)
                    });
                if (ci != null)
                {
                    UnicodeCategory c = ((UnicodeCategory)value);
                    return new InstanceDescriptor(
                        ci,
                        new object[] {
                            c.UnicodeType,
                            c.Name,
                            c.FirstCode,
                            c.LastCode
                        }
                    );
                }
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
        
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(InstanceDescriptor))
            {
                return true;
            }
            
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }
        
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            InstanceDescriptor id = value as InstanceDescriptor;
            if (id != null)
            {
                IEnumerator ie = id.Arguments.GetEnumerator();
                object[] obj = new object[4];
                int i = 0;
                while (ie.MoveNext())
                {
                    obj[i++] = ie.Current;
                }
                
                return new UnicodeCategory((UnicodeType)obj[0], (string)obj[1], (int)obj[2], (int)obj[3]);
            }
            
            string categoryName = value as string;
            if (categoryName != null)
            {
                UnicodeView uv = ((UnicodeView)context.Instance);
                UnicodeCollection ul = uv.Collection;
                UnicodeCategory cat = ul.GetCategory(categoryName);
                return new UnicodeCategory(cat);
            }
            
            return base.ConvertFrom(context, culture, value);
        }
        
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
