//-----------------------------------------------------------------------
// <copyright file="ToolBarsDialog.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
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
// <date>31.10.2011</date>
// <time>14:01</time>
// <summary>Defines the ToolBarsDialog class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Controls;
    using ComponentLib.Core;
    
    /// <summary>
    /// Description of ToolBarsDialog.
    /// </summary>
    public partial class ToolBarsDialog : KryptonForm
    {
        private const string separator = "--- line separator ---";
        
        private bool changed;
        private bool applyData;
        private Dictionary<Command, ToolStripMenuItem> commands;
        private ToolbarCommandCollection toolBar;
        private IEnumerable<ToolbarCommand> defToolbar;
        private Dictionary<Command, string> changedImages;
        private Dictionary<Command, ToolStripItemDisplayStyle> styles;
        
        public ToolBarsDialog()
        {
            InitializeComponent();
            this.changedImages = new Dictionary<Command, string>();
            this.styles = new Dictionary<Command, ToolStripItemDisplayStyle>();
        }
        
        public KryptonListBox CurrentListBox
        {
            get
            {
                if (this.listCurrentActions.SelectedIndex != -1)
                {
                    return this.listCurrentActions;
                }
                
                return this.listAvailableActions;
            }
        }
        
        public bool Changed
        {
            get
            {
                return this.changed;
            }
            
            set
            {
                this.changed = value;
                this.buttonApply.Enabled = this.changed;
            }
        }
        
        private Command CurrentCommand
        {
            get
            {
                if (this.CurrentListBox == this.listCurrentActions)
                {
                    if (this.listCurrentActions.SelectedItem != null)
                    {
                        Command cmd = ((KryptonListItem)this.listCurrentActions.SelectedItem).Tag as Command;
                        return cmd;
                    }
                }
                
                return null;
            }
        }
        
        public static bool Show(Dictionary<Command, ToolStripMenuItem> commands, ToolbarCommandCollection toolBar, IEnumerable<ToolbarCommand> def)
        {
            ToolBarsDialog dlg = new ToolBarsDialog();
            return dlg.ShowDialog(commands, toolBar, def);
        }
        
        public bool ShowDialog(Dictionary<Command, ToolStripMenuItem> commands, ToolbarCommandCollection toolBar, IEnumerable<ToolbarCommand> def)
        {
            this.applyData = false;
            this.Changed = false;
            this.commands = commands;
            this.toolBar = toolBar;
            this.defToolbar = def;
            this.CreateListCommands(toolBar.Commands);
            this.listAvailableActions.SelectedIndex = 0;
            if (ShowDialog() == DialogResult.OK)
            {
                this.Apply();
                return true;
            }
            
            return this.applyData;
        }
        
        private void Apply()
        {
            toolBar.Clear();
            int index = 0;
            foreach (KryptonListItem item in this.listCurrentActions.Items)
            {
                Command cmd = (Command)item.Tag;
                if (cmd == null)
                {
                    toolBar.Add(new ToolbarCommand(new Command(), index++));
                }
                else
                {
                    if (this.changedImages.ContainsKey(cmd))
                    {
                        cmd.ImageKey = this.changedImages[cmd];
                    }
                    
                    if (this.styles.ContainsKey(cmd))
                    {
                        toolBar.Add(new ToolbarCommand(cmd, index++, this.styles[cmd]));
                    }
                    else
                    {
                        toolBar.Add(new ToolbarCommand(cmd, index++));
                    }
                }
            }
            
            this.applyData = true;
        }
        
        private bool IsCurrentCommand(Command command)
        {
            foreach (KryptonListItem item in this.listCurrentActions.Items)
            {
                if ((Command)item.Tag == command)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private void CreateListCommands(IEnumerable<ToolbarCommand> toolbars)
        {
            this.CreateListCurrentCommands(toolbars);
            
            this.listAvailableActions.Items.Clear();
            KryptonListItem item = new KryptonListItem(separator);
            item.Image = new Bitmap(16, 16);
            this.listAvailableActions.Items.Add(item);
            foreach (Command cmd in this.commands.Keys)
            {
                if (!this.IsCurrentCommand(cmd))
                {
                    item = new KryptonListItem(this.commands[cmd].Text);
                    item.Tag = cmd;
                    if (string.IsNullOrEmpty(cmd.ImageKey))
                    {
                        item.Image = new Bitmap(16, 16);
                    }
                    else
                    {
                        item.Image = CommandImageCollection.GetImage(cmd.DefaultImageKey);
                    }
                    
                    this.listAvailableActions.Items.Add(item);
                }
            }
        }
        
        private void CreateListCurrentCommands(IEnumerable<ToolbarCommand> toolbars)
        {
            this.listCurrentActions.Items.Clear();
            this.styles.Clear();
            KryptonListItem item;
            foreach (ToolbarCommand cmd in toolbars)
            {
                if (cmd.Command.IsSeparator)
                {
                    item = new KryptonListItem(separator);
                }
                else
                {
                    item = new KryptonListItem(this.commands[cmd.Command].Text);
                    item.Tag = cmd.Command;
                }
                
                if (string.IsNullOrEmpty(cmd.Command.ImageKey))
                {
                    item.Image = new Bitmap(16, 16);
                }
                else
                {
                    if (this.changedImages.ContainsKey(cmd.Command))
                    {
                        item.Image = CommandImageCollection.GetImage(this.changedImages[cmd.Command]);
                    }
                    else
                    {
                        item.Image = CommandImageCollection.GetImage(cmd.Command.ImageKey);
                    }
                }
                
                this.listCurrentActions.Items.Add(item);
                this.styles.Add(cmd.Command, cmd.DisplayStyle);
            }
        }
        
        public void UpdateButtons()
        {
            this.buttonArrows1.SetEnabledButton(ButtonDirection.Up, this.CurrentListBox.SelectedIndex > 0);
            this.buttonArrows1.SetEnabledButton(ButtonDirection.Down, this.CurrentListBox.SelectedIndex < this.CurrentListBox.Items.Count - 1);
            this.buttonArrows1.SetEnabledButton(ButtonDirection.Right, this.CurrentListBox == this.listAvailableActions);
            this.buttonArrows1.SetEnabledButton(ButtonDirection.Left, this.CurrentListBox == this.listCurrentActions);
            this.buttonChangeIcon.Enabled = this.CurrentListBox == this.listCurrentActions;
            this.checkImage.Enabled = this.CurrentCommand != null;
            this.checkText.Enabled = this.CurrentCommand != null;
        }
        
        private void MoveItemUp()
        {
            KryptonListBox list = this.CurrentListBox;

            KryptonListItem item = (KryptonListItem)list.SelectedItem;
            int index = list.SelectedIndex;
            
            list.Items.RemoveAt(index);
            list.Items.Insert(index - 1, item);
            list.SelectedItem = item;
            this.Changed = true;
        }
        
        private void MoveItemDown()
        {
            KryptonListBox list = this.CurrentListBox;

            KryptonListItem item = (KryptonListItem)list.SelectedItem;
            int index = list.SelectedIndex;
            
            list.Items.RemoveAt(index);
            list.Items.Insert(index + 1, item);
            list.SelectedItem = item;
            this.Changed = true;
        }
        
        private void MoveToCurrentActions()
        {
            KryptonListItem item = (KryptonListItem)this.listAvailableActions.SelectedItem;
            int index = this.listAvailableActions.SelectedIndex;
            Command cmd = (Command)item.Tag;
            if (cmd != null)
            {
                this.listAvailableActions.Items.Remove(item);
                if (cmd.ImageKey != cmd.DefaultImageKey)
                {
                    if (this.changedImages.ContainsKey(cmd))
                    {
                        this.changedImages[cmd] = cmd.DefaultImageKey;
                    }
                    else
                    {
                        this.changedImages.Add(cmd, cmd.DefaultImageKey);
                    }
                }
            }
            
            this.listCurrentActions.Items.Add(item);
            if (index >= this.listAvailableActions.Items.Count)
            {
                index--;
            }
            
            this.listAvailableActions.SelectedIndex = index;
            this.Changed = true;
        }
        
        private void DeleteCurrentAction()
        {
            KryptonListItem item = (KryptonListItem)this.listCurrentActions.SelectedItem;
            int index = this.listCurrentActions.SelectedIndex;
            Command cmd = (Command)item.Tag;
            this.listCurrentActions.Items.Remove(item);
            if (cmd != null)
            {
                if (this.changedImages.ContainsKey(cmd))
                {
                    this.changedImages.Remove(cmd);
                }
                
                item.Image = CommandImageCollection.GetImage(cmd.DefaultImageKey);
                this.listAvailableActions.Items.Add(item);
            }
            
            if (index >= this.listCurrentActions.Items.Count)
            {
                index--;
            }
            
            this.listCurrentActions.SelectedIndex = index;
            this.Changed = true;
        }
        
        private bool changedSelectedIndex;
        
        private void ListAvailableActionsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!changedSelectedIndex)
            {
                this.changedSelectedIndex = true;
                try
                {
                    this.listCurrentActions.SelectedIndex = -1;
                    this.UpdateButtons();
                }
                finally
                {
                    this.changedSelectedIndex = false;
                }
            }
        }
        
        private bool manualChangeStyle = false;
        
        private void ListCurrentActionsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!changedSelectedIndex)
            {
                this.changedSelectedIndex = true;
                try
                {
                    this.listAvailableActions.SelectedIndex = -1;
                    this.UpdateButtons();
                    this.manualChangeStyle = true;
                    try
                    {
                        this.checkImage.Checked = false;
                        this.checkText.Checked = false;
                        if (this.listCurrentActions.SelectedItem != null)
                        {
                            Command cmd = ((KryptonListItem)this.listCurrentActions.SelectedItem).Tag as Command;
                            if (cmd != null && this.styles.ContainsKey(cmd))
                            {
                                this.checkImage.Checked = (this.styles[cmd] & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image;
                                this.checkText.Checked = (this.styles[cmd] & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text;
                            }
                        }
                    }
                    finally
                    {
                        this.manualChangeStyle = false;
                    }
                }
                finally
                {
                    this.changedSelectedIndex = false;
                }
            }
        }
        
        private void ButtonChangeIconClick(object sender, EventArgs e)
        {
            KryptonListItem item = (KryptonListItem)this.listCurrentActions.SelectedItem;
            if (item.Tag != null)
            {
                Command cmd = (Command)item.Tag;
                string newImage = ImagesDialog.Show();
                if (!string.IsNullOrEmpty(newImage))
                {
                    item.Image = CommandImageCollection.GetImage(newImage);
                    if (this.changedImages.ContainsKey(cmd))
                    {
                        this.changedImages[cmd] = newImage;
                    }
                    else
                    {
                        this.changedImages.Add(cmd, newImage);
                    }
                }
                
                this.Changed = true;
            }
        }
        
        private void ButtonArrows1ButtonArrowClick(object sender, ButtonArrowClickEventArgs e)
        {
            switch (e.Direction)
            {
                case ButtonDirection.Up:
                    this.MoveItemUp();
                    break;
                case ButtonDirection.Right:
                    this.MoveToCurrentActions();
                    break;
                case ButtonDirection.Down:
                    this.MoveItemDown();
                    break;
                case ButtonDirection.Left:
                    this.DeleteCurrentAction();
                    break;
                default:
                    throw new Exception("Invalid value for ButtonDirection");
            }
        }
        
        private void ButtonDefaultClick(object sender, EventArgs e)
        {
            this.changedImages.Clear();
            foreach (ToolbarCommand cmd in this.defToolbar)
            {
                if (cmd.Command.ImageKey != cmd.Command.DefaultImageKey)
                {
                    this.changedImages.Add(cmd.Command, cmd.Command.DefaultImageKey);
                }
            }
            
            this.CreateListCommands(this.defToolbar);
            this.listAvailableActions.SelectedIndex = 0;
            this.Changed = true;
        }
        
        private void ButtonApplyClick(object sender, EventArgs e)
        {
            this.Apply();
            this.Changed = false;
        }
        
        private void ChangeStyle(object sender, EventArgs e)
        {
            if (this.manualChangeStyle)
            {
                return;
            }
            
            if (this.listCurrentActions.SelectedItem != null)
            {
                Command cmd = ((KryptonListItem)this.listCurrentActions.SelectedItem).Tag as Command;
                if (cmd != null)
                {
                    ToolStripItemDisplayStyle style = ToolStripItemDisplayStyle.None;
                    if (this.checkImage.Checked)
                    {
                        style |= ToolStripItemDisplayStyle.Image;
                    }
                    
                    if (this.checkText.Checked)
                    {
                        style |= ToolStripItemDisplayStyle.Text;
                    }
                    
                    if (this.styles.ContainsKey(cmd))
                    {
                        this.styles[cmd] = style;
                    }
                    else
                    {
                        this.styles.Add(cmd, style);
                    }
                    
                    this.Changed = true;
                }
            }
        }
    }
}
