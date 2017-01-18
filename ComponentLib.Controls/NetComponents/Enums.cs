//-----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sergey Teplyashin">
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
// <date>12.04.2013</date>
// <time>8:37</time>
// <summary>Defines the enums.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    
    public enum TabControlView
    {
        OneNote,
        
        Square,
        
        Rounded,
        
        Buttons
    }
    
    public enum GradientFill
    {
        Top,
        
        Center,
        
        Bottom,
        
        Linear,
        
        Solid
    }
    
    public enum TabTextDirection
    {
        Horizontal,
        
        Vertical
    }
    
    /// <summary>
    /// Варианты отображения кнопок управления
    /// </summary>
    public enum TabButtonStyles
    {
        None,
        
        NextPrev,
        
        Context,
        
        All
    }
    
    public enum TabButtonAction
    {
        None,
        
        Left,
        
        Right,
        
        Up,
        
        Down,
        
        Action,
        
        Close
    }
    
    internal enum TabButtonStyle
    {
        LeftDown,
        
        RightUp,
        
        Action,
        
        Close
    }
}
