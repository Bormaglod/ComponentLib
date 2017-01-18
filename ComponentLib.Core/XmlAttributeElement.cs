//-----------------------------------------------------------------------
// <copyright file="XmlAttributeElement.cs" company="Sergey Teplyashin">
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
// <date>13.05.2011</date>
// <time>10:46</time>
// <summary>Defines the XmlAttributeElement class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core.Xml
{
    #region Using directives
    
    using System;
    
    #endregion
    
    /// <summary>
    /// Description of AttributeElement.
    /// </summary>
    public class XmlAttributeElement
    {
        private bool check;
        
        public XmlAttributeElement(string name, string value) : this(name, value, string.Empty)
        {
            this.check = false;
        }
        
        public XmlAttributeElement(string name, string value, string checkValue)
        {
            this.Name = name;
            this.Value = value;
            this.CheckValue = checkValue;
            this.check = true;
        }
        
        public string Name { get; set; }
        
        public string Value { get; set; }
        
        public string CheckValue { get; set; }
        
        public bool Checked
        {
            get { return this.check && (string.IsNullOrEmpty(this.Value) || this.Value == this.CheckValue); }
        }
    }
}
