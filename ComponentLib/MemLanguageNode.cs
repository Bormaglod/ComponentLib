//-----------------------------------------------------------------------
// <copyright file="MemLanguageNode.cs" company="Тепляшин Сергей Васильевич">
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
// <time>13:29</time>
// <summary>Defines the MemLanguageNode class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    
    /// <summary>
    /// Contains translation texts that have been loaded into memory
    /// </summary>
    public class MemLanguageNode : LanguageNode
    {
        /// <summary>
        /// int is LCID, NameValueCollection contains all phrases.
        /// </summary>
        readonly Dictionary<int, NameValueCollection> _languages = new Dictionary<int, NameValueCollection>();

        /// <summary>Instantiates a LanguageNode that holds the language data in memory</summary>
        /// <param name="defaultLCID">The defualt language code</param>
        /// <param name="name">The name of the node</param>
        public MemLanguageNode(int defaultLCID, string name) : base(defaultLCID)
        {
            Name = name;
        }

        /// <summary>Returns the requested entry using the systems current language</summary>
        /// <param name="textName">Name of the entry to retrieve</param>
        /// <returns>The entry or if not found in the current language, default language or in parent nodes returns [textName]</returns>
        public override string this[string textName]
        {
            get
            {
                int lcId = Thread.CurrentThread.CurrentCulture.LCID;
                return this[textName, lcId];
            }
        }
		
        /// <summary>
        /// A requested text entry in the requested language
        /// </summary>
        /// <param name="textName">Name of the entry to retrieve</param>
        /// <param name="lcid">Language code for the language to retrieve the text in</param>
        /// <returns>The entry or if not found in the current language, default language or in parent nodes returns [textName]</returns>
        public override string this[string textName, int lcid]
        {
            get
            {
                lock (_languages)
                {
                    if (!_languages.ContainsKey(lcid))
                    {
                        if (_languages.ContainsKey(DefaultLCID))
                        {
                            return this[textName, DefaultLCID];
                        }
                        
                        return ParentNode != null ? ParentNode[textName, lcid] : string.Format("[{0}]", textName);
                    }

                    if (ParentNode != null && textName.StartsWith("../"))
                    {
                        return ParentNode[textName.Remove(0, 3), lcid];
                    }
                        
                    if (_languages[lcid][textName] == null && ParentNode != null)
                    {
                        return ParentNode[textName, lcid];
                    }

                    return _languages[lcid][textName] ?? string.Format("[{0}]", textName);
                }
            }
        }

        /// <summary>
        /// Returns the number of languages in the node
        /// </summary>
        /// <remarks>Returns the number of languages and Not text entries</remarks>
        public override int Count
        {
            get { return _languages.Count; }
        }

        /// <summary>
        /// Adds a text entry to the language node
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <param name="lcid">Language of the entry</param>
        /// <param name="text">The text of the entry</param>
        public override void Add(string name, int lcid, string text)
        {
            lock (_languages)
            {
                if (!_languages.ContainsKey(lcid))
                {
                    _languages.Add(lcid, new NameValueCollection());
                }

				if (_languages[lcid][name] != null)
				{
				    throw new InvalidOperationException("Value '" + name + "' already added.");
				}

                _languages[lcid].Add(name, text);
            }
        }

		/// <summary>
		/// Sets a localized text string. If the a string with the specified name exists it will be overwritten.
		/// </summary>
		/// <param name="name">Name identifying the string. Used to fetch the string later on.</param>
		/// <param name="lcid">locale</param>
		/// <param name="text">Localized string</param>
		public override void Set(string name, int lcid, string text)
		{
			lock (_languages)
			{
				if (!_languages.ContainsKey(lcid))
				{
				    _languages.Add(lcid, new NameValueCollection());
				}

				_languages[lcid].Set(name, text);
			}	
		}

        /// <summary>
        /// Adds a sub category
        /// </summary>
        /// <param name="name">Name of the sub category</param>
        /// <exception cref="ArgumentException">If a category with the specified name already exists</exception>
        /// <exception cref="ArgumentNullException">If name is null</exception>
        public override LanguageNode AddNode(string name)
        {
            if (SubNodes.ContainsKey(name))
            {
                throw new ArgumentException("A category with specified name already exists.", "name");
            }
            
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            lock(SubNodes)
            {
                SubNodes.Add(name, new MemLanguageNode(DefaultLCID, name));
                SubNodes[name].ParentNode = this;

                return SubNodes[name];
            }
        }

        /// <summary>
        /// Returns number of text entries in the requested language
        /// </summary>
        /// <param name="lcid">The language to examine</param>
        public override int GetTextCount(int lcid)
        {
            lock (_languages)
            {
                return _languages.ContainsKey(lcid) ? _languages[lcid].Count : 0;
            }
        }

        /// <summary>
        /// Determine if a category contains a specific language.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public override bool Contains(int lcid)
        {
            lock (_languages)
            {
                return _languages.ContainsKey(lcid);
            }
        }

        /// <summary>Empties all saved values in the node and its subnodes</summary>
        public override void ClearHierarchy()
        {
            _languages.Clear();
            foreach (KeyValuePair<string, LanguageNode> child in SubNodes)
            {
                child.Value.ClearHierarchy();
            }
        }
    }
}
