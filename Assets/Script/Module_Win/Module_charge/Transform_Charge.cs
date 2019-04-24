// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：充值窗口组件
// 作成時間：2018-07-26
// 類を作る：Transform_Charge.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;
using DataItem;
public class Transform_Charge : MonoBehaviour
{
    public Transform FatherObj;
    public GameObject listobj;
    public HttpModel Buy;
    public Transform BuyWidnow;
    DataDic<ShopData> _data;
    void Start()
    {
        DataManager.GetDataManager.shop.EventObj.Addlistener(UpdateData);
    }


    public void UpdateData(object dataObj)
    {

        _data = dataObj as DataDic<ShopData>;
        GameManager.GetGameManager.ClearChild(FatherObj);
        foreach (ShopData child in _data.Data)
        {
            GameObject item = ObjectPool.GetInstance().GetObj(listobj, FatherObj);
            TransformData Transformdata = item.GetComponent<TransformData>();
            Transformdata.GetObjectValue<Text>("desc").text = child.msg;
            Transformdata.GetObjectValue<Text>("piece").text = child.price.ToString();
            ConfigManager.GetConfigManager.SetCarImage(Transformdata.GetObjectValue<RawImage>("head"), child.index - 1);
            Transformdata.GetObjectValue<Button>("buy").onClick.AddListener(delegate ()
            {
                GameManager.GetGameManager.OpenWindow(BuyWidnow);
                TransformData td = BuyWidnow.GetComponent<TransformData>();
                td.GetObjectValue<Text>("desc").text = child.msg;
                td.GetObjectValue<Text>("price").text = child.price.ToString();
                ConfigManager.GetConfigManager.SetCarImage(td.GetObjectValue<RawImage>("head"), child.index - 1);
                Button buyBtn = td.GetObjectValue<Button>("btn");
                buyBtn.onClick.RemoveAllListeners();
                buyBtn.onClick.AddListener(() =>
                  {
                      Buy.Data.AddData("tp", child.tp);
                      Buy.Get();
                  });
            });
        }
    }
}
