// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：在这里修改脚本说明
// 作成時間：#CreateTime#
// 類を作る：GameEvent.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEvent : MonoBehaviour {

    [SerializeField]
    private HttpModel setphone,yzm;
    [SerializeField]
    private Button button_sendphone,button_yzm;
    [SerializeField]
    private Transform PhoneSetWindow;
    [SerializeField]
    private CreatIP Login;

    public void CheckPhone(string data)
    {
        if (data == "\"\"")
        {
            GameManager.GetGameManager.OpenWindow(PhoneSetWindow);
            button_sendphone.onClick.AddListener(delegate ()
            {
                setphone.Get();
            });
            button_yzm.onClick.AddListener(delegate ()
            {
                yzm.Get();
            });
            
        }
        else
        {
            Login.LoadSuS();
        }
    }
}
