using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
public class GlobName : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Type t = typeof(GlobName);
        MethodInfo mt = t.GetMethod("DebugMsO");
        var obj = System.Activator.CreateInstance(t);
        if (mt == null)        
        {
            Debug.Log("没有获取到相应的函数！！");
        }
        else
        {
            mt.Invoke(obj, new object[] { "000","111"});
        }
    }


    public void DebugMsO(string A,string B)
    {
        Debug.Log(A+"输出"+B);
    }
}
