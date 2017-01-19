//-----------------------------------------------------------------------
// <copyright file="HSVColor.cs" company="Тепляшин Сергей Васильевич">
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
// <date>07.05.2013</date>
// <time>10:07</time>
// <summary>Defines the HSVColor class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core.Drawing
{
    using System;
    using System.Drawing;
    
    public class HSVColor
    {
        /// <summary>
        /// Функция возвращает объект HSVColor.
        /// </summary>
        /// <param name="hue">Тон (значение от 0 до 360 грудусов.</param>
        /// <param name="saturation">Насыщеность (значение от 0 до 1).</param>
        /// <param name="value">Значение (или якрость) (значение от 0 до 1).</param>
        /// <returns></returns>
        public static HSVColor FromHSV(float hue, float saturation, float value)
        {
            return new HSVColor()
            {
                Hue = hue,
                Saturation = saturation,
                Value = value
            };
        }
        
        public static HSVColor FromRGB(int R, int G, int B) => (HSVColor)Color.FromArgb(R, G, B);
        
        public static HSVColor FromRGB(Color color) => (HSVColor)color;
        
        public static implicit operator HSVColor(Color color)
        {
            HSVColor c = new HSVColor();
            float R = color.R / 255;
            float G = color.G / 255;
            float B = color.B / 255;
            
            float minval = Math.Min(R, Math.Min(G, B));
            float maxval = Math.Max(R, Math.Max(G, B));
            float mdiff  = maxval - minval;
            
            c.Hue = 0;
            if (Math.Abs(maxval - R) < double.Epsilon && G >= B)
            {
                c.Hue = 60 * (G - B) / mdiff;
            }
            else if (Math.Abs(maxval - R) < double.Epsilon && G < B)
            {
                c.Hue = 60 * (G - B) / mdiff + 360;
            }
            else if (Math.Abs(maxval - G) < double.Epsilon)
            {
                c.Hue = 60 * (B - R) / mdiff + 120;
            }
            else if (Math.Abs(maxval - B) < double.Epsilon)
            {
                c.Hue = 60 * (R - G) / mdiff + 240;
            }
            
            c.Saturation = Math.Abs(maxval) < double.Epsilon ? 0 : 1 - minval / maxval;
            
            c.Value = maxval;
            
            return c;
        }
        
        public double Hue { get; set; }

        public double Saturation { get; set; }

        public double Value { get; set; }
    }
}
