using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonDt
{
	public Sprite normal, selen;
	public Button btn;
	public GameObject Obj;

}

public class ButtonChangeGroup : MonoBehaviour {

	public  List<ButtonDt> all = new List<ButtonDt>();
	public void Start()
	{
		foreach(ButtonDt child in all)
		{
			child.btn.onClick.AddListener (delegate() 
			{
					foreach(ButtonDt item in all)
					{
						if(item.Obj)
						item.Obj.SetActive(false);
						if(item.normal)
						item.btn.image.sprite=item.normal;

					}
					if(child.Obj)
					child.Obj.SetActive(true);
					if(child.selen)
					child.btn.image.sprite=child.selen;
			});
		}
	}

	public void SetFist()
	{
		foreach(ButtonDt item in all)
		{
			
			if(item.Obj)
			item.Obj.SetActive(false);
			if(item.normal)
			item.btn.image.sprite=item.normal;
		}
		if(all[0].Obj)
		all[0].Obj.SetActive(true);
		if(all[0].selen)
			all[0].btn.image.sprite=all[0].selen;	
	}
}
