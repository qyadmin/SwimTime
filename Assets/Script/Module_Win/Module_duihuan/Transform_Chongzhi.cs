// ==================================================================
// 作    者：Pablo.风暴洋-宋杨
// 説明する：打开充值界面生成以太币充值二维码
// 作成時間：2019-04-25
// 類を作る：BarcodeCam.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================
using UnityEngine;
using UnityEngine.UI;

public class Transform_Chongzhi : MonoBehaviour
{
    [SerializeField]
    private Image chongzhiQRcodeImg;
    [SerializeField]
    private HttpModel http_getChongzhiQRcode;
    [SerializeField]
    private BarcodeCam barcodeCam;

    public void GetChongzhiQRcodeImg()
    {
        http_getChongzhiQRcode.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
        {
            if (msg.Data["ok"].ToString() == "ok")
            {
                string chongzhiAddressQRcodeBase64String = msg.Data["address"].ToString();
                barcodeCam.GetQRcodeImg(chongzhiQRcodeImg, chongzhiAddressQRcodeBase64String);
            }
        });
        http_getChongzhiQRcode.Get();
    }
}
