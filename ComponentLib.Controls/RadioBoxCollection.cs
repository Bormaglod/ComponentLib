//-----------------------------------------------------------------------
// <copyright file="RadioBoxCollection.cs" company="Sergey Teplyashin">
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
// <date>01.06.2012</date>
// <time>14:48</time>
// <summary>Defines the RadioBoxCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    public class RadioBoxCollection : CollectionBase
    {
        readonly RadioBox owner;
        
        public RadioBoxCollection(RadioBox owner)
        {
            this.owner = owner;
        }
        
        public RadioBoxCollection(RadioBox owner, IEnumerable<string> collection) : this(owner)
        {
            foreach (string item in collection)
            {
                List.Add(item);
            }
        }
        
        public string this[int index]
        {
            get { return (string)List[index]; }
            set { List[index] = value; }
        }
        
        public int Add(string item)
        {
            int index = List.Add(item);
            return index;
        }
        
        public int IndexOf(string item)
        {
            return List.IndexOf(item);
        }
        
        public void Insert(int index, string item)
        {
            List.Insert(index, item);
        }
        
        public void Remove(string item)
        {
            List.Remove(item);
        }
        
        public bool Contains(string item)
        {
            return List.Contains(item);
        }
        
        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            owner.InsertButton(index, (string)value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            owner.RemoveButton((string)value);
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            base.OnSet(index, oldValue, newValue);
            owner.UpdateButton(index, (string)oldValue, (string)newValue);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(string))
            {
                throw new ArgumentException("Value must be of type String.", "value");
            }
        }
    }
}
