using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;
public class ObtainObjByComponent : ScriptableWizard { 

	public string componentName = "Text"; 

	[MenuItem("GameSetting/Check/检查名字")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<ObtainObjByComponent>("检查字符串", "check"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");
	}
	void OnWizardCreate () { 
		GetObjectByComponent(); 
	} 

	public void Repalce(string OLDNAME,string NEWNAME)
	{
		foreach(GameObject child in ErrorList)
		{ 
			string str = child.GetComponent<Text> ().text;
			Debug.Log (str);
			str=str.Replace (OLDNAME,NEWNAME);
			child.GetComponent<Text> ().text = str;
		}
	}



	public void GetObjectByComponent()
	{ 
		ErrorList.Clear ();
		Debug.Log(componentName); 
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject){ 
			if(nowObj.GetComponent(componentName)){ 
				if(nowObj.GetComponent<Text>().text.Contains (Name))
				ErrorList.Add (nowObj);
			} 
		} 
//		if(componentObj.Count > 0){ 
//			Selection.objects = componentObj.ToArray(); 
//
//		}else{ 
//			Selection.objects = componentObj.ToArray(); 
//			Debug.Log("no Object"); 
//		} 

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

	public List<GameObject> ErrorList=new List<GameObject>();
	private string Name;
	private string ReplaceName;
	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	Vector2 ScrollView;
		
	void OnGUI()
	{
		ScrollView=GUILayout.BeginScrollView (ScrollView);
		{
			GUILayout.Space (10);
			Name = EditorGUILayout.TextField ("输入查找字符串:", Name);

			if (GUILayout.Button ("检查", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponent (); 
			}
	
			GUILayout.Space (30);
			_serializedObject.Update ();

			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (_assetLstProperty, true);
			if (EditorGUI.EndChangeCheck ()) {
				_serializedObject.ApplyModifiedProperties ();
			}
	
			GUILayout.Space (30);
			if (ErrorList.Count > 0) {
				ReplaceName = EditorGUILayout.TextField ("输入替换字符串:", ReplaceName);
				if (GUILayout.Button ("替换", GUILayout.Width (250), GUILayout.Height (30))) {
					Repalce (Name, ReplaceName); 
				}
			}

	
			if (GUILayout.Button ("关闭窗口", GUILayout.Width (150), GUILayout.Height (20))) {
				this.Close ();
			}
		}
		GUILayout.EndScrollView ();
	}
} 