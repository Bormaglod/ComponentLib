//-----------------------------------------------------------------------
// <copyright file="OutlookGridAlphabeticGroup.cs" company="Sergey Teplyashin">
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
// <date>27.01.2011</date>
// <time>14:51</time>
// <summary>Defines the OutlookGridAlphabeticGroup class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    
    /// <summary>
    /// Description of OutlookGridAlphabeticGroup.
    /// </summary>
    public class OutlookGridAlphabeticGroup : OutlookgGridDefaultGroup
    {
        public OutlookGridAlphabeticGroup() : base()
        {
        }
        
        public override string Text
        {
            get
            {
                return string.Format("Alphabetic: {1} ({2})", columnView.HeaderText, Value.ToString(), count == 1 ? "1 item" : count.ToString() + " items");
            }
            set { textGroup = value; }
        }

        public override object Value
        {
            get { return val; }
            set { val = value.ToString().Substring(0,1).ToUpper(); }
        }

        #region ICloneable Members
        /// <summary>
        /// each group class must implement the clone function
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            OutlookGridAlphabeticGroup gr = new OutlookGridAlphabeticGroup();
            gr.columnView = this.columnView;
            gr.val = this.val;
            gr.collapsedGroup = this.collapsedGroup;
            gr.textGroup = this.textGroup;
            gr.heightGroup = this.heightGroup;
            return gr;
        }

        #endregion

        #region IComparable Members
        /// <summary>
        /// overide the CompareTo, so only the first character is compared, instead of the whole string
        /// this will result in classifying each item into a letter of the Alphabet.
        /// for instance, this is usefull when grouping names, they will be categorized under the letters A, B, C etc..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            return string.Compare(val.ToString(), obj.ToString().Substring(0, 1).ToUpper());
        }

        #endregion IComparable Members

    }
}
