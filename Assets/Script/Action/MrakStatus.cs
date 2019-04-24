using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrakStatus : MonoBehaviour {

    [SerializeField]
    private GameObject mrak;
    public void SetStatus(string data)
    {
        if(data=="0")
        mrak.SetActive(false);
        else
            mrak.SetActive(true);
    }

    public void SetStatusTurn(string data)
    {
        if (data == "0")
            mrak.SetActive(true);
        else
            mrak.SetActive(false);
    }

    public void SetStatusTurnBack(string data)
    {
        if (data != "0")
            mrak.SetActive(true);
        else
            mrak.SetActive(false);
    }
}
