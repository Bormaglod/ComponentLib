//-----------------------------------------------------------------------
// <copyright file="RepositoryException.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
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
// <date>23.04.2012</date>
// <time>11:51</time>
// <summary>Defines the RepositoryException class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Exceptions
{
    using System;
    
    public static class RepoErrors
    {
        public const int KeyExist              = 0x0001;
        public const int KeyEmpty              = 0x0002;
        public const int KeyUniqueError        = 0x0003;
        public const int EntityExist           = 0x0010;
        public const int EntityNotExist        = 0x0011;
        public const int EntityUsed            = 0x0012;
        public const int EntityDependenceError = 0x0013;
        public const int FieldValueRequired    = 0x0050;
        public const int CheckError            = 0x0060;
    }
    
    public class RepositoryException : Exception
    {
        Entity _entity;
        readonly int _code;
        
        public RepositoryException(string message, int code, Entity entity) : base(message)
        {
            _entity = entity;
            _code = code;
        }
        
        public Entity Entity
        {
            get { return _entity; }
        }
        
        public int Code
        {
            get { return _code; }
        }
    }
}
