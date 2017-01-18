//-----------------------------------------------------------------------
// <copyright file="TextBoxNumber.cs" company="Sergey Teplyashin">
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
// <date>07.06.2011</date>
// <time>8:55</time>
// <summary>Defines the TextBoxNumber class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class TextBoxNumber : UserControl
    {
    	decimal currentValue;
    	
        public TextBoxNumber()
        {
            InitializeComponent();
            Prefix = string.Empty;
            Suffix = string.Empty;
            currentValue = numericTextBox.Value;
        }
        
        [Category("Action")]
        public event EventHandler<EventArgs> ValueChanged;
        
        [Category("Action")]
        public event EventHandler<ValueEventArgs> ValueChecking;
        
        [Category("Appearance")]
		[Localizable(true)]
        public string Prefix
        {
            get
            {
                return labelPrefix.Text;
            }
            
            set
            {
                labelPrefix.Text = value;
                labelPrefix.Visible = !string.IsNullOrEmpty(value);
            }
        }
        
        [Category("Appearance")]
		[Localizable(true)]
        public string Suffix
        {
            get
            {
                return labelSuffix.Text;
            }
            
            set
            {
                labelSuffix.Text = value;
                labelSuffix.Visible = !string.IsNullOrEmpty(value);
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Value
        {
            get
            {
                return numericTextBox.Value;
            }
            
            set
            {
            	if (IsValidatingValue(value))
            	{
            		numericTextBox.Value = value;
            	}
            }
        }
        
        [Category("Data")]
        [DefaultValue(typeof(decimal), "100")]
        public decimal Maximum
        {
            get { return numericTextBox.Maximum; }
            set { numericTextBox.Maximum = value; }
        }
        
        [Category("Data")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum
        {
            get { return numericTextBox.Minimum; }
            set { numericTextBox.Minimum = value; }
        }
        
        [Category("Data")]
        [DefaultValue(typeof(decimal), "1")]
        public decimal Increment
        {
            get { return numericTextBox.Increment; }
            set { numericTextBox.Increment = value; }
        }
        
        [Category("Data")]
        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get { return numericTextBox.ThousandsSeparator; }
            set { numericTextBox.ThousandsSeparator = value; }
        }
        
        [Category("Data")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return numericTextBox.DecimalPlaces; }
            set { numericTextBox.DecimalPlaces = value; }
        }
        
        void NumericTextBoxResize(object sender, EventArgs e)
        {
			Height = numericTextBox.Height;
        }
        
        bool IsValidatingValue(decimal value)
        {
        	if (ValueChecking != null)
        	{
        		ValueEventArgs e = new ValueEventArgs(value);
        		ValueChecking(this, e);
        		return e.Correct;
        	}
        	
        	return true;
        }
        
		void NumericTextBoxValueChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, new EventArgs());
			}
		}
    }
}
