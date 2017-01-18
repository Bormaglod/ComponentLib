//-----------------------------------------------------------------------
// <copyright file="ItemsContent.cs" company="Sergey Teplyashin">
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
// <time>20:14</time>
// <summary>Defines the ItemsContent class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Reports
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Documents;

    public class ItemsContent : Section
    {
        static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemsContent), new PropertyMetadata(OnItemsSourceChanged));
        static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemsContent), new PropertyMetadata(OnItemTemplateChanged));
        static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register("ItemsPanel", typeof(DataTemplate), typeof(ItemsContent), new PropertyMetadata(OnItemsPanelChanged));

        public ItemsContent()
        {
            Helpers.FixupDataContext(this);
            Loaded += ItemsContent_Loaded;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public DataTemplate ItemsPanel
        {
            get { return (DataTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }
        
        void ItemsContent_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateContent(ItemsPanel, ItemTemplate, ItemsSource);
        }

        void GenerateContent(DataTemplate itemsPanel, DataTemplate itemTemplate, IEnumerable itemsSource)
        {
            Blocks.Clear();
            if (itemTemplate != null && itemsSource != null)
            {
                FrameworkContentElement panel = null;

                foreach (object data in itemsSource)
                {
                    if (panel == null)
                    {
                        if (itemsPanel == null)
                        {
                            panel = this;
                        }
                        else
                        {
                            FrameworkContentElement p = Helpers.LoadDataTemplate(itemsPanel);
                            if (!(p is Block))
                            {
                                throw new Exception("ItemsPanel must be a block element");
                            }
                            
                            Blocks.Add((Block)p);
                            panel = Attached.GetItemsHost(p);
                            if (panel == null)
                            {
                                throw new Exception("ItemsHost not found. Did you forget to specify Attached.IsItemsHost?");
                            }
                        }
                    }
                    
                    FrameworkContentElement element = Helpers.LoadDataTemplate(itemTemplate);
                    element.DataContext = data;
                    Helpers.UnFixupDataContext(element);
                    if (panel is Section)
                    {
                        ((Section)panel).Blocks.Add(Helpers.ConvertToBlock(data, element));
                    }
                    else if (panel is TableRowGroup)
                    {
                        ((TableRowGroup)panel).Rows.Add((TableRow)element);
                    }
                    else
                    {
                        throw new Exception(String.Format("Don't know how to add an instance of {0} to an instance of {1}", element.GetType(), panel.GetType()));
                    }
                }
            }
        }

        void GenerateContent()
        {
            GenerateContent(ItemsPanel, ItemTemplate, ItemsSource);
        }

        void OnItemsSourceChanged(IEnumerable newValue)
        {
            if (IsLoaded)
            {
                GenerateContent(ItemsPanel, ItemTemplate, newValue);
            }
        }

        void OnItemTemplateChanged(DataTemplate newValue)
        {
            if (IsLoaded)
            {
                GenerateContent(ItemsPanel, newValue, ItemsSource);
            }
        }

        void OnItemsPanelChanged(DataTemplate newValue)
        {
            if (IsLoaded)
            {
                GenerateContent(newValue, ItemTemplate, ItemsSource);
            }
        }

        static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsContent)d).OnItemsSourceChanged((IEnumerable)e.NewValue);
        }

        static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsContent)d).OnItemTemplateChanged((DataTemplate)e.NewValue);
        }

        static void OnItemsPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsContent)d).OnItemsPanelChanged((DataTemplate)e.NewValue);
        }
    }
}
