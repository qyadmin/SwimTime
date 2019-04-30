using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Reflection;
using System;
using DG.Tweening;
using UnityEngine.UI;
using Dh_json;
using Newtonsoft.Json;

public abstract class DataBase<T> where T : class, new()
{
    public string _code { get; set; }

    public bool IsSelf(string code)
    {
        return _code == code ? true : false;
    }

    protected bool IsOk = true;

    //protected T Get(object item)
    //{
    //    return JsonMapper.ToObject<T>(JsonMapper.ToJson(item));
    //}
    protected T Get(object item)
    {
        return DataJson.ToObject<T>(JsonMapper.ToJson(item));
    }

    //消息分发器
    public EventPatcher<object> EventObj = new EventPatcher<object>();

    //消息同步
    public abstract void SyncData();

    //消息接收
    public abstract void ReceiveData(object[] data);

    public void ReceiveTag(object[] data)
    {
        if (IsSelf(data[3].ToString()))
            ReceiveData(data);
    }
    //消息更新
    public abstract void Update(object[] data);

}

public enum UpdateCode
{
    Update,
    Add,
    Remove
}

public class DataDic<T> : DataBase<T> where T : class, new()
{
    private Dictionary<string, T> Body = new Dictionary<string, T>();

    public List<T> Data { get { return new List<T>(Body.Values); } }

    private T _currObj;
    private string _currKey;
    private UpdateCode _updateModel;

    public T CurrObj { get { return _currObj; } }
    public string CurrKey { get { return _currKey; } }
    public UpdateCode UpdateModel { get { return _updateModel; } }

    public DataDic(string code)
    {
        _code = code;
        DataPool.GetInstance().SendDataMessage.Addlistener(ReceiveTag);
    }
    public override void SyncData()
    {

        if (IsOk && EventObj != null)
        {
            EventObj.Send(this);
        }
        else
        {
            Debug.Log("数据刷新失败,格式错误或者无绑定事件");
            IsOk = true;
        }
    }

    public T CanMove(object[] item)
    {
        T obj = new T();
        if (!((JsonData)item[0]).IsArray)
        {
            obj = Get(item[0]);
        }
        else
            IsOk = false;
        return obj;
    }

    public void Add(string key, T item)
    {
        Body.Add(key, item);
        _currObj = item;
        _updateModel = UpdateCode.Add;
        SyncData();
    }
    public void Add(object[] item)
    {
        string keyname = item[2].ToString();
        JsonData key = (JsonData)(item[0]);
        Body.Add(key[keyname].ToString(), CanMove(item));
        _currObj = CanMove(item);
        _updateModel = UpdateCode.Add;
    }

    public void Remove(string Key)
    {
        Body.Remove(Key);
        _currKey = Key;
        _updateModel = UpdateCode.Remove;
        SyncData();
    }
    public void Remove(object[] item)
    {
        Body.Remove(item[2].ToString());
        _currKey = item[2].ToString();
        _updateModel = UpdateCode.Remove;
    }

    public T GetItem(string key)
    {
        T obj = null;
        Body.TryGetValue(key, out obj);
        return obj;
    }

    public override void Update(object[] items)
    {
        Body.Clear();
        if (((JsonData)items[0]).IsArray)
        {
            string keyname = items[2].ToString();
            int i = 0;
            foreach (JsonData child in ((JsonData)items[0]))
            {
                T newitem = Get(child);
                string name = (bool)items[4] ? child[keyname].ToString() : i.ToString();
                if (Body.ContainsKey(name))
                    Body.Remove(name);
                Body.Add(name, newitem);
                i++;
            }
        }
        else
        {
            IsOk = false;
            Debug.Log("无法遍历非数组类型");
            Body.Clear();
            SyncData();
            _updateModel = UpdateCode.Update;
        }
    }

    public override void ReceiveData(object[] data)
    {
        Type t = typeof(DataDic<T>);
        MethodInfo mt = t.GetMethod(data[1].ToString(), new Type[] { typeof(object[]) });
        //var obj = System.Activator.CreateInstance(t);
        if (mt == null)
        {
            Debug.Log("没有获取可执行的函数！**" + data[1].ToString());
            IsOk = false;
        }
        else
        {
            mt.Invoke(this, new object[] { data });
        }
        SyncData();
    }

}


public class DataInfo<T> : DataBase<T> where T : class, new()
{
    private List<T> Body = new List<T>();

    public T Data { get { return Body[0]; } }

    public DataInfo(string code)
    {
        // DataPool.GetInstance().AddDataItem(code,this);
        _code = code;
        DataPool.GetInstance().SendDataMessage.Addlistener(ReceiveTag);
    }
    public override void SyncData()
    {
        if (IsOk && EventObj != null)
            EventObj.Send(this);
        else
        {
            Debug.Log("数据刷新失败,格式错误或者无绑定事件");
            IsOk = true;
        }
    }

    public override void Update(object[] items)
    {
        Body.Clear();
        if (!((JsonData)items[0]).IsArray)
        {
            T newitem = Get((JsonData)(items[0]));
            Body.Add(newitem);
        }
        else
        {
            IsOk = false;
            Debug.Log("无法遍历非数组类型");
        }
    }

