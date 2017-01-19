//-----------------------------------------------------------------------
// <copyright file="HSLColor.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2017 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>20.11.2006</date>
// <time>0:23</time>
// <summary>Defines the HSLColor class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Drawing
{
    using System;
    using System.Drawing;
    
    public class HSLColor
    {
        public static HSLColor FromHSL(double hue, double saturation, double luminance)
        {
            return new HSLColor()
            {
                Hue = hue,
                Saturation = saturation,
                Luminance = luminance
            };
        }
        
        public static implicit operator HSLColor(Color c)
        {
            HSLColor cn = new HSLColor();

            byte minval = Math.Min(c.R, Math.Min(c.G, c.B));
            byte maxval = Math.Max(c.R, Math.Max(c.G, c.B));
            double mdiff  = (double)maxval - (double)minval;
            double msum   = (double)maxval + (double)minval;
   
            cn.Luminance = msum / 510.0f;

            if (maxval == minval) 
            {
                cn.Saturation = 0.0f;
                cn.Hue = 0.0f; 
            }   
            else 
            { 
                double rnorm = (maxval - c.R ) / mdiff;      
                double gnorm = (maxval - c.G ) / mdiff;
                double bnorm = (maxval - c.B ) / mdiff;   

                cn.Saturation = (cn.Luminance <= 0.5f) ? (mdiff / msum) : (mdiff / (510.0f - msum));

                if (c.R == maxval)
                {
                    cn.Hue = 60.0f * (6.0f + bnorm - gnorm);
                }
                
                if (c.G == maxval)
                {
                    cn.Hue = 60.0f * (2.0f + rnorm - bnorm);
                }
                
                if (c.B == maxval)
                {
                    cn.Hue = 60.0f * (4.0f + gnorm - rnorm);
                }
                
                if (cn.Hue > 360.0f)
                {
                    cn.Hue = cn.Hue - 360.0f;
                }
            }

            return cn;
        }

        public static implicit operator Color(HSLColor cn)
        {
            byte r, g, b;

            if (Math.Abs(cn.Saturation) < double.Epsilon)
            {
                r = g = b = (byte)(cn.Luminance * 255.0);
            }
            else
            {
                double rm1, rm2;
         
                if (cn.Luminance <= 0.5f)
				{
                    rm2 = cn.Luminance + cn.Luminance * cn.Saturation;
                }
				else
				{
                    rm2 = cn.Luminance + cn.Saturation - cn.Luminance * cn.Saturation;
                }
                
                rm1 = 2.0f * cn.Luminance - rm2;

                r = ToRGBHelper(rm1, rm2, cn.Hue + 120.0f);   
                g = ToRGBHelper(rm1, rm2, cn.Hue);
                b = ToRGBHelper(rm1, rm2, cn.Hue - 120.0f);
            }

            return Color.FromArgb(r, g, b);
        }
    
        static byte ToRGBHelper(double rm1, double rm2, double rh)
        {
            if (rh > 360.0f)
            {
                rh -= 360.0f;
            }
            else if (rh < 0.0f)
            {
                rh += 360.0f;
            }
 
            if (rh <  60.0f)
            {
                rm1 = rm1 + (rm2 - rm1) * rh / 60.0f;
            }
            else if (rh < 180.0f)
            {
                rm1 = rm2;
            }
            else if (rh < 240.0f)
            {
                rm1 = rm1 + (rm2 - rm1) * (240.0f - rh) / 60.0f;
            }
                   
            return (byte)(rm1 * 255);
        }

        public double Hue { get; set; }

        public double Saturation { get; set; }

        public double Luminance { get; set; }
    }
}
