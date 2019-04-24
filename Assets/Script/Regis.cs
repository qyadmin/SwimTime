using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Regis : MonoBehaviour {
    [SerializeField]
    InputField PassWord,PassWord_;
    [SerializeField]
    InputField JYPassWord, JYPassWord_;
    [SerializeField]
    InputField YanZhengMa;
    [SerializeField]
    Text YanZhengMa_;
    [SerializeField]
    UnityEvent suc, fal;
    //    [SerializeField]
    //    Text Eorro;
    //    [SerializeField]
    //    HttpMessageModel htp;
    public Toggle xy;
    public void Validation()
    {
        if (xy.isOn)
            suc.Invoke();
        else
            MessageManager._Instantiate.Show("你没有同意变态矿工的协议");
    }

    public void Succece()
    {
       // Eorro.text = htp.Data.GetBase.msg;
    }
    public void Fail()
    {
        //Eorro.text = htp.Data.GetBase.msg;
    }
}
