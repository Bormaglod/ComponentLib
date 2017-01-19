//-----------------------------------------------------------------------
// <copyright file="ToolbarCommandCollection.cs" company="Тепляшин Сергей Васильевич">
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
// <time>15:29</time>
// <summary>Defines the ToolbarCommandCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Xml;
    
    public class ToolbarCommandCollection : ICollection<ToolbarCommand>
    {
        List<ToolbarCommand> commands;
        CommandCollection globalCommands;
        string application;
        string toolbarName;
        string fileName;
        
        public ToolbarCommandCollection(string app, string name, CommandCollection globalCommands)
        {
            this.application = app;
            this.toolbarName = name;
            this.globalCommands = globalCommands;
            this.commands = new List<ToolbarCommand>();
            this.fileName = FileNames.ToolBarsFile;
        }
        
        public ToolbarCommandCollection(string app, string name, CommandCollection globalCommands, string fileName, IEnumerable<ToolbarCommand> defaultCommands) : this(app, name, globalCommands)
        {
            if (fileName == FileNames.ToolBarsFile)
            {
                this.fileName = FileNames.GetToolbarsFile();
            }
            
            if (string.IsNullOrEmpty(this.fileName) || !LoadFromXml(this.fileName))
            {
                AddRange(defaultCommands);
            }
        }
        
        public ToolbarCommandCollection(string app, string name, CommandCollection globalCommands, IEnumerable<ToolbarCommand> defaultCommands) : this(app, name, globalCommands, string.Empty, defaultCommands)
        {
        }
        
        public ToolbarCommandCollection(string app, string name, CommandCollection globalCommands, string fileName) : this(app, name, globalCommands, fileName, null)
        {
        }
        
        public string Application
        {
            get { return application; }
        }
        
        public string ToolbarName => toolbarName;
        
        public CommandCollection GlobalCommands => globalCommands;
        
        #region ICollection<Command> interface implemented
        
        /// <summary>
        /// Gets the number of elements contained in the ObjectAccessCollection.
        /// </summary>
        public int Count => commands.Count;
        
        /// <summary>
        /// Gets a value indicating whether the ObjectAccessCollection is read-only.
        /// </summary>
        public bool IsReadOnly => false;
        
        public void Add(ToolbarCommand cmd)
        {
            if (commands.Find(f => f.Command == cmd.Command) == null)
            {
                commands.Add(cmd);
            }
        }
        
        public bool Remove(ToolbarCommand item) => commands.Remove(item);
        
        public void Clear()
        {
            commands.Clear();
        }
        
        public bool Contains(ToolbarCommand command) => commands.Contains(command);
        
        public void CopyTo(ToolbarCommand[] array, int arrayIndex)
        {
            commands.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<ToolbarCommand> GetEnumerator() => commands.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        #endregion
        
        public bool ContainsCommand(Command command) => commands.FirstOrDefault(x => x.Command == command) != null;
        
        public IEnumerable<ToolbarCommand> Commands
        {
            get
            {
                return from cmd in commands
                    orderby cmd.Position
                    select cmd;
            }
        }
        
        public void AddRange(IEnumerable<ToolbarCommand> commands)
        {
            foreach (ToolbarCommand cmd in commands)
            {
                Add(cmd);
            }
        }
        
        public bool LoadFromXml(string fileName)
        {
            int pos = 0;
            if (System.IO.File.Exists(fileName))
            {
                Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode nodeCommands = doc.SelectSingleNode(string.Format("/Toolbars/{0}/{1}", this.Application, this.ToolbarName));
                if (nodeCommands != null)
                {
                    foreach (XmlNode node in nodeCommands.ChildNodes)
                    {
                        if (node.Name == "Separator")
                        {
                            commands.Add(new ToolbarCommand(new Command(), pos++));
                        }
                        else
                        {
                            string commandName = node.Attributes["Name"].Value;
                            Command cmd = globalCommands.GetCommand(commandName);
                            if (cmd != null)
                            {
                                bool viewImage = node.AttributeValueAsBool("ViewImage", true);
                                bool viewText = node.AttributeValueAsBool("ViewText", true);
                                commands.Add(new ToolbarCommand(cmd, pos++, viewImage, viewText));
                            }
                        }
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
            XmlNode nodeToolbar = null;
            if (System.IO.File.Exists(fileName))
            {
                doc.Load(fileName);
                root = doc.SelectSingleNode("/Toolbars");
                if (root != null)
                {
                    nodeApp = root.SelectSingleNode(Application);
                    if (nodeApp != null)
                    {
                        nodeToolbar = nodeApp.SelectSingleNode(ToolbarName);
                    }
                }
            }
            else
            {
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty));
            }
            
            if (root == null)
            {
                root = doc.CreateElement("Toolbars");
                doc.AppendChild(root);
            }
            
            if (nodeApp == null)
            {
                nodeApp = doc.CreateElement(Application);
                root.AppendChild(nodeApp);
            }
            
            if (nodeToolbar == null)
            {
                nodeToolbar = doc.CreateElement(ToolbarName);
                nodeApp.AppendChild(nodeToolbar);
            }
            else
            {
                nodeToolbar.RemoveAll();
            }
            
            foreach (ToolbarCommand cmd in this)
            {
                XmlElement elem = cmd.Command.IsSeparator ? doc.CreateElement("Separator") : doc.CreateElement("Command");
                if (!cmd.Command.IsSeparator)
                {
                    doc.AddAttribute(elem, "Name", cmd.Command.ToString());
                    doc.AddAttribute(elem, "ViewImage", cmd.ViewImage.ToString());
                    doc.AddAttribute(elem, "ViewText", cmd.ViewText.ToString());
                }
                
                nodeToolbar.AppendChild(elem);
            }
            
            doc.Save(fileName);
        }
    }
}
