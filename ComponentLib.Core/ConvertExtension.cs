//-----------------------------------------------------------------------
// <copyright file="ConvertExtension.cs" company="Тепляшин Сергей Васильевич">
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
// <date>12.03.2012</date>
// <time>14:40</time>
// <summary>Defines the ConvertExtension class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    
    public static class ConvertExtension
    {
        public static IList<T> ToList<T>(string convertingValue)
        {
            return ToList<T>(convertingValue, ";");
        }
        
        public static IList<T> ToList<T>(string convertingValue, string delimiter)
        {
            List<T> res = new List<T>();
            if (!string.IsNullOrEmpty(convertingValue))
            {
                string[] ts = convertingValue.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in ts)
                {
                    res.Add((T)Convert.ChangeType(s, typeof(T), CultureInfo.InvariantCulture));
                }
            }
            
            return res;
        }
        
        public static string ToString<T>(IEnumerable<T> list)
        {
            return ToString<T>(list, ";");
        }
        
        public static string ToString<T>(IEnumerable<T> list, string delimiter)
        {
            string res = string.Empty;
            IEnumerator<T> en = list.GetEnumerator();
            bool next = en.MoveNext();
			while (next)
			{
				res += en.Current.ToString();
				next = en.MoveNext();
				if (next)
				{
					res += delimiter;
				}
			}
            
            return res;
        }
        
        public static string ToString(IEnumerable list)
        {
            return ToString(list, ";");
        }
        
        public static string ToString(IEnumerable list, string delimiter)
        {
            string res = string.Empty;
            IEnumerator en = list.GetEnumerator();
            bool next = en.MoveNext();
			while (next)
			{
				res += en.Current.ToString();
				next = en.MoveNext();
				if (next)
				{
					res += delimiter;
				}
			}
            
            return res;
        }
    }
}
