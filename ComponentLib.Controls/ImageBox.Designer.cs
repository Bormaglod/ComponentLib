///-----------------------------------------------------------------------
/// <copyright file="?.cs" company="Sergey Teplyashin">
///     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
/// </copyright>
/// <author>Тепляшин Сергей Васильевич</author>
/// <email>sergey-teplyashin@yandex.ru</email>
/// <license>
///     This program is free software; you can redistribute it and/or modify
///     it under the terms of the GNU General Public License as published by
///     the Free Software Foundation; either version 3 of the License, or
///     (at your option) any later version.
///
///     This program is distributed in the hope that it will be useful,
///     but WITHOUT ANY WARRANTY; without even the implied warranty of
///     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///     GNU General Public License for more details.
///
///     You should have received a copy of the GNU General Public License
///     along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>
/// <date>07.06.2012</date>
/// <time>15:25</time>
/// <summary>Defines the ? class.</summary>
///-----------------------------------------------------------------------
namespace ComponentLib.Controls
{
    partial class ImageBox
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the control.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageBox));
            this.panelButtons = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonAddInternet = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonEdit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panelMain = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.groupImage = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonToolNext = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonToolPrev = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonToolAdd = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonToolAddInternet = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonToolEdit = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonToolDelete = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.gridImages = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupImage.Panel)).BeginInit();
            this.groupImage.Panel.SuspendLayout();
            this.groupImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridImages)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.flowPanelButtons);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(297, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(165, 319);
            this.panelButtons.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.panelButtons.TabIndex = 0;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.buttonAdd);
            this.flowPanelButtons.Controls.Add(this.buttonAddInternet);
            this.flowPanelButtons.Controls.Add(this.buttonEdit);
            this.flowPanelButtons.Controls.Add(this.buttonDelete);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.Location = new System.Drawing.Point(0, 0);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(165, 319);
            this.flowPanelButtons.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(159, 25);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Values.Image")));
            this.buttonAdd.Values.Text = "Добавить";
            this.buttonAdd.Click += new System.EventHandler(this.ButtonToolAddClick);
            // 
            // buttonAddInternet
            // 
            this.buttonAddInternet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddInternet.Location = new System.Drawing.Point(3, 34);
            this.buttonAddInternet.Name = "buttonAddInternet";
            this.buttonAddInternet.Size = new System.Drawing.Size(159, 25);
            this.buttonAddInternet.TabIndex = 1;
            this.buttonAddInternet.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddInternet.Values.Image")));
            this.buttonAddInternet.Values.Text = "Добавить из интернета";
            this.buttonAddInternet.Click += new System.EventHandler(this.ButtonToolAddInternetClick);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(3, 65);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(159, 25);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Values.Image")));
            this.buttonEdit.Values.Text = "Изменить";
            this.buttonEdit.Click += new System.EventHandler(this.ButtonToolEditClick);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(3, 96);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(159, 25);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Values.Image")));
            this.buttonDelete.Values.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonToolDeleteClick);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupImage);
            this.panelMain.Controls.Add(this.gridImages);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(297, 319);
            this.panelMain.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.panelMain.TabIndex = 1;
            // 
            // groupImage
            // 
            this.groupImage.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonToolNext,
            this.buttonToolPrev,
            this.buttonToolAdd,
            this.buttonToolAddInternet,
            this.buttonToolEdit,
            this.buttonToolDelete});
            this.groupImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupImage.HeaderVisibleSecondary = false;
            this.groupImage.Location = new System.Drawing.Point(0, 0);
            this.groupImage.Name = "groupImage";
            // 
            // groupImage.Panel
            // 
            this.groupImage.Panel.Controls.Add(this.pictureBox);
            this.groupImage.Size = new System.Drawing.Size(297, 214);
            this.groupImage.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.groupImage.StateCommon.HeaderPrimary.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupImage.TabIndex = 2;
            this.groupImage.ValuesPrimary.Heading = "";
            // 
            // buttonToolNext
            // 
            this.buttonToolNext.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Near;
            this.buttonToolNext.Image = global::ComponentLib.Controls.Resources.arrow_right;
            this.buttonToolNext.UniqueName = "3B758ED6CBDF4054DF936EA0E8E431F5";
            this.buttonToolNext.Click += new System.EventHandler(this.ButtonToolNextClick);
            // 
            // buttonToolPrev
            // 
            this.buttonToolPrev.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Near;
            this.buttonToolPrev.Image = global::ComponentLib.Controls.Resources.arrow_left;
            this.buttonToolPrev.UniqueName = "680519A1CC124A7E53BC795B5D35A7A8";
            this.buttonToolPrev.Click += new System.EventHandler(this.ButtonToolPrevClick);
            // 
            // buttonToolAdd
            // 
            this.buttonToolAdd.Image = global::ComponentLib.Controls.Resources.add;
            this.buttonToolAdd.UniqueName = "34CF7A9C0F354C24A4A1030EDDB33A0B";
            this.buttonToolAdd.Click += new System.EventHandler(this.ButtonToolAddClick);
            // 
            // buttonToolAddInternet
            // 
            this.buttonToolAddInternet.Image = global::ComponentLib.Controls.Resources.world_add;
            this.buttonToolAddInternet.UniqueName = "FBE68A0308E64C161BA6BA2B2F4322E6";
            this.buttonToolAddInternet.Click += new System.EventHandler(this.ButtonToolAddInternetClick);
            // 
            // buttonToolEdit
            // 
            this.buttonToolEdit.Image = global::ComponentLib.Controls.Resources.picture_edit;
            this.buttonToolEdit.UniqueName = "4F6C3FCF40DA4EEB478AE864198EAD0F";
            this.buttonToolEdit.Click += new System.EventHandler(this.ButtonToolEditClick);
            // 
            // buttonToolDelete
            // 
            this.buttonToolDelete.Image = global::ComponentLib.Controls.Resources.delete;
            this.buttonToolDelete.UniqueName = "06587519567045465BBCDDF34DA332A3";
            this.buttonToolDelete.Click += new System.EventHandler(this.ButtonToolDeleteClick);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Image = global::ComponentLib.Controls.Resources.Photo;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(295, 187);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // gridImages
            // 
            this.gridImages.AllowUserToAddRows = false;
            this.gridImages.AllowUserToDeleteRows = false;
            this.gridImages.AllowUserToResizeColumns = false;
            this.gridImages.AllowUserToResizeRows = false;
            this.gridImages.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridImages.ColumnHeadersVisible = false;
            this.gridImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.gridImages.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gridImages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridImages.Location = new System.Drawing.Point(0, 214);
            this.gridImages.MultiSelect = false;
            this.gridImages.Name = "gridImages";
            this.gridImages.ReadOnly = true;
            this.gridImages.RowHeadersVisible = false;
            this.gridImages.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2);
            this.gridImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridImages.Size = new System.Drawing.Size(297, 105);
            this.gridImages.StateCommon.Background.Color1 = System.Drawing.Color.Transparent;
            this.gridImages.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.gridImages.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // ImageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Name = "ImageBox";
            this.Size = new System.Drawing.Size(462, 319);
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupImage.Panel)).EndInit();
            this.groupImage.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupImage)).EndInit();
            this.groupImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridImages)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.PictureBox pictureBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonAdd;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonAddInternet;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonEdit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView gridImages;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolDelete;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolEdit;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolAddInternet;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolAdd;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolNext;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonToolPrev;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup groupImage;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelMain;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelButtons;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
    }
}
