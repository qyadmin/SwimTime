using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using System.Linq;
public class SaveZH : MonoBehaviour {

	public InputField Name;
	public InputField Password;
    public HttpModel GetCode;
	void Start()
	{
		//Name.text = LoadName ();
		//Password.text = Loadpassword ();
        //GetCode.Get();   
    }


	public void SetName(Dropdown URLList)
	{
		string aa=Static.Instance.GetValue ("tel");
		string bb=Static.Instance.GetValue ("password");
		SaveName (aa);
		password (bb);
		SaveURL (URLList);
		SaveNameLsit (Static.Instance.GetNeedNameList(Name.text,Password.text));
	}



	public void SaveNameLsit(string Namelist)
	{
        Static.Instance.DeleteFile(Application.persistentDataPath, "nameList.txt");
        Static.Instance.CreateFile(Application.persistentDataPath, "nameList.txt", Namelist);
	}


    public void SaveNowUser()
    {
       // SaveName(Name.text);
       // password(Password.text);
    }


	public void SaveName(string Name)
	{
        Static.Instance.DeleteFile(Application.persistentDataPath, "name.txt");
        Static.Instance.CreateFile(Application.persistentDataPath, "name.txt", Name);
	}

	public void password(string Name)
	{
        Static.Instance.DeleteFile(Application.persistentDataPath, "password.txt");
        Static.Instance.CreateFile(Application.persistentDataPath, "password.txt", Name);
	}



	public string  LoadName()
	{
		ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, "name.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		return sr;
	}

    public string  Loadpassword()
	{
		ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, "password.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		return sr;
	}


	public string  LoadNameList()
	{
		ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, "nameList.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}



	public void SaveURL(Dropdown URLList)
	{
        Static.Instance.DeleteFile(Application.persistentDataPath, "URL.txt");
        Static.Instance.CreateFile(Application.persistentDataPath, "URL.txt", URLList.value.ToString());
	}


	public string  LoadURL()
	{
		ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, "URL.txt");
		String sr = null;
		if (infoall == null)
			return string.Empty;
		foreach (string str in infoall)
		{
			sr += str;
		}
		Debug.Log (sr);
		return sr;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}