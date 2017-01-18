///-----------------------------------------------------------------------
/// <copyright file="PropertyCollection.cs" company="Sergey Teplyashin">
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
/// <date>21.02.2013</date>
/// <time>13:11</time>
/// <summary>Defines the PropertyCollection class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    #region Using directives
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    #endregion
    
    /// <summary>
    /// Description of PropertyCollection.
    /// </summary>
    public class PropertyCollection<T> where T: BaseObject
    {
        private T obj;
        private List<PropertyObject> properties;
        
        public PropertyCollection(T obj)
        {
            this.obj = obj;
            this.properties = new List<PropertyObject>();
        }
        
        public T Object
        {
            get { return this.obj; }
        }
        
        public void AddProperty(string name, object valueProperty)
        {
            this.properties.Add(new PropertyObject(name, valueProperty));
        }
        
        public object GetPropertyValue(string name)
        {
            PropertyObject po = this.properties.FirstOrDefault(p => p.Name == name);
            if (po != null)
            {
                return po.Value;
            }
            
            return null;
        }
    }
}
