//-----------------------------------------------------------------------
// <copyright file="UnicodeView.cs" company="Sergey Teplyashin">
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
// <date>27.10.2011</date>
// <time>7:57</time>
// <summary>Defines the UnicodeView class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Controls.Design;

    public class UnicodeView : ScrollableControl
    {
        static Control viewLayoutContextControl;
        
        readonly UnicodeCollection collection;
        UnicodeType currentType;
        UnicodeCategory currentCategory;
        
        IPalette palette;
        IRenderer renderer;
        
        PaletteMode paletteMode;
        PaletteRedirect paletteRedirect;
        PaletteBackInheritRedirect paletteBackground;
        PaletteBorderInheritRedirect paletteGrid;
        PaletteBackInheritRedirect paletteBackButton;
        PaletteBorderInheritRedirect paletteBorderButton;
        
        IDisposable mementoBackground;
        
        Size size;
        SizeF real_size;
        Size dimension;
        
        int selectedPos;
        int trackingPos;
        
        bool transparentBackground;
        
        public UnicodeView()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.Selectable |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            
            collection = new UnicodeCollection();
            currentType = collection[UnicodeIndex.Europe];
            currentCategory = collection.GetCategory(currentType);
            
            size = new Size(18, 18);
            SetSizeGrid();
            trackingPos = -1;
            
            AutoScroll = true;
            HScroll = false;
            Size = new Size(175, 100);
            
            KryptonManager.GlobalPaletteChanged += new EventHandler(KryptonManager_GlobalPaletteChanged);
            RefreshPalette();
        }
        
        static UnicodeView()
        {
            viewLayoutContextControl = new Control();
        }
        
        [Category("Property Changed")]
        public event EventHandler<SelectedChangedEventArgs> SelectedChanged;
        
        [Category("Property Changed")]
        public event EventHandler<SelectedChangedEventArgs> TrackingChanged;
        
        [Browsable(false)]
        public string SelectedString
        {
            get { return char.ConvertFromUtf32(SelectedCode); }
        }
        
        [Browsable(false)]
        public int SelectedCode
        {
            get { return GetCode(selectedPos); }
        }
        
        [Browsable(false)]
        public string TrackingString
        {
            get { return char.ConvertFromUtf32(GetCode(trackingPos)); }
        }
        
        [Browsable(false)]
        public UnicodeCollection Collection
        {
            get { return collection; }
        }
        
        [Category("Unicode")]
        [TypeConverter(typeof(UnicodeCategoryConverter))]
        public UnicodeCategory Category
        {
            get
            {
                return currentCategory;
            }
            
            set
            {
                currentCategory = value;
                currentType = currentCategory == null ? null : currentCategory.UnicodeType;
                selectedPos = 0;
                SetSizeGrid();
                Invalidate();
            }
        }
        
        [Category("Unicode")]
        [TypeConverter(typeof(UnicodeTypeConverter))]
        public UnicodeType UnicodeType
        {
            get
            {
                return currentType;
            }
            
            set
            {
                if (currentType != value)
                {
                    currentType = value;
                    currentCategory = currentType == null ? null : collection.GetCategory(currentType);
                    selectedPos = 0;
                    SetSizeGrid();
                    Invalidate();
                }
            }
        }
        
        [Category("Appearance")]
        public Size GridSize
        {
            get
            {
                return size;
            }
            
            set
            {
                if (size != value)
                {
                    size = value;
                    SetSizeGrid();
                    Invalidate();
                }
            }
        }
        
        [Category("Visuals")]
        [DefaultValue(PaletteBackStyle.InputControlStandalone)]
        public PaletteBackStyle BackgroundBackStyle
        {
            get
            {
                return paletteBackground.Style;
            }
            
            set
            {
                paletteBackground.Style = value;
                Refresh();
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
            if (collection != null && !dimension.IsEmpty)
            {
                Size scrollOffset = new Size(AutoScrollPosition);
                if (e.ClipRectangle.Top + scrollOffset.Width < ClientSize.Width ||
                     e.ClipRectangle.Left + scrollOffset.Height < dimension.Width * size.Width)
                {
                    DrawGrid(e.Graphics, scrollOffset);
                 
                    RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, renderer);
                    renderContext.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    renderContext.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    
                    if (selectedPos == trackingPos)
                    {
                        DrawBox(selectedPos, renderContext, PaletteState.Pressed, scrollOffset);
                    }
                    else
                    {
                        DrawBox(selectedPos, renderContext, PaletteState.Checked, scrollOffset);
                        DrawBox(trackingPos, renderContext, PaletteState.Tracking, scrollOffset);
                    }
                    
                    DrawSymbols(renderContext, scrollOffset);
                }
            }
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Application.RenderWithVisualStyles && transparentBackground)
            {
                GroupBoxRenderer.DrawParentBackground(e.Graphics, e.ClipRectangle, this);
            }
            else
            {
                Color backColor = palette.GetBackColor1(paletteBackground.Style, PaletteState.Normal);
                e.Graphics.FillRectangle(new SolidBrush(backColor), e.ClipRectangle);
            }
        }
        
        protected override void OnResize(EventArgs e)
        {
            SetSizeGrid();
            base.OnResize(e);
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (currentCategory != null)
            {
                int new_focus = GetPositionAt(e.X, e.Y);
                if (new_focus + 1 > currentCategory.CountSymbols)
                {
                    new_focus = -1;
                }
                
                if (new_focus != trackingPos)
                {
                    trackingPos = new_focus;
                    OnSelectedChanged(TrackingString);
                    Invalidate();
                }
            }
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Select();
            int sel = GetPositionAt(e.X, e.Y);
            if (sel < currentCategory.CountSymbols && sel != selectedPos)
            {
                selectedPos = sel;
                OnSelectedChanged(SelectedString);
                Invalidate();
            }
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.trackingPos = -1;
            Invalidate();
        }
        
        protected override bool IsInputKey(Keys keyData)
        {
            if (((keyData & Keys.Up) == Keys.Up)
                || ((keyData & Keys.Down) == Keys.Down)
                || ((keyData & Keys.Left) == Keys.Left)
                || ((keyData & Keys.Right) == Keys.Right))
            {
                return true;
            }

            return base.IsInputKey(keyData);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            int pos = selectedPos;
            switch (e.KeyData)
            {
                case Keys.Left:
                    if (selectedPos > 0)
                    {
                        selectedPos--;
                    }
                    
                    break;
                case Keys.Right:
                    if (selectedPos + 1 < currentCategory.CountSymbols)
                    {
                        selectedPos++;
                    }
                    
                    break;
                case Keys.Up:
                    if (selectedPos - dimension.Width >= 0)
                    {
                        selectedPos -= dimension.Width;
                    }
                    
                    break;
                case Keys.Down:
                    if (selectedPos + dimension.Width < currentCategory.CountSymbols)
                    {
                        selectedPos += dimension.Width;
                    }
                    
                    break;
            }
            
            if (pos != selectedPos)
            {
                UpdateScroll();
                OnSelectedChanged(SelectedString);
            }
            
            Invalidate();
        }
        
        int GetCode(int offset)
        {
            int code = 0;
            if (currentCategory != null && selectedPos != -1)
            {
                code = currentCategory.FirstCode + offset;
            }
            
            return code;
        }
        
        void OnSelectedChanged(string selected)
        {
            if (SelectedChanged != null)
            {
                SelectedChanged(this, new SelectedChangedEventArgs(selected));
            }
        }
        
        void OnTrackingChanged(string tracking)
        {
            if (TrackingChanged != null)
            {
                TrackingChanged(this, new SelectedChangedEventArgs(tracking));
            }
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
            paletteGrid = new PaletteBorderInheritRedirect(paletteRedirect);
            paletteBackButton = new PaletteBackInheritRedirect(paletteRedirect);
            paletteBorderButton = new PaletteBorderInheritRedirect(paletteRedirect);
            
            paletteBackground.Style = PaletteBackStyle.InputControlStandalone;
            paletteGrid.Style = PaletteBorderStyle.GridDataCellList;
            paletteBackButton.Style = PaletteBackStyle.ButtonListItem;
            paletteBorderButton.Style = PaletteBorderStyle.ButtonListItem;
            
            Refresh();
        }
        
        void SetSizeGrid()
        {
            if (currentCategory != null && ClientSize.Width != 0)
            {
                dimension.Width = ClientSize.Width / size.Width;
                dimension.Height = this.currentCategory.CountSymbols / dimension.Width;
                if (dimension.Height * dimension.Width < currentCategory.CountSymbols)
                {
                    dimension.Height++;
                }
                
                real_size = new SizeF((float)ClientSize.Width / (float)dimension.Width, size.Height);
                AutoScrollMinSize = new Size(0, dimension.Height * size.Height + 1);
            }
            else
            {
                dimension = Size.Empty;
                real_size = Size.Empty;
                AutoScrollMinSize = Size.Empty;
            }
        }
        
        Point GetLocation(int position)
        {
            return new Point(position % dimension.Width, position / dimension.Width);
        }
        
        int GetPositionAt(int x, int y)
        {
            if (currentCategory != null)
            {
                Size offset = new Size(AutoScrollPosition);
                int col = (int)(x / real_size.Width);
                int row = (int)((y - offset.Height) / real_size.Height);
                return row * dimension.Width + col;
            }
            
            return 0;
        }
        
        void UpdateScroll()
        {
            Point loc = GetLocation(selectedPos);
            Size offset = new Size(AutoScrollPosition);
            int y = loc.Y * size.Height + offset.Height;
            if (y + size.Height > ClientSize.Height)
            {
                int d = offset.Height - (y + size.Height - ClientSize.Height) - 1;
                AutoScrollPosition = new Point(0, d);
            }
            else if (y < 0)
            {
                //VerticalScroll.Value -= (y + size.Height - ClientSize.Height) + 1;
            }
        }
        
        void DrawGrid(Graphics g, Size offset)
        {
            Pen border = new Pen(paletteGrid.GetBorderColor1(PaletteState.Normal));
            
            // Горизонтальные линии
            float y = 0;
            for (int i = 0; i <= dimension.Height; i++)
            {
                int y1 = (int)(Math.Round(y)) + offset.Height;
                g.DrawLine(border, 0, y1, ClientSize.Width, y1);
                if (i != dimension.Height)
                {
                    y += real_size.Height;
                }
            }
            
            // Вертикальные линии
            float x = 0;
            for (int i = 0; i <= dimension.Width; i++)
            {
                if (i == dimension.Width)
                {
                    x = ClientSize.Width - 1;
                }
                
                int x1 = (int)(Math.Round(x));
                g.DrawLine(border, x1, offset.Height, x1, y + offset.Height);
                x += real_size.Width;
            }
        }
        
        void DrawBox(int index, RenderContext context, PaletteState state, Size offset)
        {
            Point location = GetLocation(index);
            int x = (int)(Math.Round(location.X * real_size.Width + 1));
            int y = (int)(Math.Round(location.Y * real_size.Height + 1)) + offset.Height;
            int w = (int)(Math.Round((location.X + 1) * real_size.Width)) - x;
            if (x + w >= ClientSize.Width)
            {
                w = ClientSize.Width - x - 1;
            }
            
            int h = (int)(Math.Round(real_size.Height - 1));
            Rectangle rect = new Rectangle(x, y, w, h);
            rect.Inflate(-1, -1);
            
            if (state == PaletteState.Checked)
            {
                state = palette.ColorTable.ButtonCheckedGradientBegin.IsEmpty ? PaletteState.Pressed : PaletteState.CheckedNormal;
            }
            
            VisualOrientation visualOrientation = VisualOrientation.Top;
            GraphicsPath backPath = renderer.RenderStandardBorder.GetBackPath(context, rect, paletteBorderButton, visualOrientation, state);
            mementoBackground = renderer.RenderStandardBack.DrawBack(context, rect, backPath, paletteBackButton, visualOrientation, state, mementoBackground);
            renderer.RenderStandardBorder.DrawBorder(context, rect, paletteBorderButton, visualOrientation, state);
        }
        
        void DrawSymbols(ViewContext context, Size offset)
        {
            SolidBrush brush = new SolidBrush(palette.GetContentShortTextColor1(PaletteContentStyle.ButtonListItem, PaletteState.Normal));
            Font font = palette.GetContentShortTextFont(PaletteContentStyle.ButtonListItem, PaletteState.Normal);
            
            float x = 1;
            float y = offset.Height + 1;
            int k = 0;
            for (int code = currentCategory.FirstCode; code <= currentCategory.LastCode; code++)
            {
                string s = char.ConvertFromUtf32(code);
                if (!char.IsControl(s, 0))
                {
                    RectangleF rect = new RectangleF(x, y, real_size.Width - 2, real_size.Height - 2);
                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    if (k == selectedPos)
                    {
                        if (k == trackingPos)
                        {
                            context.Graphics.DrawString(s, font, brush, rect, format);
                        }
                        else
                        {
                            context.Graphics.DrawString(s, font, brush, rect, format);
                        }
                    }
                    else
                    {
                        if (k == trackingPos)
                        {
                            context.Graphics.DrawString(s, font, brush, rect, format);
                        }
                        else
                        {
                            context.Graphics.DrawString(s, font, brush, rect, format);
                        }
                    }
                }
                
                x += real_size.Width;
                if (x > ClientSize.Width)
                {
                    x = 1;
                    y += real_size.Height;
                }
                
                k++;
            }
        }
    }
}
