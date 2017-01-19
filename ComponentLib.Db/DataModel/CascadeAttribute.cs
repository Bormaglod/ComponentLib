//-----------------------------------------------------------------------
// <copyright file="CascadeAttribute.cs" company="Тепляшин Сергей Васильевич">
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
// <date>23.04.2012</date>
// <time>12:16</time>
// <summary>Defines the CascadeAttribute class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.DataModel
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class CascadeAttribute : Attribute
    {
        public bool OnUpdate { get; set; } = true;

        public bool OnDelete { get; set; } = false;

        public bool OnInside { get; set; } = false;
    }
}
