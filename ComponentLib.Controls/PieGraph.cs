//-----------------------------------------------------------------------
// <copyright file="PieGraph.cs" company="Sergey Teplyashin">
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
// <date>08.02.2013</date>
// <time>13:44</time>
// <summary>Defines the PieGraph class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using ComponentLib.Controls.ThirdParty.CornerBowl;
    
    public class PieGraph : Control
    {
        SeriesList series;
        int interval;
        bool needInitData;
        bool drawBorder;
        Series tracking;
        List<Series> needDrawSeries;
        bool updated;
        CornerBowlPopup popup;
        
        public PieGraph()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.Selectable |
                ControlStyles.UserPaint | 
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            
            this.series = new SeriesList(this);
            this.interval = 3;
            this.needInitData = true;
            this.needDrawSeries = new List<Series>();
            this.popup = new CornerBowlPopup();
        }
        
        public event EventHandler<SelectSeriesEventArgs> SelectSeries;
        
        public int Interval
        {
            get
            {
                return interval;
            }
            
            set
            {
                if (interval != value)
                {
                    interval = value;
                    needInitData = true;
                    Invalidate();
                }
            }
        }
        
        public bool DrawBorder
        {
            get
            {
                return drawBorder;
            }
            
            set
            {
                if (drawBorder != value)
                {
                    drawBorder = value;
                    Invalidate();
                }
            }
        }
        
        public Series AddValue(string name, string title, float valueData, Series parent)
        {
            return AddValue(name, title, CurColor, valueData, parent);
        }
        
        public Series AddValue(string name, string title, Color baseColor, float valueData, Series parent)
        {
            Series s = series.AddSeries(name, title, baseColor, valueData, parent);
            if (!updated)
            {
                needInitData = true;
                Invalidate();
            }
            
            return s;
        }
        
        public void BeginUpdate()
        {
            updated = true;
        }
        
        public void EndUpdated()
        {
            if (updated)
            {
                updated = false;
                needInitData = true;
                Invalidate();
            }
        }
        
        public void Clear()
        {
            series.Clear();
            if (!updated)
            {
                needInitData = true;
                Invalidate();
            }
        }
        
         Color[] colors = new Color[] {
            Color.FromArgb(75, 56, 255),
            Color.FromArgb(119, 119, 255),
            Color.FromArgb(165, 170, 255),
            Color.FromArgb(199, 33, 255),
            Color.FromArgb(204, 109, 255),
            Color.FromArgb(208, 155, 255),
            Color.FromArgb(183, 251, 255),
            Color.FromArgb(28, 194, 255),
            
            Color.FromArgb(255, 33, 158),
            Color.FromArgb(255, 124, 226),
            Color.FromArgb(255, 193, 234),
            Color.FromArgb(255, 43, 68),
            Color.FromArgb(255, 124, 140),
            Color.FromArgb(255, 105, 45),
            Color.FromArgb(255, 151, 48),
            Color.FromArgb(255, 208, 79),
            Color.FromArgb(255, 225, 137),
            
            Color.FromArgb(196, 255, 35),
            Color.FromArgb(212, 255, 114),
            Color.FromArgb(114, 255, 38),
            Color.FromArgb(117, 255, 135),
            Color.FromArgb(43, 255, 71),
            Color.FromArgb(43, 255, 138),
            Color.FromArgb(119, 255, 198),
            Color.FromArgb(35, 255, 236),
            Color.FromArgb(119, 255, 228)
        };
        
        int color = 0;
        
        Color CurColor
        {
            get
            {
                return color == colors.Length ? colors[color = 0] : colors[color++];
            }
        }
        
        void CalculateSeries(RectangleF rect, float pieWidth, float startAngle, float startSum, IEnumerable<Series> seriesLevel)
        {
            float sum = seriesLevel.Sum(s => s.ValueData);
            float innerRadius = rect.Width / 2;
            float outerRadius = innerRadius + pieWidth;
            float alpha = startAngle;
            foreach (Series s in seriesLevel)
            {
                float delta = 360 * s.ValueData / startSum;
                s.CreateData(rect, innerRadius, outerRadius, alpha, delta);
                
                IEnumerable<Series> childs = this.series.GetChilds(s);
                if (childs.FirstOrDefault() != null)
                {
                    float d = outerRadius - innerRadius + this.interval;
                    RectangleF childRect = RectangleF.Inflate(rect, d, d);
                    CalculateSeries(childRect, pieWidth, alpha, startSum, childs);
                }
                
                alpha += delta;
            }
        }
        
        void CalculateSeries()
        {
            IEnumerable<Series> rootSeries = series.GetSeries(0);
            
            RectangleF drawRect = new RectangleF(Padding.Left, Padding.Top, Width - Padding.Horizontal - 1, Height - Padding.Vertical - 1);
            float minWidth = drawRect.Width > drawRect.Height ? drawRect.Height : drawRect.Width;
            float w = (minWidth - 2 * interval * series.Levels) / (2 * series.Levels + 2);
            float xc = drawRect.Width / 2 + drawRect.Left;
            float yc = drawRect.Height / 2 + drawRect.Top;
            
            RectangleF rect = new RectangleF(xc - w - interval, yc - w - interval, (w + interval) * 2, (w + interval) * 2);
            CalculateSeries(rect, w, 0, rootSeries.Sum(s => s.ValueData), rootSeries);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (series.CountData > 0)
            {
                if (needInitData)
                {
                    CalculateSeries();
                    needInitData = false;
                }
                
                IEnumerable<Series> drawingSeries = needDrawSeries.Count > 0 ? needDrawSeries : series.GetSeries();

                foreach (Series s in drawingSeries)
                {
                    s.Draw(e.Graphics);
                }
                
                needDrawSeries.Clear();
            }
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            needInitData = true;
        }
        
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            needInitData = true;
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Series s = GetSeriesAt(e.X, e.Y);
            if (s != null)
            {
                OnSelectSeries(s);
            }
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Series s = GetSeriesAt(e.X, e.Y);
            if (s != tracking)
            {
                popup.HidePopup();
                if (tracking != null)
                {
                    tracking.Tracking = false;
                }
                
                if (s != null)
                {
                    s.Tracking = true;
                }

                tracking = s;
                Invalidate();
            }
            
            if (s != null)
            {
                popup.MessageText = string.Format("Сумма: {0}", s.ValueData);
                popup.TitleText = string.Format("{0}: {1}", s.Title, s.Name);
                popup.F1HelpText = string.Format("{0}% к общему обороту", (this.series.GetPercentRoot(s) * 100).ToString("0.00"));
                popup.ShowPopup(PointToScreen(new Point(e.Location.X, e.Location.Y + Cursor.Size.Height)));
            }
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (tracking != null)
            {
                tracking.Tracking = false;
                tracking = null;
                Invalidate();
            }
            
            popup.HidePopup();
        }
        
        Series GetSeriesAt(float x, float y)
        {
            return series.GetSeries().FirstOrDefault(s => s.Contains(new PointF(x, y)));
        }
        
        void OnSelectSeries(Series s)
        {
            if (SelectSeries != null)
            {
                SelectSeries(this, new SelectSeriesEventArgs(s));
            }
        }
    }
}
