using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class HttpModel : MonoBehaviour
{
    public TypeGo DataType;

    public NewMessageInfo Data;
    public List<GetMessageModel> MsgList = new List<GetMessageModel>();
    public UnityEvent Suc, Fal;
    //临时事件
    public EventPatcher<ReturnHttpMessage> HttpSuccessCallBack = new EventPatcher<ReturnHttpMessage>();
    public bool IsLock = false;

    public UnityEvent DoAction;
    public bool NoShow = false;
    public bool HideMessage = false;
    public bool OnlyError;
    public bool IsAdd;
    [TextArea(2, 5)]
    public string ShowAddMessage;
    private string message = null;
    public float WaitTime = 0;

    public bool isLoacl;
    public string LocalHttp;
    public bool IsShell;

    public bool NoldIcon;
    public bool IsWait;
    public bool NoSus;
    public bool GetTWO;
    void Awake()
    {
        IsLock = Static.Instance.Lock;
        //isLoacl = Static.Instance.isLocal;
    }

    public bool isLocksend = false;
    //创建网络返回消息
    ReturnHttpMessage msg = new ReturnHttpMessage();
    public void Get()
    {
        //DebugAction(Data.DebugData);
        //return;
        if (isLocksend)
            return;
        msg.Code = HttpCode.FAILED;
        StartLoad();
        Data.ErrorCode = "-1";
        Data.BackData.Clear();
        message = null;
        if (!isLoacl)
            message += "?";
        if (IsLock)
            message += EncryptDecipherTool.UserMd5();

        //URL请求拼接
        string url = null;
        if (isLoacl)
            url = Static.Instance.LocalURL + LocalHttp;
        else
            url = Static.Instance.URL + Data.URL;

        if (Data.SendData.Count > 0)
        {
            foreach (DataValue child in Data.SendData)
            {
                string str = child.GetString();
                if (str == "Error")
                {
                    EndLoad();
                    return;
                }
                if (str == "Gone")
                    continue;
                //if(message.Substring(message.Length-2,1)=="?")
                //    message +=child.Name + "=" + str;
                //else
                message += "&" + child.Name + "=" + str;
            }
        }

        message = EncryptDecipherTool.GetListOld(message, IsLock);
        url = url + message;
        url = Uri.EscapeUriString(url);
        StartCoroutine(GetMessage(url));
    }

    IEnumerator GetMessage(string url)
    {
        // if (!NoldIcon)
        Debug.Log(url+"               "+transform.name);
        WWW www = new WWW(url);
        yield return www;
        EndLoad();
        if (IsWait)
            yield return new WaitForSeconds(0.1f);
        if (www.error != null)
        {
            if (!NoShow)
                MessageManager._Instantiate.Show("网络连接失败！");
            Data.ShowMessage = www.error;
            msg.Code = HttpCode.ERROR;
        }
        else
        {
            if (www.text == string.Empty)
                MessageManager._Instantiate.Show("您的操作频繁，请稍后尝试");

            Data.ShowMessage = www.text;
            //try
            //{
            string jsondata = System.Text.Encoding.UTF8.GetString(www.bytes, 0, www.bytes.Length);
            jsondata = jsondata.Remove(0, Data.CutCount);
            foreach (HttpModel child in Data.ShareModel)
            {
                child.DebugAction(jsondata);
            }

            string str = jsondata.Replace("\"", "\'");
            DebugAction(str);
            //}
            //    catch
            //{

            //    MessageManager._Instantiate.Show("数据解析异常！");
            //    EndLoad();
            //}
        }
    }


    public void DebugAction(string DebugData)
    {
        //if(GameManager.GetGameManager!=null)
        //GameManager.GetGameManager.CatchData(DebugData);        
        string jsondata = DebugData;
        int a = 0;
        Static.Instance.DeleteFile(Application.persistentDataPath, "json.txt");
        Static.Instance.CreateFile(Application.persistentDataPath, "json.txt", jsondata);
        ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, "json.txt");
        String sr = null;
        foreach (string str in infoall)
        {
            sr += str;
        }
        JsonData jd = JsonMapper.ToObject(sr);
        msg.Data = jd;
        if (jd.IsArray)
        {
            string jddata = "{" + "\"" + Data.DataName + "\":" + JsonMapper.ToJson(jd) + "}";
            Data.ShowMessage = jddata;
            jd = JsonMapper.ToObject(jddata);
        }
        if (!GetTWO)
        {
            Data.ErrorMsg = jd.Keys.Contains("msg") ? jd["msg"].ToString() : "";
            Data.ErrorCode = jd.Keys.Contains("ok") ? jd["ok"].ToString() : "-1";
            if (jd.Keys.Contains("sb"))
                DataManager.GetDataManager.UpdateSb(jd["sb"].ToString());
        }
        else
        {
            Data.ErrorMsg = jd[Data.HeaderName].Keys.Contains("msg") ? jd[Data.HeaderName]["msg"].ToString() : "";
            Data.ErrorCode = jd[Data.HeaderName].Keys.Contains("ok") ? jd[Data.HeaderName]["ok"].ToString() : "-1";
        }

        if (IsShell)
        {
            if (Data.ErrorCode == "ok")
            {
                if (jd.Keys.Contains(Data.HeaderName))
                    jd = jd[Data.HeaderName];
                else
                {
                    Debug.Log("没有获得headName");
                    return;
                }
            }
        }

        foreach (GetMessageModel child in MsgList)
        {
            child.SetValue(jd);
        }

        if (Data.ErrorCode == "ok" || NoSus)
        {
            //Data.ErrorCode="ok";
            msg.Code = HttpCode.SUCCESS;
            List<string> Savename = new List<string>();
            Dictionary<string, string> SaveMessage = new Dictionary<string, string>();
            switch (DataType)
            {
                case TypeGo.GetTypeA:
                    break;
                case TypeGo.GetTypeB:
                    if (jd.Keys.Contains(Data.DataName))
                    {
                        foreach (Transform child in Data.MyListMessage.FatherObj)
                        {
                            ObjectPool.GetInstance().RecycleObj(child.gameObject);
                        }
                        Data.MyListMessage.SetVaule(jd[Data.DataName]);
                    }
                    break;

                case TypeGo.GetTypeC:
                    if (jd.Keys.Contains(Data.DataName))
                    {
                        if (Data.ErrorCode != "ok")
                        {
                            MessageManager._Instantiate.Show(Data.ErrorMsg);
                            return;
                        }
                        else
                            Data.MyListMessage.SetValueSingle(jd[Data.DataName]);
                    }
                    else                    
                        Data.MyListMessage.SetValueSingle(jd);                    
                    break;

                case TypeGo.GetTypeD:
                    Data.MyListMessage.SendData(jd);
                    break;

                case TypeGo.GetTypeE:

                    if (jd.Keys.Contains(Data.DataName))
                    {
                        string name = Data.NeedReplayName ? Data.ReplayName : Data.DataName;
                        bool HaveKey = Data.AddTag == string.Empty ? false : true;
                        object[] data = new object[] { jd[Data.DataName], Data.Receivemodel, Data.AddTag, name, HaveKey };
                        DataManager.GetDataManager.OnResponesObj.SendMessage("Receive_Data", data);
                    }
                    break;
            }

            if (Data.Action)
            {
                Data.GetData(SaveMessage);
            }
        }

        HttpSuccessCallBack.Send(msg);
        HttpSuccessCallBack.ClearAllEevnt();
        if (Data.ErrorCode == "ok" || NoSus)
        {
            Suc.Invoke();
        }
        else
        {
            Fal.Invoke();
        }

        if (!HideMessage)
        {
            ShowMessageWait();
        }

        //        if (MessageManager._Instantiate)
        //            MessageManager._Instantiate.ShowErrorCode(Data.ErrorCode);

        Debug.Log(Data.ShowMessage);
    }


    public void ShowMessageWait()
    {
        if (OnlyError)
        {
            Debug.Log(Data.ErrorCode);
            if (Data.ErrorCode != "ok" && !NoSus)
                MessageManager._Instantiate.Show(Data.ErrorMsg);
            return;
        }
        else
        {
            if (Data.ErrorMsg == string.Empty && IsAdd)
                Data.ErrorMsg = ShowAddMessage;
            MessageManager._Instantiate.Show(Data.ErrorMsg);
        }
    }

    void QuiteGo()
    {
        SceneManager.LoadScene("mainmeun");
    }


    private void StartLoad()
    {
        if (NoldIcon)
            return;
        MessageManager._Instantiate.AddLockNub();
        isLocksend = true;
    }

    private void EndLoad()
    {
        if (NoldIcon)
            return;
        MessageManager._Instantiate.DisLockNub();
        isLocksend = false;
    }
}
