// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：数据池
// 作成時間：2018-07-30
// 類を作る：DataPool.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPool {

    private static DataPool instance;
     public static DataPool GetInstance()
    {
        if (instance == null)
        {
            instance = new DataPool();
        }
        return instance;
    }

    public EventPatcher<object[]> SendDataMessage = new EventPatcher<object[]>();

    private  Dictionary<string, object> MessageData = new Dictionary<string, object>();
    public  void AddDataItem(string key,object item)
    {
        MessageData.Add(key, item);
    }

    public  object GetDataItem(string key)
    {
        object obj = null;
        MessageData.TryGetValue(key, out obj);
        return obj;
    }
}
