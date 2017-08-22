using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

static partial class Utilities
{
    public static string FirstLetterToUpper(string str)
    {
        if (str != null)
        {
            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1).ToLower();
            else
                return str.ToUpper();
        }
        return str;
    }

    private static CultureInfo ci = new CultureInfo("en-US");

    public static string ToTitleCase(this string str, TitleCase tcase)
    {
        str = str.ToLower();
        switch (tcase)
        {
            case TitleCase.First:
                var strArray = str.Split(' ');
                if (strArray.Length > 1)
                {
                    strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);
                    return string.Join(" ", strArray);
                }
                break;
            case TitleCase.All:
                return ci.TextInfo.ToTitleCase(str);
            default:
                break;
        }
        return ci.TextInfo.ToTitleCase(str);
    }

    public static string ReplaceTextKeyword(string wholeText, string oldValue, string newValue)
    {
        return wholeText.Replace(oldValue, newValue);
    }

    public enum TitleCase
    {
        First,
        All
    }
}
