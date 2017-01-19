//-----------------------------------------------------------------------
// <copyright file="RecentFiles.cs" company="Тепляшин Сергей Васильевич">
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
// <date>25.04.2013</date>
// <time>8:53</time>
// <summary>Defines the RecentFiles class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using Design;
    
    public class RecentFiles : Component
    {
        string folder = "UserApplication";
        string fileName = "RecentFiles.xml";
        Environment.SpecialFolder target = Environment.SpecialFolder.ApplicationData;
        ObjectAccessCollection<File> files = new ObjectAccessCollection<File>();

        public RecentFiles()
        {
            LoadFromXml();
        }
        
        public event EventHandler<EventArgs> Loaded;
        
        [Editor(typeof(RecentFileEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectAccessCollection<File> Files => files;
        
        [RefreshProperties(RefreshProperties.All)]
        public Environment.SpecialFolder Target
        {
            get
            {
                return target;
            }
            
            set
            {
                if (target != value)
                {
                    target = value;
                    LoadFromXml();
                }
            }
        }
        
        [RefreshProperties(RefreshProperties.All)]
        public string Folder
        {
            get
            {
                return folder;
            }
            
            set
            {
                if (folder != value)
                {
                    folder = value;
                    LoadFromXml();
                }
            }
        }
        
        [RefreshProperties(RefreshProperties.All)]
        public string FileName
        {
            get
            {
                return fileName;
            }
            
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    LoadFromXml();
                }
            }
        }
        
        string FullFileName => string.Join("\\", new string[] { Environment.GetFolderPath(target), folder, fileName });
        
        public void LoadFromXml()
        {
            files = new ObjectAccessCollection<File>(FullFileName, "RecentFiles");
            Loaded?.Invoke(this, new EventArgs());
        }
        
        public void SaveToXml()
        {
            files.SaveToXml(FullFileName, "RecentFiles");
        }
    }
}
