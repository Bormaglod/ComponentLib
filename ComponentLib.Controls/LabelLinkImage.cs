//-----------------------------------------------------------------------
// <copyright file="LabelLinkImage.cs" company="Sergey Teplyashin">
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
// <date>18.02.2013</date>
// <time>13:19</time>
// <summary>Defines the LabelLinkImage class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    
    public partial class LabelLinkImage : UserControl
    {
        public LabelLinkImage()
        {
            InitializeComponent();
        }
        
        public event EventHandler<EventArgs> SelectLink;
        
        public Image ImageLink
        {
            get
            {
                return imageLink.Image;
            }
            
            set
            {
                imageLink.Image = value;
                Height = imageLink.Image.Height + Padding.Vertical;
                UpdateControls();
            }
        }
        
        public string TextLink
        {
            get
            {
                return labelLink.Values.Text;
            }
            
            set
            {
                labelLink.Values.Text = value;
            }
        }
        
        public bool Bold
        {
            get
            {
                return labelLink.LabelStyle == LabelStyle.BoldControl;
            }
            
            set
            {
                labelLink.LabelStyle = value ? LabelStyle.BoldControl : LabelStyle.NormalControl;
            }
        }
        
        public Color TextColor
        {
            get
            {
                return labelLink.StateCommon.ShortText.Color1;
            }
            
            set
            {
                labelLink.StateCommon.ShortText.Color1 = value;
            }
        }
        
        private void UpdateControls()
        {
            int x = imageLink.Image.Width + 8;
            int y = (Height - labelLink.Height) / 2;
            labelLink.Location = new Point(x, y);
        }
        
        void OnSelectlink()
        {
            if (SelectLink != null)
            {
                SelectLink(this, new EventArgs());
            }
        }
        
        void LabelLinkImageResize(object sender, EventArgs e)
        {
            UpdateControls();
        }
        
        void LabelLinkMouseEnter(object sender, EventArgs e)
        {
            FontStyle s = FontStyle.Underline;
            if (Bold)
            {
                s |= FontStyle.Bold;
            }
            
            labelLink.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 8.25F, s);
        }
        
        void LabelLinkMouseLeave(object sender, EventArgs e)
        {
            labelLink.StateCommon.ShortText.Font = null;
        }
        
        void LabelLinkClick(object sender, EventArgs e)
        {
            OnSelectlink();
        }
        
        void ImageLinkClick(object sender, EventArgs e)
        {
            OnSelectlink();
        }
    }
}
