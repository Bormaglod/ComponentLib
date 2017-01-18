/*
Copyright © 2010 Corner Bowl Software Corporation
All rights reserved.
http://www.CornerBowl.com
Licensed under Microsoft Public License (Ms-PL)
http://www.microsoft.com/opensource/licenses.mspx
*/

namespace ComponentLib.Controls.ThirdParty.CornerBowl
{
    #region Using directives
    
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    
    #endregion
    
    public class CornerBowlPopupForm : Form
    {
        #region Win32 API
        //Obtained from www.PInvoke.net

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_NOACTIVATE = 4;

        #region UpdateLayeredWindow
        
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
           ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, uint crKey,
           [In] ref BLENDFUNCTION pblend, uint dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        private const int AC_SRC_OVER = 0;
        private const int AC_SRC_ALPHA = 1;
        private const int ULW_ALPHA = 2;
        
        #endregion
        
        #region GDI
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);
        
        #endregion
        
        #endregion

        #region Constants
        
        private const int WS_EX_LAYERED = 0x80000;
        private const int LINE_HEIGHT = 2;
        private const int BORDER_THICKNESS = 1;
        private const int TOTAL_BORDER_THICKNESS = BORDER_THICKNESS + BORDER_THICKNESS;
        private const float GOLDEN_RATIO = 1.61803399f;
        
        //private const int Properties_Resources_help_Width = 16;
        
        #endregion

        #region Variables
        
        private CornerBowlPopup popup;
        private Size titleSize;
        private Size messageSize;
        private Size f1HelpSize;
        private Point locPoint;
        
        #endregion
        
        public CornerBowlPopupForm(CornerBowlPopup popup, Point locPoint)
        {
            this.popup = popup;
            this.locPoint = locPoint;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            #region Popup Size
            
            Size = CalculateSize();
            
            #endregion

            #region Popup Location
            
            Point screenPosition = this.locPoint;//this.popup.PointToScreen(new Point(this.popup.RightPopupMargin + this.popup.Width + this.popup.RightPopupMargin, -this.popup.TitlePadding.Top));
            if (screenPosition.X + Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                //if the popup will not display entirely on the screen move it to the left of the control
                //int x = this.popup.PointToScreen(new Point(0, 0)).X - this.popup.LeftPopupMargin - Size.Width;
                int x = Screen.PrimaryScreen.WorkingArea.Width - this.popup.LeftPopupMargin - Size.Width;
                if (x >= 0)//if the left coordinate is left of the screen (negative) display what you can on the right
                {
                    screenPosition.X = x;
                }
            }
            
            Location = screenPosition;
            
            #endregion

            #region Draw Bitmap
            
            using (Bitmap bmp = new Bitmap(Size.Width, Size.Height))
            {
                this.DrawBitmap(bmp);
                this.SelectBitmap(bmp);
            }
            
            #endregion

            ShowWindow(Handle, SW_NOACTIVATE);
        }
        
