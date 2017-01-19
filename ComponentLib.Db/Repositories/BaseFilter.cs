//-----------------------------------------------------------------------
// <copyright file="BaseFilter.cs" company="Тепляшин Сергей Васильевич">
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
// <date>01.11.2016</date>
// <time>12:51</time>
// <summary>Defines the BaseFilter class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    
    public class BaseFilter
    {
        bool enabled;
        
        public event EventHandler<EventArgs> EnableChanged;
            
        public event EventHandler<EventArgs> DataChanged;
        
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    DoEnableChanged();
                }
            }
        }
        
        protected virtual void OnEnableChanged() {}
        
        protected virtual void OnDataChanged() {}
        
        protected void DoEnableChanged()
        {
            EnableChanged?.Invoke(this, EventArgs.Empty);
            OnEnableChanged();
        }
        
        protected void DoDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
            OnDataChanged();
        }
    }
}
