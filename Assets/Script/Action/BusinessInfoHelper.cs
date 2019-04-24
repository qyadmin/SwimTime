using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BusinessInfoHelper:MonoBehaviour
{
	public static BusinessInfoHelper Instance;
	public UpdateDele EventUpdate;

	public GameObject[] HttpModel;

	private ShowMessage_Http ShowMessage;
	void Awake()
	{
		Instance = this;
		ShowMessage = GameObject.Find ("MessageObj").GetComponent<ShowMessage_Http>();
	}

    public void UpdateDate()
	{
		StartCoroutine ("threadStart");
	}

    public bool isDone = true;
    IEnumerator threadStart()
    {
        int nub = 0;
		float a = Time.time;
        while (nub<HttpModel.Length)
        {
           if (isDone)
           {
                isDone = false;
				if(HttpModel[nub] != null)
				HttpModel[nub].SendMessage("Get");
				else
					isDone = true;
                nub++;

            }
          yield return 5;
		  if (Time.time - a > 5.0f) 
		  {
			StopCoroutine ("threadStart");
		  }
        }
		if(EventUpdate!=null)
		EventUpdate.Invoke ();
    }


	public void LoadForget()
	{
		SceneManager.LoadScene ("forget");
	}
		

	public void Show(string GetMessage)
	{
		ShowMessage.SetMessage (GetMessage);
	}

	public void Show(Text GetMessage)
	{
		ShowMessage.SetMessage (GetMessage.text);
	}
}




