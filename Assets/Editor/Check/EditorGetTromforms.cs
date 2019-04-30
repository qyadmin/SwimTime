using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;

public class EditorGetTromforms : ScriptableWizard { 

	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	Vector2 ScrollView;
	[MenuItem("GameSetting/Check/获取组件")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<EditorGetTromforms>("获取组件", "EditorGetTromforms"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");
	}
		
	public void GetObjectByComponentObj(string Name)
	{ 
		ErrorList.Clear ();
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.name==Name)
			{ 
				ErrorList.Add (nowObj);
			} 
		} 
	} 

	public void GetObjectByComponent(string Name)
	{ 
		ErrorList.Clear ();
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.GetComponent(Name))
			{ 
				ErrorList.Add (nowObj);
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

	public List<GameObject> ErrorList=new List<GameObject>();
	private string Tex;
	private string Name;
	void OnGUI()
	{
		ScrollView=GUILayout.BeginScrollView (ScrollView);
		{
			GUILayout.Space (10);
			Tex = EditorGUILayout.TextField("组件名称:",Tex);
			if (GUILayout.Button ("检查", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponent (Tex); 
			}
			Name = EditorGUILayout.TextField("物体名称:",Name);
			if (GUILayout.Button ("查找", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponentObj (Name);
			}

			GUILayout.Space (30);
			_serializedObject.Update ();

			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (_assetLstProperty, true);
			if (EditorGUI.EndChangeCheck ()) {
				_serializedObject.ApplyModifiedProperties ();
			}

			if (GUILayout.Button ("删除", GUILayout.Width (250), GUILayout.Height (30))) {

				foreach (var child in ErrorList) 
				{
					DestroyImmediate( child.GetComponent (Tex));
				}
			}

			if (GUILayout.Button ("关闭窗口", GUILayout.Width (150), GUILayout.Height (20))) {
				this.Close ();
			}
		}
		GUILayout.EndScrollView();
	}
} 