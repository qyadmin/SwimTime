// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：时间格式转换器
// 作成時間：2017-11-13
// 類を作る：TimeTransform.cs
// 版    本：v 1.0
// 会    社：上海盛赫科技广州分部
// QQと微信：731483140
// ==================================================================

using System;
using System.Globalization;

public class TimeTransform
{
    private static TimeTransform instance;
    private float currTimer;
    private float year;
    private float month;
    private float day;
    private float hours;
    private float minutes;
    private float seconds;
    public TimeTransform()
    {
    }
    public static TimeTransform getInstance()
    {
        if (instance == null)
        {
            instance = new TimeTransform();
        }
        return instance;
    }
    /**根据秒数 0-9填充"0"**/
    public string secondFill(string orgStr, int len = 2, string fillStr = "0")
    {
        string rs = orgStr;
        while (rs.Length < len)
        {
            rs = fillStr + rs;
        }
        return rs;
    }
    public string secondFill(int orgStr, int len = 2, string fillStr = "0")
    {
        return secondFill(orgStr.ToString(), len, fillStr);
    }
    /**根据秒数 算出时间 格式
* 00:00:00
* **/
    public string transSeconds6(int sec, bool showHour = true, bool showSecs = true)
    {
        sec = sec < 0 ? 0 : sec;
        int hour = (int)Math.Floor((double)sec / 3600);
        int min = (int)Math.Floor((double)sec / 60) % 60;
        int secs = (int)Math.Floor((double)sec % 60);
        if (showHour == true || hour > 0)
        {
            return secondFill(hour) +
                ":" + secondFill(min) +
                    (showSecs ? (":" + secondFill(secs)) : "");
        }
        else
        {
            return secondFill(min) +
                (showSecs ? (":" + secondFill(secs)) : "");
        }
    }

    /**根据秒数 算出时间 格式
* 00小时00分00秒
* **/
    public string transSeconds9(int sec)
    {
        sec = sec < 0 ? 0 : sec;
        int hour = (int)Math.Floor((double)sec / 3600);
        int min = (int)Math.Floor((double)sec / 60) % 60;
        int secs = (int)Math.Floor((double)sec % 60);

        return secondFill(hour) + "小时" + secondFill(min) + "分" + secondFill(secs) + "秒";
    }

    /// <summary>
    /// 根据时间戳计算日期： xxx年xx月xx日 xx:xx
    /// </summary>
    public string transSeconds8(ulong sec, string str = "yyyy-MM-dd HH:mm:ss")
    {
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(sec + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);

        DateTime dt = dateTimeStart.Add(toNow);
        string timeStr = dt.ToString(str, DateTimeFormatInfo.CurrentInfo);
        return timeStr;
    }

    /// <summary>
    /// 充值界面  显示开服活动截止时间： xxx年xx月xx日 23时59分 
    /// </summary>
    /// <param name="sec"></param>
    /// <returns></returns>
    public string abortSeconds(int sec)
    {
        DateTime t = DateTime.Now;
        DateTime d1 = DateTime.Parse(t.ToString("每天"));
        DateTime d2 = new DateTime(1970, 1, 1);
        double d = d1.Subtract(d2).TotalMilliseconds;

        d += sec * 1000;

        DateTime time = DateTime.MinValue;
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(d2);
        time = startTime.AddMilliseconds(d);
        return LanUtil.StringProfile(time.ToString("每天"), " 23", "时", "59", "分");
    }

    /**根据秒数 算出时间 格式
* 1天10时20分
* **/
    public string transSeconds1(int HowManySecond, bool showSecond = true)
    {
        if (HowManySecond == 0)
        {
            return "0";
        }
        string ShowStr = "";
        if (HowManySecond >= (86400))
        {
            ShowStr += (HowManySecond / 86400) + "天";
            HowManySecond %= 86400;
        }
        if (HowManySecond >= 3600)
        {
            ShowStr += (HowManySecond / 3600) + "时";
            HowManySecond %= 3600;
        }
        if (HowManySecond >= 60)
        {
            ShowStr += (HowManySecond / 60) + "分";
            HowManySecond %= 60;
        }
        if (HowManySecond > 0 && showSecond == true)
        {
            ShowStr += HowManySecond + "秒";
        }
        return ShowStr;
    }

