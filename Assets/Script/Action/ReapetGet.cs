using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapetGet : MonoBehaviour {

    [SerializeField]
    private int timestart=1;
    [SerializeField]
    private HttpModel Loop;
    [SerializeField]
    private bool Stop;
	void Start ()
    {
        InvokeRepeating("Getmrak", timestart, 5);
	}

    void Getmrak()
    {
        if(!Stop)
        Loop.Get();
    }

}
