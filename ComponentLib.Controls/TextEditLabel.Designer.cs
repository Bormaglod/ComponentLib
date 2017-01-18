//-----------------------------------------------------------------------
// <copyright file="TextEditLabel.Designer.cs" company="Sergey Teplyashin">
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
// <date>11.11.2011</date>
// <time>9:47</time>
// <summary>Defines the TextEditLabel class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    partial class TextEditLabel
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
            this.headerLabel = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.textBox = new ComponentLib.Controls.TextEdit();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.headerLabel.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(85, 58);
            this.headerLabel.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
                                    | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.headerLabel.StateCommon.Border.Rounding = 7;
            this.headerLabel.TabIndex = 3;
            this.headerLabel.Values.Heading = "";
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.EmptyText = "Emtpy string";
            this.textBox.Location = new System.Drawing.Point(85, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(341, 24);
            this.textBox.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
                                    | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.textBox.StateCommon.Border.Rounding = 7;
            this.textBox.StateCommon.Content.Color1 = System.Drawing.SystemColors.ControlDark;
            this.textBox.TabIndex = 4;
            this.textBox.Text = "Emtpy string";
            this.textBox.ValueText = null;
            this.textBox.PaletteChanged += new System.EventHandler(this.TextBoxPaletteChanged);
            // 
            // TextEditLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.headerLabel);
            this.Name = "TextEditLabel";
            this.Size = new System.Drawing.Size(426, 58);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private ComponentFactory.Krypton.Toolkit.KryptonHeader headerLabel;
        private ComponentLib.Controls.TextEdit textBox;
    }
}
