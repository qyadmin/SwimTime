// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：圆形图片工具属性设置
// 作成時間：2017-08-14
// 類を作る：SetPropertyUtilityExt.cs
// 版    本：v 1.0
// 会    社：广州恩赐方信息科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;

/// <summary>
/// 圆形图片工具属性设置
/// </summary>
internal static class SetPropertyUtilityExt
{
    /// <summary>
    /// 颜色设置
    /// </summary>
    /// <param name="currentValue">当前颜色参数</param>
    /// <param name="newValue">新颜色参数</param>
    /// <returns></returns>
    public static bool SetColor(ref Color currentValue, Color newValue)
    {
        if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
            return false;

        currentValue = newValue;
        return true;
    }

    /// <summary>
    /// 设置结构
    /// </summary>
    /// <typeparam name="T">待定类型</typeparam>
    /// <param name="currentValue">当前颜色参数</param>
    /// <param name="newValue">新颜色参数</param>
    /// <returns></returns>
    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
        if (currentValue.Equals(newValue))
            return false;

        currentValue = newValue;
        return true;
    }

    /// <summary>
    /// 设置类
    /// </summary>
    /// <typeparam name="T">待定类型</typeparam>
    /// <param name="currentValue">当前颜色参数</param>
    /// <param name="newValue">新颜色参数</param>
    /// <returns></returns>
    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
        if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
            return false;

        currentValue = newValue;
        return true;
    }
}

