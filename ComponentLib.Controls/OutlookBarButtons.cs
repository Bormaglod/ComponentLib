//-----------------------------------------------------------------------
// <copyright file="OutlookBarButtons.cs" company="Sergey Teplyashin">
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
// <date>02.01.2015</date>
// <time>0:58</time>
// <summary>Defines the OutlookBarButtons class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections;
    using System.Drawing;
    
    /// <summary>
    /// the OutlookBarButtons class contains the list of this.listButtons
    /// it manages adding and removing this.listButtons, and updates the Outlookbar control
    /// respectively. Note that this is a class, not a control!
    /// </summary>
    public class OutlookBarButtons : CollectionBase
    {
        protected OutlookBar parentBar;
            
        internal OutlookBarButtons(OutlookBar parentBar)
        {
            this.parentBar = parentBar;
        }
        
        public OutlookBarButton this[int index]
        {
            get { return (OutlookBarButton)List[index]; }
        }
        
        public OutlookBar Parent
        {
            get { return parentBar; }
        }

        public void Add(OutlookBarButton item)
        {
            if (List.Count == 0)
            {
                Parent.SelectedButton = item;
            }
            
            List.Add(item);
            item.Parent = Parent;
            Parent.ButtonlistChanged();
        }

        public OutlookBarButton Add(string text, Image image)
        {
            OutlookBarButton b = new OutlookBarButton(parentBar);
            b.Text = text;
            b.Image = image;
            Add(b);
            return b;
        }

        public OutlookBarButton Add(string text)
        {
            return Add(text, null);
        }

        /*public OutlookBarButton Add()
        {
            return Add();
        }*/

        public void Remove(OutlookBarButton button)
        {
            if (Parent.SelectedButton == button)
            {
                Parent.SelectedButton = null;
            }
            
            List.Remove(button);
            Parent.ButtonlistChanged();
        }

        public int IndexOf(OutlookBarButton value)
        {
            return List.IndexOf(value);
        }
        
        protected override void OnInsertComplete(int index, object value)
        {
            OutlookBarButton b = (OutlookBarButton)value;
            b.Parent = parentBar;
            Parent.ButtonlistChanged();
            base.OnInsertComplete(index, value);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            OutlookBarButton b = (OutlookBarButton)newValue;
            b.Parent = parentBar;
            Parent.ButtonlistChanged();
            base.OnSetComplete(index, oldValue, newValue);
        }

        protected override void OnClearComplete()
        {
            Parent.ButtonlistChanged();
            base.OnClearComplete();
        }
    }
}
