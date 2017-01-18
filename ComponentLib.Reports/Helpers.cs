﻿//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="Sergey Teplyashin">
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
// <time>19:40</time>
// <summary>Defines the Helpers class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Reports
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System;

    static class Helpers
    {
        /// <summary>
        /// If you use a bindable flow document element more than once, you may encounter a "Collection was modified" exception.
        /// The error occurs when the binding is updated because of a change to an inherited dependency property. The most common scenario 
        /// is when the inherited DataContext changes. It appears that an inherited properly like DataContext is propagated to its descendants. 
        /// When the enumeration of descendants gets to a BindableXXX, the dependency properties of that element change according to the new 
        /// DataContext, which change the (non-dependency) properties. However, for some reason, changing the flow content invalidates the 
        /// enumeration and raises an exception. 
        /// To work around this, one can either DataContext="{Binding DataContext, RelativeSource={RelativeSource AncestorType=FrameworkElement}}" 
        /// in code. This is clumsy, so every derived type calls this function instead (which performs the same thing).
        /// See http://code.logos.com/blog/2008/01/data_binding_in_a_flowdocument.html
        /// </summary>
        /// <param name="element"></param>
        public static void FixupDataContext(FrameworkContentElement element)
        {
            Binding b = new Binding(FrameworkContentElement.DataContextProperty.Name);
            // another approach (if this one has problems) is to bind to an ancestor by ElementName
            b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(FrameworkElement), 1);
            element.SetBinding(FrameworkContentElement.DataContextProperty, b);
        }

        public static void UnFixupDataContext(DependencyObject dp)
        {
            while (InternalUnFixupDataContext(dp))
            {
            }
        }
        
        /// <summary>
        /// Convert "data" to a flow document block object. If data is already a block, the return value is data recast.
        /// </summary>
        /// <param name="dataContext">only used when bindable content needs to be created</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Block ConvertToBlock(object dataContext, object data)
        {
            var block = data as Block;
            if (block != null)
            {
                return block;
            }
            
            var inline = data as Inline;
            if (inline != null)
            {
                return new Paragraph(inline);
            }
            
            var bindingBase = data as BindingBase;
            if (bindingBase != null)
            {
                BindableRun run = new BindableRun();
                var bindingContext = dataContext as BindingBase;
                if (bindingContext != null)
                {
                    run.SetBinding(FrameworkContentElement.DataContextProperty, bindingContext);
                }
                else
                {
                    run.DataContext = dataContext;
                }
                
                run.SetBinding(BindableRun.BoundTextProperty, bindingBase);
                return new Paragraph(run);
            }
            else
            {
                Run run = new Run();
                run.Text = (data == null) ? String.Empty : data.ToString();
                return new Paragraph(run);
            }
        }
        
        public static FrameworkContentElement LoadDataTemplate(DataTemplate dataTemplate)
        {
            object content = dataTemplate.LoadContent();
            var fragment = content as Fragment;
            if (fragment != null)
            {
                return (FrameworkContentElement)fragment.Content;
            }
            
            var textBlock = content as TextBlock;
            if (textBlock != null)
            {
                InlineCollection inlines = textBlock.Inlines;
                if (inlines.Count == 1)
                {
                    return inlines.FirstInline;
                }
                else
                {
                    Paragraph paragraph = new Paragraph();
                    
                    // we can't use an enumerator, since adding an inline removes it from its collection
                    while (inlines.FirstInline != null)
                    {
                        paragraph.Inlines.Add(inlines.FirstInline);
                    }
                    
                    return paragraph;
                }
            }

            throw new Exception("Data template needs to contain a <Fragment> or <TextBlock>");
        }
        
        static bool InternalUnFixupDataContext(DependencyObject dp)
        {
            // only consider those elements for which we've called FixupDataContext(): they all belong to this namespace
            if (dp is FrameworkContentElement && dp.GetType().Namespace == typeof(Helpers).Namespace)
            {
                Binding binding = BindingOperations.GetBinding(dp, FrameworkContentElement.DataContextProperty);
                if (binding != null
                    && binding.Path != null && binding.Path.Path == FrameworkContentElement.DataContextProperty.Name
                    && binding.RelativeSource != null && binding.RelativeSource.Mode == RelativeSourceMode.FindAncestor && binding.RelativeSource.AncestorType == typeof(FrameworkElement) && binding.RelativeSource.AncestorLevel == 1)
                {
                    BindingOperations.ClearBinding(dp, FrameworkContentElement.DataContextProperty);
                    return true;
                }
            }
            
            // as soon as we have disconnected a binding, return. Don't continue the enumeration, since the collection may have changed
            foreach (object child in LogicalTreeHelper.GetChildren(dp))
            {
                var dependencyObject = child as DependencyObject;
                if (dependencyObject != null)
                {
                    if (InternalUnFixupDataContext(dependencyObject))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
    }
}
