//-----------------------------------------------------------------------
// <copyright file="NetTabButton.cs" company="Sergey Teplyashin">
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
// <date>22.11.2006</date>
// <time>13:01</time>
// <summary>Defines the NetTabButton class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    
    class NetTabButton : ButtonBase
    {
        NetTabControl owner;
        TabButtonAction buttonAction;
        
        bool selected;
        bool down;
        
        public NetTabButton(NetTabControl owner, TabButtonAction action)
        {
            this.buttonAction = action;
            this.owner = owner;
            
            this.selected = false;
            this.down = false;

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.FixedWidth, true);
            
            Size = new Size(15, 15);
            Visible = false;
            TabStop = false;
        }
        
        public event EventHandler<EventArgs> ButtonPresed;
        
        public static int SizeButton
        {
            get { return 15; }
        }
        
        public TabButtonAction ButtonAction
        {
            get
            {
                return buttonAction;
            }
            
            set
            {
                if (buttonAction != value)
                {
                    buttonAction = value;
                    Invalidate();
                }
            }
        }
        
        protected void DrawButton(Graphics g)
        {
            Point[] points;
            
            Brush fig = new SolidBrush(Color.Black);
            Pen p_fig = new Pen(Color.Black);
            try
            {
                switch (buttonAction)
                {
                    case TabButtonAction.Action:
                        g.FillRectangle(fig, new Rectangle(3, 3, 9, 2));
                        points = new Point[3];
                        points[0] = new Point(3, 7);
                        points[1] = new Point(12, 7);
                        points[2] = new Point(7, 12);
                        g.FillPolygon(fig, points);
                        break;
                    case TabButtonAction.Close:
                        p_fig.Width = 2;
                        g.DrawLine(p_fig, 3, 3, 11, 11);
                        g.DrawLine(p_fig, 3, 11, 11, 3);
                        p_fig.Width = 1;
                        g.DrawLine(p_fig, 3, 3, 11, 11);
                        g.DrawLine(p_fig, 3, 11, 11, 3);
                        break;
                    case TabButtonAction.Down:
                        points = new Point[3];
                        points[0] = new Point(3, 5);
                        points[1] = new Point(12, 5);
                        points[2] = new Point(7, 10);
                        g.FillPolygon(fig, points);
                        break;
                    case TabButtonAction.Up:
                        points = new Point[3];
                        points[0] = new Point(2, 10);
                        points[1] = new Point(13, 10);
                        points[2] = new Point(7, 4);
                        g.FillPolygon(fig, points);
                        break;
                    case TabButtonAction.Left:
                        points = new Point[3];
                        points[0] = new Point(5, 7);
                        points[1] = new Point(10, 2);
                        points[2] = new Point(10, 12);
                        g.FillPolygon(fig, points);
                        break;
                    case TabButtonAction.Right:
                        points = new Point[3];
                        points[0] = new Point(5, 2);
                        points[1] = new Point(10, 7);
                        points[2] = new Point(5, 12);
                        g.FillPolygon(fig, points);
                        break;
                }
            }
            finally
            {
                fig.Dispose();
                p_fig.Dispose();
            }
        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.None;
            
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (selected)
            {
                using (Brush brush = new SolidBrush(down ? Color.FromArgb(255, 153, 175, 212) : Color.FromArgb(255, 194, 207, 229)))
                {
                    g.FillRectangle(brush, rect);
                }
                
                using (Pen border = new Pen(Color.FromArgb(255, 51, 94, 168)))
                {
                    g.DrawRectangle(border, rect);
                }
            }
            else
            {
                rect.Width++;
                rect.Height++;
                using (Brush bg = new SolidBrush(owner.BackColor))
                {
                    g.FillRectangle(bg, rect);
                }
            }
            
            DrawButton(g);
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            selected = (ClientRectangle.Contains(e.X, e.Y));
            Invalidate();
            base.OnMouseMove(e);
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            if (selected)
            {
                selected = false;
                Invalidate();
            }
            
            base.OnMouseLeave(e);
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!down)
            {
                down = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }
        
        protected void OnButtonPressed()
        {
            if (ButtonPresed != null)
            {
                ButtonPresed(this, new EventArgs());
            }
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (down)
            {
                down = false;
                Invalidate();
                OnButtonPressed();
            }
            
            base.OnMouseUp(e);
        }
    }
}
