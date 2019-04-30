using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TypeClass;
namespace TypeClass
{
	public enum ReceiveType
	{
		Update,
		Add,
		Remove
	}

	public enum SaveNameType
	{
		None,
		SaveMySelfName,
		SaveOtherName,
		ActionEvent,
		Chosetype

	}

    //判断类型
    public enum CheckType
    {
        isEqual,
        rule
    }

	public enum GetTypeValue
	{
		GetFromValue,
		GetFormText,
		GetFromInputField,
		GetFromList,
		GetFromListOther
	}

	public enum GetBackTypeValue
	{
		GetValue,
		NoGetValue,
	}


	public enum Valuetype
	{
		GetInt,
		GetString
	}
}

public class EmptyItem
{
    public EmptyItem()
    {
        //空值过滤器
        Items.Add("tuijianren");
    }

    public bool Calculate(string ValueA, string ValueB, string key)
    {
        int A = 0;
        int B = 0;
        bool isSuccessedA = int.TryParse(ValueA, out A);
        bool isSuccessedB = int.TryParse(ValueB, out B);
        Debug.Log(A + "**" + B);
        if (!isSuccessedA|| !isSuccessedB)
        {
            MessageManager._Instantiate.Show("输入的数值无法解析");
        }
        bool IsGone = false;
        switch (key)
        {
            case "%":
                IsGone = A % B == 0 ? true : false;
                break;
            case "<":
                IsGone = A < B ? true : false;
                break;
            case ">":
                IsGone = A >= B ? true : false;
                break;
        }
        return IsGone;
    }

    private List<string> Items = new List<string>();

    public  bool IsGone(string itemename)
    {
        if (Items.Contains(itemename))
            return true;
        return false;
    }

    public bool RuleValue(string key,string GetValue)
    {
        string[] str = key.Split(new char[] { ',' });
        bool isGone = false;
        foreach (string child in str)
        {
            if (child == string.Empty)
                continue;
            string cutForamt = child.Replace("(", string.Empty).Replace(")", string.Empty);
            isGone = Calculate(GetValue, cutForamt.Substring(1, cutForamt.Length - 1), cutForamt.Substring(0, 1));
            if (!isGone)
                break;
        }
        return isGone;
    }
}

[System.Serializable]
public class DataValue
{
	public bool IsSave;
	public string Name;
	public string OtherName;

	public GetTypeValue MyType;
    public CheckType Rule;
	public Text SetText;
	public InputField SetInputField;
	public string SetValue;

	//public TagerType MyTager;
	public string MakeValue;

    public bool IsCheck;
    public InputField InputText;
    public string WarmMessage;
    public string RuleFromat;

    //可为空值的过滤器
    private EmptyItem EmptyBody=new EmptyItem();

	public string GetString()
	{
        string ValueData = null;
		switch (MyType)
		{
		case GetTypeValue.GetFromValue:
			ValueData = SetValue;
		break;
		case GetTypeValue.GetFormText:
			ValueData = SetText.text;
		break;
		case GetTypeValue.GetFromInputField:
			ValueData = SetInputField.text;
		break;
		case GetTypeValue.GetFromList:
			ValueData = Static.Instance.GetValue(Name);
		break;
		case GetTypeValue.GetFromListOther:
			ValueData = Static.Instance.GetValue(OtherName);
		break;
		}
		if(IsSave)
			Static.Instance.AddValue(Name,ValueData);
        if (IsCheck)
        {
            switch (Rule)
            {
                case CheckType.isEqual:
                    if (ValueData != InputText.text)
                    {
                        MessageManager._Instantiate.Show(WarmMessage);
                        return "Error";
                    }
                    break;
                case CheckType.rule:
                    ValueData = ValueData == string.Empty ? "0" : ValueData;
                    if (!EmptyBody.RuleValue(RuleFromat, ValueData))
                    {
                        MessageManager._Instantiate.Show(WarmMessage);
                        return "Error";
                    }
                    break;
            }
        }
        if (ValueData == string.Empty)
        {
            if (!EmptyBody.IsGone(Name))
            {
                Debug.Log("不包含"+Name);
                MessageManager._Instantiate.Show("参数不能为空");
                Debug.Log("参数不能为空");
                return "Error";
            }
            else
                return "Gone";
        }
		return ValueData;

	}
}

[System.Serializable]
public class BackDataValue
{
	public GetBackTypeValue MyType;
	public bool IsSave;
	public string Name;
	public Text SetText;


	public void SetString(string BackValue)
	{
		switch (MyType)
		{
		case GetBackTypeValue.GetValue:
			SetText.text = BackValue;
		break;
		}
		if(IsSave)
			Static.Instance.AddValue(Name,BackValue);
	}
}

public class EventClass
{
	public UnityEvent Fuc;
	public UnityEvent Fal;
}

public class ReturnHttpMessage
{
    public LitJson.JsonData Data;
    public HttpCode Code;
}

public enum HttpCode
{
   SUCCESS,
   FAILED,
   ERROR
}