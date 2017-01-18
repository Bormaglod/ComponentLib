//-----------------------------------------------------------------------
// <copyright file="UnicodeDescription" company="Тепляшин Сергей Васильевич">
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
// <date>23.11.2011</date>
// <time>10:22</time>
// <summary>Defines the UnicodeDescription class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Text
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    
    public class UnicodeDescription
    {
        enum StringType { Name = 1, Alias = 2, Comment = 3 };

        int code;
        string name;
        List<string> aliases = new List<string>();
        List<string> comments = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbolCode"></param>
        /// <param name="stream"></param>
        /// <exception cref="CodeNotFoundException"></exception>
        public UnicodeDescription(int symbolCode, Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (ReadCode(reader))
                {
                    if (code == symbolCode)
                    {
                        ReadContent(reader);
                        break;
                    }
                    
                    MoveToNextCode(reader);
                }
            }
            
            if (code == -1)
            {
                throw new CodeNotFoundException(string.Format(Strings.CodeNotFound, symbolCode));
            }
        }
        
        public UnicodeDescription(int symbolCode, string symbolName)
        {
            code = symbolCode;
            name = symbolName;
        }
        
        public void AddAlias(string alias)
        {
            aliases.Add(alias);
        }
        
        public void AddComment(string comment)
        {
            comments.Add(comment);
        }
        
        public int Code
        {
            get { return code; }
        }
        
        public string CodeUTF8
        {
            get
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Symbol);
                string res = string.Empty;
                foreach (byte b in bytes)
                {
                    res += string.Format("0x{0} ", b.ToString("X2"));
                }
                
                return res;
            }
        }
        
        public string CodeUTF16
        {
            get { return string.Format("0x{0}", code.ToString("X4")); }
        }
        
        public string Symbol
        {
            get { return code == -1 ? string.Empty : char.ConvertFromUtf32(code); }
        }
        
        public string Name
        {
            get { return name; }
        }

        public IEnumerable<string> Aliases
        {
            get { return aliases; }
        }
        
        public IEnumerable<string> Comments
        {
            get { return comments; }
        }
        
        public void Write(BinaryWriter writer)
        {
            // Код символа
            writer.Write(Code);
            
            // Позиция следующего символа
            long pos = writer.BaseStream.Position;
            writer.Write((long)-1);
            
            // Количество строк
            writer.Write(1 + aliases.Count + comments.Count);
            
            WriteString(writer, StringType.Name, name);
            foreach (string alias in aliases)
            {
                WriteString(writer, StringType.Alias, alias);
            }
            
            foreach (string comment in comments)
            {
                WriteString(writer, StringType.Comment, comment);
            }
            
            long nextPos = writer.BaseStream.Position;
            writer.BaseStream.Seek(pos, SeekOrigin.Begin);
            writer.Write(nextPos);
            writer.BaseStream.Seek(nextPos, SeekOrigin.Begin);
        }
        
        public void Read(BinaryReader reader)
        {
            ReadCode(reader);
            ReadContent(reader);
        }
        
        public override string ToString()
        {
            return string.Format("U+{0} {1}", code.ToString("X4"), name);
        }
        
        void MoveToNextCode(BinaryReader reader)
        {
            long pos = reader.ReadInt64();
            reader.BaseStream.Seek(pos, SeekOrigin.Begin);
        }
        
        bool ReadCode(BinaryReader reader)
        {
            aliases.Clear();
            comments.Clear();
            name = string.Empty;
            code = -1;
            try
            {
                code = reader.ReadInt32();
                return true;
            }
            catch (EndOfStreamException)
            {
                return false;
            }
        }
        
        void ReadContent(BinaryReader reader)
        {
            reader.ReadInt64();
            int countString = reader.ReadInt32();
            for (int i = 0; i < countString; i++)
            {
                StringType type = (StringType)reader.ReadByte();
                int len = reader.ReadInt32();
                byte[] bytes = reader.ReadBytes(len);
                switch (type)
                {
                    case StringType.Name:
                        name = Encoding.ASCII.GetString(bytes);
                        break;
                    case StringType.Alias:
                        string alias = Encoding.ASCII.GetString(bytes);
                        aliases.Add(alias);
                        break;
                    case StringType.Comment:
                        string comment = Encoding.ASCII.GetString(bytes);
                        comments.Add(comment);
                        break;
                }
            }
        }
        
        void WriteString(BinaryWriter writer, StringType type, string text)
        {
            byte[] textBin = Encoding.ASCII.GetBytes(text);
            writer.Write((byte)type);
            writer.Write(textBin.Length);
            writer.Write(textBin);
        }
    }
}
