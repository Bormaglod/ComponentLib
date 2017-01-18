//-----------------------------------------------------------------------
// <copyright file="ImagesDialog.Designer.cs" company="Sergey Teplyashin">
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
    partial class ImagesDialog
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesDialog));
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.buttonBrowse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.comboCategory = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonRadioButton2 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonRadioButton1 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.textFilter = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.listImages = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            resources.ApplyResources(this.kryptonGroupBox1, "kryptonGroupBox1");
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            resources.ApplyResources(this.kryptonGroupBox1.Panel, "kryptonGroupBox1.Panel");
            this.kryptonGroupBox1.Panel.Controls.Add(this.buttonBrowse);
            this.kryptonGroupBox1.Panel.Controls.Add(this.comboCategory);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonRadioButton2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonRadioButton1);
            this.kryptonGroupBox1.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.kryptonGroupBox1.Values.Description = resources.GetString("kryptonGroupBox1.Values.Description");
            this.kryptonGroupBox1.Values.Heading = resources.GetString("kryptonGroupBox1.Values.Heading");
            this.kryptonGroupBox1.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonGroupBox1.Values.ImageTransparentColor")));
            // 
            // buttonBrowse
            // 
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Values.ExtraText = resources.GetString("buttonBrowse.Values.ExtraText");
            this.buttonBrowse.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonBrowse.Values.ImageTransparentColor")));
            this.buttonBrowse.Values.Text = resources.GetString("buttonBrowse.Values.Text");
            // 
            // comboCategory
            // 
            resources.ApplyResources(this.comboCategory, "comboCategory");
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.DropDownWidth = 160;
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.SelectedIndexChanged += new System.EventHandler(this.ComboCategorySelectedIndexChanged);
            // 
            // kryptonRadioButton2
            // 
            resources.ApplyResources(this.kryptonRadioButton2, "kryptonRadioButton2");
            this.kryptonRadioButton2.Name = "kryptonRadioButton2";
            this.kryptonRadioButton2.Values.ExtraText = resources.GetString("kryptonRadioButton2.Values.ExtraText");
            this.kryptonRadioButton2.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonRadioButton2.Values.ImageTransparentColor")));
            this.kryptonRadioButton2.Values.Text = resources.GetString("kryptonRadioButton2.Values.Text");
            // 
            // kryptonRadioButton1
            // 
            resources.ApplyResources(this.kryptonRadioButton1, "kryptonRadioButton1");
            this.kryptonRadioButton1.Checked = true;
            this.kryptonRadioButton1.Name = "kryptonRadioButton1";
            this.kryptonRadioButton1.Values.ExtraText = resources.GetString("kryptonRadioButton1.Values.ExtraText");
            this.kryptonRadioButton1.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonRadioButton1.Values.ImageTransparentColor")));
            this.kryptonRadioButton1.Values.Text = resources.GetString("kryptonRadioButton1.Values.Text");
            // 
            // kryptonLabel1
            // 
            resources.ApplyResources(this.kryptonLabel1, "kryptonLabel1");
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Values.ExtraText = resources.GetString("kryptonLabel1.Values.ExtraText");
            this.kryptonLabel1.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonLabel1.Values.ImageTransparentColor")));
            this.kryptonLabel1.Values.Text = resources.GetString("kryptonLabel1.Values.Text");
            // 
            // textFilter
            // 
            resources.ApplyResources(this.textFilter, "textFilter");
            this.textFilter.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
                                    this.buttonSpecAny1});
            this.textFilter.Name = "textFilter";
            // 
            // buttonSpecAny1
            // 
            resources.ApplyResources(this.buttonSpecAny1, "buttonSpecAny1");
            this.buttonSpecAny1.UniqueName = "7C280734868A4B8A7DA38019FA2E2969";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Values.ExtraText = resources.GetString("buttonCancel.Values.ExtraText");
            this.buttonCancel.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Values.Image")));
            this.buttonCancel.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonCancel.Values.ImageTransparentColor")));
            this.buttonCancel.Values.Text = resources.GetString("buttonCancel.Values.Text");
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Values.ExtraText = resources.GetString("buttonOK.Values.ExtraText");
            this.buttonOK.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Values.Image")));
            this.buttonOK.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonOK.Values.ImageTransparentColor")));
            this.buttonOK.Values.Text = resources.GetString("buttonOK.Values.Text");
            // 
            // listImages
            // 
            resources.ApplyResources(this.listImages, "listImages");
            this.listImages.MultiSelect = false;
            this.listImages.Name = "listImages";
            this.listImages.SmallImageList = this.imageList;
            this.listImages.UseCompatibleStateImageBehavior = false;
            this.listImages.View = System.Windows.Forms.View.List;
            this.listImages.DoubleClick += new System.EventHandler(this.ListImagesDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageList, "imageList");
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormSelectImage
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.listImages);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonGroupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView listImages;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonCancel;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton kryptonRadioButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton kryptonRadioButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboCategory;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonBrowse;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
    }
}
