using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MessageManager : MonoBehaviour {

    public static MessageManager _Instantiate;

    private ShowMessage_Http ShowMessage;

    private GameObject ShowLoad;

    private void Awake()
    {
        _Instantiate = this;
    }

    void Start ()
    {
        ShowLoad = GameObject.Find("HttpBack");
        ShowMessage = GameObject.Find("MessageObj").GetComponent<ShowMessage_Http>();
    }
	
    //默认显示2秒
    public void Show(string GetMessage)
    {
        ShowMessage.SetMessage(GetMessage);
    }

    //增加显示2秒+waittime
    public void Show(string GetMessage,float waittime)
    {
        ShowMessage.SetMessage(GetMessage,waittime);
    }

    public void Show(Text GetMessage)
    {
        ShowMessage.SetMessage(GetMessage.text);
    }

    public GameObject Window;
    public Text Message_Window;
    public Text Title;
    public Button submitBtn;
    public void WindowShowMessage(Text GetText)
    {
        Message_Window.text = GetText.text;
        submitBtn.gameObject.SetActive(false);
        GameManager.GetGameManager.OpenWindow(Window.transform);
    }

    public void WindowShowMessage(string GetText, string title = GlobalData.prompt)
    {
        Message_Window.text = GetText;
        Title.text = title;
        submitBtn.gameObject.SetActive(false);
        GameManager.GetGameManager.OpenWindow(Window.transform);
    }

    public void WindowShowMessage(string GetText, Action action, string buttonName, string title = GlobalData.prompt, bool isCloseSelf = true)
    {
        Message_Window.text = GetText;
        Title.text = title;
        submitBtn.gameObject.SetActive(true);
        submitBtn.GetComponentInChildren<Text>().text = buttonName;
        if (isCloseSelf)
            submitBtn.onClick.AddListener(() => { GameManager.GetGameManager.CloseWindow(Window.transform); });
        else
        {
            Action _action = submitBtn.GetComponent<ButtonClickAction>().ActionEvent;
            while (_action != null)
                _action -= _action;
            submitBtn.GetComponent<ButtonClickAction>().ActionEvent += action;
        }
        GameManager.GetGameManager.OpenWindow(Window.transform);
    }

    public void QuiteGame()
    {
        SceneManager.LoadScene("mainmeun");
    }

    [SerializeField]

    private Text ErrorCode;
    public void ShowErrorCode(string GetError)
    {
        if(this.ErrorCode != null)
        ErrorCode.text = GetError;
    }

    public int LoadNub = 0;
    public void AddLockNub()
    {
        LoadNub++;
    }
    public void DisLockNub()
    {
        LoadNub--;
    }

    public int StatusNub = 0;
    private void Update()
    {
        if (LoadNub <= 0)
        {          
            ShowLoad.transform.localScale = new Vector3(0, 0, 0);
            StatusNub = 0;
            LoadNub = 0;
        }
        else
        {         
            ShowLoad.transform.localScale = new Vector3(1, 1, 1);
            StatusNub++;
            if (StatusNub > 800)
            {
                StopAllCoroutines();
                Show("请求超时！");
                LoadNub = 0;
            }
        }

    }
}
