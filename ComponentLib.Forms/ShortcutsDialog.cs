//-----------------------------------------------------------------------
// <copyright file="ShortcutsDialog.cs" company="Sergey Teplyashin">
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
// <date>28.10.2011</date>
// <time>14:09</time>
// <summary>Defines the ShortcutsDialog class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using Core;

    /// <summary>
    /// Description of ShortcutsDialog.
    /// </summary>
    public partial class ShortcutsDialog : KryptonForm
    {
        private Dictionary<Command, ToolStripMenuItem> shortcuts;
        private Dictionary<Command, Keys> newShortcuts;
        private string filter;
        
        public ShortcutsDialog()
        {
            InitializeComponent();
            newShortcuts = new Dictionary<Command, Keys>();
            filter = string.Empty;
        }
        
        public static bool Show(Dictionary<Command, ToolStripMenuItem> shortcuts)
        {
            return (new ShortcutsDialog()).ShowDialog(shortcuts);
        }
        
        public bool ShowDialog(Dictionary<Command, ToolStripMenuItem> shortcuts)
        {
            this.shortcuts = shortcuts;
            Fill(false);
            if (ShowDialog() == DialogResult.OK)
            {
                foreach (Command cmd in this.shortcuts.Keys)
                {
                    if (newShortcuts.ContainsKey(cmd))
                    {
                        cmd.ShortcutKey = newShortcuts[cmd];
                    }
                }
                
                return true;
            }

            return false;
        }
        
        public Command Current
        {
            get
            {
                if (gridShortcuts.CurrentRow != null)
                {
                    Command cmd = (Command)gridShortcuts.CurrentRow.Cells[0].Value;
                    return cmd;
                }
                
                return null;
            }
        }
        
        private void Fill(bool showChanged)
        {
            gridShortcuts.Rows.Clear();
            foreach (Command cmd in shortcuts.Keys)
            {
                if (cmd.CommandValue.ToUpperInvariant().Contains(filter.ToUpperInvariant()))
                {
                    int r = gridShortcuts.Rows.Add();
                    gridShortcuts.Rows[r].Cells[0].Value = cmd;
                    gridShortcuts.Rows[r].Cells[1].Value = shortcuts[cmd].Text;
                    gridShortcuts.Rows[r].Cells[2].Value = KeysToString(newShortcuts.ContainsKey(cmd) ? newShortcuts[cmd] : cmd.ShortcutKey);
                    if (showChanged)
                    {
                        SetFontRow(gridShortcuts.Rows[r]);
                    }
                }
            }
            
            this.UpdateKeys(Current);
        }
        
        private void AppendString(StringBuilder builder, string appendedString)
        {
            if (builder.Length != 0)
            {
                builder.Append(" + ");
            }
            
            builder.Append(appendedString);
        }
        
        private string KeysToString(Keys key)
        {
            StringBuilder s = new StringBuilder(40);
            if ((key & Keys.Control) != 0)
            {
                AppendString(s, "Control");
                key = key & (~Keys.Control);
            }
            
            if ((key & Keys.Alt) != 0)
            {
                AppendString(s, "Alt");
                key = key & (~Keys.Alt);
            }
            
            if ((key & Keys.Shift) != 0)
            {
                AppendString(s, "Shift");
                key = key & (~Keys.Shift);
            }
            
            if (key != Keys.None)
            {
                AppendString(s, key.ToString());
                return s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        
        private void UpdateKeys(Command command)
        {
            if (command == null)
            {
                return;
            }
            
            if (newShortcuts.ContainsKey(command))
            {
                textShortcut.Text = KeysToString(newShortcuts[command]);
            }
            else
            {
                textShortcut.Text = KeysToString(command.ShortcutKey);
            }
            
            labelDefault.Text = KeysToString(command.DefaultShortcutKey);
        }
        
        private bool NewShortcut(Command command, Keys key)
        {
            if (!newShortcuts.ContainsKey(command) && command.ShortcutKey == key)
            {
                return false;
            }
            
            if (key != Keys.None)
            {
                foreach (Command cmd in shortcuts.Keys)
                {
                    Keys k = newShortcuts.ContainsKey(cmd) ? newShortcuts[cmd] : cmd.ShortcutKey;
                    if (k == key)
                    {
                        //KryptonMessageBox.Show(string.Format("The {0} key combination has already been allocated to the {1} action. Do you want to reassign it from that action to the current one?", ), "Key Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        KryptonMessageBox.Show(string.Format(Strings.KeyInUse, KeysToString(key), cmd.CommandValue), Strings.KeyConflict, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            
            if (newShortcuts.ContainsKey(command))
            {
                newShortcuts[command] = key;
            }
            else
            {
                newShortcuts.Add(command, key);
            }
            
            return true;
        }
        
        private void SetFontRow(DataGridViewRow row)
        {
            Font font = Font;
            Command cmd = (Command)row.Cells[0].Value;
            if (newShortcuts.ContainsKey(cmd))
            {
                if (cmd.ShortcutKey != newShortcuts[cmd])
                {
                    font = new Font(Font, FontStyle.Bold);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                row.Cells[i].Style.Font = font;
            }
        }
        
        private void SetFontCurrentRow()
        {
            SetFontRow(gridShortcuts.CurrentRow);
        }
        
        private void KryptonTextBox1PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            bool controlDown = e.Control && e.KeyCode == Keys.ControlKey;
            bool shiftDown = e.Shift && e.KeyCode == Keys.ShiftKey;
            bool altDown = e.Alt && e.KeyCode == Keys.Menu;
            if (!(controlDown || shiftDown || altDown))
            {
                if (NewShortcut(Current, e.KeyData))
                {
                    StringBuilder s = new StringBuilder(40);
                    
                    if (e.Control)
                    {
                        AppendString(s, "Control");
                    }
                    
                    if (e.Alt)
                    {
                        AppendString(s, "Alt");
                    }
                    
                    if (e.Shift)
                    {
                        AppendString(s, "Shift");
                    }
                    
                    AppendString(s, e.KeyCode.ToString());
                    textShortcut.Text = s.ToString();
                    
                    gridShortcuts.CurrentRow.Cells[2].Value = textShortcut.Text;
                    SetFontCurrentRow();
                }
            }
        }
        
        private void GridShortcutsSelectionChanged(object sender, EventArgs e)
        {
            UpdateKeys(Current);
        }
        
        private void ButtonShortcutDefaultClick(object sender, EventArgs e)
        {
            if (NewShortcut(Current, Current.DefaultShortcutKey))
            {
                textShortcut.Text = KeysToString(Current.DefaultShortcutKey);
                gridShortcuts.CurrentRow.Cells[2].Value = textShortcut.Text;
                SetFontCurrentRow();
            }
        }
        
        private void ButtonSpecAny2Click(object sender, EventArgs e)
        {
            NewShortcut(Current, Keys.None);
            textShortcut.Text = string.Empty;
            gridShortcuts.CurrentRow.Cells[2].Value = string.Empty;
            SetFontCurrentRow();
        }
        
        private void TextFilterTextChanged(object sender, EventArgs e)
        {
        	filter = textFilter.Text;
        	Fill(true);
        }
        
        private void ButtonSpecAny1Click(object sender, EventArgs e)
        {
        	textFilter.Text = string.Empty;
        }
        
        private void ButtonDefaultClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.gridShortcuts.Rows)
            {
                Command cmd = (Command)row.Cells[0].Value;
                if (NewShortcut(cmd, cmd.DefaultShortcutKey))
                {
                    string key = KeysToString(cmd.DefaultShortcutKey);
                    row.Cells[2].Value = key;
                    SetFontRow(row);
                    if (row == gridShortcuts.CurrentRow)
                    {
                        textShortcut.Text = key;
                    }
                }
            }
        }
    }
}
