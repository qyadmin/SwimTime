using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System;
using TypeClass;
using UnityEngine.Events;


[System.Serializable]
public class GetMessageModel
{
	public string Name;
	public Text ShowInText;
	public InputField ShowIn_InputText;
	public string EventName;
	public GameObject MessageObj;
	public MsgType Mytype;
	public void SetValue(JsonData GetData)
	{
		string Getstr=GetData.Keys.Contains (Name) ? GetData [Name].ToString () : "";
		switch (Mytype) 
		{
		case MsgType.Text:
			ShowInText.text = Getstr;
			break;
		case MsgType.InputText:
			ShowIn_InputText.text = Getstr;
			break;
		case MsgType.AcitonEvent:
			MessageObj.SendMessage (EventName,Getstr);
			break;
		}
	}
}

public enum MsgType
{
	Text,
	InputText,
	AcitonEvent
}


public delegate void UpdateDele();
public class Registrationinfo
{
    public string Name;
    public string PhoneNum;
    public string LoginPassWord;
    public string TransactPassWord;
    public string ReferrerPhoneNum;
}
public class Logininfo
{
    public string ID;
    public string PassWord;
    public string VerCode;
}
public class Topinfo
{
    public Image HeadImg;
    public string NameID;
    public string BoZhong;
    public string JinJu;
    public string Active;
}
public class Messageinfo
{
    public Image HeadImg;
    public string Name;
    public string NameID;
    public string PhoneID;
    public string PassWord;
}
[System.Serializable]
public class ImageSend
{
    public string Name;
    public string Value;
}

public class Dic
{
	public Dictionary<string, string> DataDic = new Dictionary<string, string>();
	public string GetVaule(string name)
	{
		string a = null;
		DataDic.TryGetValue (name,out a);
		return a;
	}

	public string GetKeys(string value)
	{
		string keys = null;
		foreach (var key in DataDic)
		{
			//Debug.Log(key.Value);
			if (key.Value == value)
			{
				keys = key.Key;
			}
		}
		return keys;
	}
    public Dic()
    {
        Static.Instance.ClearData += Clear;
    }
    public void Clear()
    {
        DataDic.Clear();
    }
}
	
public enum GetBackType
{
	Text,
	InputText,
	Iamge,
	AcitonEvent,

}
	
[System.Serializable]
public class BaseData
{
    public string code;
	public Text codetext;
    public string result;
	public Text resulttext;
    public string msg;
	public Text msgtext;
    public InputField msgInputtext;
    public string url;
	public Text urltext;
}
[System.Serializable]
public class SendMessage
{
    public string Name;
    public InputField SendData;
	public bool IsGetOther;
	public string OtherName;
	public bool IsNub;
	public int Nub;
    public bool IsSave = false;
	public string MakeValue;

	public string SetValue()
	{
		string GetValue=null;
		if (IsNub) 
		{
			GetValue= Nub.ToString();
			return GetValue;
		}
		if (IsGetOther) 
		{
			GetValue=Static.Instance.GetValue(OtherName).ToString();
			return GetValue;

		}
		if (SendData != null)
		{
			GetValue=SendData.text;
			if (IsSave)
			{
				Static.Instance.AddValue(Name, SendData.text);
			}
		}
		else
		{
			GetValue=Static.Instance.GetValue(Name);
		}             

		return GetValue;
	}
}
[System.Serializable]
public class GetDataWorker
{
    public string ShowData;
    public Text GetDataObj;
    public bool IsSave=false;
}
[System.Serializable]
public class MessageInfo
{
    public int CutCount;
    public string DataName;
    public string URL;
    public BaseData GetBase;
    public SendMessage[] SendData;
    public bool Action = false;
   // public GetData[] BackDataGet;
    public List<string> BackData = new List<string>();
    [TextArea(2, 5)]
    public string ShowMessage;
    public string[] GetDataList;
    public void GetData(string name,string value)
    {
        foreach (string child in GetDataList)
        {
            if (name == child)
            {
                Static.Instance.AddData(name, value);
                break;
            }
        }
    }
    public void Clear()
    {
        BackData.Clear();
    }
}

[System.Serializable]
public class CheckMessage
{
    public InputField InputText;
    public string WarmMessage;
}
	
