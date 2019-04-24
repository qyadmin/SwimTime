// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：个人信息窗口组件
// 作成時間：2018-07-26
// 類を作る：Transform_User.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;
using DataItem;
using LitJson;
public class Transform_User : MonoBehaviour
{
    //个人信息界面信息数据
    public Text _userNameText, out_userNameText;
    public Text _userIDText, out_userIDText, _yuguangid;
    public Text _userMoneyTxt, _MainMoney, _car;
    public Text _userLvlTxt, out_userLvlTxt;
    public Text _user_ethTxt;
    public Text _user_addressTxt;
    public RawImage Head, headout;
    public Transform shangjiwd;
    public HttpModel sj;
    private DataInfo<UserData> _data;
    public HttpModel http_userInfo;
    public Button headBtn;
    public Text ethinfoTxt;
    public Button tuiguangBtn;
    public HttpModel http_getQRcode;
    public RawImage QRcode;
    void Gedata(object data)
    {

    }



    void Start()
    {
        DataManager.GetDataManager.user.EventObj.Addlistener(UpdateData);
        sj.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage obj)
       {
           if (obj.Code == HttpCode.SUCCESS)
               GameManager.GetGameManager.CloseWindow(shangjiwd);
       });


        tuiguangBtn.onClick.AddListener(() =>
        {
            http_getQRcode.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
            {
                LoadImage.GetLoadIamge.Load(msg.Data["img"].ToString(), new RawImage[] { QRcode });
            });
            http_getQRcode.Get();
        });
        headBtn.onClick.AddListener(() =>
        {
            //http_ethInfo.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
            //{
            //    if (msg.Data["ok"].ToString() == "ok")
            //    {
            //        ethinfoTxt.text = msg.Data["ethinfo"].ToString();
            //    }
            //});
            //http_ethInfo.Get();
            http_userInfo.Get();
        });
    }
    private void UpdateData(object data)
    {
        if (data == null) return;
        _data = data as DataInfo<UserData>;
        Static.Instance.AddValue("phone", _data.Data.phone.ToString());
        out_userNameText.text = _userNameText.text = _data.Data.nickname;
        _yuguangid.text = out_userIDText.text = _userIDText.text = "ID: " + _data.Data.id.ToString();
        _userMoneyTxt.text = _data.Data.sb.ToString();
        _MainMoney.text = _data.Data.sb.ToString();
        _car.text = _data.Data.cra.ToString();
        out_userLvlTxt.text = _userLvlTxt.text = "等级:" + GlobalData.GameConstConfig.GetGuojikuangyouUserLevel(_data.Data.level);
        _user_ethTxt.text = _data.Data.eth.ToString();
        _user_addressTxt.text = _data.Data.address.ToString();
        LoadImage.GetLoadIamge.Load(_data.Data.avatar, new RawImage[] { Head, headout });
        Debug.Log(_data.Data.superior + "--------------superior");
        if (_data.Data.superior == "" || _data.Data.superior == "0")
        {
            GameManager.GetGameManager.OpenWindow(shangjiwd);
        }
    }
}
