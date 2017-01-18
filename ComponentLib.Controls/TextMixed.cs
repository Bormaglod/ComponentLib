//-----------------------------------------------------------------------
// <copyright file="TextMixed.cs" company="Sergey Teplyashin">
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
// <date>26.11.2012</date>
// <time>9:03</time>
// <summary>Defines the TextMixed class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;
    
    public class TextMixed : Control
    {
        static Control viewLayoutContextControl;
        
        IPalette palette;
        IRenderer renderer;
        
        bool transparentBackground;
        
        PaletteMode paletteMode;
        PaletteRedirect paletteRedirect;
        PaletteBackInheritRedirect paletteBackground;
        PaletteContentInheritRedirect labelStyle;
        
        string mixedText;
        string inputText;
        Random random;
        Size textSize;
        bool autoWidth;
        PointF[] locations;
        Color selectedColor;
        Color correctColor;
        Color incorrectColor;
        PointF locationMixed;
        
        public TextMixed()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.UserPaint, true);
            mixedText = string.Empty;
            inputText = string.Empty;
            random = new Random(DateTime.Now.Millisecond);
            textSize = new Size(100, 50);
            Size = new Size(100, 50);
            selectedColor = SystemColors.GrayText;
            correctColor = Color.Green;
            incorrectColor = Color.Red;
            
            KryptonManager.GlobalPaletteChanged += new EventHandler(this.KryptonManager_GlobalPaletteChanged);
            RefreshPalette();
        }
        
        static TextMixed()
        {
            viewLayoutContextControl = new Control();
        }
        
        public bool AutoWidth
        {
            get
            {
                return autoWidth;
            }
            
            set
            {
                autoWidth = value;
                CalculatePositions();
                Invalidate();
            }
        }
        
        public Color CorrectColor
        {
            get
            {
                return correctColor;
            }
            
            set
            {
                correctColor = value;
                Invalidate();
            }
        }
        
        public Color IncorrectColor
        {
            get
            {
                return incorrectColor;
            }
            
            set
            {
                incorrectColor = value;
                Invalidate();
            }
        }
        
        public string InputText
        {
            get
            {
                return inputText;
            }
            
            set
            {
                inputText = value;
                Invalidate();
            }
        }
        
        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            
            set
            {
                selectedColor = value;
                Invalidate();
            }
        }

        public Size TextSize
        {
            get
            {
                return textSize;
            }
            
            set
            {
                if (textSize.Width == value.Width && textSize.Height == value.Height)
                {
                    return;
                }
                
                textSize = value;
                CalculateLocationMixed();
                if (!autoWidth)
                {
                    RecalcLettersXPosition();
                }
                
                Invalidate();
            }
        }
        
        public override string Text
        {
            get
            {
                return base.Text;
            }
            
            set
            {
                base.Text = value;
                GenerateMixedText();
                CalculatePositions();
                Invalidate();
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
        [DefaultValue(PaletteContentStyle.LabelNormalControl)]
        public PaletteContentStyle LabelStyle
        {
            get
            {
                return labelStyle.Style;
            }
            
            set
            {
                labelStyle.Style = value;
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
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            using (Graphics g = CreateGraphics())
            {
                CalculateLocationMixed(g);
            }
            
            Invalidate();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mixedText.Length == 0)
            {
                return;
            }
            
            if (locations == null)
            {
                CalculatePositions();
            }
            
            string sel = inputText;
            for (int i = 0; i < locations.Length; i++)
            {
                string s = mixedText.Substring(i, 1);
                float x = locationMixed.X + locations[i].X;
                float y = locationMixed.Y + locations[i].Y;

                RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, this.renderer);
                renderContext.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                renderContext.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                
                Brush b;
                if (inputText.Length >= Text.Length)
                {
					b = string.Compare(inputText, Text, StringComparison.Ordinal) == 0 ? new SolidBrush(correctColor) : new SolidBrush(incorrectColor);
                }
                else
                {
                    int n = sel.IndexOf(s, StringComparison.Ordinal);
                    if (n != -1)
                    {
                        b = new SolidBrush(selectedColor);
                        sel = sel.Remove(n, 1);
                    }
                    else
                    {
                        b = new SolidBrush(palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal));
                    }
                }
                
                Font font = palette.GetContentShortTextFont(labelStyle.Style, PaletteState.Normal);
                renderContext.Graphics.DrawString(s, font, b, x, y);
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
                pevent.Graphics.FillRectangle(new SolidBrush(backColor), pevent.ClipRectangle);
            }
        }
        
        void RefreshPalette()
        {
			palette = paletteMode == PaletteMode.Global ? KryptonManager.CurrentGlobalPalette : KryptonManager.GetPaletteForMode(paletteMode);
            
            renderer = palette.GetRenderer();
            
            paletteRedirect = new PaletteRedirect(palette);
            paletteBackground = new PaletteBackInheritRedirect(paletteRedirect);
            labelStyle = new PaletteContentInheritRedirect(paletteRedirect);
            
            paletteBackground.Style = PaletteBackStyle.InputControlStandalone;
            labelStyle.Style = PaletteContentStyle.LabelNormalControl;
            
            Refresh();
        }
        
        void CalculateLocationMixed()
        {
            using (Graphics g = CreateGraphics())
            {
                CalculateLocationMixed(g);
            }
        }
        
        void CalculateLocationMixed(Graphics g)
        {
            if (mixedText.Length == 0)
            {
                locationMixed = PointF.Empty;
            }
            else
            {
                float x = (Width - GetRealWidth(g)) / 2;
                float y = (Height - textSize.Height) / 2;
                locationMixed = new PointF(x, y);
            }
        }
        
        float GetRealWidth(Graphics g)
        {
            return autoWidth ? GetMaxCharWidth(g) * mixedText.Length : textSize.Width;
        }
        
        void CalculatePositions()
        {
            using (Graphics g = CreateGraphics())
            {
                CalculateLocationMixed(g);
                CalculateLetterPosition(g);
            }
        }
        
        void CalculateLetterPosition()
        {
            using (Graphics g = CreateGraphics())
            {
                CalculateLetterPosition(g);
            }
        }
        
        void RecalcLettersXPosition()
        {
            using (Graphics g = CreateGraphics())
            {
                float dx = GetRealWidth(g) / mixedText.Length;
                float x = 0;
                for (int i = 0; i < mixedText.Length; i++)
                {
                    locations[i] = new PointF(x, locations[i].Y);
                    x += dx;
                }
            }
        }
        
        void CalculateLetterPosition(Graphics g)
        {
            locations = new PointF[mixedText.Length];
            float dx = GetRealWidth(g) / mixedText.Length;
            float x = 0;
            for (int i = 0; i < mixedText.Length; i++)
            {
                string s = mixedText.Substring(i, 1);
                SizeF size = g.MeasureString(s, Font);
                float y;
                if (textSize.Height > size.Height)
                {
                    y = random.Next(Convert.ToInt32((textSize.Height - size.Height) * 100)) / 100;
                }
                else
                {
                    y = 0;
                }

                locations[i] = new PointF(x, y);
                x += dx;
            }
        }
        
        float GetMaxCharWidth(Graphics g)
        {
            float max = 0;
            for (int i = 0; i < mixedText.Length; i++)
            {
                string s = mixedText.Substring(i, 1);
                SizeF size = g.MeasureString(s, Font);
                if (size.Width > max)
                {
                    max = size.Width;
                }
            }
            
            return max;
        }
        
        void GenerateMixedText()
        {
            mixedText = string.Empty;
            string s = Text;
            while (s.Length > 0)
            {
                int n = random.Next(s.Length);
                mixedText += s[n];
                s = s.Remove(n, 1);
            }
            
            mixedText += s;
        }
        
        void KryptonManager_GlobalPaletteChanged(object sender, EventArgs e)
        {
            RefreshPalette();
        }
    }
}
