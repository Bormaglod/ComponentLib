//---------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.Microsoft
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Windows.Forms.VisualStyles;
    using System.Diagnostics;
    using ComponentFactory.Krypton.Toolkit;

    public class TreeGridCell : KryptonDataGridViewTextBoxCell
    {
        private const int INDENT_WIDTH = 20;
        private const int INDENT_MARGIN = 5;
        private int glyphWidth;
        private int calculatedLeftPadding;
        internal bool IsSited;
        private Padding previousPadding;
        private int imageWidth = 0;
        private int imageHeight = 0;
        private int imageHeightOffset = 0;
        private Rectangle lastKnownGlyphRect;

        public TreeGridCell()
        {            
            this.glyphWidth = 15;
            this.calculatedLeftPadding = 0;
            this.IsSited = false;
        }

        public override object Clone()
        {
            TreeGridCell c = (TreeGridCell)base.Clone();
            
            c.glyphWidth = this.glyphWidth;
            c.calculatedLeftPadding = this.calculatedLeftPadding;

            return c;
        }

        internal protected virtual void UnSited()
        {
            // The row this cell is in is being removed from the grid.
            this.IsSited = false;
            this.Style.Padding = this.previousPadding;
        }

        internal protected virtual void Sited()
        {
            // when we are added to the DGV we can realize our style
            this.IsSited = true;

            // remember what the previous padding size is so it can be restored when unsiting
            this.previousPadding = this.Style.Padding;

            this.UpdateStyle();
        }        

        internal protected virtual void UpdateStyle(){
            // styles shouldn't be modified when we are not sited.
            if (this.IsSited == false)
            {
                return;
            }

            int level = this.Level;

            Padding p = this.previousPadding;
            Size preferredSize;

            using (Graphics g = this.OwningNode.grid.CreateGraphics())
            {
                preferredSize = this.GetPreferredSize(g, this.InheritedStyle, this.RowIndex, new Size(0, 0));
            }

            Image image = this.OwningNode.Image;

            if (image != null)
            {
                // calculate image size
                this.imageWidth = image.Width + 2;
                this.imageHeight = image.Height + 2;
            }
            else
            {
                this.imageWidth = 0;//this.glyphWidth;
                this.imageHeight = 0;
            }

            // TODO: Make this cleaner
            if (preferredSize.Height < this.imageHeight)
            {
                this.Style.Padding = new Padding(p.Left + (level * INDENT_WIDTH) + this.imageWidth + INDENT_MARGIN,
                                                 p.Top + (this.imageHeight / 2), p.Right, p.Bottom + (this.imageHeight / 2));
                this.imageHeightOffset = 2;
            }
            else
            {
                this.Style.Padding = new Padding(p.Left + (level * INDENT_WIDTH) + this.imageWidth + INDENT_MARGIN,
                                                 p.Top, p.Right, p.Bottom );
            }

            this.calculatedLeftPadding = ((level - 1) * glyphWidth) + this.imageWidth + INDENT_MARGIN;
        }

        public int Level
        {
            get
            {
                TreeGridNode row = this.OwningNode;
                if (row != null)
                {
                    return row.Level;
                }
                else
                {
                    return -1;
                }
            }
        }

        protected virtual int GlyphMargin
        {
            get
            {
                return ((this.Level - 1) * INDENT_WIDTH) + INDENT_MARGIN;
            }
        }

        protected virtual int GlyphOffset
        {
            get
            {
                return ((this.Level - 1) * INDENT_WIDTH);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            TreeGridNode node = this.OwningNode;
            if (node == null)
            {
                return;
            }

            Image image = node.Image;

            if (this.imageHeight == 0 && image != null)
            {
                this.UpdateStyle();
            }

            // paint the cell normally
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // TODO: Indent width needs to take image size into account
            Rectangle glyphRect = new Rectangle(cellBounds.X + this.GlyphMargin, cellBounds.Y, INDENT_WIDTH, cellBounds.Height - 1);
            int glyphHalf = glyphRect.Width / 2;

            //TODO: This painting code needs to be rehashed to be cleaner
            int level = this.Level;

            //TODO: Rehash this to take different Imagelayouts into account. This will speed up drawing
            //        for images of the same size (ImageLayout.None)
            if (image != null)
            {
                Point pp;
                if (this.imageHeight > cellBounds.Height)
                {
                    pp = new Point(glyphRect.X + this.glyphWidth, cellBounds.Y + this.imageHeightOffset);
                }
                else
                {
                    pp = new Point(glyphRect.X + this.glyphWidth, (cellBounds.Height / 2 - this.imageHeight / 2) + cellBounds.Y);
                }

                // Graphics container to push/pop changes. This enables us to set clipping when painting
                // the cell's image -- keeps it from bleeding outsize of cells.
                System.Drawing.Drawing2D.GraphicsContainer gc = graphics.BeginContainer();
                {
                    graphics.SetClip(cellBounds);
                    graphics.DrawImageUnscaled(image, pp);
                }
                
                graphics.EndContainer(gc);
            }

            // Paint tree lines            
            if (node.grid.ShowLines)
            {
                using (Pen linePen = new Pen(SystemBrushes.ControlDark, 1.0f))
                {
                    linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    bool isLastSibling = node.IsLastSibling;
                    bool isFirstSibling = node.IsFirstSibling;
                    if (node.Level == 1)
                    {
                        // the Root nodes display their lines differently
                        if (isFirstSibling && isLastSibling)
                        {
                            // only node, both first and last. Just draw horizontal line
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isFirstSibling)
                        {
                            // first sibling doesn't draw the line extended above. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.X + 4, cellBounds.Bottom);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }
                    }
                    else
                    {
                        if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }

                        // paint lines of previous levels to the root
                        TreeGridNode previousNode = node.Parent;
                        int horizontalStop = (glyphRect.X + 4) - INDENT_WIDTH;

                        while (!previousNode.IsRoot)
                        {
                            if (previousNode.HasChildren && !previousNode.IsLastSibling)
                            {
                                // paint vertical line
                                graphics.DrawLine(linePen, horizontalStop, cellBounds.Top, horizontalStop, cellBounds.Bottom);
                            }
                            
                            previousNode = previousNode.Parent;
                            horizontalStop = horizontalStop - INDENT_WIDTH;
                        }
                    }
                }
            }

            if (node.HasChildren || node.grid.VirtualNodes)
            {
                // Paint node glyphs                
                if (node.IsExpanded)
                {
                    node.grid.rOpen.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                }
                else
                {
                    node.grid.rClosed.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                }
            }
        }
        
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);

            TreeGridNode node = this.OwningNode;
            if (node != null)
            {
                node.grid.inExpandCollapseMouseCapture = false;
            }
        }
        
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.Location.X > this.InheritedStyle.Padding.Left)
            {
                base.OnMouseDown(e);
            }
            else
            {
                // Expand the node
                //TODO: Calculate more precise location
                TreeGridNode node = this.OwningNode;
                if (node != null)
                {
                    node.grid.inExpandCollapseMouseCapture = true;
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    else
                    {
                        node.Expand();
                    }
                }
            }
        }
        
        public TreeGridNode OwningNode
        {
            get { return base.OwningRow as TreeGridNode; }
        }
    }

    public class TreeGridColumn : DataGridViewTextBoxColumn
    {
        internal Image defaultNodeImage;
        
        public TreeGridColumn()
        {        
            this.CellTemplate = new TreeGridCell();
        }

        // Need to override Clone for design-time support.
        public override object Clone()
        {
            TreeGridColumn c = (TreeGridColumn)base.Clone();
            c.defaultNodeImage = this.defaultNodeImage;
            return c;
        }

        public Image DefaultNodeImage
        {
            get { return this.defaultNodeImage; }
            set { this.defaultNodeImage = value; }
        }
    }
}