[System.Serializable]
public class NewMessageInfo
{
	public string AddTag;
	public ReceiveType Receivemodel;
    public List<HttpModel> ShareModel = new List<HttpModel>();
	public int CutCount;
    public string HeaderName;
    public string DataName;
    public bool NeedReplayName;
    public string ReplayName;
    public string URL;
	public BaseData GetBase;
	[Tooltip("添加需要发送的参数")]
	public List<DataValue>SendData=new List<DataValue>();
    public void RemoveData(string Name)
    {
        foreach (DataValue child in SendData)
        {
            if (child.Name == Name)
            {
                SendData.Remove(child);
                break;
            }
        }
    }

    public void AddData(string key,string Value)
    {
        RemoveData(key);
        DataValue data = new DataValue();
        data.MyType = GetTypeValue.GetFromValue;
        data.Name = key;
        data.SetValue = Value;
        SendData.Add(data);
    }


    public bool Action = false;
	[Tooltip("获取数据列表")]
	public BackDataValue[] BackDataGet;
	[Tooltip("显示返回数据列表")]
	public List<string> BackData = new List<string>();
    //调试数据
    [TextArea(5, 5)]
    public string DebugData;
    public string ErrorCode="0";
    public string ErrorMsg = "0";
    //调试数据
    [TextArea(2, 5)]
	public string ShowMessage;
	[Tooltip("保存列表中返回的数据")]
	public string[] GetDataList;

	public ListMessage MyListMessage;

	public void GetData(Dictionary<string,string> AllMessage)
	{
		foreach (BackDataValue child in BackDataGet)
		{
			string obj = null;
			AllMessage.TryGetValue(child.Name, out obj);
			child.SetString (obj);
		}
	}

	public void Clear()
	{
		BackData.Clear();
	}
}

public enum TypeGo
{
	GetTypeA,
	GetTypeB,
	GetTypeC,
	GetTypeD,
    GetTypeE
}

[System.Serializable]
public class ListGetValue
{
	public GetBackType MyType;
	public bool IsInt;
	public string Name;
	public Text Showtext;
	public InputField ShowInputtext;
	public Image ShowTexture;
	public bool IsSave;
	public string EventName;
	public GameObject EventObj;
    public bool IsDis;
    public string DisNub="$1.0f";
}
	

public enum ObjType
{
	Text,
	Image,
	GameObject
}

[System.Serializable]
public class TypeEvent
{
    public string AddToListValue;
	public int Nub;
	public int SaveNub;
	public HttpModel SendModel;
	public void SetData(string Value)
	{
        SendModel.Data.RemoveData(AddToListValue);
        DataValue data = new DataValue ();
		data.MyType = GetTypeValue.GetFromValue;
		data.Name = AddToListValue;
		data.SetValue = Value;
		if(SendModel!=null)
		SendModel.Data.SendData.Add (data);
	}
}


[System.Serializable]
public class ListBB
{
	public string Name;
	public ObjType MyObjeType;
	public SaveNameType MySaveType;
	public string OtherName;
	public string EventName;
	public int index=0;
    public bool NotString;
	public List<TypeEvent> NubList = new List<TypeEvent> ();
}

//列表对象
[System.Serializable]
public class ListMessage
{
    public string ActionEventName;
	public GameObject EventObj;
	public Transform FatherObj;
	public GameObject InsObj;
	public List<TypeEvent> OffectNubList = new List<TypeEvent> ();
	public List<ListBB> NameList=new List<ListBB>();
	public List<ListGetValue> NameSingleList=new List<ListGetValue>();


	public GameObject MessageObj;
	public string ActionName;
	public void SendData(JsonData GetData)
	{
		MessageObj.SendMessage (ActionName,GetData);
	}



	public void SetOffectList(int index)
	{
		OffectNubList.Clear ();
		Debug.Log (index+"数组长度与"+NameList [index].NubList.Count);
		foreach (TypeEvent child in NameList [index].NubList) 
		{
			OffectNubList.Add (child);
		}
	
	}

	public void GetOffectList(int index)
	{
		NameList [index].NubList.Clear ();
		Debug.Log (index+"数组长度与"+NameList [index].NubList.Count);
		foreach (TypeEvent child in OffectNubList) 
		{
			NameList [index].NubList.Add (child);
		}
	}

