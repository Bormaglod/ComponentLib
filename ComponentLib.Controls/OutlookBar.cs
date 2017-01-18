//-----------------------------------------------------------------------
// <copyright file="OutlookBar.cs" company="Sergey Teplyashin">
//     Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
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
// <date>26.10.2011</date>
// <time>8:37</time>
// <summary>Defines the OutlookBar class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class OutlookBar : UserControl
    {
        /// <summary>
        /// this.listButtons contains the list of clickable OutlookBarButtons
        /// </summary>
        OutlookBarButtons listButtons;

        /// <summary>
        /// this variable remembers which button is currently selected
        /// </summary>
        OutlookBarButton selectedButtonBar;

        /// <summary>
        /// this variable remembers the button index over which the mouse is moving
        /// </summary>
        int hoveringButtonIndex = -1;

        /// <summary>
        /// property to set the buttonHeigt
        /// default is 30
        /// </summary>
        int buttonBarHeight;
        
        Color gradientButtonDark = Color.FromArgb(178, 193, 140);
        Color gradientButtonLight = Color.FromArgb(234, 240, 207);
        Color gradientButtonHvrDark = Color.FromArgb(247, 192, 91);
        Color gradientButtonHvrLight = Color.FromArgb(255, 255, 220);
        Color gradientButtonSlctdDark = Color.FromArgb(239, 150, 21);
        Color gradientButtonSlctdLight = Color.FromArgb(251, 230, 148);
        
        public OutlookBar()
        {
            InitializeComponent();
            this.listButtons = new OutlookBarButtons(this);
            this.buttonBarHeight = 30; // set default to 30
        }
        
        [Description("Specifies the heightButton of each button on the OutlookBar"), Category("Layout")]
        public int ButtonHeight
        {
            get { return buttonBarHeight; }
            set { buttonBarHeight = value > 18 ? value : 18; }
        }
        
        
        [Description("Dark gradient color of the button"), Category("Appearance")]
        public Color GradientButtonNormalDark
        {
            get { return gradientButtonDark; }
            set { gradientButtonDark = value; }
        }
        
        [Description("Light gradient color of the button"), Category("Appearance")]
        public Color GradientButtonNormalLight
        {
            get { return gradientButtonLight; }
            set { gradientButtonLight = value; }
        }

        [Description("Dark gradient color of the button when the mouse is moving over it"), Category("Appearance")]
        public Color GradientButtonHoverDark
        {
            get { return gradientButtonHvrDark; }
            set { gradientButtonHvrDark = value; }
        }

        [Description("Light gradient color of the button when the mouse is moving over it"), Category("Appearance")]
        public Color GradientButtonHoverLight
        {
            get { return gradientButtonHvrLight; }
            set { gradientButtonHvrLight = value; }
        }

        [Description("Dark gradient color of the seleced button"), Category("Appearance")]
        public Color GradientButtonSelectedDark
        {
            get { return gradientButtonSlctdDark; }
            set { gradientButtonSlctdDark = value; }
        }

        [Description("Light gradient color of the seleced button"), Category("Appearance")]
        public Color GradientButtonSelectedLight
        {
            get { return gradientButtonSlctdLight; }
            set { gradientButtonSlctdLight = value; }
        }

        /// <summary>
        /// when a button is selected programatically, it must update the control
        /// and repaint the this.listButtons
        /// </summary>
        [Browsable(false)]
        public OutlookBarButton SelectedButton
        {
            get { return selectedButtonBar; }
            
            set
            {
                // assign new selected button
                PaintSelectedButton(selectedButtonBar, value);

                // assign new selected button
                selectedButtonBar = value;
                
                if (SelectButton != null)
                {
                    SelectButton(this, new ButtonSelectEventArgs(selectedButtonBar));
                }
            }
        }

        /// <summary>
        /// readonly list of this.listButtons
        /// </summary>
        //[Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OutlookBarButtons Buttons
        {
            get { return listButtons; }
        }

        public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);
        public delegate void ButtonSelectEventHandler(object sender, ButtonSelectEventArgs e);

        public new event ButtonClickEventHandler Click;
        public event ButtonSelectEventHandler SelectButton;

        void PaintSelectedButton(OutlookBarButton prevButton, OutlookBarButton newButton)
        {
            if (prevButton == newButton)
            {
                return; // no change so return immediately
            }

            int selIdx = -1;
            int valIdx = -1;
            
            // find the indexes of the previous and new button
            selIdx = listButtons.IndexOf(prevButton);
            valIdx = listButtons.IndexOf(newButton);

            // now reset selected button
            // mouse is leaving control, so unhighlight anythign that is highlighted
            Graphics g = Graphics.FromHwnd(Handle);
            if (selIdx >= 0)
            {
                // un-highlight current hovering button
                listButtons[selIdx].PaintButton(g, 1, selIdx * (buttonBarHeight + 1) + 1, false, false);
            }

            if (valIdx >= 0)
            {
                // highlight newly selected button
                listButtons[valIdx].PaintButton(g, 1, valIdx * (buttonBarHeight + 1) + 1, true, false);
            }
            
            g.Dispose();
        }

        /// <summary>
        /// returns the button given the coordinates relative to the Outlookbar control
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public OutlookBarButton HitTest(int x, int y)
        {
            int index = (y - 1) / (buttonBarHeight + 1);
            if (index >= 0 && index < listButtons.Count)
            {
                return listButtons[index];
            }
            
            return null;
        }

        /// <summary>
        /// this function will setup the control to cope with changes in the buttonlist 
        /// that is, addition and removal of this.listButtons
        /// </summary>
        internal void ButtonlistChanged()
        {
            if (!DesignMode) // only set sizes automatically at runtime
            {
                MaximumSize = new Size(0, listButtons.Count * (buttonBarHeight + 1) + 1);
            }

            Invalidate();
        }

        void OutlookBar_Load(object sender, EventArgs e)
        {
            // initiate the render style flags of the control
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                true
                );
        }

        void OutlookBar_Paint(object sender, PaintEventArgs e)
        {
            int top = 1;
            foreach (OutlookBarButton b in Buttons)
            {
                b.PaintButton(e.Graphics, 1, top, b.Equals(selectedButtonBar), false);
                top += b.Height + 1;
            }
        }

        void OutlookBar_Click(object sender, EventArgs e)
        {
            // case to MouseEventArgs so position and mousebutton clicked can be used
            MouseEventArgs mea = e as MouseEventArgs;
            if (mea == null)
            {
                return;
            }

            // only continue if left mouse button was clicked
            if (mea.Button != MouseButtons.Left)
            {
                return;
            }
            
            int index = (mea.Y - 1) / (buttonBarHeight + 1);

            if (index < 0 || index >= listButtons.Count)
            {
                return;
            }

            OutlookBarButton button = listButtons[index];
            if (button == null || !button.Enabled)
            {
                return;
            }

            // ok, all checks passed so assign the new selected button
            // and raise the event
            SelectedButton = button;

            ButtonClickEventArgs bce = new ButtonClickEventArgs(selectedButtonBar, mea);
            if (Click != null) // only invoke on left mouse click
            {
                Click.Invoke(this, bce);
            }
        }

        void OutlookBar_DoubleClick(object sender, EventArgs e)
        {
            //TODO: only if you intend to support a doubleclick
            // this can be implemented exactly like the click event
        }


        void OutlookBar_MouseLeave(object sender, EventArgs e)
        {
            // mouse is leaving control, so unhighlight anything that is highlighted
            if (hoveringButtonIndex >= 0)
            {
                // so we need to change the hoveringButtonIndex to the new index
                Graphics g = Graphics.FromHwnd(Handle);
                OutlookBarButton b1 = listButtons[hoveringButtonIndex];

                // un-highlight current hovering button
                b1.PaintButton(g, 1, hoveringButtonIndex * (buttonBarHeight + 1) + 1, b1.Equals(selectedButtonBar), false);
                hoveringButtonIndex = -1;
                g.Dispose();
            }
        }

        void OutlookBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                // determine over which button the mouse is moving
                int index = (e.Location.Y - 1) / (buttonBarHeight + 1);
                if (index >= 0 && index < listButtons.Count)
                {
                    if (hoveringButtonIndex == index )
                    {
                        return; // nothing changed so we're done, current button stays highlighted
                    }

                    // so we need to change the hoveringButtonIndex to the new index
                    Graphics g = Graphics.FromHwnd(Handle);

                    if (hoveringButtonIndex >= 0)
                    {
                        OutlookBarButton b1 = listButtons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (buttonBarHeight + 1) + 1, b1.Equals(selectedButtonBar), false);
                    }
                    
                    // highlight new hovering button
                    OutlookBarButton b2 = listButtons[index];
                    b2.PaintButton(g, 1, index * (buttonBarHeight + 1) + 1, b2.Equals(selectedButtonBar), true);
                    hoveringButtonIndex = index; // set to new index
                    g.Dispose();

                }
                else
                {
                    // no hovering button, so un-highlight all.
                    if (hoveringButtonIndex >= 0)
                    {
                        // so we need to change the hoveringButtonIndex to the new index
                        Graphics g = Graphics.FromHwnd(Handle);
                        OutlookBarButton b1 = listButtons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (buttonBarHeight + 1) + 1, b1.Equals(selectedButtonBar), false);
                        hoveringButtonIndex = -1;
                        g.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// isResizing is used as a signal, so this method is not called recusively
        /// this prevents a stack overflow
        /// </summary>
        bool isResizing;
        void OutlookBar_Resize(object sender, EventArgs e)
        {
            // only set sizes automatically at runtime
            if (!DesignMode)
            {
                if (!isResizing)
                {
                    isResizing = true;
                    if ((Height - 1) % (buttonBarHeight + 1) > 0)
                    {
                        Height = ((Height - 1) / (buttonBarHeight + 1)) * (buttonBarHeight + 1) + 1;
                    }
                    
                    Invalidate();
                    isResizing = false;
                }
            }
        }
    }
}
