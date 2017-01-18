//-----------------------------------------------------------------------
// <copyright file="NetTabPage.cs" company="Sergey Teplyashin">
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
// <date>19.11.2006</date>
// <time>20:42</time>
// <summary>Defines the NetTabPage class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ComponentLib.Controls.Design;
    
    /// <summary>
    /// Represents a single tab page in a <see cref="NetTabControl"></see>.
    /// </summary>
    [ToolboxItem(false)]
    [Designer(typeof(NetTabPageDesigner))]
    public class NetTabPage : Panel
    {
        string title;
        
        public NetTabPage()
        {
            base.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            this.CreateColors();
        }
        
        void CreateColors()
        {
            TabColor = new GradientColor(Color.White, Color.Orange, GradientFill.Top);
            PageColor = new GradientColor(Color.White, Color.WhiteSmoke, GradientFill.Bottom);
            TabActiveColor = new GradientColor(Color.White, Color.Orange, GradientFill.Top);
            
            PageColor.ColorChanged += delegate { Invalidate(); };
            TabColor.ColorChanged += delegate
            {
                if (Owner != null)
                {
                    Owner.Invalidate();
                }
            };
            
            TabActiveColor.ColorChanged += delegate
            {
                if (Owner != null)
                {
                    Owner.Invalidate();
                }
            };
        }
        
        [Category("Property Changed")]
        public event EventHandler<EventArgs> TitleChanged;
        
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor TabColor { get; set; }
        
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor PageColor { get; set; }
        
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor TabActiveColor { get; set; }
        
        [Localizable(true)]
        [Category("Appearance")]
        public string Title
        {
            get
            {
                return title;
            }
            
            set 
            { 
                if (title != value)
                {
                    title = value;
                    OnTitleChanged();
                }
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [DefaultValue(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right)]
        public override AnchorStyles Anchor
        {
            get
            {
                return base.Anchor;
            }
            
            set
            {
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            
            set
            {
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
        
        NetTabControl Owner
        {
            get { return Parent as NetTabControl; }
        }
        
        internal void UpdateBackground()
        {
            PageColor.Update(new Rectangle(-2, -2, Width + 1, Height + 1), Owner.Alignment);
            TabColor.Update(Owner.BoundsNormalTab, Owner.Alignment);
            TabActiveColor.Update(Owner.BoundsSelectTab, Owner.Alignment);
            Invalidate();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.None;
            
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            
            g.FillRectangle(PageColor.Brush, rect);
            g.DrawRectangle(PageColor.Pen, rect);
        }
        
        protected void OnTitleChanged()
        {
            if (TitleChanged != null)
            {
                TitleChanged(this, new EventArgs());
            }
        }
        
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (Owner != null)
            {
                UpdateBackground();
            }
        }
    }
}
