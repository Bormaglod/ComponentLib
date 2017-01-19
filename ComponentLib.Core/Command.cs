//-----------------------------------------------------------------------
// <copyright file="Command.cs" company="Тепляшин Сергей Васильевич">
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
// <date>02.11.2011</date>
// <time>12:21</time>
// <summary>Defines the Command class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Windows.Forms;
    using System.Xml;
    using Xml;
    
    public class Command : IXmlData
    {
        string category;
        string name;
        string defaultImageKey;
        Keys defaultShortcutKey;
        
        public Command() : this(string.Empty, string.Empty, string.Empty, Keys.None)
        {
        }
        
        public Command(string category, string name) : this(category, name, string.Empty, Keys.None)
        {
        }
        
        public Command(string category, string name, Keys defaultShortcutKey) : this(category, name, string.Empty, defaultShortcutKey)
        {
        }
        
        public Command(string category, string name, string defaultImageKey, Keys defaultShortcutKey)
        {
            this.category = category;
            this.name = name;
            this.ImageKey = defaultImageKey;
            this.defaultImageKey = defaultImageKey;
            this.defaultShortcutKey = defaultShortcutKey;
            this.ShortcutKey = defaultShortcutKey;
        }
        
        public bool IsSeparator => string.IsNullOrEmpty(name);
        
        public string Category => category;
        
        public string Name => name;
        
        public string ImageKey { get; set; }
        
        public string DefaultImageKey => defaultImageKey;
        
        public Keys ShortcutKey { get; set; }
        
        public Keys DefaultShortcutKey => defaultShortcutKey;
        
        public string CommandValue => string.Format("{0}.{1}", Category, Name);
        
        public override string ToString() => CommandValue;

        #region IXmlData interface implemented
        
        public XmlElement CreateXmlElement(XmlDocument document)
        {
            XmlElement cmdElement = document.CreateElement("Command");
            document.AddAttribute(cmdElement, "Name", Name);
            document.AddAttribute(cmdElement, "Category", Category);
            document.AddAttribute(cmdElement, "Image", ImageKey);
            document.AddAttribute(cmdElement, "DefaultImage", DefaultImageKey);
            document.AddAttribute(cmdElement, "Shortcut", ShortcutKey.ToString());
            document.AddAttribute(cmdElement, "DefaultShortcut", DefaultShortcutKey.ToString());
            return cmdElement;
        }
        
        public void LoadFromXml(XmlNode node)
        {
            name = node.Attributes["Name"].Value;
            category = node.Attributes["Category"].Value;
            ImageKey = node.AttributeValueOrDefault("Image");
            defaultImageKey = node.AttributeValueOrDefault("DefaultImage");
            ShortcutKey = (Keys)Enum.Parse(typeof(Keys), node.AttributeValueOrDefault("Shortcut"));
            defaultShortcutKey = (Keys)Enum.Parse(typeof(Keys), node.AttributeValueOrDefault("DefaultShortcut"));
        }
        
        #endregion
    }
}