	public string[] GetNameArray(int Index)
	{
		List<string> allname = new List<string> ();	
		if (InsObj != null) 
		{
			
		switch (NameList [Index].MyObjeType) 
		{
		case ObjType.Text:
				foreach (Transform child in InsObj.transform) 
				{
					if(child.GetComponent<Text>())
					allname.Add (child.name);
				}
			break;
		case ObjType.Image:
				foreach (Transform child in InsObj.transform) 
				{
					if(child.GetComponent<Image>())
						allname.Add (child.name);
				}
			break;
		case ObjType.GameObject:
				foreach (Transform child in InsObj.transform) 
				{
					allname.Add (child.name);
				}
			break;
		}
		   
		}
		return allname.ToArray ();
	}

	public  List<string> GetNameList(int Index)
	{
		List <string> MyList = new List<string> ();
		string[] getlist = GetNameArray (Index);
		for (int i = 0; i < getlist.Length; i++) 
		{
			MyList.Add(getlist [i]);
		}
		return MyList;
	}

    //字符串协议制定
    private string ChangeForm(string name,string formname)
    {
        string str = string.Empty;
        switch (formname.Substring(0,1))
        {
            case "/":
                str=(float.Parse(name) / float.Parse(formname.Replace("/",string.Empty))).ToString();
            break;
            case "$":
                str= formname.Replace("s",name).Replace("$",string.Empty);
            break;
        }

        return str;
     }


	//*****
	public void SetValueSingle(JsonData Getjson)
	{
        
        if (Getjson.Count == 0)
            return;
		foreach (ListGetValue child in NameSingleList) 
		{
            if (!Getjson.Keys.Contains(child.Name))
            {
				continue;
            }
			else 
			{              
				if (child.IsSave)
					Static.Instance.AddValue (child.Name,Getjson [child.Name].ToString ());
                switch (child.MyType) 
				{
				case GetBackType.Text:
                        if (child.Showtext)
                        {
                            if (child.IsDis)
                            {
                                child.Showtext.text = ChangeForm(Getjson[child.Name].ToString(), child.DisNub);
                            }
                            else
                                child.Showtext.text = Getjson[child.Name].ToString();
                        }
				break;
				case GetBackType.InputText:
					if (child.ShowInputtext) {
                            if (child.IsDis)
                                child.ShowInputtext.text = ChangeForm(Getjson[child.Name].ToString(), child.DisNub);
                                //child.ShowInputtext.text = (float.Parse(Getjson[child.Name].ToString())/child.DisNub).ToString();
                            else
                                child.ShowInputtext.text = Getjson[child.Name].ToString();
                        
                            if (child.IsInt)
							child.ShowInputtext.text = System.Math.Floor (float.Parse (child.ShowInputtext.text)).ToString ();
					}
					break;
				case GetBackType.Iamge:
					Texture2D texture = Base64StringToTexture2D(Getjson [child.Name].ToString ());
					Sprite sprites = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
					child.ShowTexture.GetComponent<Image>().sprite = sprites;
				break;
				case GetBackType.AcitonEvent:                   
					child.EventObj.SendMessage (child.EventName, ObjectToString(Getjson [child.Name]));
				break;
				}
			}
		}
	}

    public void SetValueAll(JsonData GetData)
    {
        if (!FatherObj.gameObject.activeSelf)
        {
            return;
        }
            if (GetData == null)
            return;
        List<JsonData> Getjson = new List<JsonData>();
        for (int i = 0; i < GetData.Count; i++)
        {
            Getjson.Add(GetData[i]);
        }
        foreach (JsonData child in Getjson)
        {
            GameObject NewList =ObjectPool.GetInstance().GetObj(InsObj);
            NewList.transform.SetParent(FatherObj);
            NewList.transform.localScale = new Vector3(1, 1, 1);
            NewList.SetActive(true);
            NewList.name = InsObj.name;
            if(FatherObj.gameObject.activeInHierarchy)
            NewList.gameObject.SendMessage(ActionEventName,JsonMapper.ToJson(child));
        }
    }


