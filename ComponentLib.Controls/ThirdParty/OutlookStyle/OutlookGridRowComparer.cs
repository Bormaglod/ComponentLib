//-----------------------------------------------------------------------
// <copyright file="OutlookGridRowComparer.cs" company="Sergey Teplyashin">
//     Copyright (c) 2006 Herre Kuijpers - <herre@xs4all.nl>
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
// <time>14:53</time>
// <summary>Defines the OutlookGridRowComparer class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    /// <summary>
    /// the OutlookGridRowComparer object is used to sort unbound data in the OutlookGrid.
    /// currently the comparison is only done for string values. 
    /// therefore dates or numbers may not be sorted correctly.
    /// Note: this class is not implemented optimally. It is merely used for demonstration purposes
    /// </summary>
    public class OutlookGridRowComparer : IComparer
    {
        ListSortDirection direction;
        int columnIndex;

        public OutlookGridRowComparer(int columnIndex, ListSortDirection direction)
        {
            this.columnIndex = columnIndex;
            this.direction = direction;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            OutlookGridRow obj1 = (OutlookGridRow)x;
            OutlookGridRow obj2 = (OutlookGridRow)y;
            return string.Compare(obj1.Cells[this.columnIndex].Value.ToString(), obj2.Cells[this.columnIndex].Value.ToString()) * (direction == ListSortDirection.Ascending ? 1 : -1);
        }
        #endregion
    }
}
