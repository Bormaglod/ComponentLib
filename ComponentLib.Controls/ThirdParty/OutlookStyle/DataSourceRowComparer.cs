//-----------------------------------------------------------------------
// <copyright file="DataSourceRowComparer.cs" company="Sergey Teplyashin">
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
// <time>14:58</time>
// <summary>Defines the DataSourceRowComparer class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    using System.Collections;
    
    /// <summary>
    /// because the DataSourceRow class is a wrapper class around the real data,
    /// the compared object used to sort the real data is wrapped by this DataSourceRowComparer class.
    /// </summary>
    internal class DataSourceRowComparer : IComparer
    {
        IComparer baseComparer;
        public DataSourceRowComparer(IComparer baseComparer)
        {
            this.baseComparer = baseComparer;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            DataSourceRow r1 = (DataSourceRow)x;
            DataSourceRow r2 = (DataSourceRow)y;
            return baseComparer.Compare(r1.BoundItem, r2.BoundItem);
        }

        #endregion
    }
}
