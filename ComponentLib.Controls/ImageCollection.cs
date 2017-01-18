//-----------------------------------------------------------------------
// <copyright file=ImageCollection.cs company="Sergey Teplyashin">
//     Copyright (c) 2010-2016 Sergey Teplyashin. All rights reserved.
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
// <time>20:05</time>
// <summary>Defines the ImageCollection.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    
    class ImageCollection : IDisposable
    {
        readonly KryptonDataGridView viewer;
        readonly Dictionary<string, Image> images;
        
        public ImageCollection(KryptonDataGridView viewer)
        {
            this.viewer = viewer;
            images = new Dictionary<string, Image>();
        }
        
        ~ImageCollection()
        {
            Dispose();
        }
        
        public Image this[int index]
        {
            get
            {
                DataGridViewImageColumn column = (DataGridViewImageColumn)viewer.Columns[index];
                return images.ContainsKey(column.Description) ? images[column.Description] : null;
            }
        }
        
        public Image this[string name]
        {
            get { return images.ContainsKey(name) ? images[name] : null; }
        }
        
        public void Dispose()
        {
            foreach (string key in images.Keys)
            {
                images[key].Dispose();
            }
        }
        
        public void Clear()
        {
            Dispose();
            images.Clear();
            viewer.Columns.Clear();
        }
        
        public Image Add(string fileName)
        {
            if (File.Exists(fileName))
            {
                
                MemoryStream mem = new MemoryStream(File.ReadAllBytes(fileName));
                Image image = new Bitmap(mem);
                images.Add(fileName, image);
                
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.Description = fileName;
                
                int col = viewer.Columns.Add(column);
                if (viewer.Rows.Count == 0)
                {
                    viewer.Rows.Add();
                }
                
                viewer[col, 0].Value = image;
                
                return image;
            }
            
            return null;
        }
    }
}
