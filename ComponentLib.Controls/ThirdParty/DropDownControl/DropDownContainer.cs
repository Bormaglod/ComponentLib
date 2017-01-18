///-----------------------------------------------------------------------
/// <copyright file="DropDownContainer.cs" company="Richard Blythe">
///     Copyright (c) 2010 Richard Blythe. All rights reserved.
/// </copyright>
/// <author>Richard Blythe</author>
/// <license>
///     Code Project Open License (CPOL)
///     See <http://www.codeproject.com/info/cpol10.aspx>.
/// </license>
/// <date>20.07.2010</date>
/// <summary>Defines the DropDownContainer class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.DropDownControl
{
    #region Using directives
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    
    #endregion
    
    internal sealed class DropDownContainer : Form, IMessageFilter
    {
        public bool Freeze;
        
        public DropDownContainer(Control dropDownItem)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            dropDownItem.Location = new Point(1, 1);
            this.Controls.Add(dropDownItem);
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            Application.AddMessageFilter(this);
        }
        
        public bool PreFilterMessage(ref Message m)
        {
            if (!this.Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
            {
                OnDropStateChange(DropState.Closing);
                this.Close();
            }

            return false;
        }

        public delegate void DropWindowArgs(DropState state);
        public event DropWindowArgs DropStateChange;
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0,0,this.ClientSize.Width - 1, this.ClientSize.Height - 1));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.RemoveMessageFilter(this);
            this.Controls.RemoveAt(0); //prevent the control from being disposed
            base.OnClosing(e);
        }
        
        void OnDropStateChange(DropState state)
        {
            if (DropStateChange != null)
            {
                DropStateChange(state);
            }
        }
    }
}
