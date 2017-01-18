//-----------------------------------------------------------------------
// <copyright file="TreeViewColumns.cs" company="Sergey Teplyashin">
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
// <date>28.10.2011</date>
// <time>9:02</time>
// <summary>Defines the TreeViewColumns class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;
    using ComponentFactory.Krypton.Toolkit;
    using ComponentLib.Controls.Design;

    [ToolboxBitmapAttribute(typeof(TreeView))]
    [Designer(typeof(TreeViewColumnsDesigner))]
    public class TreeViewColumns : TreeView
    {
        static Control viewLayoutContextControl;
        
        const int ImageSizePM = 9;
        const int ImageSizeCB = 13;
        
        IPalette palette;
        IRenderer renderer;
        
        PaletteMode paletteMode;
        PaletteRedirect paletteRedirect;
        PaletteBackInheritRedirect paletteBackground;
        PaletteBorderInheritRedirect paletteLines;
        PaletteBackInheritRedirect paletteBackButton;
        PaletteBorderInheritRedirect paletteBorderButton;
        
        IDisposable mementoBackground;
        
        TreeNode tracking;
        
        public TreeViewColumns()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            //FullRowSelect = true;

            this.tracking = this.SelectedNode;

            KryptonManager.GlobalPaletteChanged += new EventHandler(this.KryptonManager_GlobalPaletteChanged);
            this.RefreshPalette();
        }
        
        static TreeViewColumns()
        {
            viewLayoutContextControl = new Control();
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
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                KryptonManager.GlobalPaletteChanged -= new EventHandler(KryptonManager_GlobalPaletteChanged);
            }
            
            base.Dispose(disposing);
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color backColor = this.palette.GetBackColor1(paletteBackground.Style, PaletteState.Normal);
            SolidBrush backBrush = new SolidBrush(backColor);
            pevent.Graphics.FillRectangle(backBrush, pevent.ClipRectangle);
        }
        
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
        	PaletteState state;
        	
        	bool selected = (e.State & TreeNodeStates.Selected) != 0;
        	bool hot = tracking == e.Node;
        	if (selected && hot)
        	{
        		state = PaletteState.Pressed;
        	}
        	else if (selected)
        	{
        		state = PaletteState.Checked;
        	}
        	else if (hot)
        	{
        		state = PaletteState.Tracking;
        	}
        	else
        	{
        		state = PaletteState.Normal;
        	}

        	if (e.Node.IsVisible)
        	{
        		RenderContext renderContext = new RenderContext(this, e.Graphics, e.Bounds, renderer);
        		renderContext.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        		renderContext.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        		
        		DrawBackground(renderContext, e.Bounds, e.Node, state);
        		if (ShowLines)
        		{
        			DrawLines(renderContext, e.Node);
        		}
        		
        		if (((ShowRootLines) || (e.Node.Level > 0)) && ShowPlusMinus)
        		{
        			DrawIndicator(renderContext, e.Bounds, e.Node);
        		}
        		
        		if (CheckBoxes)
        		{
        			DrawChecked(renderContext, e.Bounds, e.Node);
        		}
        		
        		if (ImageList != null)
        		{
        			DrawImage(renderContext, e.Bounds, e.Node);
        		}
        		
        		DrawText(renderContext, e.Bounds, e.Node);
        	}
        	else
        	{
        		e.DrawDefault = true;
        	}
        }
        
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            tracking = SelectedNode;
        }

        /*public void ClearNodes()
        {
            SelectedNode = null;
            Nodes.Clear();
        }*/
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (HotTracking)
            {
                TreeNode node = GetNodeAt(e.X, e.Y);
                if (node != tracking)
                {
                    TreeNode old = tracking;
                    tracking = node;
                    DrawTreeNode(old);
                    DrawTreeNode(tracking);
                }
            }
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            TreeNode clickedNode = GetNodeAt(e.X, e.Y);
            //if (NodeBounds(clickedNode).Contains(e.X, e.Y)) {
                SelectedNode = clickedNode;
            //}
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (tracking != null)
            {
                TreeNode old = tracking;
                tracking = null;
                DrawTreeNode(old);
            }
        }
        
        internal void DrawTreeNode(TreeNode node)
        {
            if (node != null)
            {
                Rectangle r = new Rectangle(0, node.Bounds.Top, ClientSize.Width, node.Bounds.Height);
                Invalidate(GetBoundsNode(r, node));
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
            paletteLines = new PaletteBorderInheritRedirect(paletteRedirect);
            paletteBackButton = new PaletteBackInheritRedirect(paletteRedirect);
            paletteBorderButton = new PaletteBorderInheritRedirect(paletteRedirect);
            
            paletteBackground.Style = PaletteBackStyle.InputControlStandalone;
            paletteLines.Style = PaletteBorderStyle.GridDataCellList;
            paletteBackButton.Style = PaletteBackStyle.ButtonListItem;
            paletteBorderButton.Style = PaletteBorderStyle.ButtonListItem;
            
            Refresh();
        }
        
        void DrawBackground(RenderContext context, Rectangle rect, TreeNode node, PaletteState state)
        {
            if (state == PaletteState.Normal)
            {
                return;
            }
            
            context.Graphics.FillRectangle(Brushes.White, rect);
            rect = GetBoundsNode(rect, node);
            VisualOrientation visualOrientation = VisualOrientation.Top;
            GraphicsPath backPath = renderer.RenderStandardBorder.GetBackPath(context, rect, paletteBorderButton, visualOrientation, state);
            
            if (state == PaletteState.Checked)
            {
				state = palette.ColorTable.ButtonCheckedGradientBegin.IsEmpty ? PaletteState.Pressed : PaletteState.CheckedNormal;
            }
            
            mementoBackground = renderer.RenderStandardBack.DrawBack(context, rect, backPath, paletteBackButton, visualOrientation, state, mementoBackground);
            renderer.RenderStandardBorder.DrawBorder(context, rect, paletteBorderButton, visualOrientation, state);
        }
        
        void DrawLines(ViewContext context, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            
            Rectangle rect = node.Bounds;
            TreeNode curNode = node;
            Pen pen = new Pen(paletteLines.GetBorderColor1(PaletteState.Normal));
            while ((curNode != null) && (ShowRootLines || (curNode.Parent != null)))
            {
                
                int dx = ShowRootLines ? Indent + 3 : 0;
                int d = node.Level != 0 ? Indent + Indent * (curNode.Level - 1) + dx : dx;            
                int x = d - (dx + ImageSizePM) / 2 + 5;
                if (!ShowRootLines)
                {
                    x -= Indent - 11;
                }
                
                int width = d - x - 2;
                int y = rect.Top;
                int y2 = rect.Top + rect.Height;
                
                if (curNode == node)
                {
                    int midy = y + rect.Height / 2;
                    context.Graphics.DrawLine(pen, x, midy, x + width, midy);
                    if (curNode.NextNode == null)
                    {
                        y2 = y + rect.Height / 2;
                    }
                }

                if ((curNode.Parent == null) && (curNode.PrevNode == null) && (curNode == node))
                {
                    y = rect.Height / 2;
                }
                
                if (curNode.NextNode != null || curNode == node)
                {
                    context.Graphics.DrawLine(pen, x, y, x, y2);
                }

                curNode = curNode.Parent;
            }
        }
        
        void DrawIndicator(ViewContext context, Rectangle rect, TreeNode node)
        {
            if (node.GetNodeCount(false) > 0)
            {
                Rectangle r = GetBoundsIndicator(rect, node);
                int dy = (int)Math.Round((float)(r.Height - ImageSizePM) / 2);
                if (Application.RenderWithVisualStyles)
                {
                    VisualStyleRenderer rd;
                
					rd = node.IsExpanded ? new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened) : new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed);
                    rd.DrawBackground(context.Graphics, new Rectangle(r.X, r.Y + dy, ImageSizePM, ImageSizePM));
                }
                else
                {
                    Image img = node.IsExpanded ? Resources.minus : Resources.plus;
                    context.Graphics.DrawImageUnscaled(img, new Point(r.X, r.Y + dy));
                }
            }
        }
        
        void DrawChecked(RenderContext context, Rectangle rect, TreeNode node)
        {
            Rectangle r = GetBoundsChecked(rect, node);
            int dy = (int)Math.Round((float)(r.Height - ImageSizeCB) / 2);
            Rectangle checkRect = new Rectangle(r.X, r.Y + dy, ImageSizeCB, ImageSizeCB);
            CheckState state = node.Checked ? CheckState.Checked : CheckState.Unchecked;
            //bool isTracking = this.tracking == node;
            bool isTracking = false;
            renderer.RenderGlyph.DrawCheckBox(context, checkRect, palette, Enabled, state, isTracking, false);
        }
        
        void DrawImage(ViewContext context, Rectangle rect, TreeNode node)
        {
            if (node.ImageIndex != -1 || !string.IsNullOrEmpty(node.ImageKey))
            {
                int index = string.IsNullOrEmpty(node.ImageKey) ? node.ImageIndex : ImageList.Images.IndexOfKey(node.ImageKey);
                Rectangle r = GetBoundsImage(rect, node);
                ImageList.Draw(context.Graphics, new Point(r.X, r.Y), index);
            }
        }
        
        void DrawText(ViewContext context, Rectangle rect, TreeNode node)
        {
            Rectangle r = GetBoundsNode(rect, node);
            
            Font nodeFont = node.NodeFont;
            if (nodeFont == null)
            {
                nodeFont = palette.GetContentShortTextFont(PaletteContentStyle.ButtonListItem, PaletteState.Normal);
            }
            
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.FormatFlags = StringFormatFlags.NoWrap;
            format.Trimming = StringTrimming.EllipsisCharacter;
            SolidBrush brush = new SolidBrush(palette.GetContentShortTextColor1(PaletteContentStyle.ButtonListItem, PaletteState.Normal));
            context.Graphics.DrawString(node.Text, nodeFont, brush, r, format);

            TreeNodeColumn tnc = node as TreeNodeColumn;
            if (tnc != null)
            {
                format.Alignment = StringAlignment.Far;
                context.Graphics.DrawString(tnc.FarText, nodeFont, brush, r, format);
            }
        }
        
        Rectangle GetBoundsIndicator(Rectangle rect, TreeNode node)
        {
            Rectangle res = rect;
            
            int dx = ShowRootLines ? Indent + 3 : 0;
            int d = node.Level != 0 ? Indent + Indent * (node.Level - 1) + dx : dx;
            d -= (dx + ImageSizePM) / 2;
            if (!ShowRootLines)
            {
                d -= Indent - 11;
            }
            
            res.Offset(d + 1, 0);
            
            return res;
        }
        
        Rectangle GetBoundsChecked(Rectangle rect, TreeNode node)
        {
            Rectangle res = rect;
            
            int dx = ShowRootLines ? Indent + 5 : 5;
            if (node.Level != 0)
            {
                res.Offset(Indent + Indent * (node.Level - 1) + dx, 0);
            }
            else
            {
                res.Offset(dx, 0);
            }
            
            return res;
        }
        
        Rectangle GetBoundsImage(Rectangle rect, TreeNode node)
        {
            Rectangle res = rect;
            int dx = ShowRootLines ? Indent + 5 : 5;
            int d = node.Level != 0 ? Indent + Indent * (node.Level - 1) + dx : dx;
            if (CheckBoxes)
            {
                d += 16;
            }
            
            res.Offset(d, 0);
            res.Width -= d;
            
            return res;
        }
        
        Rectangle GetBoundsNode(Rectangle rect, TreeNode node)
        {
            Rectangle res = rect;
            int dx = ShowRootLines ? Indent + 3 : 3;
            int d = node.Level != 0 ? Indent + Indent * (node.Level - 1) + dx : dx;
            if (CheckBoxes)
            {
                d += 16;
            }
            
            if (ImageList != null)
            {
                d += ImageList.ImageSize.Width + 4;
            }
                
            res.Offset(d, 0);
            res.Width -= d;
            
            return res;
        }
    }
}

