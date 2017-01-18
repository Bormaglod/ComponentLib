//-----------------------------------------------------------------------
// <copyright file="Series.cs" company="Sergey Teplyashin">
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
// <date>14.02.2013</date>
// <time>9:11</time>
// <summary>Defines the Series class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using ComponentLib.Drawing;
    
    public class Series
    {
        float valueData;
        string name;
        Series parent;
        int level;
        readonly GraphicsPath path;
        SeriesList owner;
        
        internal Series(SeriesList owner, string name, string title, Color baseColor, float valueData, Series parent)
        {
            this.valueData = valueData;
            this.name = name;
            this.parent = parent;
            this.level = this.parent == null ? 0 : parent.Level + 1;
            this.BaseColor = baseColor;
            this.path = new GraphicsPath();
            this.owner = owner;
            this.Title = title;
        }
        
        public int Level
        {
            get { return level; }
        }
        
        public float ValueData
        {
            get { return valueData; }
        }
        
        internal Series Parent
        {
            get { return parent; }
        }
        
        public Color BaseColor { get; set; }
        
        public string Title { get; set; }
        
        public string Name
        {
            get { return this.name; }
        }
        
        internal bool Tracking { get; set; }
        
        internal void CreateData(RectangleF bounds, float innerRadius, float outerRadius, float startAngle, float sweepAngle)
        {
            path.Reset();

            path.StartFigure();
            path.AddArc(bounds, startAngle, sweepAngle);
            float d = outerRadius - innerRadius;
            bounds.Inflate(d, d);
            path.AddArc(bounds, startAngle + sweepAngle, -sweepAngle);
            path.CloseFigure();
        }
        
        internal void Draw(Graphics g)
        {
            Color c = BaseColor;
            if (Tracking)
            {
                HSLColor color = (HSLColor)BaseColor;
                color.Luminance /= 1.2;
                c = (Color)color;
            }
            
            Brush b = new SolidBrush(c);
            g.FillPath(b, path);
            
            if (owner.Owner.DrawBorder)
            {
                g.DrawPath(new Pen(owner.Owner.ForeColor), path);
            }
        }
        
        internal bool Contains(PointF p)
        {
            return path.IsVisible(p);
        }
    }
}
