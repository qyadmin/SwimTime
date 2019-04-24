// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：兑换窗口组件
// 作成時間：2017-07-26
// 類を作る：Transform_Duihuan.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class Transform_Duihuan : MonoBehaviour
{
    public Button submitBtn, goldAddBtn;
    public HttpModel http_duihuan;
    public InputField numInput;
    public Transform WindowBASE_duihuan;
    public Button chongzhiBtn;
    void Start()
    {
        goldAddBtn.onClick.AddListener(Init);
        chongzhiBtn.onClick.AddListener(Chongzhi);
    }

    private void Init()
    {
        GameManager.GetGameManager.OpenWindow(WindowBASE_duihuan);
        numInput.text = "";
        DuihuanEveny();
    }
    private void DuihuanEveny()
    {
        submitBtn.onClick.AddListener(() =>
        {
            http_duihuan.Data.AddData("num", numInput.text);
            http_duihuan.Get();
        });
    }

    private void Chongzhi()
    {
        MessageManager._Instantiate.Show("此功能暂时未开通");
    }
}
