//-----------------------------------------------------------------------
// <copyright file="EmptyLanguageNode.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2016 Тепляшин Сергей Васильевич. All rights reserved.
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
// <date>11.04.2011</date>
// <time>13:28</time>
// <summary>Defines the EmptyLanguageNode class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Globalization
{
    using System;
    
    /// <summary>
    /// This node is returned instead of null.
    /// </summary>
    public class EmptyLanguageNode : LanguageNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyLanguageNode"/> class.
        /// </summary>
        /// <param name="defaultLCID">The default LCID.</param>
        public EmptyLanguageNode(int defaultLCID)
            : base(defaultLCID)
        {
        }

        /// <summary>
        /// Add a localized text string.
        /// </summary>
        /// <param name="lcid">locale</param>
        /// <param name="name">Name identifying the string. Used to fetch the string later on.</param>
        /// <param name="text">Localized string</param>        
        public override void Add(string name, int lcid, string text)
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Sets a localized text string. If the a string with the specified name exists it will be overwritten.
		/// </summary>
		/// <param name="name">Name identifying the string. Used to fetch the string later on.</param>
		/// <param name="lcid">locale</param>
		/// <param name="text">Localized string</param>
		public override void Set(string name, int lcid, string text)
		{
			throw new Exception("The method or operation is not implemented.");
		}

        /// <summary>
        /// Get a localized text string in the current language.
        /// </summary>
        /// <param name="textName">Phrase to find.</param>
        /// <returns>text if found; [textName] if not.</returns>
        /// <example>
        /// <code>
        /// lang["Name"] // => "Name"
        /// lang["Naem"] // => "[Naem]" since it's missing
        /// </code>
        /// </example>
        public override string this[string textName]
        {
            get { return EmptyValue(textName); }
        }

        /// <summary>
        /// Get a localized text string
        /// </summary>
        /// <param name="lcid"></param>
        /// <param name="textName">Phrase to find.</param>
        /// <returns>text if found; [textName] if not.</returns>
        /// <example>
        /// <code>
        /// lang["Name"] // => "Name"
        /// lang["Naem"] // => "[Naem]" since it's missing
        /// </code>
        /// </example>
        public override string this[string textName, int lcid]
        {
            get { return EmptyValue(textName); }
        }

        /// <summary>
        /// Number languages
        /// </summary>
        public override int Count
        {
            get { return 0; }
        }

        /// <summary>
        /// Number of translated texts in the specified language
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public override int GetTextCount(int lcid)
        {
            return 0;
        }

        /// <summary>
        /// Determine if a category contains a specific language.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public override bool Contains(int lcid)
        {
            return false;
        }

        /// <summary>Unimplemented function to fulfill the requirements of <see cref="LanguageNode"/> base class</summary>
        /// <param name="name">The name to add</param>
        public override LanguageNode AddNode(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>Empties all saved values in the node and its sub nodes</summary>
        public override void ClearHierarchy()
        {
            throw new NotImplementedException();
        }
    }
}
