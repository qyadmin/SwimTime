using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;

public class EditorBackSetting: ScriptableWizard { 

	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	Vector2 ScrollView;
	[MenuItem("GameSetting/Back编辑")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<EditorBackSetting>("BackSetting", "EditorChangeImage"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");

	}


	public void ChangeSize()
	{
		foreach (RectTransform child in ErrorList) 
		{
			child.sizeDelta += ScaleVector;
		}
	}


	public void ChangePosition()
	{
		foreach (RectTransform child in ErrorList) 
		{
			child.localPosition += MoveVector;
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
					ErrorList.Add (nowObj.GetComponent<RectTransform>());
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

	public List<RectTransform> ErrorList=new List<RectTransform>();
	private Sprite Tex;
	private Sprite ReplaceTex;

	public Vector2 ScaleVector;
	public Vector3 MoveVector;
	void OnGUI()
	{
		ScrollView=GUILayout.BeginScrollView (ScrollView);
		{
			GUILayout.Space (10);
			Tex = EditorGUILayout.ObjectField ("增加一个贴图", Tex, typeof(Sprite), true) as Sprite;

			if (GUILayout.Button ("获取当前BACK组件", GUILayout.Width (250), GUILayout.Height (30))) {
				GetObjectByComponent (Tex); 
			}

			GUILayout.Space (30);
			MoveVector = EditorGUILayout.Vector3Field("偏移坐标",MoveVector);
			if (GUILayout.Button ("变换位置", GUILayout.Width (250), GUILayout.Height (30))) {
				ChangePosition ();
			}

			GUILayout.Space (30);
			ScaleVector = EditorGUILayout.Vector2Field("尺寸比例",ScaleVector);
			if (GUILayout.Button ("变换尺寸", GUILayout.Width (250), GUILayout.Height (30))) {
				ChangeSize ();
			}
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
		GUILayout.EndScrollView();
	}
} 