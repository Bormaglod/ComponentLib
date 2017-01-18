//-----------------------------------------------------------------------
// <copyright file="Commands.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
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
// <date>19.04.2013</date>
// <time>9:03</time>
// <summary>Defines the Commands class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Windows.Forms;

    [ProvideProperty("Command", typeof(IComponent))]
    public class Commands : Component, IExtenderProvider
    {
        static Hashtable properties = new Hashtable();
        IComponentChangeService componentChangeService;
        
        public Commands()
        {
        }
        
        public event EventHandler<EventArgs> Execute;
        
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            
            set
            {
                base.Site = value;
                componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (componentChangeService != null)
                {
                    componentChangeService.ComponentAdded += new ComponentEventHandler(this.Commands_ComponentAdded);
                    componentChangeService.ComponentRemoved += new ComponentEventHandler(this.Commands_ComponentRemoved);
                }
            }
        }
        
        public int Count
        {
            get { return properties.Count; }
        }
        
        #region IExtenderProvider implemented
        
        public bool CanExtend(object extendee)
        {
            return
                extendee is ToolStripMenuItem ||
                extendee is ToolStripButton ||
                extendee is IButtonControl ||
                extendee is LabelLinkImage;
        }
        
        #endregion
        
        [DisplayName("Command")]
        public string GetCommand(IComponent component)
        {
            return properties.Contains(component) ? (string)properties[component] : string.Empty;
        }
        
        public void SetCommand(IComponent component, string command)
        {
            if (properties.Contains(component))
            {
                properties[component] = command;
            }
            else
            {
                properties.Add(component, command);
            }
        }
        
        protected void OnExecute()
        {
            if (Execute != null)
            {
                Execute(this, new EventArgs());
            }
        }
        
        void Commands_ComponentAdded(object sender, ComponentEventArgs e)
        {
            LabelLinkImage lli = e.Component as LabelLinkImage;
            if (lli != null)
            {
                lli.SelectLink += new EventHandler<EventArgs>(this.LabelLink_SelectLink);
            }
        }
        
        void Commands_ComponentRemoved(object sender, ComponentEventArgs e)
        {
            LabelLinkImage lli = e.Component as LabelLinkImage;
            if (lli != null)
            {
                lli.SelectLink -= new EventHandler<EventArgs>(this.LabelLink_SelectLink);
            }
            
            properties.Remove(e.Component);
        }
        
        void LabelLink_SelectLink(object sender, EventArgs e)
        {
            OnExecute();
        }
    }
}