    public override void ReceiveData(object[] data)
    {
        Type t = typeof(DataInfo<T>);
        MethodInfo mt = t.GetMethod(data[1].ToString(), new Type[] { typeof(object[]) });
        //var obj = System.Activator.CreateInstance(t);
        if (mt == null)
        {
            Debug.Log("没有获取可执行的函数！**" + data[1].ToString());
            IsOk = false;
        }
        else
        {
            mt.Invoke(this, new object[] { data });
        }
        SyncData();
    }
}

[System.Serializable]
public class WindowDotweeCtrl
{
    public List<GameObject> WindowList = new List<GameObject>();

    [SerializeField]
    private SizeType Ani_Type;

    private bool _lock;

    public void OpenByDotween(GameObject go)
    {
        if (_lock)
            return;
        _lock = true;


        switch (Ani_Type)
        {
            case SizeType.FullSize:
                go.transform.localScale = Vector3.one;
                go.SetActive(true);
                go.GetComponent<Image>().enabled = true;
                _lock = false;
                break;
            case SizeType.Scale:
                go.SetActive(true);
                go.GetComponent<Image>().enabled = false;
                go.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .15f).OnComplete(delegate ()
                {
                    go.transform.DOScale(Vector3.one, .15f).OnComplete(delegate ()
                    {
                        go.GetComponent<Image>().enabled = true;
                        _lock = false;
                    });
                });
                break;
            case SizeType.FilledByVerticalAndTop:
                go.transform.localScale = Vector3.one;
                go.SetActive(true);
                go.GetComponent<Image>().enabled = true;
                go.transform.Find("board").GetComponent<Image>().DOFillAmount(1, .35f);
                _lock = false;
                break;
        }
    }

    public void CloseByDotween(GameObject go)
    {
        go.GetComponent<Image>().enabled = false;
        switch (Ani_Type)
        {
            case SizeType.FullSize:
                go.SetActive(false);
                break;
            case SizeType.Scale:
                go.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .15f).OnComplete(delegate ()
                {
                    go.transform.DOScale(Vector3.zero, .15f).OnComplete(delegate ()
                    {
                        go.SetActive(false);
                    });
                });
                break;
            case SizeType.FilledByVerticalAndTop:
                go.transform.Find("board").GetComponent<Image>().fillAmount = 0;
                go.SetActive(false);
                break;
        }
    }
}

public enum SizeType
{
    FullSize,
    Scale,
    FilledByVerticalAndTop
}


public class DicBase<T>
{
    public Dictionary<string, object> Body = new Dictionary<string, object>();

    public T GetTValue(string Key)
    {
        object Obj = null;
        foreach (var child in Body)
        {
            if (child.Key == Key)
                Obj = child.Value;
        }
        return (T)Obj;
    }
    public object GetObjectValue(string Key)
    {
        object Obj = null;
        foreach (var child in Body)
        {
            if (child.Key == Key)
                Obj = child.Value;
        }
        return Obj;
    }

    public void Add(string Key, object Value)
    {
        if (Body.ContainsKey(Key))
            Body.Remove(Key);
        Body.Add(Key, Value);
    }

    public void Remove(string Key)
    {
        if (Body.ContainsKey(Key))
            Body.Remove(Key);
    }

}


public class Dic<U, T> where T : class
{
    public Dictionary<U, T> Body = new Dictionary<U, T>();

    public T GetTValue(U Key)
    {
        T obj = null;
        Body.TryGetValue(Key, out obj);
        return obj;
    }

    public void Add(U Key, T Value)
    {
        if (Body.ContainsKey(Key))
            Body.Remove(Key);
        Body.Add(Key, Value);
    }

    public void Remove(U Key)
    {
        if (Body.ContainsKey(Key))
            Body.Remove(Key);
    }

}


public class DicObject<T> : DicBase<T> where T : UnityEngine.Object
{
    public void Creat(IEnumerable<T> Group)
    {
        foreach (T child in Group)
        {
            Body.Add(child.name, child);
        }
    }
}

public class DicVar<T> : DicBase<T> where T : KeyObj
{
    public void Creat(IEnumerable<T> Group)
    {
        foreach (T child in Group)
        {
            Body.Add(child.key, child);
        }
    }
}


public class DicResources<T> : DicBase<T> where T : UnityEngine.Object
{
    public void LoadAssets(string Key, string path)
    {
        T[] Data = Resources.LoadAll<T>(path);
        Add(Key, Data);
    }

    public T[] GetValueGroup(string key)
    {
        return GetObjectValue(key) as T[];
    }

    public DicObject<T> GetValueDic(string key)
    {
        DicObject<T> ItemBody = new DicObject<T>();
        ItemBody.Creat(GetValueGroup(key));
        return ItemBody;
    }
}


public class DicMode<T> where T : KeyData
{
    private Dictionary<string, T> Body = new Dictionary<string, T>();

    public DicMode(IEnumerable<T> array)
    {
        foreach (T child in array)
        {
            Body.Add(child.key, child);
        }
    }
    public T GetItem(string key)
    {
        T item = null;
        Body.TryGetValue(key, out item);
        return item;
    }

    public string GetTag(string key)
    {
        T item = null;
        Body.TryGetValue(key, out item);
        if (item == null)
            return null;
        return item.key;
    }

    public void Clear()
    {
        foreach (KeyData child in Body.Values)
            child.Clear();
    }
}

[System.Serializable]
public class KeyData
{
    public string key;
    public virtual void Clear()
    {

    }
}

public class KeyObj
{
    public string key;
}


