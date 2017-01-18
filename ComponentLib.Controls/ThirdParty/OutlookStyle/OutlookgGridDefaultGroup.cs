//-----------------------------------------------------------------------
// <copyright file="OutlookgGridDefaultGroup.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
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
// <date>27.01.2011</date>
// <time>14:49</time>
// <summary>Defines the OutlookgGridDefaultGroup class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// each arrange/grouping class must implement the IOutlookGridGroup interface
    /// the Group object will determine for each object in the grid, whether it
    /// falls in or outside its group.
    /// It uses the IComparable.CompareTo function to determine if the item is in the group.
    /// </summary>
    public class OutlookgGridDefaultGroup : IOutlookGridGroup
    {
        protected object val;
        protected string textGroup;
        protected bool collapsedGroup;
        protected DataGridViewColumn columnView;
        protected int count;
        protected int heightGroup;
        
        public OutlookgGridDefaultGroup()
        {
            this.val = null;
            this.columnView = null;
            this.heightGroup = 34; // default height
        }
        
        #region IOutlookGridGroup Members

        public virtual string Text
        {
            get
            {
                if (this.columnView == null)
                {
                    return string.Format("Unbound group: {0} ({1})", Value.ToString(), this.count == 1 ? "1 item" : this.count.ToString() + " items");
                }
                else
                {
                    return string.Format("{0}: {1} ({2})", this.columnView.HeaderText, Value.ToString(), this.count == 1 ? "1 item" : this.count.ToString() + " items"); 
                }
        	}
        	
            set { this.textGroup = value; }
        }

        public virtual object Value
        {
            get { return this.val; }
            set { this.val = value; }
        }

        public virtual bool Collapsed
        {
            get { return this.collapsedGroup; }
            set { this.collapsedGroup = value; }
        }

        public virtual DataGridViewColumn Column
        {
            get { return this.columnView; }
            set { this.columnView = value; }
        }

        public virtual int ItemCount
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public virtual int Height
        {
            get { return this.heightGroup; }
            set { this.heightGroup = value; }
        }

        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            OutlookgGridDefaultGroup gr = new OutlookgGridDefaultGroup();
            gr.columnView = this.columnView;
            gr.val = this.val;
            gr.collapsedGroup = this.collapsedGroup;
            gr.textGroup = this.textGroup;
            gr.heightGroup = this.heightGroup;
            return gr;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// this is a basic string comparison operation. 
        /// all items are grouped and categorised based on their string-appearance.
        /// </summary>
        /// <param name="obj">the value in the related column of the item to compare to</param>
        /// <returns></returns>
        public virtual int CompareTo(object obj)
        {
            return string.Compare(val.ToString(), obj.ToString());
        }

        #endregion
    }
}
