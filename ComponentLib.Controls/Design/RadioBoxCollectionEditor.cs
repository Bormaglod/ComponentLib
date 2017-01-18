//-----------------------------------------------------------------------
// <copyright file="RadioBoxCollectionEditor.cs" company="Sergey Teplyashin">
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
// <date>01.06.2012</date>
// <time>15:01</time>
// <summary>Defines the RadioBoxCollectionEditor class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    
    public class RadioBoxCollectionEditor : /*CollectionEditor*/ UITypeEditor
    {
        /*public RadioBoxCollectionEditor(Type type) : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(string);
        }
        
        protected override object CreateInstance(Type itemType)
        {
            return string.Empty;
        }*/
        IWindowsFormsEditorService service;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (service != null)
                {
                    using (FormStringCollection form = new FormStringCollection((ICollection)value))
                    {
                        if (service.ShowDialog(form) == DialogResult.OK)
                        {
                            RadioBox box = (RadioBox)context.Instance;
                            value = new RadioBoxCollection(box, form.Collection);
                        }
                    }
                }
            }
            
            return value;
        }
    }
}
