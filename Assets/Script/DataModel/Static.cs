using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System;
public class Static
{
    private static Static instance;
    public static Static Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Static();
                //instance.SetDataLoop();
            }
            return instance;
        }
    }

    public GameSetting SetData;

    public void SetDataLoop()
    {
        SetData = Resources.LoadAll<GameSetting>("")[0] as GameSetting;
        this.Lock = SetData.Md5Losck;
        this.Level = SetData.Level;
        this.SaveName = SetData.SaveName;
        this.IsDebug = SetData.IsDebug;

    }

    public bool Getjwt()
    {
        string jwt = null;
        jwt = GetLoadFile("jwt.text");
        if (jwt == null)
        {
            Debug.Log("没有找到JWT");
            return false;
        }
        return true;
    }


    public string GameData = string.Empty;

    private ShowMessage_Http GetShowModle;


    public string jwt;
    public bool IsDebug = false;
    public string Level = "0.0.0";
    public bool Lock = false;
    //是否阅读过协议
    public bool AgreeDeals = false;

    public bool MusicSwich = false;
    public bool MusicSwichButton = true;

	public string URL = "http://32d755.cn:19033/";//"http://47.105.79.66:8888/"; //"http://32d755.cn:19033/"
    public string LocalURL = "http://32d755.cn:19033/";//"http://192.168.88.66:19003/"  //"http://47.94.99.18:19033/"
    public string SaveName = "MXXZ";
    public bool isLocal = false;
    public Logininfo CurrentAccount = new Logininfo();
    private Dictionary<string, string> SaveNameMessage = new Dictionary<string, string>();

    public void ClearName()
    {
        SaveNameMessage.Clear();
    }
    public string GetPassWord(string Name)
    {
        string passWord = null;
        SaveNameMessage.TryGetValue(Name, out passWord);
        return passWord;
    }

    public void AddName(string Name, string PassWord)
    {
        SaveNameMessage.Add(Name, PassWord);
    }

    public Dictionary<string, Dic> SaveShopList = new Dictionary<string, Dic>();
    public void AddShopList(string Name, Dic GetDic)
    {
        if (SaveShopList.ContainsKey(Name))
        {
            SaveShopList.Remove(Name);
        }
        SaveShopList.Add(Name, GetDic);
    }

    public Dictionary<string, Dic> SaveShopType = new Dictionary<string, Dic>();

    public void AddShopType(string aaa, Dic bbb)
    {
        if (SaveShopType.ContainsKey(aaa))
            SaveShopType.Remove(aaa);
        SaveShopType.Add(aaa, bbb);
    }

    public Dictionary<string, string> SetNameListBack(string GetList)
    {
        SaveNameMessage.Clear();
        if (GetList != string.Empty)
        {
            string[] ZPArray = GetList.Split('#');
            string NameList = ZPArray[0];
            string PassList = ZPArray[1];
            string[] NameArray = NameList.Split('*');
            string[] PassArray = PassList.Split('*');
            for (int i = 0; i < NameArray.Length; i++)
            {
                if (NameArray[i] == string.Empty)
                    continue;
                if (SaveNameMessage.ContainsKey(NameArray[i]))
                    SaveNameMessage.Remove(NameArray[i]);
                SaveNameMessage.Add(NameArray[i], PassArray[i]);
            }
        }

        return SaveNameMessage;
    }

    public string GetShopTypeKeys(string Value)
    {
        //Debug.Log(Value);
        string keys = "没有找到";
        foreach (var key in SaveShopType)
        {
            if (key.Value.GetKeys(Value) != null)
                keys = key.Key;
        }
        return keys;
    }
    public string GetNeedNameList(string NowName, string NowPassWord)
    {
        string Name = string.Empty;
        string password = string.Empty;
        Name += "*" + NowName;
        password += "*" + NowPassWord;
        foreach (KeyValuePair<string, string> child in SaveNameMessage)
        {
            if (child.Key == NowName)
                continue;
            Name += "*" + child.Key;
            password += "*" + child.Value;
        }

        string AllString = Name + "#" + password;
        return AllString;
    }
    public Logininfo LoginAccount = new Logininfo();

    public MessageInfo Info = new MessageInfo();
    Dictionary<string, string> SaveMessage = new Dictionary<string, string>();


    public string GetValue(string Name)
    {
        string ValueGet = null;
        bool a = SaveMessage.TryGetValue(Name, out ValueGet);
        if (a)
            return ValueGet;
        else
            return "null";
    }
    public void AddValue(string Name, string ValueAdd)
    {
        string a = GetValue(Name);
        if (a == null)
            SaveMessage.Add(Name, ValueAdd);
        else
        {
            SaveMessage.Remove(Name);
            SaveMessage.Add(Name, ValueAdd);
        }
    }


    Dictionary<string, string> SaveBackMessage = new Dictionary<string, string>();

    public string GetBackValue(string Name)
    {
        string ValueGet = null;
        SaveMessage.TryGetValue(Name, out ValueGet);
        return ValueGet;
    }
    public void AddBackValue(string Name, string ValueAdd)
    {
        string a = GetValue(Name);
        if (a == null)
            SaveMessage.Add(Name, ValueAdd);
    }

    public Dictionary<string, Dic> SaveTuDi = new Dictionary<string, Dic>();

    public void AddTuDi(string name, Dic GetDic)
    {
        if (SaveTuDi.ContainsKey(name))
            SaveTuDi.Remove(name);
        SaveTuDi.Add(name, GetDic);
    }

    public Dictionary<string, Dic> SaveFriend = new Dictionary<string, Dic>();

    public void AddFriend(string aaa, Dic bbb)
    {
        if (SaveFriend.ContainsKey(aaa))
            SaveFriend.Remove(aaa);
        SaveFriend.Add(aaa, bbb);
    }

    public Dictionary<string, Dic> SaveGrownInfo = new Dictionary<string, Dic>();

    public void SaveGrown(string aaa, Dic bbb)
    {
        if (SaveGrownInfo.ContainsKey(aaa))
            SaveGrownInfo.Remove(aaa);
        SaveGrownInfo.Add(aaa, bbb);
    }

    public Dictionary<string, Dic> SaveTradingInfo = new Dictionary<string, Dic>();
    public Dictionary<string, string> SaveData = new Dictionary<string, string>();

    public string GetData(string Name)
    {
        string ValueGet = null;
        SaveData.TryGetValue(Name, out ValueGet);
        return ValueGet;
    }
    public void AddData(string Name, string ValueAdd)
    {
        string a = GetData(Name);
        if (a == null)
            SaveData.Add(Name, ValueAdd);
    }

    public delegate void UpdateAllData();
    public UpdateAllData ClearData;
    public void UpdateData()
    {
        SaveTuDi.Clear();
        SaveData.Clear();
        SaveBackMessage.Clear();
        //ClearData.Invoke();
    }

    public void UpdateAllObj()
    {
        UpdateData();
        if (BusinessInfoHelper.Instance != null)
            BusinessInfoHelper.Instance.UpdateDate();
    }

    public void ClearAll()
    {
        UpdateData();
        SaveMessage.Clear();
    }


    public void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + SaveName + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }

    /**
     * path：读取文件的路径
     * name：读取文件的名称
     */
    public ArrayList LoadFile(string path, string name)
    {
        //使用流的形式读取
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + SaveName + name);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            arrlist.Add(line);
        }
        //关闭流
        sr.Close();
        //销毁流
        sr.Dispose();
        //将数组链表容器返回
        return arrlist;
    }

    public string GetLoadFile(string name)
    {
        ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, name);
        String sr = null;
        if (infoall == null)
            return null;
        foreach (string str in infoall)
        {
            sr += str;
        }
        return sr;
    }

    /**
     * path：删除文件的路径
     * name：删除文件的名称
     */
    public void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + SaveName + name);
    }

    public class aciton<T>
    {
        public delegate void ACTION(T OBJ);
        public ACTION EVENT;
    }
}
