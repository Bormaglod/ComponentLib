//-----------------------------------------------------------------------
// <copyright file="Culture.cs" company="Тепляшин Сергей Васильевич">
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
// <date>28.12.2010</date>
// <time>20:32</time>
// <summary>Defines the Culture class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Globalization
{
    using System;

    public class Culture : IComparable<Culture>
    {
        
        public Culture(string nameCulture, string localeCulture, int cultureIdentifier)
        {
            Name = nameCulture;
            Locale = localeCulture;
            Id = cultureIdentifier;
        }
        
        public string Name { get; set; }

        public string Locale { get; set; }

        public int Id { get; set; }
        
        public static bool operator == (Culture leftOperand, Culture rightOperand)
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
        
        public static bool operator != (Culture leftOperand, Culture rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
        
        public static bool operator < (Culture leftOperand, Culture rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) < 0;
        }
        
        public static bool operator > (Culture leftOperand, Culture rightOperand)
        {
            return leftOperand.CompareTo(rightOperand) > 0;
        }
        
        public Culture Clone() => new Culture(Name, Locale, Id);
        
        public int CompareTo(Culture other) => string.Compare(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
        
        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            Culture other = obj as Culture;
            if (other == null)
            {
                return false;
            }
            
            return Name == other.Name && Locale == other.Locale && Id == other.Id;
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (Name != null)
                {
                    hashCode += 1000000007 * Name.GetHashCode();
                }
                
                if (Locale != null)
                {
                    hashCode += 1000000009 * Locale.GetHashCode();
                }
                
                hashCode += 1000000021 * Id.GetHashCode();
            }
            
            return hashCode;
        }
        
        #endregion

        public override string ToString() => Name;
    }
}
