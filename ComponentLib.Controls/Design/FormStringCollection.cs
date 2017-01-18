//-----------------------------------------------------------------------
// <copyright file="FormStringCollection.cs" company="Sergey Teplyashin">
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
// <date>09.02.2011</date>
// <time>9:46</time>
// <summary>Defines the FormStringCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using ComponentFactory.Krypton.Toolkit;

    public partial class FormStringCollection : KryptonForm
    {
        public FormStringCollection(ICollection collectionData)
        {
            InitializeComponent();
            
            foreach (var s in collectionData)
            {
                listStrings.Items.Add(s);
            }
            
            if (listStrings.Items.Count > 0)
            {
                listStrings.SelectedIndex = 0;
            }
            
            UpdateButtons();
        }
        
        public IEnumerable<string> Collection
        {
            get { return listStrings.Items.OfType<string>(); }
        }
        
        void UpdateButtons()
        {
            buttonEdit.Enabled = listStrings.SelectedIndex != -1;
            buttonDelete.Enabled = buttonEdit.Enabled;
            buttonUp.Enabled = buttonEdit.Enabled && listStrings.SelectedIndex > 0;
            buttonDown.Enabled = buttonEdit.Enabled && listStrings.SelectedIndex < listStrings.Items.Count - 1;
        }
        
        void ButtonAddClick(object sender, EventArgs e)
        {
            string res = KryptonInputBox.Show(Strings.InputValue, Strings.Value, string.Empty);
            if (!string.IsNullOrEmpty(res))
            {
                listStrings.Items.Add(res);
                UpdateButtons();
            }
        }
        
        void ButtonEditClick(object sender, EventArgs e)
        {
            if (listStrings.SelectedIndex != -1)
            {
                string def = (string)listStrings.SelectedItem;
                string res = KryptonInputBox.Show(Strings.InputValue, Strings.Value, def);
                if (def != res)
                {
                    listStrings.Items[listStrings.SelectedIndex] = res;
                }
            }
        }
        
        void ButtonDeleteClick(object sender, EventArgs e)
        {
            if (listStrings.SelectedIndex != -1)
            {
                listStrings.Items.RemoveAt(listStrings.SelectedIndex);
                UpdateButtons();
            }
        }
        
        void ButtonUpClick(object sender, EventArgs e)
        {
            if (listStrings.SelectedIndex > 0)
            {
                string sel = (string)listStrings.SelectedItem;
                int newPos = listStrings.SelectedIndex - 1;
                listStrings.Items.Remove(sel);
                listStrings.Items.Insert(newPos, sel);
                listStrings.SelectedItem = sel;
            }
        }
        
        void ButtonDownClick(object sender, EventArgs e)
        {
            if (listStrings.SelectedIndex != -1 && listStrings.SelectedIndex < listStrings.Items.Count - 1)
            {
                string sel = (string)listStrings.SelectedItem;
                int newPos = listStrings.SelectedIndex + 1;
                listStrings.Items.Remove(sel);
                listStrings.Items.Insert(newPos, sel);
                listStrings.SelectedItem = sel;
            }
        }
        
        void ListStringsSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }
    }
}
