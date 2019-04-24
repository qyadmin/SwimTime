// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：常量字符串填充工具
// 作成時間：2018-08-01
// 類を作る：LanUtil.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections.Generic;
using System.Text;

public class LanUtil
{
    private static int index;
    public static int getStrLen(string str)
    {
        if (str == null)
        {
            return 0;
        }
        int now = 0;
        for (int i = 0; i < str.Length; i++)
        {
            now += str[i] < 255 ? 1 : 2;
        }
        return now;
    }


    public static bool isNumber(string str)
    {
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsNumber(chars[i]) == false)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 多个字符串连接的话，用这个方法会好一些-----yann----
    /// </summary>
    /// <param name="arr"></param>
    /// <returns>string</returns>
    public static string StringProfile(params string[] arr)
    {
        abc2.Remove(0, abc2.Length);
        int tLength = arr.Length;
        for (int i = 0; i < tLength; i++)
        {
            abc2.Append(arr[i]);
        }
        return abc2.ToString();
    }
    static StringBuilder abc1 = new StringBuilder();
    static StringBuilder abc2 = new StringBuilder();
    static StringBuilder abc3 = new StringBuilder();
    static StringBuilder abc4 = new StringBuilder();
    public static string formateStr(params string[] arr)
    {
        abc1.Remove(0, abc1.Length);
        int tLength = arr.Length;
        for (int i = 0; i < tLength; i++)
        {
            if (i == 0)
            {
                abc1.Append(arr[i]);
            }
            else
            {
                abc1.Replace(StringProfile("{", i.ToString(), "}"), arr[i]);
            }
        }
        return abc1.ToString();
    }

    public static string replaceStr(string content,string sourceStr,string targetStr)
    {
        return  content.Replace(sourceStr, targetStr);
    }
    public static string formateStr(string str, Dictionary<string, string> dic)
    {
        abc3.Remove(0, abc3.Length);
        abc3.Append(str);
        foreach (var timeItem in dic)
        {
            if (timeItem.Key != null && timeItem.Value != null)
            {
                abc3.Replace(StringProfile("{", timeItem.Key, "}"), timeItem.Value);
            }
        }
        return abc3.ToString();
    }
    public static string formateStr(string str, Dictionary<string, object> dic)
    {
        abc4.Remove(0, abc4.Length);
        abc4.Append(str);
        foreach (var timeItem in dic)
        {
            if (timeItem.Key != null && timeItem.Value != null)
            {
                abc4.Replace(StringProfile("{", timeItem.Key, "}"), timeItem.Value.ToString());
            }
        }
        return abc4.ToString();
    }
}