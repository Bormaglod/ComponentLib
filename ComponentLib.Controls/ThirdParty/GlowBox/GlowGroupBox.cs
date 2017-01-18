//-----------------------------------------------------------------------
// <copyright file="GlowGroupBox.cs" company="SSDiver2112">
//     Copyright (c) 2012 SSDiver2112. All rights reserved.
// </copyright>
// <author>SSDiver2112</author>
// <link>http://www.codeproject.com/Members/SSDiver2112</lonk>
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
// <summary>Defines the GlowGroupBox class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.GlowBox
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Windows.Forms;
    
    /// <summary>
    /// Panel Control to add Glow Effect to all of the Child Controls
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(GlowGroupBox))]
    [System.Diagnostics.DebuggerStepThrough()]
    public partial class GlowGroupBox : Panel
    {
        private bool glowOn;
        private Color glowColor = Color.Maroon;
        
        public GlowGroupBox()
        {
            ControlAdded += this.GlowBox_ControlAdded;
            
            this.InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        
        /// <summary>
        /// Get or Set the color of the Glow.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [Category("GlowBox")]
        [Description("Get or Set the color of the Glow")]
        [DefaultValue(typeof(Color), "Maroon")]
        public Color GlowColor
        {
            get
            {
                return this.glowColor;
            }
            
            set
            {
                this.glowColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Turn the Glow effect on or off.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [Category("GlowBox")]
        [Description("Turn the Glow effect on or off")]
        [DefaultValue(false)]
        public bool GlowOn
        {
            get
            {
                return this.glowOn;
            }
            
            set
            {
                this.glowOn = value;
                Invalidate();
            }
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (DesignMode == true && Controls.Count == 0)
            {
                TextRenderer.DrawText(e.Graphics, "Drop controls\n\ton the GlowGroupBox", new Font("Arial", 8, FontStyle.Bold), new Point(20, 20), Color.DarkBlue);
                TextRenderer.DrawText(e.Graphics, "SSDiver2112", new Font("Arial", 7, FontStyle.Bold), new Point(Width - 75, Height - 17), Color.LightGray);

            }
            else if (this.glowOn)
            {
                foreach (Control control in Controls)
                {
                    if (control.Focused == true)
                    {
                        bool GlowK = true;

                        //Check if the control has the ReadOnly property and if so, its value.
                        PropertyInfo prop = control.GetType().GetProperty("ReadOnly");
                        if (prop != null)
                        {
                            GlowK = (bool)prop.GetValue(control, null);
                        }

                        if (GlowK)
                        {
                            using (GraphicsPath gp = new GraphicsPath())
                            {
                                int Glow = 15;
                                int Feather = 50;
                                
                                //Get a Rectangle a little smaller than the control's
                                //and make a GraphicsPath with it
                                Rectangle rect = new Rectangle(control.Bounds.X, control.Bounds.Y, control.Bounds.Width - 1, control.Bounds.Height - 1);
                                rect.Inflate(-1, -1);
                                gp.AddRectangle(rect);

                                //Draw multiple rectangles with increasing thickness and transparency
                                for (int i = 1; i <= Glow; i += 2)
                                {
                                    int aGlow = Convert.ToInt32(Feather - ((Feather / Glow) * i));
                                    using (Pen pen = new Pen(Color.FromArgb(aGlow, this.glowColor), i))
                                    {
                                        pen.LineJoin = LineJoin.Round;
                                        e.Graphics.DrawPath(pen, gp);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void GlowBox_ControlAdded(object sender, ControlEventArgs e)
        {
            // Add handlers to let the gGlowBox know when the child control gets Focus 
            e.Control.GotFocus += this.ChildGotFocus;
            e.Control.LostFocus += this.ChildLostFocus;
        }

        private void ChildGotFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ChildLostFocus(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