    /// <summary>
    /// 10时20分
    /// </summary>
    /// <param name="HowManySecond"></param>
    /// <param name="showSecond"></param>
    /// <returns></returns>
    public string transSeconds3(int HowManySecond, bool showSecond = true)
    {
        if (HowManySecond == 0)
        {
            if (showSecond)
            {
                return "0" + "秒";
            }
            else
            {
                return "0" + "分";
            }
        }
        string ShowStr = "";
        if (HowManySecond >= 3600)
        {
            ShowStr += (HowManySecond / 3600) + "时";
            HowManySecond %= 3600;
        }
        if (HowManySecond >= 60)
        {
            ShowStr += (HowManySecond / 60) + "分";
            HowManySecond %= 60;
        }
        if (HowManySecond > 0 && showSecond == true)
        {
            ShowStr += HowManySecond + "秒";
        }
        return ShowStr;
    }

    /// <summary>
    /// 11时00分   0时30分  11时30分
    /// </summary>
    /// <param name="HowManySecond"></param>
    /// <returns></returns>
    public string transSeconds12(int HowManySecond)
    {
        if (HowManySecond == 0)
        {
            return string.Format("{0}" + "时" + "{1}" + "分", 0, 0);
        }
        string ShowStr = "";

        if (HowManySecond >= 3600)
        {
            if (HowManySecond % 3600 != 0)
            {
                ShowStr += (HowManySecond / 3600) + "时";
                HowManySecond %= 3600;
            }
            else
            {
                ShowStr += (HowManySecond / 3600) + "时" + 0 + "分";
                HowManySecond %= 3600;
            }
        }
        else
        {
            ShowStr += 0 + "时";
            HowManySecond %= 3600;
        }
        if (HowManySecond >= 60)
        {
            ShowStr += (HowManySecond / 60) + "分";
            HowManySecond %= 60;
        }
        if (HowManySecond > 0 && HowManySecond < 60)
        {
            ShowStr = string.Format("{0}" + "时" + "{1}" + "分", 0, 0);
        }
        return ShowStr;
    }
    /**根据秒数 算出时间 格式
* 大于1天显示天  大于小时显示小时  大于分钟显示分钟
* **/
    public string transTimeLeft(int HowManySecond)
    {
        if (HowManySecond == 0)
        {
            return "0";
        }
        string ShowStr = "";
        if (HowManySecond >= (86400))
        {
            ShowStr += (HowManySecond / 86400) + "天";
            HowManySecond %= 86400;
            return ShowStr;
        }
        if (HowManySecond >= 3600)
        {
            ShowStr += (HowManySecond / 3600) + "时";
            HowManySecond %= 3600;
            return ShowStr;
        }
        if (HowManySecond >= 60)
        {
            ShowStr += (HowManySecond / 60) + "分";
            HowManySecond %= 60;
            return ShowStr;
        }
        return 0 + "分";

    }



    /**根据秒数 算出时间 格式
* 多于7天显示7天前   少于一天显示xx小时 
* **/
    public string transSeconds2(long HowManySecond)
    {
        if (HowManySecond == 0)
        {
            return "0";
        }
        string ShowStr = "";
        if (HowManySecond >= (86400))
        {
            if ((HowManySecond / 86400) > 7)
            {
                ShowStr = "7天前";
            }
            else
            {
                ShowStr = (int)(HowManySecond / 86400) + "天前";
            }
        }
        else if (HowManySecond >= 3600)
        {
            ShowStr = (int)(HowManySecond / 3600) + "小时前";
        }
        else
        {
            ShowStr = "刚刚";
        }
        return ShowStr;
    }
    /**根据秒数 算出时间 格式
    * 大于1天显示天，小于1天 显示00：00：00
    * **/
    public string transSeconds4(long HowManySecond)
    {
        if (HowManySecond == 0)
        {
            return "0";
        }
        string ShowStr = "";
        if (HowManySecond >= (86400))
        {
            ShowStr = (int)(HowManySecond / 86400) + "天前";
        }
        else
        {
            ShowStr = transSeconds6((int)HowManySecond);
        }
        return ShowStr;
    }

