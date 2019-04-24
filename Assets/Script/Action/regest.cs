using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class regest : MonoBehaviour {

    public HttpModel Resget;
    public Toggle xy;
    public void ResgetGo()
    {
        if (xy.isOn)
            Resget.Get();
        else
            MessageManager._Instantiate.Show("你没有同意变态矿工的协议");
    }
}
