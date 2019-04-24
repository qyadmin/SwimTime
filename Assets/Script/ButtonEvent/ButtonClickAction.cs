using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public  class  ButtonClickAction : ButtonEventBase
{
	public override void Start()
	{
        base.Start();		
	}

    public override void AddListener(Action GetListener)
	{
        base.AddListener(GetListener);
	}

	public override void OnPointerClick(PointerEventData eventData)
	{     
        base.OnPointerClick(eventData);
	}
		
}