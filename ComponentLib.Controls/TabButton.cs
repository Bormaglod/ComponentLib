///-----------------------------------------------------------------------
/// <copyright file="TabButton.cs" company="Sergey Teplyashin">
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
/// <date>23.11.2006</date>
/// <time>13:01</time>
/// <summary>Defines the TabButton class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    #region Using directives
    
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    
    #endregion
    
    public class TabButton : ButtonBase
    {
        /*private TabButtonAction buttonAction;
        private bool selected;
        private Brush bg;
        private Brush bg_select = new SolidBrush(Color.FromArgb(255, 194, 207, 229));
        private Pen border = new Pen(Color.FromArgb(255, 51, 94, 168));
        private Brush fig = new SolidBrush(Color.Black);
        private Pen p_fig = new Pen(Color.Black);
        private Brush bg_press = new SolidBrush(Color.FromArgb(255, 153, 175, 212));        
        private TabControl owner;
        private bool down;*/
        
        public TabButton(/*TabControl owner, TabButtonAction action*/)
        {
            /*this.buttonAction = action;
            this.selected = false;
            this.down = false;
            
            this.owner = owner;
            this.bg = new SolidBrush(owner.BackColor);
            
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.FixedWidth, true);
            
            Size = new Size(15, 15);
            
            Visible = false;
            TabStop = false;*/
        }
        
        /*public event EventHandler<EventArgs> ButtonPressed;
        
        public static int SizeButton
        {
            get { return 15; }
        }
        
        public TabButtonAction ButtonAction
        {
            get
            {
                return this.buttonAction;
            }
            
            set
            {
                if (this.buttonAction != value)
                {
                    this.buttonAction = value;
                    Invalidate();
                }
            }
        }
        
        protected void DrawButton(Graphics g)
        {
            Point[] points;
            
            switch (this.buttonAction)
            {
                case TabButtonAction.Action:
                    g.FillRectangle(this.fig, new Rectangle(3, 3, 9, 2));
                    points = new Point[3];
                    points[0] = new Point(3, 7);
                    points[1] = new Point(12, 7);
                    points[2] = new Point(7, 12);
                    g.FillPolygon(this.fig, points);
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
                    g.FillPolygon(this.fig, points);
                    break;
                case TabButtonAction.Up:
                    points = new Point[3];
                    points[0] = new Point(2, 10);
                    points[1] = new Point(13, 10);
                    points[2] = new Point(7, 4);
                    g.FillPolygon(this.fig, points);
                    break;
                case TabButtonAction.Left:
                    points = new Point[3];
                    points[0] = new Point(5, 7);
                    points[1] = new Point(10, 2);
                    points[2] = new Point(10, 12);
                    g.FillPolygon(this.fig, points);
                    break;
                case TabButtonAction.Right:
                    points = new Point[3];
                    points[0] = new Point(5, 2);
                    points[1] = new Point(10, 7);
                    points[2] = new Point(5, 12);
                    g.FillPolygon(this.fig, points);
                    break;
            }
        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.None;
            
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (this.selected)
            {
                if (this.down)
                {
                    g.FillRectangle(this.bg_press, rect);
                }
                else
                {
                    g.FillRectangle(this.bg_select, rect);
                }
                
                g.DrawRectangle(this.border, rect);
            }
            else
            {
                rect.Width++;
                rect.Height++;
                g.FillRectangle(this.bg, rect);    
            }
            
            this.DrawButton(g);
        }
        
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            this.selected = this.ClientRectangle.Contains(mevent.X, mevent.Y);
            Invalidate();
            base.OnMouseMove(mevent);
        }
        
        protected override void OnMouseLeave(EventArgs eventargs)
        {
            if (this.selected)
            {
                this.selected = false;
                Invalidate();
            }
            
            base.OnMouseLeave(eventargs);
        }
        
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (!this.down)
            {
                this.down = true;
                Invalidate();
            }
            
            base.OnMouseDown(mevent);
        }
        
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.down)
            {
                this.down = false;
                Invalidate();
                this.OnButtonPressed();
            }
            
            base.OnMouseUp(mevent);
        }
        
        public void OnButtonPressed()
        {
            if (this.ButtonPressed != null)
            {
                this.ButtonPressed(this, new EventArgs());
            }
        }*/
    }
}
