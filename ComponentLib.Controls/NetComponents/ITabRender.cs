﻿///-----------------------------------------------------------------------
/// <copyright file="ITabRender.cs" company="Sergey Teplyashin">
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
/// <date>16.04.2013</date>
/// <time>8:56</time>
/// <summary>Defines the ITabRender interface.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.NetComponents
{
    #region Using directives
    
    using System;
    using System.Drawing.Drawing2D;
    
    #endregion
    
    public interface ITabRender
    {
        GraphicsPath NormalPath { get; }
        GraphicsPath SelectedPath { get; }
    }
}
