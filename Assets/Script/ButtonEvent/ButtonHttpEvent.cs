using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public  class ButtonHttpEvent : ButtonEventBase {

    private TypeEvent MyModel;
    private string SaveValue;
    public override void Start()
    {
        base.Start();
    }
    public void AddHttpListener(TypeEvent GetModel, string GetValue)
    {
        ActionEvent += GetModel.SendModel.Get;
        MyModel = GetModel;
        SaveValue = GetValue;
    }

    public override void AddListener(Action GetListener)
    {
        base.AddListener(GetListener);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (MyModel != null)
            MyModel.SetData(SaveValue);
        base.OnPointerClick(eventData);
    }
}
