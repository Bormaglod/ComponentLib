//-----------------------------------------------------------------------
// <copyright file="TreeNodeColumn.cs" company="Sergey Teplyashin">
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
// <date>23.09.2010</date>
// <time>22:10</time>
// <summary>Defines the TreeNodeColumn class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Windows.Forms;
    
    public class TreeNodeColumn : TreeNode
    {
        string farText;
        
        public TreeNodeColumn()
        {
            farText = string.Empty;
        }

        public TreeNodeColumn(string text) : base(text)
        {
        }
        
        public string FarText
        {
            get
            {
                return farText;
            }
            
            set
            {
                if (farText != value)
                {
                    farText = value;
                    TreeViewColumns treeView = TreeView as TreeViewColumns;
                    if (treeView != null)
                    {
                        treeView.DrawTreeNode(this);
                    }
                }
            }
        }
    }
}
