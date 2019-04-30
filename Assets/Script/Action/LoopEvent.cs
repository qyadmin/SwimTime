using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopEvent : MonoBehaviour {

	[SerializeField]
	HttpModel http_gg;
	// Use this for initialization
	void Start () {
		//InvokeRepeating ("UpdateGg", 2, 30);
	}
	
	// Update is called once per frame
	void UpdateGg () 
	{
		http_gg.Get ();
	}
}
