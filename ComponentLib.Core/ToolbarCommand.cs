//-----------------------------------------------------------------------
// <copyright file="ToolbarCommand.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2017 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>02.11.2011</date>
// <time>15:28</time>
// <summary>Defines the ToolbarCommand class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System.Windows.Forms;
    
    public class ToolbarCommand
    {
        Command command;
        ToolStripItemDisplayStyle displayStyle;
        
        public ToolbarCommand(Command command, int position) : this(command, position, false, false)
        {
        }
        
        public ToolbarCommand(Command command, int position, bool viewText) : this(command, position, true, viewText)
        {
        }
        
        public ToolbarCommand(Command command, int position, ToolStripItemDisplayStyle style) : this(command, position)
        {
            displayStyle = style;
        }
        
        public ToolbarCommand(Command command, int position, bool viewImage, bool viewText)
        {
            this.command = command;
            this.Position = position;
            if (viewImage == viewText)
            {
                displayStyle = viewImage ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.None; 
            }
            else
            {
                displayStyle = viewImage ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.Text;
            }
        }
        
        public Command Command => command;
        
        public int Position { get; set; }
        
        public bool ViewImage => displayStyle == ToolStripItemDisplayStyle.Image || displayStyle == ToolStripItemDisplayStyle.ImageAndText;
        
        public bool ViewText => displayStyle == ToolStripItemDisplayStyle.Text || displayStyle == ToolStripItemDisplayStyle.ImageAndText;
        
        public ToolStripItemDisplayStyle DisplayStyle => displayStyle;
    }
}
