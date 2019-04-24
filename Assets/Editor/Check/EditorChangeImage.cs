using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;

public class EditorChangeImage : ScriptableWizard { 
 
	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	Vector2 ScrollView;
	[MenuItem("GameSetting/Check/替换图片")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<EditorChangeImage>("替换图片", "EditorChangeImage"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");
	}

	public void Peplatext()
	{ 
		foreach(Image nowObj in ErrorList)
		{ 
			nowObj.sprite = ReplaceTex;
		} 
	} 


	public void GetObjectByComponent(Sprite GetTex)
	{ 
		ErrorList.Clear ();
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.GetComponent<Image>())
			{ 
				Debug.Log (nowObj.name);
				if(nowObj.GetComponent<Image>().sprite==GetTex)
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
	private Sprite Tex;
	private Sprite ReplaceTex;

	void OnGUI()
	{
		ScrollView=GUILayout.BeginScrollView (ScrollView);
		{
			GUILayout.Space (10);
			Tex = EditorGUILayout.ObjectField ("增加一个贴图", Tex, typeof(Sprite), true) as Sprite;

			if (GUILayout.Button ("检查", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponent (Tex); 
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
				ReplaceTex = EditorGUILayout.ObjectField ("增加一个替换贴图", ReplaceTex, typeof(Sprite), true) as Sprite;
				if (GUILayout.Button ("替换", GUILayout.Width (250), GUILayout.Height (30))) {
					Peplatext (); 
				}
			}


			if (GUILayout.Button ("关闭窗口", GUILayout.Width (150), GUILayout.Height (20))) {
				this.Close ();
			}
		}
		GUILayout.EndScrollView();
	}
} 