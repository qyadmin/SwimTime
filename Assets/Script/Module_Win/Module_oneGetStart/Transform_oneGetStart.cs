// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：一键启动
// 作成時間：2018-08-03
// 類を作る：Transform_oneGetStart.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Transform_oneGetStart : MonoBehaviour
{
    public HttpModel http_OneGetStart;
    public Button oneGetStartBtn;

    private DataDic<DataItem.MachineInfoData> data;
    private Coroutine StartIntermittenceShow;

    private void Start()
    {
        DataManager.GetDataManager.machineInfo.EventObj.Addlistener(UpdateData);
    }
    private void UpdateData(object data)
    {
        this.data = data as DataDic<DataItem.MachineInfoData>;
        oneGetStartBtn.onClick.AddListener(() =>
        {
         
        });
    }
}