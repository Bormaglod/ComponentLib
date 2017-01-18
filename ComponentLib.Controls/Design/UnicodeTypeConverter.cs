//-----------------------------------------------------------------------
// <copyright file="UnicodeTypeConverter.cs" company="Sergey Teplyashin">
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
// <time>10:04</time>
// <summary>Defines the UnicodeTypeConverter class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    
    public class UnicodeTypeConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            UnicodeView uv = ((UnicodeView)context.Instance);
            UnicodeCollection ul = uv.Collection;
            return new TypeConverter.StandardValuesCollection(ul.GetTypes().ToList());
        }
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
            
        }
        
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) && (value is UnicodeType))
            {
                ConstructorInfo ci = typeof(UnicodeType).GetConstructor(
                    new [] {
                        typeof(int), 
                        typeof(string)
                    });
                if (ci != null)
                {
                    UnicodeType c = ((UnicodeType)value);
                    return new InstanceDescriptor(
                        ci,
                        new object[] {
                            c.Id,
                            c.Name
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
        
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            InstanceDescriptor id = (value as InstanceDescriptor);
            if (id != null)
            {
                IEnumerator ie = id.Arguments.GetEnumerator();
                object[] obj = new object[2];
                int i = 0;
                while (ie.MoveNext())
                {
                    obj[i++] = ie.Current;
                }
                
                return new UnicodeType((UnicodeIndex)obj[0], (string)obj[1]);
            }
            
            string unicodeName = value as string;
            if (unicodeName != null)
            {
                UnicodeView uv = ((UnicodeView)context.Instance);
                UnicodeCollection ul = uv.Collection;
                return new UnicodeType(ul[unicodeName]);
            }
            
            return base.ConvertFrom(context, culture, value);
        }
        
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
