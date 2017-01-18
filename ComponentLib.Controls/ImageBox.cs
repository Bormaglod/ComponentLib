//-----------------------------------------------------------------------
// <copyright file="ImageBox.cs" company="Sergey Teplyashin">
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
// <date>07.06.2012</date>
// <time>15:25</time>
// <summary>Defines the ImageBox class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ImageBox))]
    public partial class ImageBox : UserControl
    {
        bool smallButtons;
        ImageCollection images;
        string current;
        bool showAddButton;
        bool showAddInternetButton;
        bool showEditButton;
        bool showDeleteButton;
        bool imageFileChanged;
        
        public ImageBox()
        {
            InitializeComponent();
            CompactMode = false;
            ShowThumbnails = true;
            ShowAddButton = true;
            ShowAddInternetButton = true;
            ShowEditButton = true;
            ShowDeleteButton = true;
            groupImage.ValuesPrimary.Image = null;
            gridImages.RowTemplate.Height = this.gridImages.Height;
            
            images = new ImageCollection(gridImages);
            current = string.Empty;
        }
        
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowThumbnails
        {
            get
            {
                return gridImages.Visible;
            }
            
            set
            {
                gridImages.Visible = value;
                buttonToolNext.Visible = !value;
                buttonToolPrev.Visible = !value;
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool CompactMode
        {
            get
            {
                return groupImage.HeaderVisiblePrimary;
            }
            
            set
            {
                groupImage.HeaderVisiblePrimary = value;
                panelButtons.Visible = !value;
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool SmallButtons
        {
            get
            {
                return smallButtons;
            }
            
            set
            {
                smallButtons = value;
                if (smallButtons)
                {
                    panelButtons.Size = new Size(31, panelButtons.Height);
                    buttonAdd.Text = string.Empty;
                    buttonDelete.Text = string.Empty;
                    buttonEdit.Text = string.Empty;
                    buttonAddInternet.Text = string.Empty;
                    ResizeButtons(25);
                }
                else
                {
                    ResizeButtons(159);
                    panelButtons.Size = new Size(165, panelButtons.Height);
                    buttonAdd.Text = Strings.Add;
                    buttonDelete.Text = Strings.Delete;
                    buttonEdit.Text = Strings.Edit;
                    buttonAddInternet.Text = Strings.Addinternet;
                }
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowAddButton
        {
            get
            {
                return showAddButton;
            }
            
            set
            {
                if (showAddButton != value)
                {
                    showAddButton = value;
                    buttonAdd.Visible = value;
                    buttonToolAdd.Visible = value;
                }
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowAddInternetButton
        {
            get
            {
                return showAddInternetButton;
            }
            
            set
            {
                if (showAddInternetButton != value)
                {
                    showAddInternetButton = value;
                    buttonAddInternet.Visible = value;
                    buttonToolAddInternet.Visible = value;
                }
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowEditButton
        {
            get
            {
                return showEditButton;
            }
            
            set
            {
                if (showEditButton != value)
                {
                    showEditButton = value;
                    buttonEdit.Visible = value;
                    buttonToolEdit.Visible = value;
                }
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowDeleteButton
        {
            get
            {
                return showDeleteButton;
            }
            
            set
            {
                if (showDeleteButton != value)
                {
                    showDeleteButton = value;
                    buttonDelete.Visible = value;
                    buttonToolDelete.Visible = value;
                }
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
                Header = value;
            }
        }
        
        [Category("Appearance")]
        public string Header
        {
            get { return groupImage.ValuesPrimary.Heading; }
            set { groupImage.ValuesPrimary.Heading = value; }
        }
        
        [Browsable(false)]
        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }
        
        [Browsable(false)]
        public string CurrentImageFileName
        {
            get { return current; }
        }
        
        [Browsable(false)]
        public bool ImageFileChanged
        {
            get { return imageFileChanged; }
        }
        
        [Browsable(false)]
        public bool IsEmptyImageFile
        {
            get { return string.IsNullOrWhiteSpace(current); }
        }
        
        [Category("Behavior")]
        public event EventHandler<StringRequestEventArgs> FileNameRequested;
        
        [Category("Behavior")]
        public event EventHandler<EventArgs> ImageDeleted;
        
        public void ClearImages()
        {
            current = string.Empty;
            images.Clear();
            pictureBox.Image = Resources.Photo;
        }
        
        public void AddImage(string imageFile)
        {
            if (!string.IsNullOrWhiteSpace(imageFile))
            {
                current = imageFile;
                if (!ShowThumbnails)
                {
                    images.Clear();
                }
                
                pictureBox.Image = images.Add(current);
            }
        }
        
        void ResizeButtons(int width)
        {
            foreach (Control c in flowPanelButtons.Controls)
            {
                c.Width = width;
            }
        }
        
        void OnAddImage()
        {
            if (FileNameRequested != null)
            {
                StringRequestEventArgs request = new StringRequestEventArgs();
                FileNameRequested(this, request);
                AddImage(request.RequestValue);
            }
            
            imageFileChanged = true;
        }
        
        void OnDeleteImage()
        {
            ClearImages();
            if (ImageDeleted != null)
            {
                ImageDeleted(this, new EventArgs());
            }
            
            imageFileChanged = true;
        }
        
        void ButtonToolAddClick(object sender, EventArgs e)
        {
            OnAddImage();
        }
        
        void ButtonToolAddInternetClick(object sender, EventArgs e)
        {
        }
        
        void ButtonToolEditClick(object sender, EventArgs e)
        {
        }
        
        void ButtonToolDeleteClick(object sender, EventArgs e)
        {
            OnDeleteImage();
        }
        
        void ButtonToolPrevClick(object sender, EventArgs e)
        {
        }
        
        void ButtonToolNextClick(object sender, EventArgs e)
        {
        }
    }
}
