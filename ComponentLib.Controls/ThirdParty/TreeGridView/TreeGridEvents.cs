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

    public class TreeGridNodeEventBase
    {
        private TreeGridNode node;

        public TreeGridNodeEventBase(TreeGridNode node)
        {
            this.node = node;
        }

        public TreeGridNode Node
        {
            get { return this.node; }
        }
    }
    
    public class CollapsingEventArgs : System.ComponentModel.CancelEventArgs
    {
        private TreeGridNode node;

        private CollapsingEventArgs()
        {
        }
        
        public CollapsingEventArgs(TreeGridNode node) : base()
        {
            this.node = node;
        }
        
        public TreeGridNode Node
        {
            get { return this.node; }
        }
    }
    
    public class CollapsedEventArgs : TreeGridNodeEventBase
    {
        public CollapsedEventArgs(TreeGridNode node) : base(node)
        {
        }
    }

    public class ExpandingEventArgs:System.ComponentModel.CancelEventArgs
    {
        private TreeGridNode node;

        private ExpandingEventArgs()
        {
        }
        
        public ExpandingEventArgs(TreeGridNode node) : base()
        {
            this.node = node;
        }
        public TreeGridNode Node
        {
            get { return this.node; }
        }
    }
    
    public class ExpandedEventArgs : TreeGridNodeEventBase
    {
        public ExpandedEventArgs(TreeGridNode node) : base(node)
        {
        }
    }

    public delegate void ExpandingEventHandler(object sender, ExpandingEventArgs e);
    public delegate void ExpandedEventHandler(object sender, ExpandedEventArgs e);

    public delegate void CollapsingEventHandler(object sender, CollapsingEventArgs e);
    public delegate void CollapsedEventHandler(object sender, CollapsedEventArgs e);
}
