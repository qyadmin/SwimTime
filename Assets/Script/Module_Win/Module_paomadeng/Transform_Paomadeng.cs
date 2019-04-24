// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：跑马灯组件
// 作成時間：2018-07-26
// 類を作る：Transform_Paomadeng.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transform_Paomadeng : MonoBehaviour {
    /// <summary>
    /// 跑马灯组件
    /// </summary>
    public Text paomadengTxt;
    /// <summary>
    /// 跑马灯父类
    /// </summary>
    public RectTransform textTran;
    /// <summary>
    /// 跑马灯字符串
    /// </summary>
    private string paomadengStr;

    private DataInfo<DataItem.PaomaDengData> _data;
    // Use this for initialization
    void Start()
    {
        this.SetMarquee();
        DataManager.GetDataManager.paomaDeng.EventObj.Addlistener(UpdateData);
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMarquee();
    }
    private void UpdateData(object data)
    {
        if (data == null) return;
        _data = data as DataInfo<DataItem.PaomaDengData>;
        paomadengStr = _data.Data.ToString();
    }

    private void SetMarquee()
    {
        float interval = 400, marqueeLength = 300;
        paomadengTxt.text = paomadengStr;
        textTran.anchoredPosition = new Vector2(marqueeLength, 0);
        float nextPosX = textTran.anchoredPosition.x + textTran.sizeDelta.x + interval;
        textTran.anchoredPosition = new Vector2(nextPosX, 0);
    }

    private void UpdateMarquee()
    {
        if (paomadengTxt.text == "")
            return;
        float marqueeSpeed = 90f, interval = 400, marqueeLength = 400;
        if (textTran.anchoredPosition.x > -textTran.sizeDelta.x)
            textTran.anchoredPosition = new Vector2(textTran.anchoredPosition.x - marqueeSpeed * Time.deltaTime, 0);
        else
        {
            float nextPosX = textTran.anchoredPosition.x + textTran.sizeDelta.x + interval;
            if (nextPosX < marqueeLength)
                nextPosX = marqueeLength;
            textTran.anchoredPosition = new Vector2(nextPosX, 0);
        }
    }
}
