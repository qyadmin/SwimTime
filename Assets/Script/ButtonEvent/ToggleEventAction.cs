// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：Toggle组件发生事件
// 作成時間：2018-07-27
// 類を作る：ToggleEventAction.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ToggleEventAction: ToggleEventActionBase
{
    public HttpModel Http_List;

    public override void Init()
    {
        base.Init();        
        Http_List.Get();
    }
}

[Serializable]
public class ToggleEventActionBase
{
    public Toggle toggle;

    public void RegistEvent()
    {
        this.toggle.onValueChanged.RemoveAllListeners();
        this.toggle.onValueChanged.AddListener((bool b) =>
        {
            if (b)
            {
                Init();
            }
        });
    }
    public void RegistEvent(UnityAction<bool> action)
    {
        this.toggle.onValueChanged.RemoveAllListeners();
        this.toggle.onValueChanged.AddListener(action);
    }


    public virtual void Init()
    {
       // Debug.Log(item.gameObject.name);    
        Debug.Log(toggle.name);
    }
}