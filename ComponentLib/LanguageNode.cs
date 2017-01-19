//-----------------------------------------------------------------------
// <copyright file="LanguageNode.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2017 Тепляшин Сергей Васильевич. All rights reserved.
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
// <time>13:26</time>
// <summary>Defines the LanguageNode class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Globalization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains language translations used to create multilingual applications
    /// </summary>
    /// <remarks>
    /// The LanguageNode provides a base class for different implementations of a hierachial language structure
    /// </remarks>
    public abstract class LanguageNode
    {
        /// <summary>
        /// An empty language node (used instead of null).
        /// </summary>
        public static EmptyLanguageNode Empty = new EmptyLanguageNode(0);

        int _defaultLCID;
        readonly Dictionary<string, LanguageNode> _subNodes = new Dictionary<string, LanguageNode>();

        /// <summary>
        /// Parent language category
        /// </summary>
        public LanguageNode ParentNode { get; internal set; }

        /// <summary>
        /// All sub categories
        /// </summary>
        public Dictionary<string, LanguageNode> SubNodes => _subNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageNode"/> class.
        /// </summary>
        /// <param name="defaultLCID">The default LCID.</param>
        protected LanguageNode(int defaultLCID)
        {
            _defaultLCID = defaultLCID;
        }

        /// <summary>
        /// Add a localized text string.
        /// </summary>
        /// <param name="lcid">locale</param>
        /// <param name="name">Name identifying the string. Used to fetch the string later on.</param>
        /// <param name="text">Localized string</param>
        public abstract void Add(string name, int lcid, string text);

		/// <summary>
		/// Sets a localized text string. If the a string with the specified name exists it will be overwritten.
		/// </summary>
		/// <param name="lcid">locale</param>
		/// <param name="name">Name identifying the string. Used to fetch the string later on.</param>
		/// <param name="text">Localized string</param>
    	public abstract void Set(string name, int lcid, string text);

        /// <summary>
        /// Adds a sub category
        /// </summary>
        /// <param name="name">Name of the sub category</param>
        /// <exception cref="ArgumentException">If a category with the specified name already exists</exception>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        public abstract LanguageNode AddNode(string name);

        /// <summary>
        /// Retrieves a subcategory
        /// </summary>
        /// <param name="name">The category name</param>
        /// <returns>Null if the category does not exist</returns>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        public LanguageNode GetNode(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return !_subNodes.ContainsKey(name) ? Empty : _subNodes[name];
        }

        /// <summary>
        /// Retrieves a sub node or null if the requested sub node does not exist
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <returns>The named <see cref="LanguageNode"/> or null</returns>
        /// <exception cref="ArgumentNullException">If name is null or empty</exception>
        public LanguageNode GetNodeUnsafe(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return _subNodes.ContainsKey(name) ? _subNodes[name] : null;
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
        public abstract string this[string textName] { get; }

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
        public abstract string this[string textName, int lcid] { get; }

        /// <summary>
        /// Number languages
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Returns the name of the node
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// LCID to use if the specified or current LCID is not found.
        /// </summary>
        public int DefaultLCID => _defaultLCID;

        /// <summary>
        /// Number of translated texts in the specified language
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public abstract int GetTextCount(int lcid);

        /// <summary>
        /// Value that should be returned if the text is not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EmptyValue(string name) => $"[{name}]";

        /// <summary>
        /// Set default locale
        /// </summary>
        /// <param name="lcid">Locale to set.</param>
        internal void SetDefaultLCID(int lcid)
        {
            _defaultLCID = lcid;
        }

        /// <summary>
        /// Determine if a category contains a specific language.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public abstract bool Contains(int lcid);

        /// <summary>Empties all saved values in the node and its sub nodes</summary>
        public abstract void ClearHierarchy();
    }
}
