//-----------------------------------------------------------------------
// <copyright file="DataSourceRow.cs" company="Sergey Teplyashin">
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
// <time>14:59</time>
// <summary>Defines the DataSourceRow class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    using System.Collections;

    /// <summary>
    /// The DataSourceRow is a wrapper row class around the real bound data. This row is an abstraction
    /// so different types of data can be encaptulated in this class, although for the OutlookGrid it will
    /// simply look as one type of data. 
    /// Note: this class does not implement all row wrappers optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class DataSourceRow : CollectionBase
    {
        DataSourceManager manager;
        object boundItem;
        public DataSourceRow(DataSourceManager manager, object boundItem)
        {
            this.manager = manager;
            this.boundItem = boundItem;
        }

        public object this[int index]
        {
            get
            {
                return List[index];
            }
        }

        public object BoundItem
        {
            get
            {
                return boundItem;
            }
        }

        public int Add(object val)
        {
            return List.Add(val);
        }

    }
}