	public void SetVaule(JsonData GetData)
	{
        if (GetData == null||GetData.Count==0)
			return;
		List<JsonData> Getjson = new List<JsonData> ();
		for (int i = 0; i < GetData.Count; i++) 
		{
			Getjson.Add(GetData [i]);
		}
		foreach (JsonData child in Getjson) 
		{
				GameObject NewList = ObjectPool.GetInstance().GetObj(InsObj);
				NewList.transform.SetParent (FatherObj);
				NewList.transform.localScale = new Vector3(1, 1, 1);
				NewList.SetActive (true);
            NewList.name = InsObj.name;

                List<GameObject> Objlist=new List<GameObject>();
				foreach (Transform childa in NewList.transform) 
				{
						Objlist.Add (childa.gameObject);
				}
				for (int i = 0; i < NameList.Count; i++) 
				{
					if (child [NameList [i].Name] != null)
					{
                  
						switch (NameList [i].MyObjeType) 
						{
						case ObjType.Text:
                            Debug.Log(NameList[i].index);
                            Debug.Log(NameList[i].Name);
                            Objlist[NameList[i].index].GetComponent<Text>().text = child [NameList [i].Name].ToString ();
							break;
						case ObjType.Image:

							Texture2D texture = Base64StringToTexture2D(child [NameList [i].Name].ToString ());
							Sprite sprites = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
							Objlist[NameList[i].index].GetComponent<Image>().sprite = sprites;
							break;

                        case ObjType.GameObject:
    
                            if (Objlist[NameList[i].index].GetComponent<Text>())
                            Objlist[NameList[i].index].GetComponent<Text>().text = child[NameList[i].Name].ToString();
 
                            break;
                    }                  
                }
					if (NameList [i].MySaveType==SaveNameType.SaveMySelfName) 
					{               
						Static.Instance.AddValue (NameList [i].Name, child [NameList [i].Name].ToString ());
					}
					if (NameList [i].MySaveType==SaveNameType.SaveOtherName) 
					{
						Static.Instance.AddValue (NameList [i].OtherName, child [NameList [i].Name].ToString ());
					}
					if (NameList [i].MySaveType==SaveNameType.ActionEvent) 
					{
                    if (Objlist[NameList[i].index].activeInHierarchy)
                    {
                        if (NameList[i].NotString)
                            Objlist[NameList[i].index].SendMessage(NameList[i].EventName, JsonMapper.ToJson(child[NameList[i].Name]));
                        else
                        {
                            Objlist[NameList[i].index].SendMessage(NameList[i].EventName, child[NameList[i].Name].ToString());
                        }
                    }
                }
					if (NameList [i].MySaveType==SaveNameType.Chosetype) 
					{
						foreach (TypeEvent childNub in NameList[i].NubList) 
						{
							if (child [NameList [i].Name].ToString () == childNub.Nub.ToString ()) 
						    {
								Objlist [childNub.SaveNub].SetActive (true);
							    Button GetNow = Objlist [childNub.SaveNub].GetComponent<Button> ();
							    if (GetNow!=null&&childNub.SendModel!=null) 
							    {
								    if (childNub.SendModel!=null) 
								    {
                                    if (!GetNow.gameObject.GetComponent<ButtonHttpEvent>())
                                        GetNow.gameObject.AddComponent<ButtonHttpEvent>();
                                        GetNow.gameObject.GetComponent<ButtonHttpEvent> ().AddHttpListener (childNub, child[childNub.AddToListValue].ToString());
								    }								   
							    }
							} 
							else 
							{
								Objlist [childNub.SaveNub].SetActive (false);
							}
						}
					}
				}


		}

	}


	public static  byte[] GetBytes(string str)
	{
		return System.Text.Encoding.ASCII.GetBytes(str);
		//return Encoding.ASCII.GetBytes(str.ToCharArray());
	}

	public static  string GetString(byte[] bytes)
	{
		return System.Text.Encoding.ASCII.GetString(bytes);
		//return Encoding.ASCII.GetString(bytes);
	}

	public byte[] Gettexture(string base64)
	{
		return System.Convert.FromBase64String(base64);
	}

	public  Texture2D Base64StringToTexture2D(string base64)
	{
		Texture2D tex = new Texture2D (4, 4, TextureFormat.ARGB32, false);
		try
		{
			byte[] bytes = System.Convert.FromBase64String(base64);
			tex.LoadImage(bytes);
		}
		catch(System.Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		return tex;
	}    

	public string ObjectToString(object GetData)
	{
		if (GetData.GetType() == typeof(JsonData)) 
		{
			return JsonMapper.ToJson (GetData);
		}
		return GetData.ToString ();
	}
}
