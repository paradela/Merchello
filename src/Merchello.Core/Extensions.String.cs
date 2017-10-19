﻿namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static partial class Extensions
    {
        /// <summary>
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="value">
        /// The value to replace backslashes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureForwardSlashes(this string value)
        {
            return value.Replace("\\", "/");
        }

        /// <summary>
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="value">
        /// The value to replace forward slashes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureBackSlashes(this string value)
        {
            return value.Replace("/", "\\");
        }

        public static string TrimStart(this string value, string forRemoving)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (string.IsNullOrEmpty(forRemoving)) return value;

            while (value.StartsWith(forRemoving, StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(forRemoving.Length);
            }
            return value;
        }

        public static string EnsureStartsWith(this string input, string toStartWith)
        {
            if (input.StartsWith(toStartWith)) return input;
            return toStartWith + input.TrimStart(toStartWith);
        }

        public static string EnsureStartsWith(this string input, char value)
        {
            return input.StartsWith(value.ToString()) ? input : value + input;
        }

        /// <summary>
        /// Ensures a string both starts and ends with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureStartsAndEndsWith(this string input, char value)
        {
            return input.EnsureStartsWith(value).EnsureEndsWith(value);
        }

        public static string EnsureEndsWith(this string input, char value)
        {
            return input.EndsWith(value.ToString()) ? input : input + value;
        }

        public static string EnsureEndsWith(this string input, string toEndWith)
        {
            return input.EndsWith(toEndWith) ? input : input + toEndWith;
        }

        /// <summary>
        /// Ensures a string does not end with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotEndsWith(this string input, char value)
        {
            return !input.EndsWith(value.ToString()) ? input : input.Remove(input.LastIndexOf(value), 1);
        }

        /// <summary>
        /// Ensures a string does not start with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotStartsWith(this string input, char value)
        {
            return !input.StartsWith(value.ToString()) ? input : input.Remove(0, 1);
        }

        /// <summary>
        /// Ensures a string does not start or end with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotStartsOrEndsWith(this string input, char value)
        {
            return input.EnsureNotStartsWith(value).EnsureNotEndsWith(value);
        }

        public static bool IsLowerCase(this char ch)
        {
            return ch.ToString() == ch.ToString().ToLowerInvariant();
        }

        public static bool IsUpperCase(this char ch)
        {
            return ch.ToString() == ch.ToString().ToUpperInvariant();
        }

        /// <summary>Is null or white space.</summary>
        /// <param name="str">The str.</param>
        /// <returns>The is null or white space.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return (str == null) || (str.Trim().Length == 0);
        }

        public static string IfNullOrWhiteSpace(this string str, string defaultValue)
        {
            return str.IsNullOrWhiteSpace() ? defaultValue : str;
        }

        /// <summary>
        /// formats the string with invariant culture
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string InvariantFormat(this string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Converts an integer to an invariant formatted string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToInvariantString(this int str)
        {
            return str.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToInvariantString(this long str)
        {
            return str.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Compares 2 strings with invariant culture and case ignored
        /// </summary>
        /// <param name="compare">The compare.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns></returns>
        public static bool InvariantEquals(this string compare, string compareTo)
        {
            return String.Equals(compare, compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static bool InvariantStartsWith(this string compare, string compareTo)
        {
            return compare.StartsWith(compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static bool InvariantEndsWith(this string compare, string compareTo)
        {
            return compare.EndsWith(compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static bool InvariantContains(this string compare, string compareTo)
        {
            return compare.IndexOf(compareTo, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool InvariantContains(this IEnumerable<string> compare, string compareTo)
        {
            return compare.Contains(compareTo, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if the string is a Guid
        /// </summary>
        /// <param name="str"></param>
        /// <param name="withHyphens"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str, bool withHyphens)
        {
            var isGuid = false;

            if (!String.IsNullOrEmpty(str))
            {
                Regex guidRegEx;
                if (withHyphens)
                {
                    guidRegEx =
                        new Regex(
                            @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                }
                else
                {
                    guidRegEx =
                        new Regex(
                            @"^(\{{0,1}([0-9a-fA-F]){8}([0-9a-fA-F]){4}([0-9a-fA-F]){4}([0-9a-fA-F]){4}([0-9a-fA-F]){12}\}{0,1})$");
                }
                isGuid = guidRegEx.IsMatch(str);
            }

            return isGuid;
        }

        /// <summary>
        /// Returns a new string in which all occurences of specified strings are replaced by other specified strings.
        /// </summary>
        /// <param name="text">The string to filter.</param>
        /// <param name="replacements">The replacements definition.</param>
        /// <returns>The filtered string.</returns>
        public static string ReplaceMany(this string text, IDictionary<string, string> replacements)
        {
            // be safe
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (replacements == null) throw new ArgumentNullException(nameof(replacements));

            // Have done various tests, implementing my own "super fast" state machine to handle 
            // replacement of many items, or via regexes, but on short strings and not too
            // many replacements (which prob. is going to be our case) nothing can beat this...
            // (at least with safe and checked code -- we don't want unsafe/unchecked here)

            // Note that it will do chained-replacements ie replaced items can be replaced
            // in turn by another replacement (ie the order of replacements is important)

            return replacements.Aggregate(text, (current, kvp) => current.Replace(kvp.Key, kvp.Value));
        }

        /// <summary>
        /// Returns a new string in which all occurences of specified characters are replaced by a specified character.
        /// </summary>
        /// <param name="text">The string to filter.</param>
        /// <param name="chars">The characters to replace.</param>
        /// <param name="replacement">The replacement character.</param>
        /// <returns>The filtered string.</returns>
        public static string ReplaceMany(this string text, char[] chars, char replacement)
        {
            // be safe
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (chars == null) throw new ArgumentNullException(nameof(chars));

            // see note above

            return chars.Aggregate(text, (current, c) => current.Replace(c, replacement));
        }

        internal static string ReplaceNonAlphanumericChars(this string input, char replacement)
        {
            var inputArray = input.ToCharArray();
            var outputArray = new char[input.Length];
            for (var i = 0; i < inputArray.Length; i++) outputArray[i] = char.IsLetterOrDigit(inputArray[i]) ? inputArray[i] : replacement;
            return new string(outputArray);
        }
    }
}