//-----------------------------------------------------------------------
// <copyright file="OutlookBar.Designer.cs" company="Sergey Teplyashin">
//     Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
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
// <date>26.10.2011</date>
// <time>8:37</time>
// <summary>Defines the OutlookBar class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    partial class OutlookBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OutlookBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OutlookBar";
            this.Size = new System.Drawing.Size(166, 48);
            this.DoubleClick += new System.EventHandler(this.OutlookBar_DoubleClick);
            this.Load += new System.EventHandler(this.OutlookBar_Load);
            base.Click += new System.EventHandler(this.OutlookBar_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OutlookBar_MouseMove);
            this.Resize += new System.EventHandler(this.OutlookBar_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OutlookBar_Paint);
            this.MouseLeave += new System.EventHandler(this.OutlookBar_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
