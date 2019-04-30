// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：加减按钮控制文本数字自增
// 作成時間：2018-07-26
// 類を作る：NumAddAndCut.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumAddAndCut : MonoBehaviour {
    public Button addBtn, cutBtn;
    public InputField inputNum;
    private int num = 1;
    // Use this for initialization
    void Start()
    {
        CountSelect();
    }

    public void OnDisable()
    {
        Init();
    }
    public void Init()
    {
        num = 1;
        inputNum.text = num.ToString();
    }
    private void CountSelect()
    {
        addBtn.onClick.AddListener(() =>
        {
            inputNum.text = (++num).ToString();
        });
        cutBtn.onClick.AddListener(() =>
        {
            if (num > 1)
                inputNum.text = (--num).ToString();
            else
                inputNum.text = "1";
        });
    }
}
