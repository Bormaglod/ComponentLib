//-----------------------------------------------------------------------
// <copyright file="TextBoxNumber.Designer.cs" company="Sergey Teplyashin">
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
// <date>07.06.2011</date>
// <time>8:55</time>
// <summary>Defines the TextBoxNumber class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    partial class TextBoxNumber
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
        	this.labelPrefix = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
        	this.labelSuffix = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
        	this.numericTextBox = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
        	this.SuspendLayout();
        	// 
        	// labelPrefix
        	// 
        	this.labelPrefix.Dock = System.Windows.Forms.DockStyle.Left;
        	this.labelPrefix.Location = new System.Drawing.Point(0, 0);
        	this.labelPrefix.Name = "labelPrefix";
        	this.labelPrefix.Size = new System.Drawing.Size(41, 22);
        	this.labelPrefix.TabIndex = 0;
        	this.labelPrefix.Values.Text = "prefix";
        	// 
        	// labelSuffix
        	// 
        	this.labelSuffix.Dock = System.Windows.Forms.DockStyle.Right;
        	this.labelSuffix.Location = new System.Drawing.Point(159, 0);
        	this.labelSuffix.Name = "labelSuffix";
        	this.labelSuffix.Size = new System.Drawing.Size(39, 22);
        	this.labelSuffix.TabIndex = 2;
        	this.labelSuffix.Values.Text = "suffix";
        	// 
        	// numericTextBox
        	// 
        	this.numericTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.numericTextBox.Location = new System.Drawing.Point(41, 0);
        	this.numericTextBox.Name = "numericTextBox";
        	this.numericTextBox.Size = new System.Drawing.Size(118, 22);
        	this.numericTextBox.TabIndex = 3;
        	this.numericTextBox.ValueChanged += new System.EventHandler(this.NumericTextBoxValueChanged);
        	this.numericTextBox.Resize += new System.EventHandler(this.NumericTextBoxResize);
        	// 
        	// TextBoxNumber
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.Controls.Add(this.numericTextBox);
        	this.Controls.Add(this.labelSuffix);
        	this.Controls.Add(this.labelPrefix);
        	this.Name = "TextBoxNumber";
        	this.Size = new System.Drawing.Size(198, 22);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numericTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelSuffix;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelPrefix;
    }
}
