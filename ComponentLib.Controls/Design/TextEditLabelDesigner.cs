///-----------------------------------------------------------------------
/// <copyright file="TextEditLabelDesigner.cs" company="Sergey Teplyashin">
///     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
/// </copyright>
/// <author>Тепляшин Сергей Васильевич</author>
/// <email>sergio.teplyashin@gmail.com</email>
/// <license>
///     This program is free software; you can redistribute it and/or modify
///     it under the terms of the GNU General Public License as published by
///     the Free Software Foundation; either version 3 of the License, or
///     (at your option) any later version.
///
///     This program is distributed in the hope that it will be useful,
///     but WITHOUT ANY WARRANTY; without even the implied warranty of
///     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///     GNU General Public License for more details.
///
///     You should have received a copy of the GNU General Public License
///     along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>
/// <date>27.11.2012</date>
/// <time>10:15</time>
/// <summary>Defines the TextEditLabelDesigner class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    #region Using directives
    
    using System;
    using System.Windows.Forms.Design;
    
    #endregion
    
    public class TextEditLabelDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules rules = base.SelectionRules;
                TextEditLabel control = (TextEditLabel)this.Control;
                
                rules = SelectionRules.Visible |
                    SelectionRules.Moveable |
                    SelectionRules.LeftSizeable |
                    SelectionRules.RightSizeable;
                if (control.Multiline)
                {
                    rules |= SelectionRules.TopSizeable |
                        SelectionRules.BottomSizeable;
                }
                
                return rules;
            }
        }
    }
}
