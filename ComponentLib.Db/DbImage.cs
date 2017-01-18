//-----------------------------------------------------------------------
// <copyright file=DbImage.cs company="NIIAR">
//     Copyright (c) 2016 АО ГНЦ "НИИАР". All rights reserved.
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
// <date>21.04.2016</date>
// <time>12:23</time>
// <summary>Defines the DbImage class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    using System;
    using System.IO;
    using ComponentLib.Db.Configuration;
    
    public class DbImage
    {
        string file;
        
        public string FileName
        {
            get { return string.Format(@"{0}\{1}", RepoConfig.ImagesLocation, file); }
        }
        
        public void LoadFromFile(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return;
            }
            
            if (File.Exists(source))
            {
                if (!Directory.Exists(RepoConfig.ImagesLocation))
                {
                    Directory.CreateDirectory(RepoConfig.ImagesLocation);
                }
                
                DeleteFile(FileName);
                string ext = Path.GetExtension(source);
                
                Random rnd = new Random(DateTime.Now.Millisecond);
                do
                {
                    file = string.Format(@"Image{0}{1}", rnd.Next(), ext);
                }
                while (File.Exists(FileName));
                
                File.Copy(source, FileName);
            }
        }
        
        public void DeleteFile()
        {
            DeleteFile(FileName);
            file = string.Empty;
        }
        
        public override string ToString()
        {
            return string.Format("[DbImage File={0}]", FileName);
        }

        public static DbImage Create()
        {
            return new DbImage();
        }
        
        public static DbImage Create(string fileName)
        {
            DbImage img = Create();
            img.LoadFromFile(fileName);
            return img;
        }
        
        void DeleteFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                FileAttributes fa = File.GetAttributes(fileName);
                if (fa.HasFlag(FileAttributes.ReadOnly))
                {
                    File.SetAttributes(fileName, fa ^ FileAttributes.ReadOnly);
                }
                
                File.Delete(fileName);
            }
        }
    }
}
