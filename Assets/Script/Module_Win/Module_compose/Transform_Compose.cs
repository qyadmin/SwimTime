// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：在这里修改脚本说明
// 作成時間：2018-08-07
// 類を作る：Transform_Compose.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class Transform_Compose : MonoBehaviour
{
    public Button huoBtn, muBtn;
    public Button composeBtn, composeUIBtn;
    public Image leftSelectImg, rightSelectImg;
    public Toggle _30dayTgl, _60dayTgl, _90dayTgl;
    public Transform WindowBASE_upgradeSuccess, WindowBASE_compose;
    public GameObject successEffect, failedEffect;
    public HttpModel http_compose;

    private int days = 0;

    private void Start()
    {
        this.Init();
    }

    private void Init()
    {
        composeUIBtn.onClick.AddListener(() =>
        {
            _30dayTgl.isOn = false;
            _60dayTgl.isOn = false;
            _90dayTgl.isOn = false;
            leftSelectImg.enabled = false;
            rightSelectImg.enabled = false;
            this.SelectSetting();
        });
    }

    private void SelectSetting()
    {
        _30dayTgl.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
                days = 30;
        });

        _60dayTgl.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
                days = 60;
        });

        _90dayTgl.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
                days = 90;
        });

        huoBtn.onClick.AddListener(() =>
        {
            leftSelectImg.enabled = true;
            rightSelectImg.enabled = false;
        });

        muBtn.onClick.AddListener(() =>
        {
            leftSelectImg.enabled = false;
            rightSelectImg.enabled = true;
        });

        this.ComposeEvent();
    }

    private void ComposeEvent()
    {
        composeBtn.onClick.AddListener(() =>
        {
            if (leftSelectImg.enabled == false && rightSelectImg.enabled == false)
            {
                MessageManager._Instantiate.Show(GlobalData.PLEAST_SELECT_MUENERGY_OR_HUOENERGY);
                return;
            }
            if (_30dayTgl.isOn == false && _60dayTgl.isOn == false && _90dayTgl.isOn == false)
            {
                MessageManager._Instantiate.Show(GlobalData.PLEASE_SELECT_DAYS);
                return;
            }

            http_compose.Data.AddData(GlobalData.day, days.ToString());
            http_compose.Get();
            http_compose.HttpSuccessCallBack.Addlistener((ReturnHttpMessage rhMsg) =>
            {
                Debug.Log(rhMsg.Code + "-----HTTPCODE");
                if (rhMsg.Code == HttpCode.SUCCESS)
                {
                    GameManager.GetGameManager.ThisWindowStateSetting(WindowBASE_upgradeSuccess, WindowBASE_compose);
                    successEffect.SetActive(true);
                    failedEffect.SetActive(false);
                }
            });
        });
    }
}
