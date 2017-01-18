//-----------------------------------------------------------------------
// <copyright file="Attached.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2016 Sergey Teplyashin. All rights reserved.
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
// <time>19:31</time>
// <summary>Defines the Attached class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Reports
{
    using System;
    using System.Windows;

    public static class Attached
    {
        static readonly DependencyProperty IsItemsHostProperty = DependencyProperty.RegisterAttached("IsItemsHost", typeof(bool), typeof(Attached), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.NotDataBindable, OnIsItemsHostChanged));
        static readonly DependencyProperty ItemsHostProperty = DependencyProperty.RegisterAttached("ItemsHost", typeof(FrameworkContentElement), typeof(Attached), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.NotDataBindable));

        public static bool GetIsItemsHost(DependencyObject target)
        {
            return (bool)target.GetValue(IsItemsHostProperty);
        }

        public static void SetIsItemsHost(DependencyObject target, bool value)
        {
            target.SetValue(IsItemsHostProperty, value);
        }
        
        public static FrameworkContentElement GetItemsHost(DependencyObject dp)
        {
            return (FrameworkContentElement)dp.GetValue(ItemsHostProperty);
        }

        static void SetItemsHost(FrameworkContentElement element)
        {
            FrameworkContentElement parent = element;
            while (parent.Parent != null)
                parent = (FrameworkContentElement)parent.Parent;
            parent.SetValue(ItemsHostProperty, element);
        }

        static void OnIsItemsHostChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                FrameworkContentElement element = (FrameworkContentElement)d;
                if (element.IsInitialized)
                    SetItemsHost(element);
                else
                    element.Initialized += ItemsHost_Initialized;
            }
        }

        static void ItemsHost_Initialized(object sender, EventArgs e)
        {
            FrameworkContentElement element = (FrameworkContentElement)sender;
            element.Initialized -= ItemsHost_Initialized;
            SetItemsHost(element);
        }
    }
}
