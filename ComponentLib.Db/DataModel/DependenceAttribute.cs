//-----------------------------------------------------------------------
// <copyright file="DependenceAttribute.cs" company="Тепляшин Сергей Васильевич">
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
// <date>20.06.2014</date>
// <time>13:07</time>
// <summary>Defines the DependenceAttribute class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.DataModel
{
    using System;
    
    public enum DependenceAction
    {
        SetNull,
        
        Delete,
        
        Nothing
    }
    
    public class DependenceAttribute : Attribute
    {
        DependenceAction action;
        
        public DependenceAttribute(DependenceAction action)
        {
            this.action = action;
        }

        public DependenceAction Action => action;
    }
}
