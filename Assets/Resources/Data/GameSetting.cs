using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName="Editor/GameSetting")]
public class GameSetting : ScriptableObject {

	public string Level="1.0.0";
	public bool Md5Losck=true;
	public AudioClip BackMusic;
	public AudioClip ButtonMusic;
	public AudioClip KaikenMusic;
	public Button Button_BackMusic;
	public Sprite OpneIamge;
	public Sprite CloseIamge;
	public GameObject MessageObj;
	public GameObject LoadObj;
	public Font SetFont;
	public string SaveName;
	public List<string> URL=new List<string>();
	public List<Sprite> ImageList=new List<Sprite>();
	public string[] URLList;
	public string KeyValue;
    public bool IsDebug=false;
}
