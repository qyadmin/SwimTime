using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

//泛型分发机制
public class EventPatcher<T>
{
    public Action<T> listener;

    public void Addlistener(Action<T> GetObj)
    {
        listener += GetObj;
    }

    public void Removelistener(Action<T> GetObj)
    {
        listener -= GetObj;
    }

    public void Send(T GetObj)
    {
        if(listener!=null)
        listener.Invoke(GetObj);
    }
    public void ClearAllEevnt()
    {
        while (this.listener != null)
        {
            this.listener -= this.listener;
        }
    }

    //    internal void Addlistener(Action<ReturnHttpMessage> p)
    //    {
    //        throw new NotImplementedException();
    //    }
}


//无参数分发机制
public class EventNoneParamPatcher
{
    public Action listener;

    public void Addlistener(Action GetObj)
    {
        listener += GetObj;
    }

    public void Removelistener(Action GetObj)
    {
        listener -= GetObj;
    }

    public void Send()
    {
        if (listener != null)
            listener.Invoke();
    }

    public void ClearAllEevnt()
    {
        while (this.listener != null)
        {
            this.listener -= this.listener;
        }
    }
}


    public class ConfigTag
{
    public string Tag;
    public EventPatcher<JsonData> Config = new EventPatcher<JsonData>();

    public ConfigTag()
    {
        ConfigManager.GetConfigManager.ServerBody.Addlistener(GetMessage);
        //绑定主体事件
    }

    public void Addself(string Tag,Dictionary<string, EventPatcher<JsonData>> ConfigBody)
    {
        this.Tag = Tag;
        ConfigBody.Add(Tag, Config);
    }

    public void GetMessage(JsonData data)
    {
        JsonData jd = data[Tag];
        //下发配置文件信息
        Config.Send(jd);
    }

}


public class EventConfig
{
    public EventConfig(string name,string Tag,string id)
    {
        this.tagname = name;
        this.id = id;
        //绑定层级事件
        EventPatcher<JsonData> obj = ConfigManager.GetConfigManager.GetBody(Tag);
        if (obj != null)
            obj.Addlistener(GetData);
    }

    private string tagname;//配置文件参数top
    public string SaveMessage;//配置文件消息
    public string id;//配置文件参数id

    //配置文件数据注入完成
    public void GetData(JsonData data)
    {
        SaveMessage = data[tagname + id].ToString();
    }

}


//lvl与color颜色换算
public class GetCL
{
    //获取颜色序号
    public static int GetColorNub(string lvl)
    {
        int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl)/5));
        nub = nub > 3 ? 3 : nub;
        return nub;
    }

    //获取颜色等级.暂时没用
    public static string Getlvlfloor(string lvl)
    {
      
        int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl) / 5));
        nub = nub > 3 ? 3 : nub;
        return ((nub+1)*5).ToString();
    }

    //获取颜色等级.暂时没用
    public static int Getlvlint(string lvl)
    {
        int trynub = 100;
        bool get= int.TryParse(lvl, out trynub);
        if (get)
        {
            lvl = lvl == "0" ? "1" : lvl;
            int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl) / 5));
            nub = nub > 3 ? 3 : nub;
            return ((nub + 1) * 5);
        }
        return trynub;
    }

    public static bool Compare(string V1, string V2)
    {
        return Getlvlint(V1) > Getlvlint(V2) ? true : false;
    }
}