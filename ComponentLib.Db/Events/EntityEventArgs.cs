//-----------------------------------------------------------------------
// <copyright file="EntityEventArgs.cs" company="Sergey Teplyashin">
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
// <date>24.02.2011</date>
// <time>13:18</time>
// <summary>Defines the EntityEventArgs class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    using System;
    
    public class EntityEventArgs : EventArgs
    {
        Entity e;
        ObjectAction action;
        DatabaseState dbState;
        
        public EntityEventArgs(Entity entity, ObjectAction act, DatabaseState state)
        {
            e = entity;
            action = act;
            dbState = state;
        }
        
        public Entity Entity
        {
            get { return e; }
        }
        
        public ObjectAction Action
        {
            get { return action; }
        }
        
        public DatabaseState State
        {
            get { return dbState; }
        }
    }
}
