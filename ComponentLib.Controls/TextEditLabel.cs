//-----------------------------------------------------------------------
// <copyright file="TextEditLabel.cs" company="Sergey Teplyashin">
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
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Controls.Design;

    [Designer(typeof(TextEditLabelDesigner))]
    public partial class TextEditLabel : UserControl
    {
        public TextEditLabel()
        {
            InitializeComponent();
            this.headerLabel.Values.Image = null;
            this.UpdateHeights();
        }
        
        [Category("Appearance")]
        [Localizable(true)]
        public string HeaderLabel
        {
            get { return this.headerLabel.Values.Description; }
            set { this.headerLabel.Values.Description = value; }
        }
        
        [Category("Appearance")]
        [Localizable(true)]
        public string ValueText
        {
            get { return this.textBox.ValueText; }
            set { this.textBox.ValueText = value; }
        }
        
        [Category("Appearance")]
        [Localizable(true)]
        public string EmptyText
        {
            get { return this.textBox.EmptyText; }
            set { this.textBox.EmptyText = value; }
        }
        
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.MiddleRight)]
        public ContentAlignment HeaderAlign
        {
            get
            {
                PaletteRelativeAlign alignH = this.headerLabel.StateCommon.Content.LongText.TextH;
                PaletteRelativeAlign alignV = this.headerLabel.StateCommon.Content.LongText.TextV;
                switch (alignH)
                {
                    case PaletteRelativeAlign.Far:
                        switch (alignV)
                        {
                            case PaletteRelativeAlign.Near:
                                return ContentAlignment.TopRight;
                            case PaletteRelativeAlign.Center:
                                return ContentAlignment.MiddleRight;
                            case PaletteRelativeAlign.Far:
                                return ContentAlignment.BottomRight;
                        }
                        
                        break;
                    case PaletteRelativeAlign.Center:
                        switch (alignV)
                        {
                            case PaletteRelativeAlign.Near:
                                return ContentAlignment.TopCenter;
                            case PaletteRelativeAlign.Center:
                                return ContentAlignment.MiddleCenter;
                            case PaletteRelativeAlign.Far:
                                return ContentAlignment.BottomCenter;
                        }
                        
                        break;
                    case PaletteRelativeAlign.Near:
                        switch (alignV)
                        {
                            case PaletteRelativeAlign.Near:
                                return ContentAlignment.TopLeft;
                            case PaletteRelativeAlign.Center:
                                return ContentAlignment.MiddleLeft;
                            case PaletteRelativeAlign.Far:
                                return ContentAlignment.BottomLeft;
                        }
                        
                        break;
                }
                
                return ContentAlignment.MiddleRight;
            }
            
            set
            {
                if (value == ContentAlignment.BottomLeft || value == ContentAlignment.MiddleLeft || value == ContentAlignment.TopLeft)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextH = PaletteRelativeAlign.Near;
                }
                else if (value == ContentAlignment.BottomCenter || value == ContentAlignment.MiddleCenter || value == ContentAlignment.TopCenter)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextH = PaletteRelativeAlign.Center;
                }
                else if (value == ContentAlignment.BottomRight || value == ContentAlignment.MiddleRight || value == ContentAlignment.TopRight)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextH = PaletteRelativeAlign.Far;
                }
                
                if (value == ContentAlignment.TopLeft || value == ContentAlignment.TopCenter || value == ContentAlignment.TopRight)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextV = PaletteRelativeAlign.Near;
                }
                else if (value == ContentAlignment.MiddleLeft || value == ContentAlignment.MiddleCenter || value == ContentAlignment.MiddleRight)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextV = PaletteRelativeAlign.Center;
                }
                else if (value == ContentAlignment.BottomLeft || value == ContentAlignment.BottomCenter || value == ContentAlignment.MiddleRight)
                {
                    this.headerLabel.StateCommon.Content.LongText.TextV = PaletteRelativeAlign.Far;
                }
            }
        }
        
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool Multiline
        {
            get
            {
                return this.textBox.Multiline;
            }
            
            set
            {
                this.textBox.Multiline = value;
                this.UpdateHeights();
            }
        }
        
        [Category("Layout")]
        [DefaultValue(true)]
        public bool AutoSizeHeader
        {
            get { return this.headerLabel.AutoSize; }
            set { this.headerLabel.AutoSize = value; }
        }
        
        [Category("Layout")]
        public int HeaderWidth
        {
            get { return this.headerLabel.Size.Width; }
            set { this.headerLabel.Size = new Size(value, this.headerLabel.Size.Height); }
        }
        
        private void UpdateHeights()
        {
            Height = this.textBox.Height;
        }

        private void TextBoxPaletteChanged(object sender, EventArgs e)
        {
            this.UpdateHeights();
        }
    }
}
