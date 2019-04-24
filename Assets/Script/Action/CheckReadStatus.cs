using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckReadStatus : MonoBehaviour {

    public GameObject Login, Reg,xieyiobj;
    [SerializeField]
    private Scrollbar Xieyi;
    [SerializeField]
    private GameObject ShowWarm;
    public void Check()
    {
        if (Static.Instance.AgreeDeals)
        {
            Reg.SetActive(true);
            xieyiobj.SetActive(false);
        }
        else
        {
            ShowWarm.SetActive(true);
        }
    }

    public void Stop()
    {
        xieyiobj.SetActive(false);
        Login.SetActive(false);
    }

    public void GoNo()
    {
        ShowWarm.SetActive(false);
    }

    public void SetValue()
    {
        if(Xieyi.value<0.1f&&Xieyi.value>0)
        Static.Instance.AgreeDeals = true;
    }

    [SerializeField]
    private HttpModel Http_Xieyi;
    public void ShowZhuCe()
    {
       
        if (Static.Instance.AgreeDeals)
        {
            Reg.SetActive(true);
        }
        else
        {
            xieyiobj.SetActive(true);
            Http_Xieyi.Get();
        }
    }
}


