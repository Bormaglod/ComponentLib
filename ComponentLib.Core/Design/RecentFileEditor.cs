//-----------------------------------------------------------------------
// <copyright file="RecentFileEditor.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2016 Тепляшин Сергей Васильевич
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
// <date>25.04.2013</date>
// <time>9:21</time>
// <summary>Defines the RecentFileEditor class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core.Design
{
    using System;
    using System.ComponentModel.Design;
    
    public class RecentFileEditor : CollectionEditor
    {
        public RecentFileEditor(Type type) : base(type)
        {
        }
        
        protected override string GetDisplayText(object value)
        {
            File file = value as File;
            if (file != null)
            {
                return file.ShortFileName;
            }
            
            return base.GetDisplayText(value);
        }
        
        protected override object CreateInstance(Type itemType)
        {
            object obj = null;
            if (itemType.FullName == "ComponentLib.Core.File")
            {
                ObjectAccessCollection<File> files = ((RecentFiles)Context.Instance).Files;
                obj = new File();
                files.Add((File)obj);
            }
            
            return obj;
        }
        
        
    }
}
