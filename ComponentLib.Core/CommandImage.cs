//-----------------------------------------------------------------------
// <copyright file="CommandImage.cs" company="Тепляшин Сергей Васильевич">
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
// <date>02.11.2011</date>
// <time>9:24</time>
// <summary>Defines the CommandImage class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    public class CommandImage
    {
    	readonly int size;
        readonly string name;
        readonly string category;
        
        public CommandImage(string name, string category, int size)
        {
            this.name = name;
            this.category = category;
            this.size = size;
        }

        public string Name => name;

        public string Category => category;

        public int Size => size;
    }
}
