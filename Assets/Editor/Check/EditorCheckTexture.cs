using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;
public class EditorCheckTexture : ScriptableWizard { 

	[MenuItem("GameSetting/Check/检查图片")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<EditorCheckTexture>("检查图片", "CheckIamge"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");
	}


	public void GetObjectByComponent(string GetName)
	{ 
		ErrorList.Clear ();
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.GetComponent<Image>())
			{ 
				Debug.Log (nowObj.name);
					if(!nowObj.GetComponent<Image>().mainTexture.name.Contains (GetName))
					ErrorList.Add (nowObj.GetComponent<Image>());
			} 
		} 
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

	public List<Image> ErrorList=new List<Image>();
	private string Name;
	private string ReplaceName;
	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	Vector2 ScrollView;

	void OnGUI()
	{
		ScrollView = GUILayout.BeginScrollView (ScrollView);
		{
			GUILayout.Space (10);
			Name = EditorGUILayout.TextField ("查找不包含的字符串:", Name);

			if (GUILayout.Button ("检查", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponent (Name); 
			}

			GUILayout.Space (30);
			_serializedObject.Update ();

			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (_assetLstProperty, true);
			if (EditorGUI.EndChangeCheck ()) {
				_serializedObject.ApplyModifiedProperties ();
			}

	
	
			if (GUILayout.Button ("关闭窗口", GUILayout.Width (150), GUILayout.Height (20))) {
				this.Close ();
			}
		}
		GUILayout.EndScrollView ();
	}
} 