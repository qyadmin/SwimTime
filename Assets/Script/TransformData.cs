using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TransformData : MonoBehaviour {

	public Dictionary<string,Transform > BodyDic = new Dictionary<string, Transform>();

	public U GetObjectValue<U>(string KEY) where U:UnityEngine.Object
	{
		Transform Obj = null;
		bool isget=BodyDic.TryGetValue (KEY,out Obj);
		if(isget)
			return Obj.GetComponent<U> ();
		return null;
	}

	public void Add(string Key,Transform Value)
	{
		if (BodyDic.ContainsKey(Key))
			BodyDic.Remove(Key);
		BodyDic.Add(Key, Value);
	}

	public void Remove(string Key)
	{
		if (BodyDic.ContainsKey(Key))
			BodyDic.Remove(Key);
	}

	[SerializeField]
	private Transform[] body;

	void OnEnable()
	{
		foreach (Transform child in body) 
		{
			Add (child.name,child);
		}
	}

}
