using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
       
	}

    void callback()
    {
        ObjectPool.GetInstance().RecycleObj(this.gameObject);
    }

}
