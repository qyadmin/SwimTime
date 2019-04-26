using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Runtime.InteropServices;

public class Call : MonoBehaviour
{
    public HttpModel http_userinfo;
    public class Pay
    {
        public string money;
        public string fangka;
        public string uid;
        public string t;
    }
    private Pay mPay;


    [DllImport("__Internal")]
    private static extern void _Payforios(string obj);


    public void send(int num)
    {
        mPay = new Pay();
        mPay.money = num.ToString();
        mPay.fangka = num.ToString();
        mPay.uid = Static.Instance.GetValue("phone");
        mPay.t = "房卡";
#if UNITY_ANDROID
        AndroidJavaObject javaObject = new AndroidJavaObject("io.dcloud.NjsHello");
        javaObject.CallStatic("Demo", JsonMapper.ToJson(mPay), javaObject);

#elif UNITY_IPHONE
		_Payforios(JsonMapper.ToJson(mPay));
#endif
    }

    [SerializeField]
    private Text msg;
    public void call(string data)
    {
        msg.text = data;
    }

    public void PayCallBack(string obj)
    {
        http_userinfo.Get();
    }
}
