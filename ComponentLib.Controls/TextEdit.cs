//-----------------------------------------------------------------------
// <copyright file="TextEdit.cs" company="Sergey Teplyashin">
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
// <date>21.09.2010</date>
// <time>20:50</time>
// <summary>Defines the TextEdit class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Drawing;
    using System.ComponentModel;
    using ComponentFactory.Krypton.Toolkit;

    public class TextEdit : KryptonTextBox
    {
        string emptyString;
        string valueText;
        
        public TextEdit()
        {
            this.emptyString = "Emtpy string";
        }
        
        public event EventHandler<EventArgs> ValueTextChanged;
        
        [Category("Внешний вид")]
        [Localizable(true)]
        public string EmptyText
        {
            get
            {
                return emptyString;
            }
            
            set
            {
                emptyString = value;
                UpdateText();
            }
        }
        
        [Category("Внешний вид")]
        [Localizable(true)]
        public string ValueText
        {
            get
            {
                return valueText;
            }
            
            set
            {
                valueText = value;
                OnValueTextChanged();
                UpdateText();
            }
        }
        
        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            UpdateText();
        }
        
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            UpdateText();
        }
        
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            UpdateText();
        }
        
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (Focused)
            {
                valueText = Text;
                OnValueTextChanged();
            }
        }
        
        void OnValueTextChanged()
        {
            if (ValueTextChanged != null)
            {
                ValueTextChanged(this, new EventArgs());
            }
        }
        
        void UpdateText()
        {
            if (string.IsNullOrEmpty(ValueText) && !Focused)
            {
                Text = EmptyText;
                StateCommon.Content.Color1 = SystemColors.ControlDark;
            }
            else
            {
                Text = ValueText;
                StateCommon.Content.Color1 = Color.Empty;
            }
        }
    }
}