    /// <summary>
    /// 格式为：传进来的时间戳与当前时间戳的日期差距大于1天，则显示 2.13 11：13:14 否则显示 11:13:14
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <returns></returns>
    public static string getFormatNormalTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime thatTime = dtStart.Add(toNow);
        DateTime currentTime = DateTime.Now;
        string minute = thatTime.Minute.ToString();
        string second = thatTime.Second.ToString();
        if (thatTime.Minute < 10)
        {
            minute = "0" + thatTime.Minute;
        }
        if (thatTime.Second < 10)
        {
            second = "0" + thatTime.Second;
        }
        if (currentTime.Year > thatTime.Year)
        {
            return thatTime.Year + "-" + thatTime.Month + "-" + thatTime.Day + " " + thatTime.Hour + ":" + minute + ":" + second;
        }
        if (currentTime.Month > thatTime.Month)
        {
            return thatTime.Month + "-" + thatTime.Day + " " + thatTime.Hour + ":" + minute + ":" + second;
        }
        if (currentTime.Day > thatTime.Day)
        {
            return thatTime.Month + "-" + thatTime.Day + " " + thatTime.Hour + ":" + minute + ":" + second;
        }
        return thatTime.Month + "-" + thatTime.Day + "    " + thatTime.Hour + ":" + minute;
    }

    public string transSeconds11(int sec)
    {
        int hour = (int)Math.Floor((double)sec / 3600);
        int min = (int)Math.Floor((double)sec / 60) % 60;
        int secs = (int)Math.Floor((double)sec % 60);
        int day = hour / 24;
        hour = hour % 24;

        int hourTen = hour / 10;
        int hourSin = hour % 10;

        int minTen = min / 10;
        int minSin = min % 10;

        int secTen = secs / 10;
        int secSin = secs % 10;

        return day.ToString() + "天" + hour.ToString() + "时" + min.ToString() + "分" + secs.ToString() + "秒";

    }

    public string transSeconds13(long sec, string format = null)
    {
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(sec + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);

        DateTime dt = dateTimeStart.Add(toNow);

        if (format == null)
            format = "yyyy年MM月dd日HH时mm分";
        string timeStr = dt.ToString(format, DateTimeFormatInfo.InvariantInfo);
        return timeStr;
    }
    /**根据秒数 算出时间 格式
* 1天10小时20分分钟
* **/
    public string transSeconds15(int HowManySecond, bool showSecond = true)
    {
        if (HowManySecond == 0)
        {
            return "0";
        }
        string ShowStr = "";
        if (HowManySecond >= (86400))
        {
            ShowStr += (HowManySecond / 86400) + "天";
            HowManySecond %= 86400;
        }
        if (HowManySecond >= 3600)
        {
            ShowStr += (HowManySecond / 3600) + "小时";
            HowManySecond %= 3600;
        }
        if (HowManySecond >= 60)
        {
            ShowStr += (HowManySecond / 60) + "分钟";
            HowManySecond %= 60;
        }
        if (HowManySecond > 0 && showSecond == true)
        {
            ShowStr += HowManySecond + "秒";
        }
        return ShowStr;
    }

    /*在线的显示：在线(0)
    1分钟内显示：刚离线(1-59)
    1分钟到1小时内的显示：xx分钟前(60-3599)
    1小时到24小时内的显示：xx小时前(3600-86400)
    一天到一个月内的显示：xx天前(86400-2592000)
    一个月到三个月内的显示：一个月前(2592000-7776000)
    三个月到六个月内的显示：三个月前(7776000-15552000)
    六个月到一年内的显示：半年前(15552000-)
    一年以上的显示：一年前*/
    public string transSeconds14(int sec)
    {
        sec = sec < 0 ? 0 : sec;
        int hour = (int)Math.Floor((double)sec / 3600);
        int min = (int)Math.Floor((double)sec / 60) % 60;
        int secs = (int)Math.Floor((double)sec % 60);

        if (sec == 0)
            return "在线";
        else if (sec < 60)
            return "刚离线";
        else if (sec < 60 * 60)
            return min + "分钟前";
        else if (sec < 60 * 60 * 24)
            return hour + "小时前";
        else if (sec < 60 * 60 * 24 * 30)
            return hour / 24 + "天前";
        else if (sec < 60 * 60 * 24 * 30 * 3)
            return "一个月前";
        else if (sec < 60 * 60 * 24 * 30 * 6)
            return "三个月前";
        else if (sec < 60 * 60 * 24 * 30 * 12)
            return "半年前";
        else
            return "一年前";
    }


    public static string GetUnix()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
}