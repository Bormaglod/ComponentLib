//-----------------------------------------------------------------------
// <copyright file="RadioBox.cs" company="Sergey Teplyashin">
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
// <date>01.06.2012</date>
// <time>12:44</time>
// <summary>Defines the RadioBox class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Controls.Design;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(RadioBox))]
    public partial class RadioBox : UserControl
    {
        RadioBoxCollection radioButtons;
        int columnsCount;
        int selected;
        
        public RadioBox()
        {
            InitializeComponent();
            this.radioButtons = new RadioBoxCollection(this);
            this.columnsCount = 1;
            this.selected = -1;
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(RadioBoxCollectionEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(StringCollectionConverter))]
        [Category("Appearance")]
        public RadioBoxCollection RadioButtons
        {
            get
            {
                return radioButtons;
            }
            
            set
            {
                radioButtons = value;
                UpdateButtons();
            }
        }
        
        [Category("Layout")]
        [DefaultValue(1)]
        public int ColumnsCount
        {
            get
            {
                return columnsCount;
            }
            
            set
            {
                columnsCount = value;
                UpdateButtons();
            }
        }
        
        [Category("Appearance")]
        [DefaultValue("Caption")]
        [Localizable(true)]
        public string CaptionHeading
        {
            get { return groupBox.Values.Heading; }
            set { groupBox.Values.Heading = value; }
        }
        
        [Category("Appearance")]
        [Localizable(true)]
        public string CaptionDescription
        {
            get { return groupBox.Values.Description; }
            set { groupBox.Values.Description = value; }
        }
        
        [Browsable(false)]
        public override string Text
        {
            get { return CaptionHeading; }
            set { CaptionHeading = value; }
        }
        
        [Category("Appearance")]
        public Color BackColorGroup
        {
            get { return groupBox.StateCommon.Back.Color1; }
            set { groupBox.StateCommon.Back.Color1 = value; }
        }
        
        public int SelectedIndex
        {
            get
            {
                return selected;
            }
            
            set
            {
                selected = value;
                foreach (KryptonRadioButton button in groupBox.Panel.Controls.OfType<KryptonRadioButton>())
                {
                    button.Checked = false;
                }
                
                if (selected >= 0 && selected < groupBox.Panel.Controls.Count)
                {
                    ((KryptonRadioButton)groupBox.Panel.Controls[selected]).Checked = true;
                }
            }
        }
        
        internal void UpdateButtons()
        {
            int selIndex = SelectedIndex;
            groupBox.Panel.Controls.Clear();
            if (Height < 48)
            {
                return;
            }
            
            int x = 6;
            int y = 6;
            int max_w = 0;
            int countLines = radioButtons.Count / (ColumnsCount == 0 ? 1 : ColumnsCount);
            if (countLines * 2 < radioButtons.Count)
            {
                countLines++;
            }
            
            int currentLine = 1;
            foreach (string item in radioButtons)
            {
                KryptonRadioButton radio = new KryptonRadioButton();
                radio.CheckedChanged += (sender, e) => selected = groupBox.Panel.Controls.IndexOf((Control)sender);
                radio.Location = new Point(x, y);
                radio.Text = item;
                groupBox.Panel.Controls.Add(radio);
                if (radio.Width > max_w)
                {
                    max_w = radio.Width;
                }
                
                y += radio.Height + 6;
                currentLine++;
                if (currentLine > countLines)
                {
                    currentLine = 1;
                    y = 6;
                    x += max_w + 6;
                }
            }
            
            if (selIndex != -1)
            {
                SelectedIndex = selIndex;
            }
        }
        
        internal void InsertButton(int index, string text)
        {
            UpdateButtons();
        }
        
        internal void RemoveButton(string text)
        {
            UpdateButtons();
        }
        
        internal void UpdateButton(int index, string oldValue, string newValue)
        {
            UpdateButtons();
        }
    }
}
