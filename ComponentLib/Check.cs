//-----------------------------------------------------------------------
// <copyright file="Check.cs" company="Тепляшин Сергей Васильевич">
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
// <time>13:22</time>
// <summary>Defines the Check class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib
{
    using System;
    using ComponentLib.Globalization;
    
    /// <summary>
    /// Design by contract validator
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Contains language information
        /// </summary>
        static LanguageNode _language = SetupDefaultLanguage();

        static bool isDefaultLanguage = true;

        /// <summary>
        /// "{0} must be specified."
        /// </summary>
        const string FieldRequired = "FieldRequired";

        /// <summary>
        /// "'{0}' do not equal '{1}'."
        /// </summary>
        const string FieldNotEqual = "FieldNotEqual";

        /// <summary>
        /// "'{0}' must be between {1} and {2}."
        /// </summary>
        const string FieldBetween = "FieldBeetween";

        /// <summary>
        /// "'{0}' must be between {1} and {2} characters."
        /// </summary>
        const string FieldBetweenStr = "FieldBeetwenStr";

        /// <summary>
        /// "'{0}' must be larger or equal to {1}."
        /// </summary>
        const string FieldMin = "FieldMin";

        /// <summary>
        /// "'{0}' must be larger or equal to {1}."
        /// </summary>
        const string FieldMinStr = "FieldMinStr";

        /// <summary>
        /// "'{0}' must be less or equal to {1}."
        /// </summary>
        const string FieldMax = "FieldMax";

        /// <summary>
        /// "'{0}' must be less or equal to {1}."
        /// </summary>
        const string FieldMaxStr = "FieldMaxStr";

        /// <summary>
        /// "'{0}' must be specified and not empty."
        /// </summary>
        const string FieldNotEmpty = "FieldNotEmpty";

        /// <summary>
        /// Contains language information
        /// </summary>
        /// <remarks>
        /// Language may only be specified once.
        /// </remarks>
        /// <exception cref="InvalidOperationException">If language have been previously specified.</exception>
        public static LanguageNode Language
        {
            get
            {
                return _language;
            }
            
            set
            {
                if (!isDefaultLanguage)
                {
                    throw new InvalidOperationException("Language may only be set once.");
                }
                
                _language = value;
                isDefaultLanguage = false;
            }
        }

        static LanguageNode SetupDefaultLanguage()
        {
            LanguageNode language = new MemLanguageNode(1033, "Check");
            language.Add(FieldRequired, 1033, "'{0}' is required.");
            language.Add(FieldNotEqual, 1033, "'{0}' do not equal '{1}'.");
            language.Add(FieldBetween, 1033, "'{0}' must be between {1} and {2}.");
            language.Add(FieldBetweenStr, 1033, "'{0}' must be between {1} and {2} characters.");
            language.Add(FieldMin, 1033, "{0} must be larger or equal to {1}.");
            language.Add(FieldMinStr, 1033, "{0} must be larger or equal to {1} characters.");
            language.Add(FieldMax, 1033, "{0} must be less or equal to {1}.");
            language.Add(FieldMaxStr, 1033, "{0} must be less or equal to {1} characters.");
            language.Add(FieldNotEmpty, 1033, "'{0}' must not be empty.");
            return language;
        }

        /// <summary>
        /// Two values can't be equal.
        /// </summary>
        /// <param name="value">value/constant to compare to.</param>
        /// <param name="paramValue">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        /// <remarks><paramref name="value"/> and <paramref name="paramValue"/> are both required.</remarks>
        public static void NotEqual(object value, object paramValue, string messageOrParamName)
        {
            Require(value, "value");
            Require(paramValue, messageOrParamName);
            if (!paramValue.Equals(paramValue))
            {
                return;
            }

            Throw(messageOrParamName, FieldRequired, value.ToString());
        }

        /// <summary>
        /// Value must be between (or equal) min and max
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Between(int min, int max, int value, string messageOrParamName)
        {
            if (value >= min && value <= max)
            {
                return;
            }

            Throw(messageOrParamName, FieldBetween, min.ToString(), max.ToString());
        }

        /// <summary>
        /// Betweens the specified min.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value to check. May not be null.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Between(int min, int max, string value, string messageOrParamName)
        {
           Between(min, max, value, messageOrParamName, true);
        }

        /// <summary>
        /// Betweens the specified min.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <param name="required"><paramref name="value"/> may be null if this parameter is false.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Between(int min, int max, string value, string messageOrParamName, bool required)
        {
            if (required)
            {
                Require(value, messageOrParamName);
            }
            
            if (value == null || value.Length >= min && value.Length <= max)
            {
                return;
            }

            Throw(messageOrParamName, FieldBetweenStr, min.ToString(), max.ToString());
        }

        /// <summary>
        /// Checks if the value is equal or larger.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Min(DateTime min, DateTime value, string messageOrParamName)
        {
            if (value >= min)
            {
                return;
            }

            Throw(messageOrParamName, FieldMin, min.ToString());
        }

        /// <summary>
        /// Checks if the value is equal or larger.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Min(int min, int value, string messageOrParamName)
        {
            if (value >= min)
            {
                return;
            }

            Throw(messageOrParamName, FieldMin, min.ToString());
        }

        /// <summary>
        /// Checks if the value is equal or larger.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="value">parameter value (may not be null).</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Min(int min, string value, string messageOrParamName)
        {
            Min(min, value, messageOrParamName, true);
        }

        /// <summary>
        /// Checks if the value is equal or larger.
        /// </summary>
        /// <param name="min">minimum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <param name="required"><paramref name="value"/> may be null if this parameter is false.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Min(int min, string value, string messageOrParamName, bool required)
        {
            if (required)
            {
                Require(value, messageOrParamName);
            }
            
            if (value == null || value.Length >= min)
            {
                return;
            }

            Throw(messageOrParamName, FieldMinStr, min.ToString());
        }

        /// <summary>
        /// Checks if the value is less or equal.
        /// </summary>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Max(int max, int value, string messageOrParamName)
        {
            if (value <= max)
            {
                return;
            }

            Throw(messageOrParamName, FieldMax, max.ToString());
        }

        /// <summary>
        /// Checks if the value is less or equal.
        /// </summary>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Max(int max, string value, string messageOrParamName)
        {
            Max(max, value, messageOrParamName, true);
        }

        /// <summary>
        /// Checks if the value is less or equal.
        /// </summary>
        /// <param name="max">maximum value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <param name="required"><paramref name="value"/> may be null if this parameter is false.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Max(int max, string value, string messageOrParamName, bool required)
        {
            if (required)
            {
                Require(value, messageOrParamName);
            }
            
            if (value == null || value.Length <= max)
            {
                return;
            }

            Throw(messageOrParamName, FieldMaxStr, max.ToString());
        }

        /// <summary>
        /// Checks if the value is less or equal.
        /// </summary>
        /// <param name="max">max value.</param>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Max(DateTime max, DateTime value, string messageOrParamName)
        {
            if (value <= max)
            {
                return;
            }

            Throw(messageOrParamName, FieldMin, max.ToString());
        }

        /// <summary>
        /// Parameter is required (may not be null).
        /// </summary>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void Require(object value, string messageOrParamName)
        {
            if (value != null)
            {
                return;
            }

            Throw(messageOrParamName, FieldRequired);
        }

        /// <summary>
        /// The specified string may not be null or empty.
        /// </summary>
        /// <param name="value">parameter value.</param>
        /// <param name="messageOrParamName">parameter name, or a error message.</param>
        /// <exception cref="CheckException">If contract fails.</exception>
        public static void NotEmpty(string value, string messageOrParamName)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            Throw(messageOrParamName, FieldNotEmpty);
        }

        static void Throw(string messageOrParamName, string message, params string[] arguments)
        {
            string[] args = new string[arguments.Length+1];
            arguments.CopyTo(args, 1);
            args[0] = messageOrParamName;

            if (messageOrParamName.IndexOf(' ') == -1)
            {
                string format = _language[message] ?? message;
                throw new CheckException(message, string.Format(format, args), args);
            }

            throw new CheckException(message, messageOrParamName, args);
        }
    }

    /// <summary>
    /// Exception thrown when a validation fails.
    /// </summary>
    public class CheckException : ArgumentException
    {
        readonly string _orgString;
        readonly string[] _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckException"/> class.
        /// </summary>
        /// <param name="orgMessage">The original error message (have not been formatted).</param>
        /// <param name="msg">Formatted message.</param>
        /// <param name="arguments">Message arguments.</param>
        internal CheckException(string orgMessage, string msg, string[] arguments)
            : base(msg, arguments[0])
        {
            _orgString = orgMessage ?? string.Empty;
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckException"/> class.
        /// </summary>
        /// <param name="orgMessage">The original error message (have not been formatted).</param>
        /// <param name="msg">Formatted message.</param>
        internal CheckException(string orgMessage, string msg)
            : base(msg)
        {
            _orgString = orgMessage ?? string.Empty;
            _arguments = new string[]{string.Empty};
        }

        /// <summary>
        /// Unformatted error message, {0} have not been replaced with parameter name.
        /// </summary>
        /// <remarks>
        /// Can be used if you want to translate messages.
        /// </remarks>
        public string OrgString
        {
            get { return _orgString; }
        }

        /// <summary>
        /// Arguments to string to format. First argument is parameter name.
        /// </summary>
        public string[] Arguments
        {
            get { return _arguments; }
        }
    }
}