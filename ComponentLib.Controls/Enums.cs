//-----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
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
// <date>26.10.2011</date>
// <time>8:37</time>
// <summary>Defines the enums.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    
    public enum UnicodeIndex
    {
        /// <summary>
        /// Есвропейские языки.
        /// </summary>
        Europe,
        
        /// <summary>
        /// Африканские языки.
        /// </summary>
        Africa,
        
        /// <summary>
        /// Ближневосточные языки.
        /// </summary>
        NearEast,
        
        /// <summary>
        /// Языки Южной Азии.
        /// </summary>
        SouthAsia,
        
        /// <summary>
        /// Филлипинские языки.
        /// </summary>
        Philippines,
        
        /// <summary>
        /// Языки Юго-восточной Азии.
        /// </summary>
        SouthEastAsia,
        
        /// <summary>
        /// Восточно-Азиатские языки.
        /// </summary>
        EastAsia,
        
        /// <summary>
        /// Центрально-Азиатские языки
        /// </summary>
        CenterAsia,
        
        /// <summary>
        /// Другие языки.
        /// </summary>
        OtherLanguages,
        
        /// <summary>
        /// Символы.
        /// </summary>
        Symbols,
        
        /// <summary>
        /// Математические символы.
        /// </summary>
        Mathematics,
        
        /// <summary>
        /// Фонетические символы.
        /// </summary>
        Phonetic,
        
        /// <summary>
        /// Диакретические знаки.
        /// </summary>
        Diacritic,
        
        /// <summary>
        /// Другие символы.
        /// </summary>
        Other
    }
    
    public enum ButtonDirection
    {
        None,
        
        Up,
        
        Right,
        
        Down,
        
        Left
    }
}
