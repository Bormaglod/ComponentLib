///-----------------------------------------------------------------------
/// <copyright file="DropDownControl.cs" company="Richard Blythe">
///     Copyright (c) 2010 Richard Blythe. All rights reserved.
/// </copyright>
/// <author>Richard Blythe</author>
/// <license>
///     Code Project Open License (CPOL)
///     See <http://www.codeproject.com/info/cpol10.aspx>.
/// </license>
/// <date>20.07.2010</date>
/// <summary>Defines the DropDownControl class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.DropDownControl
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ComponentFactory.Krypton.Toolkit;

    public partial class DropDownControl : UserControl
    {
        private DropDownContainer dropContainer;
        private Control dropDownItem;
        private bool closedWhileInControl;
        private Size storedSize;
        private DropState dropState;
        private string text;
        private Size anchorSize = new Size(121, 21);
        private bool designView = true;
        private Rectangle anchorClientBounds;
        
        private IRenderer renderer;
        private IPalette palette;
        private PaletteMode paletteMode;
        private PaletteRedirect paletteRedirect;
        private PaletteBackInheritRedirect paletteBack;
        private PaletteBorderInheritRedirect paletteBorder;
        private PaletteBackInheritRedirect paletteBackButton;
        private PaletteBorderInheritRedirect paletteBorderButton;
        
        private IDisposable mementoBackground;
        
        public DropDownControl()
        {
            InitializeComponent();
            this.storedSize = this.Size;
            this.BackColor = Color.White;
            this.Text = this.Name;
            
            KryptonManager.GlobalPaletteChanged += new EventHandler(this.KryptonManager_GlobalPaletteChanged);
            this.RefreshPalette();
        }
        
        public event EventHandler PropertyChanged;
        
        new public string Text
        {
            get
            {
                return this.text;
            }
            
            set
            {
               this.text = value;
               this.Invalidate();
            }
        }
        
        public Size AnchorSize
        {
            get
            {
                return this.anchorSize;
            }
            
            set
            { 
                this.anchorSize = value;
                this.Invalidate();
            }
        }
        
        public DockSide DockSide { get; set; }
        
        [Category("Visuals")]
        [DefaultValue(PaletteMode.Global)]
        public PaletteMode PaletteMode
        {
            get
            {
                return this.paletteMode;
            }
            
            set
            {
                this.paletteMode = value;
                this.RefreshPalette();
            }
        }
        
        [DefaultValue(false)]
        protected bool DesignView
        {
            get
            {
                return this.designView;
            }
            
            set
            {
                if (this.designView == value)
                {
                    return;
                }

                this.designView = value;
                if (this.designView)
                {
                    this.Size = this.storedSize;
                }
                else
                {
                    this.storedSize = this.Size;
                    this.Size = this.AnchorSize;
                }
            }
        }
        
        public Rectangle AnchorClientBounds
        {
            get { return this.anchorClientBounds; }
        }
        
        protected DropState DropState
        {
            get { return this.dropState; }
        }
        
        protected virtual bool CanDrop
        {
            get
            {
                if (this.dropContainer != null)
                {
                    return false;
                }

                if (this.dropContainer == null && this.closedWhileInControl)
                {
                    this.closedWhileInControl = false;
                    return false;
                }

                return !this.closedWhileInControl;
            }
        }
        
        public void InitializeDropDown(Control dropDownItem)
        {
            if (this.dropDownItem != null)
            {
                throw new Exception("The drop down item has already been implemented!");
            }
            
            this.DesignView = false;
            this.dropState = DropState.Closed;
            this.Size = this.AnchorSize;
            this.anchorClientBounds = new Rectangle(2, 2, this.AnchorSize.Width - 21, this.AnchorSize.Height - 4);
            //removes the dropDown item from the controls list so it 
            //won't be seen until the drop-down window is active
            if (this.Controls.Contains(this.dropDownItem))
            {
                this.Controls.Remove(this.dropDownItem);
            }
            
            this.dropDownItem = dropDownItem;
        }
        
        public void CloseDropDown()
        {

            if (this.dropContainer != null)
            {
                this.dropState = DropState.Closing;
                this.dropContainer.Freeze = false;
                this.dropContainer.Close();
            }
        }
        
        public void FreezeDropDown(bool remainVisible)
        {
            if (this.dropContainer != null)
            {
                this.dropContainer.Freeze = true;
                if (!remainVisible)
                {
                    this.dropContainer.Visible = false;
                }
            }
        }

        public void UnFreezeDropDown()
        {
            if (this.dropContainer != null)
            {
                this.dropContainer.Freeze = false;
                if (!this.dropContainer.Visible)
                {
                    this.dropContainer.Visible = true;
                }
            }
        }
        
        protected void OnPropertyChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(null, null);
            }
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignView)
            {
                this.storedSize = this.Size;
            }
            
            this.anchorSize.Width = this.Width;
            if (!this.DesignView)
            {
                this.anchorSize.Height = this.Height;
                this.anchorClientBounds = new Rectangle(2, 2, this.AnchorSize.Width - 21, this.AnchorSize.Height - 4);
            }
        }
        
        protected bool mousePressed;
        protected bool mouseTracking;
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.mousePressed = true;
            this.OpenDropDown();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.mousePressed = false;
            this.Invalidate();
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool inButton = this.GetButtonBounds().Contains(e.Location);
            if (!this.mouseTracking && inButton)
            {
                this.mouseTracking = true;
                this.Invalidate();
            }
            else
            {
                if (this.mouseTracking && !inButton)
                {
                    this.mouseTracking = false;
                    this.Invalidate();
                }
            }
        }
        
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
        
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }
        
        protected void OpenDropDown()
        {
            if (this.dropDownItem == null)
            {
                throw new NotImplementedException("The drop down item has not been initialized!  Use the InitializeDropDown() method to do so.");
            }

            if (!this.CanDrop)
            {
                return;
            }

            this.dropContainer = new DropDownContainer(this.dropDownItem);
            this.dropContainer.Bounds = this.GetDropDownBounds();
            this.dropContainer.DropStateChange += new DropDownContainer.DropWindowArgs(this.dropContainer_DropStateChange);
            this.dropContainer.FormClosed += new FormClosedEventHandler(this.dropContainer_Closed);
            this.ParentForm.Move += new EventHandler(this.ParentForm_Move);
            this.dropState = DropState.Dropping;
            this.dropContainer.Show(this);
            this.dropState = DropState.Dropped;
            this.Invalidate();
        }
        
        protected virtual Rectangle GetDropDownBounds() 
        {
            Size inflatedDropSize = new Size(this.dropDownItem.Width + 2, this.dropDownItem.Height + 2);
            Rectangle screenBounds = this.DockSide == DockSide.Left ?
                new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.X, this.Bounds.Bottom)), inflatedDropSize) 
                : new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.Right - this.dropDownItem.Width, this.Bounds.Bottom)), inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);
            //make sure we're completely in the top-left working area
            if (screenBounds.X < workingArea.X)
            {
                screenBounds.X = workingArea.X;
            }
            
            if (screenBounds.Y < workingArea.Y)
            {
                screenBounds.Y = workingArea.Y;
            }

            //make sure we're not extended past the working area's right /bottom edge
            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) 
            {
                screenBounds.X = workingArea.Right - screenBounds.Width;
            }
            
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) 
            {
                screenBounds.Y = workingArea.Bottom - screenBounds.Height;
            }

            return screenBounds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, this.renderer);
            /*renderContext.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            renderContext.Graphics.SmoothingMode = SmoothingMode.HighQuality;*/
            
            Rectangle r = new Rectangle(new Point(0, 0), this.AnchorSize);
            
            PaletteState state = PaletteState.Normal;
            switch (this.GetState())
            {
                case System.Windows.Forms.VisualStyles.ComboBoxState.Pressed:
                    state = PaletteState.Pressed;
                    break;
                case System.Windows.Forms.VisualStyles.ComboBoxState.Disabled:
                    state = PaletteState.Disabled;
                    break;
                case System.Windows.Forms.VisualStyles.ComboBoxState.Hot:
                    state = PaletteState.Tracking;
                    break;
            }
            
            GraphicsPath backPath = this.renderer.RenderStandardBorder.GetBorderPath(renderContext, r, this.paletteBorder, VisualOrientation.Top, state);
            this.mementoBackground = this.renderer.RenderStandardBack.DrawBack(renderContext, r, backPath, this.paletteBack, VisualOrientation.Top, state, this.mementoBackground);
            this.renderer.RenderStandardBorder.DrawBorder(renderContext, r, this.paletteBorder, VisualOrientation.Top, state);
            
            r = this.GetButtonBounds();
            
            backPath = this.renderer.RenderStandardBorder.GetBorderPath(renderContext, r, this.paletteBorderButton, VisualOrientation.Top, state);
            this.mementoBackground = this.renderer.RenderStandardBack.DrawBack(renderContext, r, backPath, this.paletteBackButton, VisualOrientation.Top, state, this.mementoBackground);
            this.renderer.RenderStandardBorder.DrawBorder(renderContext, r, this.paletteBorderButton, VisualOrientation.Top, state);
            
            renderContext.Graphics.DrawImage(Resources.down, r.X + 4, r.Y + 4);
            
            Rectangle rect = new Rectangle(3, 3, this.AnchorSize.Width - 23, this.AnchorSize.Height - 6);
            bool selected = this.Focused && this.DropState == DropState.Closed;
            if (selected)
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.Highlight))
                {
                    renderContext.Graphics.FillRectangle(brush, rect);
                }
            }
            
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Near;
            using (SolidBrush brush = new SolidBrush(selected ? SystemColors.HighlightText : SystemColors.ControlText))
            {
                renderContext.Graphics.DrawString(this.Text, this.Font, brush, rect, format);
            }
        }
        
        private Rectangle GetButtonBounds()
        {
            return new Rectangle(this.AnchorSize.Width - 17, 2, 15, this.AnchorSize.Height - 4);
        }
        
        private void KryptonManager_GlobalPaletteChanged(object sender, EventArgs e)
        {
            this.RefreshPalette();
        }
        
        private void RefreshPalette()
        {
            if (this.paletteMode == PaletteMode.Global)
            {
                this.palette = KryptonManager.CurrentGlobalPalette;
            }
            else
            {
                this.palette = KryptonManager.GetPaletteForMode(this.paletteMode);
            }
            
            this.renderer = this.palette.GetRenderer();
            this.paletteRedirect = new PaletteRedirect(this.palette);
            this.paletteBorder = new PaletteBorderInheritRedirect(this.paletteRedirect);
            this.paletteBack = new PaletteBackInheritRedirect(this.paletteRedirect);
            this.paletteBorderButton = new PaletteBorderInheritRedirect(this.paletteRedirect);
            this.paletteBackButton = new PaletteBackInheritRedirect(this.paletteRedirect);
            
            this.paletteBorder.Style = PaletteBorderStyle.InputControlStandalone;
            this.paletteBack.Style = PaletteBackStyle.InputControlStandalone;
            this.paletteBorderButton.Style = PaletteBorderStyle.ButtonInputControl;
            this.paletteBackButton.Style = PaletteBackStyle.ButtonListItem;
        }

        private System.Windows.Forms.VisualStyles.ComboBoxState GetState()
        {
            if (this.mousePressed || this.dropContainer != null)
            {
                return System.Windows.Forms.VisualStyles.ComboBoxState.Pressed;
            }
            else
            {
                if (this.mouseTracking)
                {
                    return System.Windows.Forms.VisualStyles.ComboBoxState.Hot;
                }
                else
                {
                    return System.Windows.Forms.VisualStyles.ComboBoxState.Normal;
                }
            }
        }
        
        private void ParentForm_Move(object sender, EventArgs e)
        {
            this.dropContainer.Bounds = this.GetDropDownBounds();
        }
        
        private void dropContainer_DropStateChange(DropState state)
        {
            this.dropState = state;
        }
        
        private void dropContainer_Closed(object sender, FormClosedEventArgs e)
        {
            if (!this.dropContainer.IsDisposed)
            {
                this.dropContainer.DropStateChange -= this.dropContainer_DropStateChange;
                this.dropContainer.FormClosed -= this.dropContainer_Closed;
                this.ParentForm.Move -= this.ParentForm_Move;
                this.dropContainer.Dispose();
            }
            
            this.dropContainer = null;
            this.closedWhileInControl = (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position));
            this.dropState = DropState.Closed;
            this.Invalidate();
        }
    }
}
