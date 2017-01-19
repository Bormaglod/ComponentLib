//-----------------------------------------------------------------------
// <copyright file="Cultures.cs" company="Тепляшин Сергей Васильевич">
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
// <date>24.09.2010</date>
// <time>20:47</time>
// <summary>Defines the Cultures class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Globalization
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public static class Cultures
    {
        public static IList<Culture> CreateListCultures()
        {
            List<Culture> cultures = new List<Culture>();
            
            CultureInfo[] cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            foreach (CultureInfo culture in cultureInfo)
            {
                cultures.Add(new Culture(culture.DisplayName, culture.ThreeLetterWindowsLanguageName, culture.LCID));
            }
            
            cultures.Sort();
            return cultures;
        }
        
        public static void FillCountriesCollection(IList items)
        {
            FillCountriesCollection(items, CreateListCultures());
        }
        
        public static void FillCountriesCollection(IList items, IList<Culture> cultures)
        {
            foreach (Culture c in cultures)
            {
                items.Add(c);
            }
        }
    }
}
