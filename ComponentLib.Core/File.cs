//-----------------------------------------------------------------------
// <copyright file="File.cs" company="Тепляшин Сергей Васильевич">
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
// <date>26.01.2011</date>
// <time>9:54</time>
// <summary>Defines the File class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.IO;
    using System.Globalization;
    using System.Xml;

    /// <summary>
    /// <para>Класс содержит описание файла, в т.ч. полный путь файла, последнее время</para>
    /// <para>доступа, заголовок файла.</para>
    /// </summary>
    public class File : IComparable, IAccess, IXmlData
    {
        /// <summary>
        /// Полный путь файла.
        /// </summary>
        string fileName;
        
        /// <summary>
        /// Initializes a new instance of the File class.
        /// </summary>
        public File()
        {
            fileName = string.Empty;
            AccessTime = DateTime.Now;
        }
        
        /// <summary>
        /// Initializes a new instance of the File class.
        /// </summary>
        /// <param name="fName">Полный путь файла.</param>
        public File(string fName)
        {
            fileName = fName;
            AccessTime = DateTime.Now;
        }
        
        /// <summary>
        /// Initializes a new instance of the File class.
        /// </summary>
        /// <param name="fName">Полный путь файла.</param>
        /// <param name="access">Дата/время доступа к файлу.</param>
        public File(string fName, DateTime access)
        {
            fileName = fName;
            AccessTime = access;
        }
        
        /// <summary>
        /// <para>Gets the file name.</para>
        /// <para>Наименование файла.</para>
        /// </summary>
        public string FileName => fileName;
        
        /// <summary>
        /// <para>Gets the file name without path and extension.</para>
        /// <para>Наименование файла без пути и расширения.</para>
        /// </summary>
        public string ShortFileName => Path.GetFileNameWithoutExtension(fileName);
        
        /// <summary>
        /// <para>Gets or sets the file title.</para>
        /// <para>Заголовок файла.</para>
        /// </summary>
        public string Title { get; set; }
        
        #region IAccess interface implemented
        
        /// <summary>
        /// <para>Gets or sets the file last access date/time.</para>
        /// <para>Дата/время последнего доступа к файлу.</para>
        /// </summary>
        public DateTime AccessTime { get; set; }
        
        #endregion
        
        #region IXmlData interface implemented
        
        public XmlElement CreateXmlElement(XmlDocument document)
        {
            XmlElement item = document.CreateElement("File");
            XmlAttribute attrName = document.CreateAttribute("Name");
            attrName.Value = FileName;
            item.Attributes.Append(attrName);
            
            XmlAttribute attrAccess = document.CreateAttribute("Access");
            attrAccess.Value = AccessTime.ToString(CultureInfo.InvariantCulture);
            item.Attributes.Append(attrAccess);
            
            XmlAttribute attrTitle = document.CreateAttribute("Title");
            attrTitle.Value = Title;
            item.Attributes.Append(attrTitle);
            
            return item;
        }
        
        public void LoadFromXml(XmlNode node)
        {
            fileName = node.Attributes["Name"].Value;
            AccessTime = DateTime.Parse(node.Attributes["Access"].Value, CultureInfo.InvariantCulture);
            Title = node.Attributes["Title"].Value;
        }
        
        #endregion
        
        public static bool operator == (File leftOperand, File rightOperand)
        {
            if (ReferenceEquals(leftOperand, rightOperand))
            {
                return true;
            }
            
            if (ReferenceEquals(leftOperand, null) || ReferenceEquals(rightOperand, null))
            {
                return false;
            }
            
            return leftOperand.Equals(rightOperand);
        }
        
        public static bool operator != (File leftOperand, File rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
        
        public static bool operator < (File leftOperand, File rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) < 0;
        }
        
        public static bool operator > (File leftOperand, File rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) > 0;
        }
        
        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString() => ShortFileName;
        
        #region IComparable interface implemented
        
        /// <summary>
        /// <para>Compares the current instance with another object of the same type</para>
        /// <para>and returns an integer that indicates whether the current instance</para>
        /// <para>precedes, follows, or occurs in the same position in the sort order</para>
        /// <para>as the other object.</para>
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// <para>A 32-bit signed integer that indicates the relative order of the objects being</para>
        /// <para>compared. The return value has these meanings:</para>
        /// </returns>
        public int CompareTo(object obj)
        {
            File file = obj as File;
            if (file != null)
            {
                return AccessTime.CompareTo(file.AccessTime);
            }
            
            throw new ArgumentException("Object is not a File");
        }
        
        #endregion
        
        #region Equals and GetHashCode implementation
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            File other = obj as File;
            return fileName == other.fileName && AccessTime == other.AccessTime;
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (FileName != null)
                {
                    hashCode += 1000000007 * FileName.GetHashCode();
                }
                
                hashCode += 1000000009 * AccessTime.GetHashCode();
            }
            
            return hashCode;
        }
        
        #endregion
    }
}
