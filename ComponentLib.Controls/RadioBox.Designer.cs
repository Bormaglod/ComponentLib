﻿///-----------------------------------------------------------------------
/// <copyright file="?.cs" company="Sergey Teplyashin">
///     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
/// </copyright>
/// <author>Тепляшин Сергей Васильевич</author>
/// <email>sergey-teplyashin@yandex.ru</email>
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
/// <date>01.06.2012</date>
/// <time>12:44</time>
/// <summary>Defines the ? class.</summary>
///-----------------------------------------------------------------------
namespace ComponentLib.Controls
{
    partial class RadioBox
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
            this.groupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox.Panel)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(204, 125);
            this.groupBox.TabIndex = 0;
            this.groupBox.Text = "Caption";
            // 
            // RadioBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox);
            this.Name = "RadioBox";
            this.Size = new System.Drawing.Size(204, 125);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox groupBox;
    }
}
