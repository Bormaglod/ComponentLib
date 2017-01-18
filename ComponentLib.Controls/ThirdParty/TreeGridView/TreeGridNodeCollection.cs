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
    using System.Collections.Generic;
    using System.Text;

    public class TreeGridNodeCollection : System.Collections.Generic.IList<TreeGridNode>, System.Collections.IList
    {
        internal System.Collections.Generic.List<TreeGridNode> list;
        internal TreeGridNode owner;
        internal TreeGridNodeCollection(TreeGridNode owner)
        {
            this.owner = owner;
            this.list = new List<TreeGridNode>();
        }

        #region Public Members
        public void Add(TreeGridNode item)
        {
            // The row needs to exist in the child collection before the parent is notified.
            item.grid = this.owner.grid;

            bool hadChildren = this.owner.HasChildren;
            item.owner = this;

            this.list.Add(item);

            this.owner.AddChildNode(item);

            // if the owner didn't have children but now does (asserted) and it is sited update it
            if (!hadChildren && this.owner.IsSited)
            {
                this.owner.grid.InvalidateRow(this.owner.RowIndex);
            }
        }

        public TreeGridNode Add(string text)
        {
            TreeGridNode node = new TreeGridNode();
            this.Add(node);

            node.Cells[0].Value = text;
            return node;
        }

        public TreeGridNode Add(params object[] values)
        {
            TreeGridNode node = new TreeGridNode();
            this.Add(node);

            int cell = 0;

            if (values.Length > node.Cells.Count )
            {
                throw new ArgumentOutOfRangeException("values");
            }

            foreach (object o in values)
            {
                node.Cells[cell].Value = o;
                cell++;
            }
            
            return node;
        }

        public void Insert(int index, TreeGridNode item)
        {
            // The row needs to exist in the child collection before the parent is notified.
            item.grid = this.owner.grid;
            item.owner = this;

            this.list.Insert(index, item);

            this.owner.InsertChildNode(index, item);
        }

        public bool Remove(TreeGridNode item)
        {
            // The parent is notified first then the row is removed from the child collection.
            this.owner.RemoveChildNode(item);
            item.grid = null;
            return this.list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            TreeGridNode row = this.list[index];

            // The parent is notified first then the row is removed from the child collection.
            this.owner.RemoveChildNode(row);
            row.grid = null;
            this.list.RemoveAt(index);
        }

        public void Clear()
        {
            // The parent is notified first then the row is removed from the child collection.
            this.owner.ClearNodes();
            this.list.Clear();
        }

        public int IndexOf(TreeGridNode item)
        {
            return this.list.IndexOf(item);
        }

        public TreeGridNode this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool Contains(TreeGridNode item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(TreeGridNode[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get{ return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get{ return false; }
        }
        
        #endregion

        #region IList Interface
        void System.Collections.IList.Remove(object value)
        {
            this.Remove(value as TreeGridNode);
        }


        int System.Collections.IList.Add(object value)
        {
            TreeGridNode item = value as TreeGridNode;
            this.Add(item);
            return item.Index;
        }

        void System.Collections.IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }


        void System.Collections.IList.Clear()
        {
            this.Clear();
        }

        bool System.Collections.IList.IsReadOnly
        {
            get { return this.IsReadOnly;}
        }

        bool System.Collections.IList.IsFixedSize
        {
            get { return false; }
        }

        int System.Collections.IList.IndexOf(object item)
        {
            return this.IndexOf(item as TreeGridNode);
        }

        void System.Collections.IList.Insert(int index, object value)
        {
            this.Insert(index, value as TreeGridNode);
        }
        
        int System.Collections.ICollection.Count
        {
            get { return this.Count; }
        }
        
        bool System.Collections.IList.Contains(object value)
        {
            return this.Contains(value as TreeGridNode);
        }
        
        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        
        object System.Collections.IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        #endregion

        #region IEnumerable<ExpandableRow> Members

        public IEnumerator<TreeGridNode> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        bool System.Collections.ICollection.IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }

}
