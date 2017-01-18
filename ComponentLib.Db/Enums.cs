//-----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sergey Teplyashin">
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
// <date>18.06.2014</date>
// <time>14:48</time>
// <summary>Defines the Enums class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    public enum ObjectAction
    {
        Undefined,
        Add,
        Delete,
        Modify
    }
    
    public enum ObjectImportAction
    {
        Skip,
        
        Replace
    }
    
    /// <summary>
    /// Состояние базы данных.
    /// </summary>
    public enum DatabaseState
    {
        /// <summary>
        /// База данных в процессе открытия.
        /// </summary>
        Opening,
        
        /// <summary>
        /// База данных в процессе создания.
        /// </summary>
        Creating,
        
        /// <summary>
        /// База данных открыта.
        /// </summary>
        Opened,
        
        /// <summary>
        /// База данных в процессе закрытия.
        /// </summary>
        Closing,
        
        /// <summary>
        /// База данных закрыта.
        /// </summary>
        Closed,
        
        Saving,
        
        Loading
    }
}
