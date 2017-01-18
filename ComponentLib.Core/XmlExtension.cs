//-----------------------------------------------------------------------
// <copyright file="XmlExtension.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
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
// <date>14.03.2012</date>
// <time>9:58</time>
// <summary>Defines the XmlExtension class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core.Xml
{
    #region Using directives
    
    using System;
    using System.Xml;
    
    #endregion
    
    /// <summary>
    /// Description of XmlExtension.
    /// </summary>
    public static class XmlExtension
    {
        /// <summary>
        /// Метод добавляет атрибут в элемент Xml
        /// </summary>
        /// <param name="doc">Xml документ, которому принадлежит element</param>
        /// <param name="element">Элемент в который добавляется атрибут.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        public static void AddAttribute(this XmlDocument doc, XmlNode element, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                XmlAttribute attr = doc.CreateAttribute(name);
                attr.Value = value;
                element.Attributes.Append(attr);
            }
        }
        
        public static void AddAttribute(this XmlDocument doc, XmlNode element, XmlAttributeElement attribute)
        {
            if (!attribute.Checked)
            {
                AddAttribute(doc, element, attribute.Name, attribute.Value);
            }
        }
        
        public static void AddAttributes(this XmlDocument doc, XmlNode element, XmlAttributeElement[] attributes)
        {
            foreach (XmlAttributeElement attr in attributes)
            {
                AddAttribute(doc, element, attr);
            }
        }
        
        public static string AttributeValueOrDefault(this XmlNode node, string attributeName)
        {
            if (node.Attributes[attributeName] == null)
            {
                return string.Empty;
            }
            else
            {
                return node.Attributes[attributeName].Value;
            }
        }
        
        public static string AttributeValueAsString(this XmlNode node, string attributeName)
        {
            return node.Attributes[attributeName].Value;
        }
        
        public static bool AttributeValueAsBool(this XmlNode node, string attributeName)
        {
            return bool.Parse(node.Attributes[attributeName].Value);
        }
        
        public static bool AttributeValueAsBool(this XmlNode node, string attributeName, bool defaultValue)
        {
            XmlAttribute attr = node.Attributes[attributeName];
            if (attr == null)
            {
                return defaultValue;
            }
            else
            {
                return bool.Parse(attr.Value);
            }
        }
        
        public static int AttributeValueAsInt(this XmlNode node, string attributeName)
        {
            return int.Parse(node.Attributes[attributeName].Value);
        }
        
        public static int AttributeValueAsInt(this XmlNode node, string attributeName, int defaultValue)
        {
            XmlAttribute attr = node.Attributes[attributeName];
            if (attr == null)
            {
                return defaultValue;
            }
            else
            {
                return int.Parse(attr.Value);
            }
        }
        
        [Obsolete]
        public static XmlElement CreateElement(this XmlDocument doc, XmlNode root, string name, XmlAttributeElement[] attributes)
        {
            XmlElement elem = doc.CreateElement(name);
            if (attributes != null)
            {
                AddAttributes(doc, elem, attributes);
            }
            
            if (root == null)
            {
                doc.AppendChild(elem);
            }
            else
            {
                root.AppendChild(elem);
            }
            
            return elem;
        }
        
        [Obsolete]
        public static XmlElement CreateElement(this XmlDocument doc, XmlNode root, string name, XmlAttributeElement attribute)
        {
            if (attribute == null)
            {
                return CreateElement(doc, root, name, new XmlAttributeElement[] {});
            }
            else
            {
                return CreateElement(doc, root, name, new XmlAttributeElement[] { attribute });
            }
        }
        
        [Obsolete]
        public static XmlElement CreateElement(this XmlDocument doc, XmlNode root, string name, string attrName, string attrValue)
        {
            return CreateElement(doc, root, name, new XmlAttributeElement(attrName, attrValue));
        }
        
        [Obsolete]
        public static XmlElement CreateElement(this XmlDocument doc, XmlElement root, string name)
        {
            return CreateElement(doc, root, name, (XmlAttributeElement)null);
        }
        
        [Obsolete]
        public static XmlElement CreateElement(this XmlDocument doc, XmlNode root, string name, string value)
        {
            XmlElement elem = doc.CreateElement(name);
            XmlText text = doc.CreateTextNode(value);
            elem.AppendChild(text);
            root.AppendChild(elem);
            return elem;
        }
        
        [Obsolete]
        public static XmlElement CreateOrReplaceElement(this XmlDocument doc, XmlNode root, string name, string value)
        {
            XmlElement elem = doc.CreateElement(name);
            XmlText text = doc.CreateTextNode(value);
            elem.AppendChild(text);
            
            XmlNode node = root.SelectSingleNode(name);
            if (node == null)
            {
                root.AppendChild(elem);
            }
            else
            {
                root.ReplaceChild(elem, node);
            }
            
            return elem;
        }
        
        public static string GetSingleValueNode(this XmlNode node, string xpath)
        {
            XmlNode n = node.SelectSingleNode(xpath);
            return n == null ? string.Empty : n.InnerText;
        }
        
        static void AddItem(XmlDocument doc, XmlElement root, XmlElement elem)
        {
            if (root == null)
            {
                doc.AppendChild(elem);
            }
            else
            {
                root.AppendChild(elem);
            }
        }
        
        public static XmlElement CreateItem(this XmlDocument doc, XmlElement root, string name)
        {
            XmlElement elem = doc.CreateElement(name);
            AddItem(doc, root, elem);
            return elem;
        }
        
        public static XmlElement CreateItem(this XmlDocument doc, XmlElement root, string name, string value)
        {
            XmlElement elem = doc.CreateElement(name);
            XmlText text = doc.CreateTextNode(value);
            elem.AppendChild(text);
            AddItem(doc, root, elem);
            return elem;
        }
        
        public static XmlElement CreateItem(this XmlDocument doc, XmlElement root, string name, XmlAttributeElement[] attributes)
        {
            XmlElement elem = doc.CreateElement(name);
            if (attributes != null)
            {
                AddAttributes(doc, elem, attributes);
            }
            
            AddItem(doc, root, elem);
            return elem;
        }
        
        public static XmlElement CreateItem(this XmlDocument doc, XmlElement root, string name, XmlAttributeElement attribute)
        {
            if (attribute == null)
            {
                return CreateItem(doc, root, name, new XmlAttributeElement[] {});
            }
            else
            {
                return CreateItem(doc, root, name, new XmlAttributeElement[] { attribute });
            }
        }
        
        public static XmlElement CreateItem(this XmlDocument doc, XmlElement root, string name, string attrName, string attrValue)
        {
            return CreateItem(doc, root, name, new XmlAttributeElement(attrName, attrValue));
        }
        
        public static XmlElement CreateOrReplaceItem(this XmlDocument doc, XmlElement root, string name, string value)
        {
            XmlElement elem = doc.CreateElement(name);
            XmlText text = doc.CreateTextNode(value);
            elem.AppendChild(text);
            
            XmlNode node = root.SelectSingleNode(name);
            if (node == null)
            {
                root.AppendChild(elem);
            }
            else
            {
                root.ReplaceChild(elem, node);
            }
            
            return elem;
        }
    }
}
