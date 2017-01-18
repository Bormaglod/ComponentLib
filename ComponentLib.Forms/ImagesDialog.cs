//-----------------------------------------------------------------------
// <copyright file="ImagesDialog.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
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
// <time>9:51</time>
// <summary>Defines the ImagesDialog class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Core;

    /// <summary>
    /// Description of ImagesDialog.
    /// </summary>
    public partial class ImagesDialog : KryptonForm
    {
        private CommandImageCollection images;
        
        public ImagesDialog()
        {
            InitializeComponent();
            this.images = new CommandImageCollection();
            foreach (CommandImage imageData in this.images)
            {
                if (imageData.Size == 16)
                {
                    Image im = CommandImageCollection.GetImage(imageData.Name);
                    this.imageList.Images.Add(imageData.Name, im);
                }
            }
            
            foreach (string category in this.images.Categories)
            {
                this.comboCategory.Items.Add(category);
            }
            
            this.comboCategory.SelectedIndex = 0;
        }
        
        new public static string Show()
        {
            ImagesDialog dlg = new ImagesDialog();
            return dlg.SelectImage();
        }
        
        private string SelectImage()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                if (this.listImages.SelectedItems.Count > 0)
                {
                    return this.listImages.SelectedItems[0].Text;
                }
            }
            
            return string.Empty;
        }
        
        private void ComboCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            this.listImages.Items.Clear();
            if (this.comboCategory.SelectedIndex != -1)
            {
                string currentCategory = (string)this.comboCategory.SelectedItem;
                foreach (CommandImage image in this.images.GetImages(currentCategory))
                {
                    if (image.Size == 16)
                    {
                        this.listImages.Items.Add(image.Name, image.Name);
                    }
                }
            }
        }
        
        private void ListImagesDoubleClick(object sender, EventArgs e)
        {
            if (this.listImages.SelectedItems.Count > 0)
            {
                this.buttonOK.PerformClick();
            }
        }
    }
}
