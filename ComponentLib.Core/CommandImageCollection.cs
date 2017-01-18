//-----------------------------------------------------------------------
// <copyright file="CommandImageCollection.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2016 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>02.11.2011</date>
// <time>9:26</time>
// <summary>Defines the CommandImageCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    
    public class CommandImageCollection : ICollection<CommandImage>
    {
        readonly List<string> categories;
        readonly List<CommandImage> images;
        
        public CommandImageCollection()
        {
            categories = new List<string>();
            images = new List<CommandImage>();
            CreateDefaultList();
        }
        
        public IEnumerable<string> Categories
        {
            get { return categories; }
        }
        
        public static Image GetImage(string name)
        {
            object obj = Resources.ResourceManager.GetObject(name, Resources.Culture);
            return (Bitmap)obj;
        }
        
        #region ICollection<Command> interface implemented
        
        /// <summary>
        /// Gets the number of elements contained in the ObjectAccessCollection.
        /// </summary>
        public int Count
        {
            get { return images.Count; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the ObjectAccessCollection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        
        public void Add(CommandImage item)
        {
            images.Add(item);
        }
        
        public bool Remove(CommandImage item)
        {
            return images.Remove(item);
        }
        
        public void Clear()
        {
            images.Clear();
        }
        
        public bool Contains(CommandImage item)
        {
            return images.Contains(item);
        }
        
        public void CopyTo(CommandImage[] array, int arrayIndex)
        {
            images.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<CommandImage> GetEnumerator()
        {
            return images.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        public IEnumerable<CommandImage> GetImages(string category)
        {
            return from image in images
                where image.Category == category
                select image;
        }
        
        void CreateDefaultList()
        {
            categories.AddRange(new [] {
                                         "Actions",
                                         "Animations",
                                         "Applications",
                                         "Categories",
                                         "Devices",
                                         "Emblems",
                                         "Emotes",
                                         "Filesystems",
                                         "International",
                                         "Mimetypes",
                                         "Places",
                                         "Status",
                                         "Testing",
                                         "Finance"
                                     });
            images.AddRange(new [] {
                                     new CommandImage("accept", "Actions", 16),
                                     new CommandImage("add", "Actions", 16),
                                     new CommandImage("cancel", "Actions", 16),
                                     new CommandImage("cross", "Actions", 16),
                                     new CommandImage("delete", "Actions", 16),
                                     new CommandImage("tick", "Actions", 16),
                                     new CommandImage("book", "Actions", 16),
                                     new CommandImage("book_add", "Actions", 16),
                                     new CommandImage("book_delete", "Actions", 16),
                                     new CommandImage("book_edit", "Actions", 16),
                                     new CommandImage("book_error", "Actions", 16),
                                     new CommandImage("book_go", "Actions", 16),
                                     new CommandImage("book_key", "Actions", 16),
                                     new CommandImage("book_link", "Actions", 16),
                                     new CommandImage("book_next", "Actions", 16),
                                     new CommandImage("book_open", "Actions", 16),
                                     new CommandImage("book_previous", "Actions", 16),
                                     new CommandImage("book_in", "Actions", 16),
                                     new CommandImage("star", "Actions", 16),
                                     new CommandImage("stop", "Actions", 16),
                                     new CommandImage("cut", "Actions", 16),
                                     new CommandImage("paste", "Actions", 16),
                                     new CommandImage("copy", "Actions", 16),
                                     new CommandImage("textfield", "Actions", 16),
                                     new CommandImage("textfield_add", "Actions", 16),
                                     new CommandImage("textfield_delete", "Actions", 16),
                                     new CommandImage("textfield_key", "Actions", 16),
                                     new CommandImage("textfield_rename", "Actions", 16),
                                     new CommandImage("chart_bar", "Actions", 16),
                                     new CommandImage("chart_bar_add", "Actions", 16),
                                     new CommandImage("chart_bar_delete", "Actions", 16),
                                     new CommandImage("chart_bar_edit", "Actions", 16),
                                     new CommandImage("chart_bar_error", "Actions", 16),
                                     new CommandImage("chart_bar_link", "Actions", 16),
                                     new CommandImage("close", "Actions", 16),
                                     new CommandImage("exit", "Actions", 16),
                                     new CommandImage("configure", "Actions", 16),
                                     new CommandImage("manual", "Actions", 16),
                                     new CommandImage("view_column", "Actions", 16),
                                     new CommandImage("property", "Actions", 16),
                                     new CommandImage("document_letter_new", "Actions", 16),
                                     new CommandImage("book_open_text_image", "Actions", 16),
                                     new CommandImage("arrow_refresh", "Actions", 16),
                                     new CommandImage("table_delete", "Actions", 16),
                                     new CommandImage("table_edit", "Actions", 16),
                                     new CommandImage("table_multiple", "Actions", 16),
                                     new CommandImage("lock", "Actions", 16),
                                     new CommandImage("lock_open", "Actions", 16),
                                 });
            images.AddRange(new [] {
                                     new CommandImage("emoticon_evilgrin", "Emotes", 16),
                                     new CommandImage("emoticon_grin", "Emotes", 16),
                                     new CommandImage("emoticon_happy", "Emotes", 16),
                                     new CommandImage("emoticon_smile", "Emotes", 16),
                                     new CommandImage("emoticon_surprised", "Emotes", 16),
                                     new CommandImage("emoticon_tongue", "Emotes", 16),
                                     new CommandImage("emoticon_unhappy", "Emotes", 16),
                                     new CommandImage("emoticon_waii", "Emotes", 16),
                                     new CommandImage("emoticon_wink", "Emotes", 16)
                                 });
            images.AddRange(new [] {
                                     new CommandImage("lesson_add", "Testing", 16),
                                     new CommandImage("lesson_remove", "Testing", 16),
                                     new CommandImage("practice_setup", "Testing", 16),
                                     new CommandImage("practice_start", "Testing", 16),
                                     new CommandImage("set_language", "Testing", 16)
                                 });
            images.AddRange(new [] {
                                     new CommandImage("disk", "Devices", 16),
                                     new CommandImage("ipod", "Devices", 16),
                                     new CommandImage("joystick", "Devices", 16),
                                     new CommandImage("keyboard", "Devices", 16),
                                     new CommandImage("monitor", "Devices", 16),
                                     new CommandImage("mouse", "Devices", 16),
                                     new CommandImage("phone", "Devices", 16),
                                     new CommandImage("printer", "Devices", 16),
                                     new CommandImage("server", "Devices", 16),
                                     new CommandImage("telephone", "Devices", 16),
                                     new CommandImage("television", "Devices", 16)
                                 });
            images.AddRange(new [] {
                                     new CommandImage("folder", "Filesystems", 16),
                                     new CommandImage("file", "Filesystems", 16),
                                     new CommandImage("back-button", "Filesystems", 16),
                                     new CommandImage("forward-button", "Filesystems", 16),
                                     new CommandImage("house", "Filesystems", 16)
                                 });
            images.AddRange(new [] {
                                     new CommandImage("coins", "Finance", 16),
                                     new CommandImage("money", "Finance", 16),
                                     new CommandImage("money_dollar", "Finance", 16),
                                     new CommandImage("money_euro", "Finance", 16),
                                     new CommandImage("money_pound", "Finance", 16),
                                     new CommandImage("money_yen", "Finance", 16)
                                 });
        }
    }
}
