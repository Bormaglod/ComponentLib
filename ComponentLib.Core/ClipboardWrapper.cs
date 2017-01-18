//-----------------------------------------------------------------------
// <copyright file="ClipboardWrapper.cs" company="Daniel Grunwald">
//     Copyright (c) Daniel Grunwald. All rights reserved.
// </copyright>
// <author>Daniel Grunwald</author>
// <email>daniel@danielgrunwald.de</email>
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
// <time>13:05</time>
// <summary>Defines the ClipboardWrapper class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    
    /// <summary>
    /// Helper class to access the clipboard without worrying about ExternalExceptions.
    /// </summary>
    public static class ClipboardWrapper
    {
        // Code duplication: TextAreaClipboardHandler.cs also has SafeSetClipboard
        [ThreadStatic] private static int safeSetClipboardDataVersion;
        
        public static bool ContainsText
        {
            get
            {
                try
                {
                    return Clipboard.ContainsText();
                }
                catch (ExternalException)
                {
                    return false;
                }
            }
        }
        
        public static string GetText()
        {
            // retry 2 times should be enough for read access
            try
            {
                return Clipboard.GetText();
            }
            catch (ExternalException)
            {
                return Clipboard.GetText();
            }
        }
        
        public static void SetText(string text)
        {
            DataObject data = new DataObject();
            data.SetData(DataFormats.UnicodeText, true, text);
            SetDataObject(data);
        }
        
        /// <summary>
        /// Gets the current clipboard content (Can return null).
        /// </summary>
        /// <returns>The IDataObject return.</returns>
        public static IDataObject GetDataObject()
        {
            // retry 2 times should be enough for read access
            try
            {
                return Clipboard.GetDataObject();
            }
            catch (ExternalException)
            {
                try
                {
                    return Clipboard.GetDataObject();
                }
                catch (ExternalException)
                {
                    return null;
                }
            }
        }
        
        public static void SetDataObject(object data)
        {
            SafeSetClipboard(data);
        }
        
        static void SafeSetClipboard(object dataObject)
        {
            // Work around ExternalException bug. (SD2-426)
            // Best reproducable inside Virtual PC.
            int version = unchecked(++safeSetClipboardDataVersion);
            try
            {
                Clipboard.SetDataObject(dataObject, true);
            }
            catch (ExternalException)
            {
                Timer timer = new Timer();
                timer.Interval = 100;
                timer.Tick += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    if (safeSetClipboardDataVersion == version)
                    {
                        try
                        {
                            Clipboard.SetDataObject(dataObject, true, 10, 50);
                        }
                        catch (ExternalException)
                        {
                        }
                    }
                };
                
                timer.Start();
            }
        }
    }
}
