﻿//-----------------------------------------------------------------------
// <copyright file="Fragment.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2017 Sergey Teplyashin. All rights reserved.
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
// <date>20.04.2016</date>
// <time>19:39</time>
// <summary>Defines the Fragment class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Reports
{
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public class Fragment : FrameworkElement
    {
        static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(FrameworkContentElement), typeof(Fragment));

        public FrameworkContentElement Content
        {
            get { return (FrameworkContentElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
}
