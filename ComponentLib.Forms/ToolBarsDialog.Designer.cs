//-----------------------------------------------------------------------
// <copyright file="ToolBarsDialog.Designer.cs" company="Sergey Teplyashin">
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
// <date>31.10.2011</date>
// <time>14:01</time>
// <summary>Defines the ToolBarsDialog class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Forms
{
    partial class ToolBarsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBarsDialog));
            this.buttonArrows1 = new ComponentLib.Controls.ButtonArrows();
            this.listAvailableActions = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.listCurrentActions = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.buttonDefault = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonApply = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonChangeIcon = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.checkText = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.checkImage = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.SuspendLayout();
            // 
            // buttonArrows1
            // 
            this.buttonArrows1.ImageButton = global::ComponentLib.Forms.Resources.arrow_up;
            resources.ApplyResources(this.buttonArrows1, "buttonArrows1");
            this.buttonArrows1.Name = "buttonArrows1";
            this.buttonArrows1.TabStop = false;
            this.buttonArrows1.TransparentBackground = true;
            this.buttonArrows1.ButtonArrowClick += new System.EventHandler<ComponentLib.Controls.ButtonArrowClickEventArgs>(this.ButtonArrows1ButtonArrowClick);
            // 
            // listAvailableActions
            // 
            resources.ApplyResources(this.listAvailableActions, "listAvailableActions");
            this.listAvailableActions.Name = "listAvailableActions";
            this.listAvailableActions.SelectedIndexChanged += new System.EventHandler(this.ListAvailableActionsSelectedIndexChanged);
            // 
            // listCurrentActions
            // 
            resources.ApplyResources(this.listCurrentActions, "listCurrentActions");
            this.listCurrentActions.Name = "listCurrentActions";
            this.listCurrentActions.SelectedIndexChanged += new System.EventHandler(this.ListCurrentActionsSelectedIndexChanged);
            // 
            // kryptonLabel1
            // 
            resources.ApplyResources(this.kryptonLabel1, "kryptonLabel1");
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Values.Text = resources.GetString("kryptonLabel1.Values.Text");
            // 
            // kryptonLabel2
            // 
            resources.ApplyResources(this.kryptonLabel2, "kryptonLabel2");
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Values.Text = resources.GetString("kryptonLabel2.Values.Text");
            // 
            // buttonDefault
            // 
            resources.ApplyResources(this.buttonDefault, "buttonDefault");
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Values.Text = resources.GetString("buttonDefault.Values.Text");
            this.buttonDefault.Click += new System.EventHandler(this.ButtonDefaultClick);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Values.Image")));
            this.buttonOK.Values.Text = resources.GetString("buttonOK.Values.Text");
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Values.Text = resources.GetString("buttonApply.Values.Text");
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Values.Image")));
            this.buttonCancel.Values.Text = resources.GetString("buttonCancel.Values.Text");
            // 
            // buttonChangeIcon
            // 
            resources.ApplyResources(this.buttonChangeIcon, "buttonChangeIcon");
            this.buttonChangeIcon.Name = "buttonChangeIcon";
            this.buttonChangeIcon.Values.Text = resources.GetString("buttonChangeIcon.Values.Text");
            this.buttonChangeIcon.Click += new System.EventHandler(this.ButtonChangeIconClick);
            // 
            // checkText
            // 
            this.checkText.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            resources.ApplyResources(this.checkText, "checkText");
            this.checkText.Name = "checkText";
            this.checkText.Values.Text = resources.GetString("checkText.Values.Text");
            this.checkText.CheckedChanged += new System.EventHandler(this.ChangeStyle);
            // 
            // checkImage
            // 
            this.checkImage.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            resources.ApplyResources(this.checkImage, "checkImage");
            this.checkImage.Name = "checkImage";
            this.checkImage.Values.Text = resources.GetString("checkImage.Values.Text");
            this.checkImage.CheckedChanged += new System.EventHandler(this.ChangeStyle);
            // 
            // ToolBarsDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.checkImage);
            this.Controls.Add(this.checkText);
            this.Controls.Add(this.buttonChangeIcon);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.listCurrentActions);
            this.Controls.Add(this.listAvailableActions);
            this.Controls.Add(this.buttonArrows1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolBarsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox checkImage;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox checkText;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonChangeIcon;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listCurrentActions;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listAvailableActions;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonApply;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonDefault;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentLib.Controls.ButtonArrows buttonArrows1;
    }
}
