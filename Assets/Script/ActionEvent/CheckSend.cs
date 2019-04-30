using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckSend : MonoBehaviour {


    [SerializeField]
    private Toggle Table;
    [SerializeField]
    private HttpModel Autreq;
    [SerializeField]
    private HttpModel REAutreq;

    private void Start()
    {
        GetComponent<ButtonEventBase>().ActionEvent += CheckState;
    }
    public void CheckState()
    {
        if (Table.isOn)
        {
            REAutreq.Get();

        }
        else
        {
            Autreq.Get();
        }

    }

}
