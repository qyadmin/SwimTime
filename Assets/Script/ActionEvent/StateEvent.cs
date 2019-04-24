using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateEvent : MonoBehaviour {


    [SerializeField]
    private GameObject NoMoney,Money;
    [SerializeField]
    private Text textmessage;
    [TextArea]
    public string message;

    public void StatePiece(string GetData)
    {
        if (GetData == "0")
        {
            if (Money)
            {
                GameManager.GetGameManager.OpenWindow(Money.transform);
            }
        }
        else
        {
            if (NoMoney)
            {
                GameManager.GetGameManager.OpenWindow(NoMoney.transform);
                textmessage.text = message.Replace("s",GetData);
            }
        }
    }
}
