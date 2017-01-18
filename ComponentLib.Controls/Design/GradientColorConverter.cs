///-----------------------------------------------------------------------
/// <copyright file="GradientColorConverter.cs" company="Sergey Teplyashin">
///     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
/// </copyright>
/// <author>Тепляшин Сергей Васильевич</author>
/// <email>sergio.teplyashin@gmail.com</email>
/// <license>
///     This program is free software; you can redistribute it and/or modify
///     it under the terms of the GNU General Public License as published by
///     the Free Software Foundation; either version 3 of the License, or
///     (at your option) any later version.
///
///     This program is distributed in the hope that it will be useful,
///     but WITHOUT ANY WARRANTY; without even the implied warranty of
///     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///     GNU General Public License for more details.
///
///     You should have received a copy of the GNU General Public License
///     along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>
/// <date>29.11.2006</date>
/// <time>19:34</time>
/// <summary>Defines the GradientColorConverter class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    #region Using directives
    
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    
    #endregion
    
    /// <summary>
    /// Класс предназначен для редактирования свойств типа GradientColor
    /// </summary>
    public class GradientColorConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            
            return base.CanConvertTo(context, destinationType);
        }
        
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            GradientColor c = value as GradientColor;
            if (destinationType == typeof(InstanceDescriptor) && c != null)
            {
                ConstructorInfo ci = typeof(GradientColor).GetConstructor(
                    new Type[] {
                        typeof(Color), 
                        typeof(Color), 
                        typeof(GradientFill)});
                
                if (ci != null)
                {
                    return new InstanceDescriptor(
                        ci,
                        new object[] { c.Color1,  c.Color2,  c.Fill }
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
            
            return base.CanConvertFrom(context, sourceType);
        }
        
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            InstanceDescriptor id = value as InstanceDescriptor;
            if (id != null)
            {
                IEnumerator ie = id.Arguments.GetEnumerator();
                object[] obj = new object[3];
                int i = 0;
                while (ie.MoveNext())
                {
                    obj[i++] = ie.Current;
                }
                
                return new GradientColor((Color)obj[0], (Color)obj[1], (GradientFill)obj[2]);
            }
            
            return base.ConvertFrom(context, culture, value);
        }
    }
}
