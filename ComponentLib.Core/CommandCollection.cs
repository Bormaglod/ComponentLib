//-----------------------------------------------------------------------
// <copyright file="CommandCollection.cs" company="Тепляшин Сергей Васильевич">
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
// <date>13.03.2012</date>
// <time>13:54</time>
// <summary>Defines the CommandCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    
    public class CommandCollection : ICollection<Command>
    {
        List<Command> commands;
        string application;
        string fileName;
        
        public CommandCollection(string app)
        {
            commands = new List<Command>();
            application = app;
            fileName = FileNames.CommandsFile;
        }
        
        public CommandCollection(string app, string fileName, IEnumerable<Command> defaultCommands) : this(app)
        {
            if (fileName == FileNames.CommandsFile)
            {
                this.fileName = FileNames.GetCommandsFile();
            }
            
            if (string.IsNullOrEmpty(this.fileName) || !this.LoadFromXml(this.fileName))
            {
                this.AddRange(defaultCommands);
            }
            
            if (defaultCommands != null)
            {
                foreach (Command cmd in defaultCommands)
                {
                    if (this.GetCommand(cmd.ToString()) == null)
                    {
                        this.Add(cmd);
                    }
                }
            }
        }
        
        public CommandCollection(string app, string fileName) : this(app, fileName, null)
        {
        }
        
        public CommandCollection(string app, IEnumerable<Command> defaultCommands) : this(app, string.Empty, defaultCommands)
        {
        }
        
        public string Application
        {
            get { return application; }
        }
        
        #region ICollection<Command> interface implemented
        
        /// <summary>
        /// Gets the number of elements contained in the ObjectAccessCollection.
        /// </summary>
        public int Count
        {
            get { return commands.Count; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the ObjectAccessCollection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        
        public void Add(Command item)
        {
            if (commands.Find(f => f.Category == item.Category && f.Name == item.Name) == null)
            {
                commands.Add(item);
            }
        }
        
        public bool Remove(Command item)
        {
            return commands.Remove(item);
        }
        
        public void Clear()
        {
            commands.Clear();
        }
        
        public bool Contains(Command item)
        {
            return commands.Contains(item);
        }
        
        public void CopyTo(Command[] array, int arrayIndex)
        {
            commands.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<Command> GetEnumerator()
        {
            return commands.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        public Command GetCommand(string fullName)
        {
            return commands.Find(f => f.ToString() == fullName);
        }
        
        public void AddRange(IEnumerable<Command> commands)
        {
            foreach (Command cmd in commands)
            {
                Add(cmd);
            }
        }
        
        public bool LoadFromXml(string fileName)
        {
        	if (System.IO.File.Exists(fileName))
            {
                Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode nodeCommands = doc.SelectSingleNode(string.Format("/Commands/{0}", Application));
                if (nodeCommands != null)
                {
                    foreach (XmlNode node in nodeCommands.ChildNodes)
                    {
                        Command cmd = new Command();
                        cmd.LoadFromXml(node);
                        commands.Add(cmd);
                    }
                    
                    return true;
                }
            }
            
            return false;
        }
        
        public void SaveToXml()
        {
            SaveToXml(fileName);
        }
        
        public void SaveToXml(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = null;
            XmlNode nodeApp = null;
            if (System.IO.File.Exists(fileName))
            {
                doc.Load(fileName);
                root = doc.SelectSingleNode("/Commands");
                if (root != null)
                {
                    nodeApp = root.SelectSingleNode(Application);
                }
            }
            else
            {
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty));
            }
            
            if (root == null)
            {
                root = doc.CreateElement("Commands");
                doc.AppendChild(root);
            }
            
            if (nodeApp != null)
            {
                nodeApp.RemoveAll();
            }
            else
            {
                nodeApp = doc.CreateElement(Application);
                root.AppendChild(nodeApp);
            }
            
            foreach (Command cmd in this)
            {
                nodeApp.AppendChild(cmd.CreateXmlElement(doc));
            }
            
            doc.Save(fileName);
        }
    }
}
