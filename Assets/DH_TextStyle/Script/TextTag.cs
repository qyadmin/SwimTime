using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextTag{

    public TextTag(List<string> allname)
    {
        TextType = "自定义风格";
        localnub = allname.IndexOf(TextType);
    }


    public string TextType;

    public int localnub;
    public bool IsControl;

	public GameObject GetObjectByComponentObj()
	{ 
		List<GameObject> allObject = GetAllObjectsInScene(); 
		GameObject OBJ = null;
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.GetComponent<Canvas>())
			{ 
				OBJ = nowObj;
				break;
			} 
		} 
		return OBJ;
	} 

	public List<GameObject> GetAllObjectsInScene() 
	{ 
		GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)); 
		List<GameObject> pReturn = new List<GameObject>(); 
		foreach (GameObject pObject in pAllObjects) 
		{ 
			if (pObject.hideFlags == HideFlags.NotEditable) 
			{ 
				//Debug.Log("1111111111111----------" + pObject.name); 
				continue; 
			} 
			if (pObject.hideFlags == HideFlags.HideAndDontSave) 
			{ 
				//Debug.Log("2222222222222----------" + pObject.name); 
				continue; 
			} 

			pReturn.Add(pObject); 
		} 

		foreach (GameObject value in pReturn) 
		{ 
			//Debug.Log("33333333333333333-----" + value.name); 
		} 
		//pReturn.Clear(); 
		//pReturn = null; 
		return pReturn; 
	} 
}
