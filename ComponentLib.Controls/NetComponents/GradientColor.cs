//-----------------------------------------------------------------------
// <copyright file="GradientColor.cs" company="Sergey Teplyashin">
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
// <date>29.11.2006</date>
// <time>19:34</time>
// <summary>Defines the GradientColor class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GradientColor : IDisposable
    {
        [NonSerialized]
        Brush brush;
        [NonSerialized]
        Pen pen;
        [NonSerialized]
        Pen borderPen;
        Rectangle rect;
        TabAlignment align;
        Color color1;
        Color color2;
        GradientFill fill;
        bool disposed;

        public GradientColor() : this(Color.White, Color.White, GradientFill.Solid)
        {
        }
        
        public GradientColor(Color color1, Color color2, GradientFill gradient)
        {
            this.color1 = color1;
            this.color2 = color2;
            this.fill = gradient;
            this.rect = new Rectangle(0, 0, 100, 100);
            this.align = TabAlignment.Top;
        }
        
        public event EventHandler<EventArgs> ColorChanged;
        
        [DefaultValue(typeof(Color), "White")]
        [NotifyParentProperty(true)]
        public Color Color1
        {
            get
            {
                return color1;
            }
            
            set
            {
                if (color1 != value)
                {
                    color1 = value;
                    CreateColors();
                    OnColorChanged();
                }
            }
        }
        
        [DefaultValue(typeof(Color), "White")]
        [NotifyParentProperty(true)]
        public Color Color2
        {
            get
            {
                return color2;
            }
            
            set
            {
                if (color2 != value)
                {
                    color2 = value;
                    CreateColors();
                    OnColorChanged();
                }
            }
        }
        
        [DefaultValue(GradientFill.Solid)]
        [NotifyParentProperty(true)]
        public GradientFill Fill
        {
            get
            {
                return fill;
            }
            
            set
            {
                if (fill != value)
                {
                    fill = value;
                    CreateColors();
                    OnColorChanged();
                }
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Brush Brush
        {
            get
            {
                if (brush == null)
                {
                    CreateColors();
                }
                
                return brush;
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Pen Pen
        {
            get
            {
                if (pen == null)
                {
                    CreateColors();
                }
                
                return pen;
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Pen BorderPen
        {
            get
            {
                if (borderPen == null)
                {
                    CreateColors();
                }
                
                return borderPen;
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Rectangle BoundsRect
        {
            get
            {
                return rect;
            }
            
            set
            {
                if (rect != value)
                {
                    rect = value;
                    CreateColors();
                }
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TabAlignment Alignment
        {
            get
            {
                return align;
            }
            
            set 
            {
                if (align != value)
                {
                    align = value;
                    CreateColors();
                }
            }
        }
        
        #region IDisposable interface implemented
        
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        #endregion
        
        internal void Update(Rectangle rectangle, TabAlignment tabAlign)
        {
            align = tabAlign;
            rect = rectangle;
            CreateColors();
        }
        
        protected void OnColorChanged()
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, new EventArgs());
            }
        }
        
        void CreateColors()
        {
            if (brush != null)
            {
                brush.Dispose();
            }
            
            if (pen != null)
            {
                pen.Dispose();
            }
            
            if (borderPen != null)
            {
                borderPen.Dispose();
            }
            
            pen = new Pen(brush = GetBrush());
            borderPen = new Pen((fill == GradientFill.Top) ? color2 : color1);
        }
        
        Brush GetBrush()
        {
            int angle = 0;
            switch (align)
            {
                case TabAlignment.Top:
                    angle = 90;
                    break;
                case TabAlignment.Bottom:
                    angle = 270;
                    break;
                case TabAlignment.Right:
                    angle = 180;
                    break;
            }
            
            Blend blend;
            Brush b;
            switch (fill)
            {
                case GradientFill.Linear:
                    b = new LinearGradientBrush(rect, color1, color2, angle, true);
                    break;
                case GradientFill.Top:
                    {
                        b = new LinearGradientBrush(rect, color1, color2, angle, true);
                        blend = new Blend();
                        float[] relativeIntensities = {1.0F, 0.8F, 0.6F, 0.4F, 0.2F, 0.0F, 0.0F};
                        float[] relativePositions = {0.0F, 0.05F, 0.1F, 0.15F, 0.2F, 0.3F, 1.0F};
                        blend.Factors = relativeIntensities;
                        blend.Positions = relativePositions;
                        ((LinearGradientBrush)b).Blend = blend;
                    }
                    
                    break;
                case GradientFill.Center:
                    {
                        b = new LinearGradientBrush(rect, color1, color2, angle, true);
                        blend = new Blend();
                        float[] relativeIntensities = {0.0F, 0.5F, 1.0F, 0.5F, 0.0F};
                        float[] relativePositions = {0.0F, 0.3F, 0.5F, 0.7F, 1.0F};
                        blend.Factors = relativeIntensities;
                        blend.Positions = relativePositions;
                        ((LinearGradientBrush)b).Blend = blend;
                    }
                    
                    break;
                case GradientFill.Bottom:
                    {
                        b = new LinearGradientBrush(rect, color1, color2, angle + 180, true);
                        blend = new Blend();
                        float[] relativeIntensities = {1.0F, 0.8F, 0.6F, 0.4F, 0.2F, 0.0F, 0.0F};
                        float[] relativePositions = {0.0F, 0.05F, 0.1F, 0.15F, 0.2F, 0.3F, 1.0F};
                        blend.Factors = relativeIntensities;
                        blend.Positions = relativePositions;
                        ((LinearGradientBrush)b).Blend = blend;
                    }
                    
                    break;
                default:
                    b = new SolidBrush(color1);
                    break;
            }
            
            return b;
        }
        
        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    
                    if (pen != null)
                    {
                        pen.Dispose();
                    }
                    
                    if (borderPen != null)
                    {
                        borderPen.Dispose();
                    }
                }

                disposed = true;
            }
        }
    }
}