        #region Properties
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                return cp;
            }
        }
        
        #endregion
        
        #region Private Functions
        
        private Size CalculateSize()
        {
            int maximumPopupWidth = this.popup.UseGoldenRatio ?
                Math.Min(this.popup.MaximumPopupWidth, this.GetGoldenRatioWidth()) :
                this.popup.MaximumPopupWidth;

            int maxTitleWidth = maximumPopupWidth - this.popup.TitlePadding.Left - this.popup.TitlePadding.Right;
            int maxMessageWidth = maximumPopupWidth - this.popup.MessagePadding.Left - this.popup.MessagePadding.Right;
            int maxF1HelpWidth = maximumPopupWidth - this.popup.F1HelpPadding.Left - this.popup.HelpImage.Width - (this.popup.HelpImage.Width / 4) - this.popup.F1HelpPadding.Right;

            Size retVal = new Size();
            using (Graphics g = CreateGraphics())
            {
                this.titleSize = MeasureString(g, this.popup.TitleText, this.popup.TitleFont, maxTitleWidth);
                this.messageSize = MeasureString(g, this.popup.MessageText, this.popup.MessageFont, maxMessageWidth);
                this.f1HelpSize = MeasureString(g, this.popup.F1HelpText, this.popup.F1HelpFont, maxF1HelpWidth);
                int titleWidth = this.popup.TitlePadding.Left + this.titleSize.Width + this.popup.TitlePadding.Right;
                int messageWidth = this.popup.MessagePadding.Left + this.messageSize.Width + this.popup.MessagePadding.Right;
                int f1HelpWidth = this.popup.F1HelpPadding.Left + this.popup.HelpImage.Width + (this.popup.HelpImage.Width / 4) + f1HelpSize.Width + this.popup.F1HelpPadding.Right;
                int width = Math.Max(titleWidth, messageWidth);
                width = Math.Max(width, f1HelpWidth);
                retVal.Width = Math.Min(maximumPopupWidth, width);

                int titleHeight = !string.IsNullOrEmpty(this.popup.TitleText) ?
                    this.popup.TitlePadding.Top + this.titleSize.Height + this.popup.TitlePadding.Bottom + LINE_HEIGHT :
                    0;

                retVal.Height = titleHeight +
                    this.popup.MessagePadding.Top + this.messageSize.Height + this.popup.MessagePadding.Bottom +
                    LINE_HEIGHT +
                    this.popup.F1HelpPadding.Top + this.f1HelpSize.Height + this.popup.F1HelpPadding.Bottom;
            }

            retVal.Width += TOTAL_BORDER_THICKNESS + this.popup.ShadowDepth;
            retVal.Height += TOTAL_BORDER_THICKNESS + this.popup.ShadowDepth;

            return retVal;
        }

        private GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            GraphicsPath gp = new GraphicsPath();

            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            if (width > 0 && height > 0)
            {
                radius = Math.Min(radius, height / 2 - 1);
                radius = Math.Min(radius, width / 2 - 1);

                gp.AddLine(x + radius, y, x + width - (radius * 2), y);
                gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
                gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
                gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
                gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
                gp.AddLine(x, y + height - (radius * 2), x, y + radius);
                gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                gp.CloseFigure();
            }
            return gp;
        }

        private void DrawBackground(Graphics g)
        {
            Rectangle messageRect = new Rectangle(
                0,
                0,
                Width - this.popup.ShadowDepth - BORDER_THICKNESS,
                Height - this.popup.ShadowDepth - BORDER_THICKNESS);
            if (messageRect.Width > 0 && messageRect.Height > 0)
            {
                using (GraphicsPath messagePath = CreateRoundRect(messageRect, 4))
                {
                    using (Brush messageBackgroundBrush = new LinearGradientBrush(
                        messageRect, this.popup.LightBackgroundColor, this.popup.DarkBackgroundColor, LinearGradientMode.Vertical))
                    {
                        g.FillPath(messageBackgroundBrush, messagePath);
                        using (Pen messageBoarderPen = new Pen(this.popup.BorderColor))
                            g.DrawPath(messageBoarderPen, messagePath);
                    }
                }
            }
        }

        private void DrawBitmap(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                DrawShadow(g);
                DrawBackground(g);
                DrawContent(g);
            }
        }

        private void DrawContent(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Pen darkPen = null;
            Pen lightPen = null;
            try
            {
                darkPen = new Pen(this.popup.DarkLineColor);
                lightPen = new Pen(this.popup.LightLineColor);
                int y = 0;
                int x = BORDER_THICKNESS + this.popup.TitlePadding.Left;
                int x2 = Width - this.popup.TitlePadding.Right - this.popup.ShadowDepth - TOTAL_BORDER_THICKNESS;

                #region Title and Line
                
                if (!string.IsNullOrEmpty(this.popup.TitleText))
                {
                    #region Title
                    Rectangle titleRect = new Rectangle(
                        BORDER_THICKNESS + this.popup.TitlePadding.Left,
                        BORDER_THICKNESS + this.popup.TitlePadding.Top,
                        this.titleSize.Width,
                        this.titleSize.Height);
                    using (Brush titleBrush = new SolidBrush(this.popup.TitleForeColor))
                        g.DrawString(this.popup.TitleText, this.popup.TitleFont, titleBrush, titleRect);
                    y = titleRect.Bottom + this.popup.TitlePadding.Bottom;
                    #endregion

                    #region Line
                    
                    g.DrawLine(darkPen, x, y, x2, y);
                    y++;
                    g.DrawLine(lightPen, x, y, x2, y);
                    y++;
                    
                    #endregion
                }
                
                #endregion

                #region Message
                
                Rectangle messageRect = new Rectangle(
                    BORDER_THICKNESS + this.popup.MessagePadding.Left,
                    y + this.popup.MessagePadding.Top,
                    this.messageSize.Width,
                    this.messageSize.Height);
                using (Brush messageBrush = new SolidBrush(this.popup.MessageForeColor))
                {
                    g.DrawString(this.popup.MessageText, this.popup.MessageFont, messageBrush, messageRect);
                }
                
                y = messageRect.Bottom + this.popup.MessagePadding.Bottom;
                
                #endregion

                #region Line
                
                g.DrawLine(darkPen, x, y, x2, y);
                y++;
                g.DrawLine(lightPen, x, y, x2, y);
                y++;
                
                #endregion

                #region Press F1
                
                g.DrawImage(this.popup.HelpImage, new Point(BORDER_THICKNESS + this.popup.F1HelpPadding.Left, y + this.popup.F1HelpPadding.Top));

                Rectangle f1HelpRect = new Rectangle(
                    BORDER_THICKNESS + this.popup.F1HelpPadding.Left + this.popup.HelpImage.Width + (this.popup.HelpImage.Width / 4),
                    y + this.popup.F1HelpPadding.Top,
                    this.f1HelpSize.Width,
                    this.f1HelpSize.Height);
                using (Brush f1HelpBrush = new SolidBrush(this.popup.F1HelpForeColor))
                {
                    g.DrawString(this.popup.F1HelpText, this.popup.F1HelpFont, f1HelpBrush, f1HelpRect);
                }
                
                #endregion
            }
            finally
            {
                if (darkPen != null)
                {
                    darkPen.Dispose();
                }

                if (lightPen != null)
                {
                    lightPen.Dispose();
                }
            }
        }

        private void DrawShadow(Graphics g)
        {
            if (this.popup.ShadowDepth > 0)
            {
                Rectangle shadowRect = new Rectangle(
                    this.popup.ShadowDepth,
                    this.popup.ShadowDepth,
                    Width - this.popup.ShadowDepth,
                    Height - this.popup.ShadowDepth);

                if (shadowRect.Width > 0 && shadowRect.Height > 0)
                {
                    using (GraphicsPath shadowPath = CreateRoundRect(shadowRect, 4))
                    {
                        using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                        {
                            Color[] colors = new Color[4];
                            float[] positions = new float[4];
                            ColorBlend sBlend = new ColorBlend();
                            colors[0] = Color.FromArgb(0, 0, 0, 0);
                            colors[1] = Color.FromArgb(32, 0, 0, 0);
                            colors[2] = Color.FromArgb(64, 0, 0, 0);
                            colors[3] = Color.FromArgb(128, 0, 0, 0);
                            positions[0] = 0.0f;
                            positions[1] = 0.015f;
                            positions[2] = 0.030f;
                            positions[3] = 1.0f;
                            sBlend.Colors = colors;
                            sBlend.Positions = positions;

                            shadowBrush.InterpolationColors = sBlend;
                            shadowBrush.CenterPoint = new Point(
                                shadowRect.Left + (shadowRect.Width / 2),
                                shadowRect.Top + (shadowRect.Height / 2));

                            g.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }
            }
        }

        private int GetGoldenRatioWidth()
        {
            using (Graphics g = CreateGraphics())
            {
                int goldenWidth = 0;
                float volumn = 0;
                for (int i = 0; i < this.popup.GoldenRatioSampleRate; i++)
                {
                    titleSize = MeasureString(g, this.popup.TitleText, this.popup.TitleFont, goldenWidth);
                    messageSize = MeasureString(g, this.popup.MessageText, this.popup.MessageFont, goldenWidth);
                    f1HelpSize = MeasureString(g, this.popup.F1HelpText, this.popup.F1HelpFont, goldenWidth);
                    int titleWidth = this.popup.TitlePadding.Left + titleSize.Width + this.popup.TitlePadding.Right;
                    int messageWidth = this.popup.MessagePadding.Left + messageSize.Width + this.popup.MessagePadding.Right;
                    int f1HelpWidth = this.popup.F1HelpPadding.Left + this.popup.HelpImage.Width + (this.popup.HelpImage.Width / 4) + f1HelpSize.Width + this.popup.F1HelpPadding.Right;
                    int width = Math.Max(titleWidth, messageWidth);
                    width = Math.Max(width, f1HelpWidth);
                    
                    int titleHeight = !string.IsNullOrEmpty(this.popup.TitleText) ?
                        this.popup.TitlePadding.Top + titleSize.Height + this.popup.TitlePadding.Bottom + LINE_HEIGHT :
                        0;

                    int height =
                        titleHeight +
                        this.popup.MessagePadding.Top + messageSize.Height + this.popup.MessagePadding.Bottom + LINE_HEIGHT +
                        this.popup.F1HelpPadding.Top + f1HelpSize.Height + this.popup.F1HelpPadding.Bottom;
                    
                    float sampleVolumn = height * width;
                    if (sampleVolumn == volumn)
                        break;
                    volumn = sampleVolumn;
                    double x = Math.Sqrt(volumn * GOLDEN_RATIO);
                    double y = volumn / x;
                    goldenWidth = 1 + (int)x;
                }
                return goldenWidth;
            }
        }

        private Size MeasureString(Graphics g, string val, Font font)
        {
            SizeF sizeF = g.MeasureString(val, font);
            return new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
        }

        private Size MeasureString(Graphics g, string val, Font font, int maxWidth)
        {
            if (maxWidth <= 0)
                return MeasureString(g, val, font);

            SizeF sizeF = g.MeasureString(val, font, maxWidth);
            return new Size(
                (int)sizeF.Width < maxWidth ? (int)sizeF.Width + 1 : maxWidth,
                (int)sizeF.Height + 1);
        }

        private void SelectBitmap(Bitmap bmp)
        {
            IntPtr hDC = GetDC(IntPtr.Zero);
            try
            {
                IntPtr hMemDC = CreateCompatibleDC(hDC);
                try
                {
                    IntPtr hBmp = bmp.GetHbitmap(Color.FromArgb(0));
                    try
                    {
                        IntPtr previousBmp = SelectObject(hMemDC, hBmp);
                        try
                        {
                            Point ptDst = new Point(Left, Top);
                            Size size = new Size(bmp.Width, bmp.Height);
                            Point ptSrc = new Point(0, 0);

                            BLENDFUNCTION blend = new BLENDFUNCTION();
                            blend.BlendOp = AC_SRC_OVER;
                            blend.BlendFlags = 0;
                            blend.SourceConstantAlpha = 255;
                            blend.AlphaFormat = AC_SRC_ALPHA;

                            UpdateLayeredWindow(
                                Handle,
                                hDC,
                                ref ptDst,
                                ref size,
                                hMemDC,
                                ref ptSrc,
                                0,
                                ref blend,
                                ULW_ALPHA);
                        }
                        finally
                        {
                            SelectObject(hDC, previousBmp);
                        }
                    }
                    finally
                    {
                        DeleteObject(hBmp);
                    }
                }
                finally
                {
                    DeleteDC(hMemDC);
                }
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, hDC);
            }
        }
        #endregion
    }
}
