//-----------------------------------------------------------------------
// <copyright file="ButtonArrows.cs" company="Sergey Teplyashin">
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
// <date>21.11.2011</date>
// <time>9:23</time>
// <summary>Defines the ButtonArrows class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;

    public class ButtonArrows : Control
    {
        class ButtonLocation
        {
            public ButtonLocation(Point location, Size size)
            {
                this.Location = location;
                this.Size = size;
                this.Enabled = true;
            }
            
            public Point Location { get; set; }
            
            public Size Size { get; set; }
            
            public bool Enabled { get; set; }
            
            public Rectangle Rect
            {
                get { return Rectangle.Inflate(new Rectangle(Location, Size), -1, -1); }
            }
            
            public bool Contains(Point p)
            {
                return Rect.Contains(p);
            }
        }
        
        static Control viewLayoutContextControl;
        
        Image imageButton;
        Dictionary<ButtonDirection, ButtonLocation> buttons;
        Dictionary<ButtonDirection, Image> images;
        ButtonDirection trackingButton;
        ButtonDirection pressedButton;
        
        IPalette palette;
        IRenderer renderer;

        PaletteMode paletteMode;
        PaletteRedirect paletteRedirect;
        PaletteBackInheritRedirect paletteBackground;
        PaletteBackInheritRedirect paletteBackButton;
        PaletteBorderInheritRedirect paletteBorderButton;
        
        IDisposable mementoBackground;
        bool transparentBackground;
        
        public ButtonArrows()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.UserPaint, true);
            UpdateStyles();
            
            Size = new Size(80, 80);
            this.buttons = new Dictionary<ButtonDirection, ButtonArrows.ButtonLocation>();
            this.images = new Dictionary<ButtonDirection, Image>();
            this.CreateButtons();
            this.CreateImages(null);
            
            KryptonManager.GlobalPaletteChanged += new EventHandler(this.KryptonManager_GlobalPaletteChanged);
            this.RefreshPalette();
        }
        
        static ButtonArrows()
        {
            viewLayoutContextControl = new Control();
        }
        
        [Category("Action")]
        public event EventHandler<ButtonArrowClickEventArgs> ButtonArrowClick;
        
        [Category("Appearance")]
        public Image ImageButton
        {
            get
            {
                return imageButton;
            }
            
            set
            {
                imageButton = value;
                CreateImages(imageButton);
                Invalidate();
            }
        }
        
        [Category("Visuals")]
        [DefaultValue(PaletteMode.Global)]
        public PaletteMode PaletteMode
        {
            get
            {
                return paletteMode;
            }
            
            set
            {
                paletteMode = value;
                RefreshPalette();
            }
        }
        
        [Category("Visuals")]
        [DefaultValue(false)]
        public bool TransparentBackground
        {
            get
            {
                return transparentBackground;
            }
            
            set
            {
                transparentBackground = value;
                RefreshPalette();
            }
        }
        
        public bool GetEnabledButton(ButtonDirection direction)
        {
            if (direction != ButtonDirection.None)
            {
                return buttons[direction].Enabled;
            }
            
            throw new InvalidEnumArgumentException("Invalid button direction argument");
        }
        
        public void SetEnabledButton(ButtonDirection direction, bool value)
        {
            if (direction != ButtonDirection.None)
            {
                ButtonLocation loc = buttons[direction];
                loc.Enabled = value;
                Invalidate();
            }
            else
            {
                throw new InvalidEnumArgumentException("Invalid button direction argument");
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                KryptonManager.GlobalPaletteChanged -= new EventHandler(KryptonManager_GlobalPaletteChanged);
            }
            
            base.Dispose(disposing);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (ButtonDirection b in buttons.Keys)
            {
                ButtonLocation loc = buttons[b];
                
                RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, renderer);
                renderContext.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                renderContext.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                
                VisualOrientation visualOrientation = VisualOrientation.Top;
                
                PaletteState state;
                
                if (loc.Enabled)
                {
                    if (pressedButton == ButtonDirection.None)
                    {
                        state = trackingButton == b ? PaletteState.Tracking : PaletteState.Normal;
                    }
                    else
                    {
                        if (pressedButton == b)
                        {
                            state = pressedButton == trackingButton ? PaletteState.Pressed : PaletteState.Tracking;
                        }
                        else
                        {
                            state = PaletteState.Normal;
                        }
                    }
                }
                else
                {
                    state = PaletteState.Disabled;
                }

                GraphicsPath backPath = renderer.RenderStandardBorder.GetBackPath(renderContext, loc.Rect, paletteBorderButton, visualOrientation, state);
                mementoBackground = renderer.RenderStandardBack.DrawBack(renderContext, loc.Rect, backPath, paletteBackButton, visualOrientation, state, mementoBackground);
                renderer.RenderStandardBorder.DrawBorder(renderContext, loc.Rect, paletteBorderButton, visualOrientation, state);
                
                if (images[b] != null)
                {
                    int x = (loc.Rect.Width - images[b].Width) / 2;
                    int y = (loc.Rect.Height - images[b].Height) / 2;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    
                    if (y < 0)
                    {
                        y = 0;
                    }
                    
                    renderContext.Graphics.DrawImage(images[b], loc.Rect.X + x, loc.Rect.Y + y);
                }
            }
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (Application.RenderWithVisualStyles && transparentBackground)
            {
                GroupBoxRenderer.DrawParentBackground(pevent.Graphics, pevent.ClipRectangle, this);
            }
            else
            {
                Color backColor = palette.GetBackColor1(paletteBackground.Style, PaletteState.Normal);
                SolidBrush backBrush = new SolidBrush(backColor);
                pevent.Graphics.FillRectangle(backBrush, pevent.ClipRectangle);
            }
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CreateButtons();
            Invalidate();
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            trackingButton = GetButton(e.Location);
            Invalidate();
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            pressedButton = GetButton(e.Location);
            Invalidate();
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (pressedButton != ButtonDirection.None && pressedButton == trackingButton && buttons[pressedButton].Enabled)
            {
                OnButtonArrowClick(pressedButton);
            }
            
            pressedButton = ButtonDirection.None;
            Invalidate();
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (pressedButton == ButtonDirection.None)
            {
                trackingButton = ButtonDirection.None;
                Invalidate();
            }
        }
        
        ButtonDirection GetButton(Point location)
        {
            foreach (ButtonDirection b in buttons.Keys)
            {
                ButtonLocation loc = buttons[b];
                if (loc.Contains(location))
                {
                    return b;
                }
            }
            
            return ButtonDirection.None;
        }
        
        void KryptonManager_GlobalPaletteChanged(object sender, EventArgs e)
        {
            RefreshPalette();
        }
        
        void RefreshPalette()
        {
            palette = paletteMode == PaletteMode.Global ? KryptonManager.CurrentGlobalPalette : KryptonManager.GetPaletteForMode(paletteMode);
            
            renderer = palette.GetRenderer();
            
            paletteRedirect = new PaletteRedirect(palette);
            paletteBackground = new PaletteBackInheritRedirect(paletteRedirect);
            paletteBackButton = new PaletteBackInheritRedirect(paletteRedirect);
            paletteBorderButton = new PaletteBorderInheritRedirect(paletteRedirect);
            
            paletteBackground.Style = PaletteBackStyle.PanelClient;
            paletteBackButton.Style = PaletteBackStyle.ButtonStandalone;
            paletteBorderButton.Style = PaletteBorderStyle.ButtonStandalone;
            
            Refresh();
        }
        
        void CreateButtons()
        {
            if (buttons != null)
            {
                buttons.Clear();
                int w = Size.Width / 3;
                int h = Size.Height / 3;
                
                int posx = (Size.Width - w) / 2;
                int posy = (Size.Height - h) / 2;
                buttons.Add(ButtonDirection.Up, new ButtonLocation(new Point(posx, 0), new Size(w, posy)));
                buttons.Add(ButtonDirection.Left, new ButtonLocation(new Point(0, posy), new Size(posx, h)));
                buttons.Add(ButtonDirection.Down, new ButtonLocation(new Point(posx, posy + h), new Size(w, Size.Height - posy - h)));
                buttons.Add(ButtonDirection.Right, new ButtonLocation(new Point(posx + w, posy), new Size(Size.Width - posx - w, h)));
            }
        }
        
        void CreateImages(Image image)
        {
            images.Clear();
            if (image != null)
            {
                images.Add(ButtonDirection.Up, image);
                
                Image img = (Image)image.Clone();
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                images.Add(ButtonDirection.Right, img);
                
                img = (Image)image.Clone();
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                images.Add(ButtonDirection.Down, img);
                
                img = (Image)image.Clone();
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                images.Add(ButtonDirection.Left, img);
            }
            else
            {
                foreach (ButtonDirection b in Enum.GetValues(typeof(ButtonDirection)))
                {
                    if (b != ButtonDirection.None)
                    {
                        images.Add(b, null);
                    }
                }
            }
        }
        
        void OnButtonArrowClick(ButtonDirection direction)
        {
            if (ButtonArrowClick != null)
            {
                ButtonArrowClick(this, new ButtonArrowClickEventArgs(direction));
            }
        }
    }
}
