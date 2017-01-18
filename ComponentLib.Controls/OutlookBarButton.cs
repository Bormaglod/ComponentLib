//-----------------------------------------------------------------------
// <copyright file="OutlookBarButton.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
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
// <date>02.01.2015</date>
// <time>1:20</time>
// <summary>Defines the OutlookBarButton class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    
    /// <summary>
    /// OutlookbarButton represents a button on the Outlookbar
    /// this is an internally used class (not a control!)
    /// </summary>
    public class OutlookBarButton // : IComponent
    {
        Image imageButton;
        string textButton;
        
        public OutlookBarButton()
        {
            this.Parent = new OutlookBar(); // set it to a dummy outlookbar control
            this.textButton = string.Empty;
        }
        
        public OutlookBarButton(OutlookBar parent)
        {
            this.Parent = parent;
            this.textButton = string.Empty;
        }
        
        [Description("Indicates wether the button is enabled"), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Enabled { get; set; }

        [Description("The image that will be displayed on the button"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Image
        {
            get { return imageButton; }
            set
            {
                imageButton = value;
                Parent.Invalidate();
            }
        }

        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Tag { get; set; }

        [Description("The text that will be displayed on the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get { return textButton; }
            set
            {
                textButton = value;
                Parent.Invalidate();
            }
        }

        public int Height
        {
            get { return Parent == null ? 30 : Parent.ButtonHeight; }
        }

        public int Width
        {
            get { return Parent == null ? 60 : Parent.Width - 2; }
        }
        
        internal OutlookBar Parent { get; set; }

        /// <summary>
        /// the outlook button will paint itself on its container (the OutlookBar)
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="IsSelected"></param>
        /// <param name="IsHovering"></param>
        public void PaintButton(Graphics graphics, int x, int y, bool IsSelected, bool IsHovering)
        {
            Brush br;
            Rectangle rect = new Rectangle(0, y, Width, Height);
            if (Enabled)
            {
                if (IsSelected)
                {
                    br = IsHovering ? new LinearGradientBrush(rect, Parent.GradientButtonSelectedDark, Parent.GradientButtonSelectedLight, 90f) : new LinearGradientBrush(rect, Parent.GradientButtonSelectedLight, Parent.GradientButtonSelectedDark, 90f);
                }
                else
                {
                    br = IsHovering ? new LinearGradientBrush(rect, Parent.GradientButtonHoverLight, Parent.GradientButtonHoverDark, 90f) : new LinearGradientBrush(rect, Parent.GradientButtonNormalLight, Parent.GradientButtonNormalDark, 90f);
                }
            }
            else
            {
                br = new LinearGradientBrush(rect, Parent.GradientButtonNormalLight, Parent.GradientButtonNormalDark, 90f);
            }

            graphics.FillRectangle(br, x, y, Width, Height);
            br.Dispose();

            if (textButton.Length > 0)
            {
                graphics.DrawString(Text, Parent.Font, Brushes.Black, imageButton == null ? 2 : 36, y + Height / 2 - Parent.Font.Height / 2);
            }

            if (imageButton != null)
            {
                graphics.DrawImage(imageButton, 36 / 2 - imageButton.Width / 2, y + Height / 2 - imageButton.Height / 2, imageButton.Width, imageButton.Height);
            }
        }
    }
}
