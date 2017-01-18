//-----------------------------------------------------------------------
// <copyright file="ShortcutsDialog.Designer.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
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
// <time>14:09</time>
// <summary>Defines the ShortcutsDialog class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Forms
{
    partial class ShortcutsDialog
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortcutsDialog));
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labelDefault = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.buttonShortcutDefault = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.textShortcut = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecAny2 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.textFilter = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.gridShortcuts = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ColumnCommand = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColumnName = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ColumnShortcuts = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.buttonDefault = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShortcuts)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            resources.ApplyResources(this.kryptonGroupBox1, "kryptonGroupBox1");
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            resources.ApplyResources(this.kryptonGroupBox1.Panel, "kryptonGroupBox1.Panel");
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.labelDefault);
            this.kryptonGroupBox1.Panel.Controls.Add(this.buttonShortcutDefault);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.textShortcut);
            this.kryptonGroupBox1.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.kryptonGroupBox1.Values.Description = resources.GetString("kryptonGroupBox1.Values.Description");
            this.kryptonGroupBox1.Values.Heading = resources.GetString("kryptonGroupBox1.Values.Heading");
            this.kryptonGroupBox1.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonGroupBox1.Values.ImageTransparentColor")));
            // 
            // kryptonLabel4
            // 
            resources.ApplyResources(this.kryptonLabel4, "kryptonLabel4");
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Values.ExtraText = resources.GetString("kryptonLabel4.Values.ExtraText");
            this.kryptonLabel4.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonLabel4.Values.ImageTransparentColor")));
            this.kryptonLabel4.Values.Text = resources.GetString("kryptonLabel4.Values.Text");
            // 
            // labelDefault
            // 
            resources.ApplyResources(this.labelDefault, "labelDefault");
            this.labelDefault.Name = "labelDefault";
            this.labelDefault.Values.ExtraText = resources.GetString("labelDefault.Values.ExtraText");
            this.labelDefault.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("labelDefault.Values.ImageTransparentColor")));
            this.labelDefault.Values.Text = resources.GetString("labelDefault.Values.Text");
            // 
            // buttonShortcutDefault
            // 
            resources.ApplyResources(this.buttonShortcutDefault, "buttonShortcutDefault");
            this.buttonShortcutDefault.Name = "buttonShortcutDefault";
            this.buttonShortcutDefault.Values.ExtraText = resources.GetString("buttonShortcutDefault.Values.ExtraText");
            this.buttonShortcutDefault.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonShortcutDefault.Values.ImageTransparentColor")));
            this.buttonShortcutDefault.Values.Text = resources.GetString("buttonShortcutDefault.Values.Text");
            this.buttonShortcutDefault.Click += new System.EventHandler(this.ButtonShortcutDefaultClick);
            // 
            // kryptonLabel2
            // 
            resources.ApplyResources(this.kryptonLabel2, "kryptonLabel2");
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Values.ExtraText = resources.GetString("kryptonLabel2.Values.ExtraText");
            this.kryptonLabel2.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonLabel2.Values.ImageTransparentColor")));
            this.kryptonLabel2.Values.Text = resources.GetString("kryptonLabel2.Values.Text");
            // 
            // textShortcut
            // 
            resources.ApplyResources(this.textShortcut, "textShortcut");
            this.textShortcut.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
                                    this.buttonSpecAny2});
            this.textShortcut.Name = "textShortcut";
            this.textShortcut.ReadOnly = true;
            this.textShortcut.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.KryptonTextBox1PreviewKeyDown);
            // 
            // buttonSpecAny2
            // 
            resources.ApplyResources(this.buttonSpecAny2, "buttonSpecAny2");
            this.buttonSpecAny2.UniqueName = "B4D5BEF9E35F49F3F8B6884EA43BEE1D";
            this.buttonSpecAny2.Click += new System.EventHandler(this.ButtonSpecAny2Click);
            // 
            // kryptonLabel1
            // 
            resources.ApplyResources(this.kryptonLabel1, "kryptonLabel1");
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Values.ExtraText = resources.GetString("kryptonLabel1.Values.ExtraText");
            this.kryptonLabel1.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("kryptonLabel1.Values.ImageTransparentColor")));
            this.kryptonLabel1.Values.Text = resources.GetString("kryptonLabel1.Values.Text");
            // 
            // textFilter
            // 
            resources.ApplyResources(this.textFilter, "textFilter");
            this.textFilter.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
                                    this.buttonSpecAny1});
            this.textFilter.Name = "textFilter";
            this.textFilter.TextChanged += new System.EventHandler(this.TextFilterTextChanged);
            // 
            // buttonSpecAny1
            // 
            resources.ApplyResources(this.buttonSpecAny1, "buttonSpecAny1");
            this.buttonSpecAny1.UniqueName = "6647DA9F506A41683B98B97165483A90";
            this.buttonSpecAny1.Click += new System.EventHandler(this.ButtonSpecAny1Click);
            // 
            // gridShortcuts
            // 
            resources.ApplyResources(this.gridShortcuts, "gridShortcuts");
            this.gridShortcuts.AllowUserToAddRows = false;
            this.gridShortcuts.AllowUserToDeleteRows = false;
            this.gridShortcuts.AllowUserToResizeRows = false;
            this.gridShortcuts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                                    this.ColumnCommand,
                                    this.ColumnName,
                                    this.ColumnShortcuts});
            this.gridShortcuts.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.gridShortcuts.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlStandalone;
            this.gridShortcuts.MultiSelect = false;
            this.gridShortcuts.Name = "gridShortcuts";
            this.gridShortcuts.ReadOnly = true;
            this.gridShortcuts.RowHeadersVisible = false;
            this.gridShortcuts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridShortcuts.SelectionChanged += new System.EventHandler(this.GridShortcutsSelectionChanged);
            // 
            // ColumnCommand
            // 
            resources.ApplyResources(this.ColumnCommand, "ColumnCommand");
            this.ColumnCommand.Name = "ColumnCommand";
            this.ColumnCommand.ReadOnly = true;
            // 
            // ColumnName
            // 
            resources.ApplyResources(this.ColumnName, "ColumnName");
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            // 
            // ColumnShortcuts
            // 
            this.ColumnShortcuts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.ColumnShortcuts, "ColumnShortcuts");
            this.ColumnShortcuts.Name = "ColumnShortcuts";
            this.ColumnShortcuts.ReadOnly = true;
            // 
            // buttonDefault
            // 
            resources.ApplyResources(this.buttonDefault, "buttonDefault");
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Values.ExtraText = resources.GetString("buttonDefault.Values.ExtraText");
            this.buttonDefault.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonDefault.Values.ImageTransparentColor")));
            this.buttonDefault.Values.Text = resources.GetString("buttonDefault.Values.Text");
            this.buttonDefault.Click += new System.EventHandler(this.ButtonDefaultClick);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Values.ExtraText = resources.GetString("buttonOK.Values.ExtraText");
            this.buttonOK.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Values.Image")));
            this.buttonOK.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonOK.Values.ImageTransparentColor")));
            this.buttonOK.Values.Text = resources.GetString("buttonOK.Values.Text");
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Values.ExtraText = resources.GetString("buttonCancel.Values.ExtraText");
            this.buttonCancel.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Values.Image")));
            this.buttonCancel.Values.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("buttonCancel.Values.ImageTransparentColor")));
            this.buttonCancel.Values.Text = resources.GetString("buttonCancel.Values.Text");
            // 
            // ShortcutsDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.gridShortcuts);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.kryptonLabel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortcutsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridShortcuts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelDefault;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView gridShortcuts;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColumnName;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonShortcutDefault;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonDefault;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textShortcut;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColumnShortcuts;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ColumnCommand;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}
