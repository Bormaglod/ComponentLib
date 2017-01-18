//-----------------------------------------------------------------------
// <copyright file="BlockTemplateContent.cs" company="Sergey Teplyashin">
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
// <time>19:36</time>
// <summary>Defines the BlockTemplateContent class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Reports
{
    using System.Windows;
    using System.Windows.Documents;

    public class BlockTemplateContent : Section
    {
        static readonly DependencyProperty TemplateProperty = DependencyProperty.Register("Template", typeof(DataTemplate), typeof(BlockTemplateContent), new PropertyMetadata(OnTemplateChanged));

        public DataTemplate Template
        {
            get { return (DataTemplate)GetValue(TemplateProperty); }
            set { SetValue(TemplateProperty, value); }
        }


        public BlockTemplateContent()
        {
            Helpers.FixupDataContext(this);
            Loaded += BlockTemplateContent_Loaded;
        }


        private void BlockTemplateContent_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateContent(Template);
        }


        private void GenerateContent(DataTemplate template)
        {
            Blocks.Clear();
            if (template != null)
            {
                FrameworkContentElement element = Helpers.LoadDataTemplate(template);
                Blocks.Add((Block)element);
            }
        }

        private void OnTemplateChanged(DataTemplate dataTemplate)
        {
            GenerateContent(dataTemplate);
        }


        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockTemplateContent)d).OnTemplateChanged((DataTemplate)e.NewValue);
        }
    }
}
