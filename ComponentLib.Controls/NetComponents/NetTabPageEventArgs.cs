//-----------------------------------------------------------------------
// <copyright file="NetTabPageEventArgs.cs" company="Sergey Teplyashin">
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
// <date>23.11.2006</date>
// <time>13:01</time>
// <summary>Defines the NetTabPageEventArgs class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    
    public class NetTabPageEventArgs : EventArgs
    {
        int index;
        readonly NetTabPage newPage;
        readonly NetTabPage oldPage;
        
        public NetTabPageEventArgs(int index, NetTabPage page)
        {
            this.index = index;
            this.newPage = page;
        }
        
        public NetTabPageEventArgs(int index, NetTabPage oldPage, NetTabPage newPage)
        {
            this.newPage = newPage;
            this.oldPage = oldPage;
            this.index = index;
        }
        
        public int Index
        {
            get { return index; }
        }
        
        public NetTabPage Page
        {
            get { return newPage; }
        }
        
        public NetTabPage NewPage
        {
            get { return newPage; }
        }
        
        public NetTabPage OldPage
        {
            get { return oldPage; }
        }
    }
}
