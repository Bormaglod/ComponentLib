//-----------------------------------------------------------------------
// <copyright file="SeriesList.cs" company="Sergey Teplyashin">
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
// <summary>Defines the SeriesList class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    
    class SeriesList
    {
        readonly List<Series> series;
        PieGraph owner;
        
        public SeriesList(PieGraph owner)
        {
            this.series = new List<Series>();
            this.owner = owner;
        }
        
        public int CountData
        {
            get { return series.Count; }
        }
        
        public int Levels
        {
            get
            {
                if (series.Count > 0)
                {
                    return series.Max(s => s.Level) + 1;
                }
                
                return 0;
            }
        }
        
        internal PieGraph Owner
        {
            get { return owner; }
        }
        
        public IEnumerable<Series> GetSeries()
        {
            return series;
        }
        
        public IEnumerable<Series> GetChilds(Series parent)
        {
            return series.Where(s => s.Parent == parent);
        }
        
        public IEnumerable<Series> GetSeries(int level)
        {
            return series.Where(s => s.Level == level);
        }
        
        public float GetPercent(Series series)
        {
            return series.ValueData / GetSeries(series.Level).Sum(s => s.ValueData);
        }
        
        public float GetPercentRoot(Series series)
        {
            return series.ValueData / GetSeries(0).Sum(s => s.ValueData);
        }
        
        public Series AddSeries(string name, string title, Color baseColor, float valueData, Series parent)
        {
            Series s = new Series(this, name, title, baseColor, valueData, parent);
            series.Add(s);
            return s;
        }
        
        public void Clear()
        {
            series.Clear();
        }
    }
}
