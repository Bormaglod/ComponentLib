///-----------------------------------------------------------------------
/// <copyright file="LabelLinkImage.Designer.cs" company="Sergey Teplyashin">
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
/// <date>18.02.2013</date>
/// <time>13:19</time>
/// <summary>Defines the LabelLinkImage class.</summary>
///-----------------------------------------------------------------------
namespace ComponentLib.Controls
{
    partial class LabelLinkImage
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the control.
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
            this.imageLink = new System.Windows.Forms.PictureBox();
            this.labelLink = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imageLink)).BeginInit();
            this.SuspendLayout();
            // 
            // imageLink
            // 
            this.imageLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageLink.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageLink.Location = new System.Drawing.Point(4, 4);
            this.imageLink.Name = "imageLink";
            this.imageLink.Size = new System.Drawing.Size(48, 48);
            this.imageLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageLink.TabIndex = 0;
            this.imageLink.TabStop = false;
            this.imageLink.Click += new System.EventHandler(this.ImageLinkClick);
            // 
            // labelLink
            // 
            this.labelLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLink.Location = new System.Drawing.Point(58, 20);
            this.labelLink.Name = "labelLink";
            this.labelLink.Size = new System.Drawing.Size(51, 16);
            this.labelLink.TabIndex = 1;
            this.labelLink.Values.Text = "Ссылка";
            this.labelLink.Click += new System.EventHandler(this.LabelLinkClick);
            this.labelLink.MouseEnter += new System.EventHandler(this.LabelLinkMouseEnter);
            this.labelLink.MouseLeave += new System.EventHandler(this.LabelLinkMouseLeave);
            // 
            // LabelLinkImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.labelLink);
            this.Controls.Add(this.imageLink);
            this.Name = "LabelLinkImage";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(327, 56);
            this.Resize += new System.EventHandler(this.LabelLinkImageResize);
            ((System.ComponentModel.ISupportInitialize)(this.imageLink)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelLink;
        private System.Windows.Forms.PictureBox imageLink;
    }
}
