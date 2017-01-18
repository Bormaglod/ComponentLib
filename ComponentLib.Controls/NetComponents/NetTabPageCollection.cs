//-----------------------------------------------------------------------
// <copyright file="NetTabPageCollection.cs" company="Sergey Teplyashin">
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
// <date>19.11.2006</date>
// <time>20:41</time>
// <summary>Defines the NetTabPageCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;
    
    public class NetTabPageCollection : CollectionBase
    {
        readonly NetTabControl owner;
        
        public NetTabPageCollection(NetTabControl owner)
        {
            this.owner = owner;
        }
        
        public event EventHandler<EventArgs> ClearComplete;
        public event EventHandler<NetTabPageEventArgs> InsertComplete;
        public event EventHandler<NetTabPageEventArgs> RemoveComplete;
        public event EventHandler<NetTabPageEventArgs> SetComplete;
        
        public NetTabPage this[int index]
        {
            get { return (List[index] as NetTabPage); }
        }
        
        public int Add(NetTabPage tabPage)
        {
            return List.Add(tabPage);
        }
        
        public void AddRange(NetTabPage[] pages)
        {
            foreach (NetTabPage page in pages)
            {
                Add(page);
            }
        }

        public void Remove(NetTabPage tabPage)
        {
            List.Remove(tabPage);
        }

        public void Insert(int index, NetTabPage tabPage)
        {
            List.Insert(index, tabPage);
        }

        public bool Contains(NetTabPage tabPage)
        {
            return List.Contains(tabPage);
        }
        
        public int IndexOf(NetTabPage tabPage)
        {
            return List.IndexOf(tabPage);
        }
        
        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(NetTabPage))
            {
                throw new ArgumentException("Value must be of type NetTabPage.", "value");
            }
        }
        
        protected override void OnClearComplete()
        {
            if (ClearComplete != null)
            {
                ClearComplete(this, new EventArgs());
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            NetTabPage page = value as NetTabPage;
            SetUpPage(page);
            if (InsertComplete != null)
            {
                InsertComplete(this, new NetTabPageEventArgs(index, page));
            }
        }
        
        protected override void OnRemoveComplete(int index, object value)
        {
            if (RemoveComplete != null)
            {
                RemoveComplete(this, new NetTabPageEventArgs(index, value as NetTabPage));
            }
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (SetComplete != null)
            {
                SetComplete(this, new NetTabPageEventArgs(index, oldValue as NetTabPage, newValue as NetTabPage));
            }
        }
        
        internal void UpdatePagesSettings(bool hidePage)
        {
            foreach (NetTabPage page in this)
            {
                SetUpPage(page, hidePage);
                page.UpdateBackground();
            }
        }
        
        void SetUpPage(NetTabPage page, bool hidePage = true)
        {
            page.SuspendLayout();
            page.Visible &= !hidePage;

            int width = owner.Size.Width;
            int height = owner.Size.Height;
            if (owner.Alignment == TabAlignment.Left || owner.Alignment == TabAlignment.Right)
            {
                width -= owner.TabHeight + 1;
                height -= 2;
            }
            else
            {
                width -= 2;
                height -= owner.TabHeight + 1;
            }
            
            page.Size = new Size(width, height);

            switch(owner.Alignment)
            {
                case TabAlignment.Left:
                    page.Location = new Point(owner.TabHeight, 1);
                    break;
                case TabAlignment.Top:
                    page.Location = new Point(1, owner.TabHeight);
                    break;
                default:
                    page.Location = new Point(1, 1);
                    break;
            }

            page.ResumeLayout(false);
        }
    }
}
