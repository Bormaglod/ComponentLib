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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing.Design;
    using System.Text;

    [ToolboxItem(false), DesignTimeVisible(false)]
    public class TreeGridNode : DataGridViewRow
    {
        internal TreeGridView grid;
        internal TreeGridNode parent;
        internal TreeGridNodeCollection owner;
        internal bool IsExpanded;
        internal bool IsRoot;
        internal bool isSited;
        internal bool isFirstSibling;
        internal bool isLastSibling;
        internal Image image;
        internal int imageIndex;

        private Random rndSeed = new Random();
        public int UniqueValue = -1;
        TreeGridCell treeCell;
        TreeGridNodeCollection childrenNodes;

        private int index;
        private int level;
        private bool childCellsCreated = false;

        // needed for IComponent
        private ISite site = null;
        private EventHandler disposed = null;

        internal TreeGridNode(TreeGridView owner) : this()
        {
            this.grid = owner;
            this.IsExpanded = true;
        }

        public TreeGridNode()
        {            
            this.index = -1;
            this.level = -1;            
            this.IsExpanded = false;
            this.UniqueValue = this.rndSeed.Next();
            this.isSited = false;
            this.isFirstSibling = false;
            this.isLastSibling = false;
            this.imageIndex = -1;
        }

        public override object Clone()
        {
            TreeGridNode r = (TreeGridNode)base.Clone();
            r.UniqueValue = -1;
            r.level = this.level;
            r.grid = this.grid;
            r.parent = this.Parent;

            r.imageIndex = this.imageIndex;
            if (r.imageIndex == -1)
            {
                r.Image = this.Image;
            }

            r.IsExpanded = this.IsExpanded;

            return r;
        }
        
        internal protected virtual void UnSited()
        {
            // This row is being removed from being displayed on the grid.
            TreeGridCell cell;
            foreach (DataGridViewCell DGVcell in this.Cells)
            {
                cell = DGVcell as TreeGridCell;
                if (cell != null)
                {
                    cell.UnSited();
                }
            }
            
            this.isSited = false;
        }

        internal protected virtual void Sited()
        {
            // This row is being added to the grid.
            this.isSited = true;
            this.childCellsCreated = true;
            Debug.Assert(this.grid != null);

            TreeGridCell cell;
            foreach (DataGridViewCell DGVcell in this.Cells)
            {
                cell = DGVcell as TreeGridCell;
                if (cell != null)
                {
                    cell.Sited();
                }
            }
        }

        // Represents the index of this row in the Grid
        [System.ComponentModel.Description("Represents the index of this row in the Grid. Advanced usage.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowIndex
        {
            get
            {
                return base.Index;
            }
        }

        // Represents the index of this row based upon its position in the collection.
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int Index
        {
            get
            {
                if (this.index == -1)
                {
                    // get the index from the collection if unknown
                    this.index = this.owner.IndexOf(this);
                }

                return this.index;
            }
            
            internal set
            {
                this.index = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public ImageList ImageList
        {
            get
            {
                if (this.grid != null)
                {
                    return this.grid.ImageList;
                }
                else
                {
                    return null;
                }
            }
        }

        private bool ShouldSerializeImageIndex()
        {
            return (this.imageIndex != -1 && this.image == null);
        }

        [Category("Appearance")]
        [Description("...")]
        [DefaultValue(-1)]
        [TypeConverter(typeof(ImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
        public int ImageIndex
        {
            get
            {
                return this.imageIndex;
            }
            
            set
            {
                this.imageIndex = value;
                if (this.imageIndex != -1)
                {
                    // when a imageIndex is provided we do not store the image.
                    this.image = null;
                }
                
                if (this.isSited)
                {
                    // when the image changes the cell's style must be updated
                    this.treeCell.UpdateStyle();
                    if (this.Displayed)
                    {
                        this.grid.InvalidateRow(this.RowIndex);
                    }
                }
            }
        }

        private bool ShouldSerializeImage()
        {
            return (this.imageIndex == -1 && this.image != null);
        }

        public Image Image
        {
            get
            {
                if (this.image == null && this.imageIndex != -1)
                {
                    if (this.ImageList != null && this.imageIndex < this.ImageList.Images.Count)
                    {
                        // get image from image index
                        return this.ImageList.Images[this.imageIndex];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    // image from image property
                    return this.image;
                }
            }
            
            set
            {
                this.image = value;
                if (this.image != null)
                {
                    // when a image is provided we do not store the imageIndex.
                    this.imageIndex = -1;
                }
                if (this.isSited)
                {
                    // when the image changes the cell's style must be updated
                    this.treeCell.UpdateStyle();
                    if (this.Displayed)
                    {
                        this.grid.InvalidateRow(this.RowIndex);
                    }
                }
            }
        }

        protected override DataGridViewCellCollection CreateCellsInstance()
        {
            DataGridViewCellCollection cells = base.CreateCellsInstance();
            cells.CollectionChanged += cells_CollectionChanged;
            return cells;
        }

        private void cells_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            // Exit if there already is a tree cell for this row
            if (this.treeCell != null)
            {
                return;
            }

            if (e.Action == System.ComponentModel.CollectionChangeAction.Add || e.Action == System.ComponentModel.CollectionChangeAction.Refresh)
            {
                TreeGridCell treeCell = null;

                if (e.Element == null)
                {
                    foreach (DataGridViewCell cell in base.Cells)
                    {
                        if (cell.GetType().IsAssignableFrom(typeof(TreeGridCell)))
                        {
                            treeCell = (TreeGridCell)cell;
                            break;
                        }
                    }
                }
                else
                {
                    treeCell = e.Element as TreeGridCell;
                }

                if (treeCell != null) 
                {
                  this.treeCell = treeCell;
                }
            }
        }

        [Category("Data")]
        [Description("The collection of root nodes in the treelist.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public TreeGridNodeCollection Nodes
        {
            get
            {
                if (childrenNodes == null)
                {
                    childrenNodes = new TreeGridNodeCollection(this);
                }
                
                return childrenNodes;
            }
            
            set
            {
                ;
            }
        }

        // Create a new Cell property because by default a row is not in the grid and won't
        // have any cells. We have to fabricate the cell collection ourself.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellCollection Cells
        {
            get
            {
                if (!childCellsCreated && this.DataGridView == null)
                {
                    if (this.grid == null)
                    {
                        return null;
                    }

                    this.CreateCells(this.grid);
                    childCellsCreated = true;
                }
                
                return base.Cells;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Level
        {
            get
            {
                if (this.level == -1)
                {
                    // calculate level
                    int walk = 0;
                    TreeGridNode walkRow = this.Parent;
                    while (walkRow != null)
                    {
                        walk++;
                        walkRow = walkRow.Parent;
                    }
                    
                    this.level = walk;
                }
                
                return this.level;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeGridNode Parent
        {
            get
            {
                return this.parent;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool HasChildren
        {
            get
            {
                return (this.childrenNodes != null && this.Nodes.Count != 0);
            }
        }

        [Browsable(false)]
        public bool IsSited
        {
            get
            {
                return this.isSited;
            }
        }
        
        [Browsable(false)]
        public bool IsFirstSibling
        {
            get
            {
                return (this.Index == 0);
            }
        }

        [Browsable(false)]
        public bool IsLastSibling
        {
            get
            {
                TreeGridNode parent = this.Parent;
                if (parent != null && parent.HasChildren)
                {
                    return (this.Index == parent.Nodes.Count - 1);
                }
                else
                {
                    return true;
                }
            }
        }
        
        public virtual bool Collapse()
        {
            return this.grid.CollapseNode(this);
        }

        public virtual bool Expand()
        {
            if (this.grid != null)
            {
                return this.grid.ExpandNode(this);
            }
            else
            {
                this.IsExpanded = true;
                return true;
            }
        }

        internal protected virtual bool InsertChildNode(int index, TreeGridNode node)
        {
            node.parent = this;
            node.grid = this.grid;

            // ensure that all children of this node has their grid set
            if (this.grid != null)
            {
                UpdateChildNodes(node);
            }

            //TODO: do we need to use index parameter?
            if ((this.isSited || this.IsRoot) && this.IsExpanded)
            {
                this.grid.SiteNode(node);
            }
            
            return true;
        }

        internal protected virtual bool InsertChildNodes(int index, params TreeGridNode[] nodes)
        {
            foreach (TreeGridNode node in nodes)
            {
                this.InsertChildNode(index, node);
            }
            
            return true;
        }

        internal protected virtual bool AddChildNode(TreeGridNode node)
        {
            node.parent = this;
            node.grid = this.grid;

            // ensure that all children of this node has their grid set
            if (this.grid != null)
            {
                UpdateChildNodes(node);
            }

            if ((this.isSited || this.IsRoot) && this.IsExpanded && !node.isSited)
            {
                this.grid.SiteNode(node);
            }

            return true;
        }
        
        internal protected virtual bool AddChildNodes(params TreeGridNode[] nodes)
        {
            //TODO: Convert the final call into an SiteNodes??
            foreach (TreeGridNode node in nodes)
            {
                this.AddChildNode(node);
            }
            
            return true;
        }

        internal protected virtual bool RemoveChildNode(TreeGridNode node)
        {
            if ((this.IsRoot || this.isSited) && this.IsExpanded )
            {
                //We only unsite out child node if we are sited and expanded.
                this.grid.UnSiteNode(node);
            }
            
            node.grid = null;    
            node.parent = null;
            return true;
        }

        internal protected virtual bool ClearNodes()
        {
            if (this.HasChildren)
            {
                for (int i = this.Nodes.Count - 1; i >= 0; i--)
                {
                    this.Nodes.RemoveAt(i);
                }
            }
            
            return true;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler Disposed
        {
            add
            {
                this.disposed += value;
            }
            
            remove
            {
                this.disposed -= value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get { return this.site; }
            set { this.site = value; }
        }

        private void UpdateChildNodes(TreeGridNode node)
        {
            if (node.HasChildren)
            {
                foreach (TreeGridNode childNode in node.Nodes)
                {
                    childNode.grid = node.grid;
                    this.UpdateChildNodes(childNode);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(36);
            sb.Append("TreeGridNode { Index=");
            sb.Append(this.RowIndex.ToString(System.Globalization.CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing)
        //    {
        //        lock(this)
        //        {
        //            if (this.site != null && this.site.Container != null)
        //            {
        //                this.site.Container.Remove(this);
        //            }

        //            if (this.disposed != null)
        //            {
        //                this.disposed(this, EventArgs.Empty);
        //            }
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
    }

}