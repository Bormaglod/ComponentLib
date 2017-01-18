//-----------------------------------------------------------------------
// <copyright file="FileNames.cs" company="Тепляшин Сергей Васильевич">
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
// <date>20.03.2012</date>
// <time>12:55</time>
// <summary>Defines the FileNames class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.IO;
    
    public static class FileNames
    {
        public static string ApplicationDir = @"{0}\ComponentLib";
        public static string CommandsFile = @"{0}\ComponentLib\Commands.xml";
        public static string ToolBarsFile = @"{0}\ComponentLib\ToolBars.xml";
        
        public static string GetCommandsFile()
        {
            #if DEBUG
            string appDir = string.Format(FileNames.ApplicationDir, string.Format(@"C:\Documents and Settings\{0}\Application Data", Environment.UserName));
            string fileName = string.Format(FileNames.CommandsFile, string.Format(@"C:\Documents and Settings\{0}\Application Data", Environment.UserName));
            #else
            string appDir = string.Format(FileNames.ApplicationDir, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string fileName = string.Format(FileNames.CommandsFile, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            #endif
            
            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }
            
            return fileName;
        }
        
        public static string GetToolbarsFile()
        {
            #if DEBUG
            string appDir = string.Format(FileNames.ApplicationDir, string.Format(@"C:\Documents and Settings\{0}\Application Data", Environment.UserName));
            string fileName = string.Format(FileNames.ToolBarsFile, string.Format(@"C:\Documents and Settings\{0}\Application Data", Environment.UserName));
            #else
            string appDir = string.Format(FileNames.ApplicationDir, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string fileName = string.Format(FileNames.ToolBarsFile, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            #endif
            
            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }
            
            return fileName;
        }
    }
}
