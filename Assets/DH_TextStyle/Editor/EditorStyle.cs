using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;
using UnityEditorInternal;
public class  EditorStyle: ScriptableWizard { 

	[System.Serializable]
	public class TextMessage
	{
		public int Nub;
		public TextTag MyTag;
		public Text Object;
        public Transform GetTransform { get { return Object.transform; } }

        public Text ChangeStyle()
        {
            UpdateModle.UpdateState(this.Object.transform);
            return this.Object;
        }

		public TextMessage(Text getText)
		{
			Object=getText;
			MyTag=getText.GetComponent<TextStyleMessage> ().DemoTag;
		}

		public void ChangeTag(string NowChangeName)
		{
			MyTag.TextType =NowChangeName;
		}

		public void Getname(List<string> nowname)
		{
			Nub = nowname.IndexOf (MyTag.TextType);
        }

		public void SetTag(List<string> nowname)
	    {
           
            if (Nub <= nowname.Count - 1)
            {
                MyTag.localnub = Nub;
            }
	    }
	}

	public List<TextMessage> EditorText=new List<TextMessage>();

    private void AddtextManager(GameObject child)
    {
            EditorText.Add(GetTextGroup(child));
    }

	public TextMessage TextGet;


	public GradientType ChoseGradientType;
	public TextGroup AllGroup=TextGroup.TypeA;
	public OutLineVector ShadowVector;


	protected SerializedObject _serializedObject;
    protected SerializedObject Text_serializedObject;
    protected SerializedProperty _assetLstProperty;


	protected SerializedProperty _OutLineProperty;
	protected SerializedProperty _ShadowProperty;
	protected SerializedProperty _GradientProperty;


	protected SerializedProperty _FontColorProperty;
	protected SerializedProperty _FontSizeProperty;
	protected SerializedProperty _FontProperty;
	protected SerializedProperty _RaycastProperty;
	protected SerializedProperty _SizeProperty;
	protected SerializedProperty _PositionProperty;


	protected SerializedProperty bool_FontColorProperty;
	protected SerializedProperty bool_FontSizeProperty;
	protected SerializedProperty bool_FontProperty;
	protected SerializedProperty bool_RaycastProperty;
	protected SerializedProperty bool_SizeProperty;
	protected SerializedProperty bool_PositionProperty;


	protected SerializedProperty _StartColorProperty;
	protected SerializedProperty _CenterColorProperty;
	protected SerializedProperty _EndColorProperty;


	protected SerializedProperty _Shadow_StartColorProperty;
	protected SerializedProperty _OutLine_StartColorProperty;


	protected SerializedProperty _EffectSizeProperty;
	protected SerializedProperty _ShadowEffectSizeProperty;
	protected SerializedProperty _GradientTypeProperty;
	protected SerializedProperty _AllGroupProperty;
	protected SerializedProperty _ShadowVectorProperty;

	protected SerializedProperty TypeNameProperty;

	private StyleSetting StyleData;

	private StyleSetting MyText;
	Vector2 ScrollView;

	private ReorderableList TextList;

	public string TypeStyleName;
	public string NowTypename;
	public string LastTypeName;
	public static int SaveNub;
	List<string> AllName=new List<string>();

	[MenuItem("GameSetting/Editor/Text_StyleManager")] 
	public static void CreateWizard () 
	{
        if (UpdateModle.GetStyleGroup().Count <= 1)
        {
            if (EditorUtility.DisplayDialog("Message", "You do not currently have editable data, please create at least one style", "OK"))
            {
                return;
            }
        }

        if(!IsOpne)
		ScriptableWizard.DisplayWizard<EditorStyle>("Text_StyleManager", "EditorFont"); 
	}
    public static void CreateWizard(int GetNub)
    {
        if (UpdateModle.GetStyleGroup().Count <= 1)
        {
            if (EditorUtility.DisplayDialog("Message", "You do not currently have editable data, please create at least one style", "OK"))
            {
                return;
            }
        }
        if (!IsOpne)
            ScriptableWizard.DisplayWizard<EditorStyle>("Text_StyleManager", "EditorFont");
        SaveNub = GetNub;
    }

    private string LastSceneName=string.Empty;
	private Text EditorDemoText;

	public Dictionary<string, StyleSetting> SaveText = new Dictionary<string, StyleSetting>();

	public StyleSetting GetContainer(string NUB)
	{
        StyleSetting obj = null;
		SaveText.TryGetValue (NUB,out obj);
		return obj;
	}


    public void UpdateType(int Nub)
    {
      
        StyleSetting[] AllSytleData = Resources.LoadAll<StyleSetting>("");

        if (AllName[Nub] == "自定义风格" || AllName[Nub] == "所有风格")
        {
            _serializedObject = new SerializedObject(AllSytleData[0]);
        }
        else
            _serializedObject = new SerializedObject(AllSytleData[Nub]);

        TypeNameProperty = Text_serializedObject.FindProperty("TypeStyleName");
        _assetLstProperty = _serializedObject.FindProperty("EditorText");

        _OutLineProperty = _serializedObject.FindProperty("OutLine");
        _ShadowProperty = _serializedObject.FindProperty("Shadow");
        _GradientProperty = _serializedObject.FindProperty("Gradient");

        _FontColorProperty = _serializedObject.FindProperty("FontColor");
        _FontSizeProperty = _serializedObject.FindProperty("FontSize");
        _FontProperty = _serializedObject.FindProperty("Font");
        _RaycastProperty = _serializedObject.FindProperty("Raycast");
        _SizeProperty = _serializedObject.FindProperty("SizeData");
        _PositionProperty = _serializedObject.FindProperty("PositionData");

        bool_FontColorProperty = _serializedObject.FindProperty("Color_Bool");
        bool_FontSizeProperty = _serializedObject.FindProperty("FontSize_Bool");
        bool_FontProperty = _serializedObject.FindProperty("Font_Bool");
        bool_RaycastProperty = _serializedObject.FindProperty("Raycast_Bool");
        bool_SizeProperty = _serializedObject.FindProperty("SizeData_Bool");
        bool_PositionProperty = _serializedObject.FindProperty("PositionData_Bool");


        _StartColorProperty = _serializedObject.FindProperty("StartColor");
        _CenterColorProperty = _serializedObject.FindProperty("CenterColor");
        _EndColorProperty = _serializedObject.FindProperty("EndColor");


        _Shadow_StartColorProperty = _serializedObject.FindProperty("ShadowStartColor");
        _OutLine_StartColorProperty = _serializedObject.FindProperty("OutLineStartColor");


        _EffectSizeProperty = _serializedObject.FindProperty("EffectSize");
        _ShadowEffectSizeProperty = _serializedObject.FindProperty("ShadowEffectSize");
        _GradientTypeProperty = _serializedObject.FindProperty("ChoseGradientType");
        _AllGroupProperty = _serializedObject.FindProperty("EditorGroup");
        _ShadowVectorProperty = _serializedObject.FindProperty("ShadowVector");
        lastNub = AllName[SaveNub];

    }
    List<string> AllNameReal = new List<string>();
    public void UpdateTag()
	{
		AllName.Clear ();
        AllNameReal.Clear();
        SaveText.Clear ();
        StyleSetting[] AllStyleData = Resources.LoadAll<StyleSetting>("");
     
		foreach (StyleSetting child in AllStyleData) 
		{
			if (SaveText.ContainsKey (child.TextMark))
				continue;
			SaveText.Add (child.TextMark,child);
			AllName.Add (child.TextMark.ToString());
            AllNameReal.Add(child.TextMark.ToString());
        }
        AllName.Add("自定义风格");
        AllNameReal.Add("自定义风格");
        AllName.Add ("所有风格");

        Debug.Log(AllName.Count);
    }


	public void UpdateAssetName()
	{
        if(LastTypeName==string.Empty)
		GetObjType (TypeStyleName);
        else
            GetObjType(LastTypeName);
        UpdateTag ();
		foreach (TextMessage child in EditorText) 
		{
			child.ChangeTag (TypeStyleName);
		}
	}

    public static bool IsOpne = false;
  
	protected void OnEnable()
	{
        IsOpne = true;
        SaveText.Clear ();
        UpdateTag();         
        UpdateSytleMessage (AllName[0]);

        Text_serializedObject= new SerializedObject(this);
        UpdateType(0);

		TextList = new ReorderableList(Text_serializedObject,
            Text_serializedObject.FindProperty("EditorText"),
			true, true, true, true);

		TextList.drawElementCallback += DrawNameElementObj;
        UpdateMessage();
    }

	private int LastindexNub=0;
	private void DrawNameElementObj(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = TextList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		if (index <= EditorText.Count - 1) 
		{
            EditorText [index].Nub = EditorGUI.Popup (new Rect (rect.x + rect.width / 3, rect.y, rect.width / 3f, EditorGUIUtility.singleLineHeight), EditorText [index].Nub, AllNameReal.ToArray ());
			EditorText [index].SetTag (AllNameReal);
		}
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width/3f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Object"), GUIContent.none);
	}


	public TextMessage GetTextGroup(GameObject obj)
	{
		TextMessage AA=new TextMessage(obj.GetComponent<Text> ());
		AA.Getname (AllName);
		return AA;
	}


	public void GetObjectByComponentObj()
	{ 
		EditorText.Clear ();
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> componentObj = new List<GameObject>(); 
		foreach(GameObject nowObj in allObject)
		{ 
			if(nowObj.GetComponent<Text>())
			{
                AddtextManager(nowObj);          
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


	void UpdateSytleMessage(string GetNub)
	{
		MyText= GetContainer (GetNub);
	}


    void MovePosition()
    {
        foreach (TextMessage child in EditorText)
        {
            child.GetTransform.position += MoveVector;
        }
    }


    private void SetTextStyle(Text GetDemoText)
    {
        if (GetDemoText == null)
            return;
        EditorDemoText = GetDemoText;
        if (!EditorDemoText.GetComponent<TextStyleMessage>())
            EditorDemoText.gameObject.AddComponent<TextStyleMessage>();
        DemoGradient = EditorDemoText.GetComponent<TextStyleMessage>();
        SetFontDemoSytle();
        EditorUtility.SetDirty(DemoGradient);
    }

	public void SetActionStyle()
	{
		foreach (TextMessage child in EditorText)
		{
            SetTextStyle(child.ChangeStyle());
            UpdateModle.UpdateState(child.Object.transform);
            child.Object.GetComponent<TextStyleMessage>().UpdateState();
        }

	}

    private TextStyleMessage DemoGradient;

	public void SetOutLine(bool or)
	{
        DemoGradient.IsDrawOutline = false;
		if (or) {
            DemoGradient.IsDrawOutline = true;
            DemoGradient.Outline_effectColor = MyText.OutLineStartColor;
            DemoGradient.Outline_effectDistance = MyText.EffectSize;
            DemoGradient.ShadowType = MyText.ShadowVector;
		} else {
            DemoGradient.IsDrawOutline = false;
		}

	}

	public void SetGradient(bool or)
	{
		DemoGradient.IsDrawGradent = false;
		if (or) {
			DemoGradient.IsDrawGradent = true;
			DemoGradient.colorTop = MyText.StartColor;
            DemoGradient.colorCenter = MyText.CenterColor;
            DemoGradient.colorBottom = MyText.EndColor;
			DemoGradient.GradientType =(TypeHV)MyText.ChoseGradientType;
		} else {
			DemoGradient.IsDrawGradent = false;
		}
	}


	public void SetShadow(bool or)
	{
        DemoGradient.IsDrawShadow = false;
		if (or) {
            DemoGradient.IsDrawShadow = true;
            DemoGradient.Shadow_effectColor = MyText.ShadowStartColor;
            DemoGradient.Shadow_effectDistance = MyText.ShadowEffectSize;
		} else {
            DemoGradient.IsDrawShadow = false;
        }		
	}
		
	public void SetFontDemoSytle()
	{

		if (IsEditor) {

			EditorDemoText.color = MyText.FontColor;

			EditorDemoText.fontSize = MyText.FontSize;

			EditorDemoText.rectTransform.sizeDelta = MyText.SizeData;

			EditorDemoText.rectTransform.localPosition = MyText.PositionData;

			EditorDemoText.font = MyText.Font;

			EditorDemoText.raycastTarget = MyText.Raycast;
		}
        else
        {
			if (MyText.Color_Bool)
				EditorDemoText.color = MyText.FontColor;
			if (MyText.FontSize_Bool)
				EditorDemoText.fontSize = MyText.FontSize;
            if (!SizeLock)
            {
                if (MyText.SizeData_Bool)
                    EditorDemoText.rectTransform.sizeDelta = MyText.SizeData;
                if (MyText.PositionData_Bool)
                    EditorDemoText.rectTransform.localPosition = MyText.PositionData;
            }
            else
            {
                MyText.SizeData_Bool = false;
                MyText.PositionData_Bool = false;
            }
			if (MyText.Font_Bool)
				EditorDemoText.font = MyText.Font;
			if (MyText.Raycast_Bool)
				EditorDemoText.raycastTarget = MyText.Raycast;
		}
		if (MyText.OutLine)
			SetOutLine (true);
		else
			SetOutLine (false);


		if (MyText.Gradient)
			SetGradient (true);
		else
			SetGradient (false);

		if (MyText.Shadow)
			SetShadow (true);
		else
			SetShadow (false);
		
	}


	public List<GameObject> GetText()
	{
		List<GameObject> allObject = GetAllObjectsInScene(); 
		List<GameObject> GetTextALL = new List<GameObject> ();
		foreach (GameObject nowObj in allObject) 
		{
			if(nowObj.GetComponent<Text>())
			{ 
				GetTextALL.Add (nowObj);
			} 
		}
		return GetTextALL;
	}


	public void GetObjType(string GetName)
	{
		EditorText.Clear ();

        foreach (GameObject nowObj in GetText())
        {
            if (!nowObj.GetComponent<TextStyleMessage>())
            {
                nowObj.AddComponent<TextStyleMessage>().DemoTag=new TextTag(AllName);
            }
        }

        if (GetName == "自定义风格")
        {
            foreach (GameObject nowObj in GetText())
            {
                if (!nowObj.GetComponent<TextStyleMessage>().UseBasicStyle)
                {
                    AddtextManager(nowObj);
                }
            }
            return;
        }
        if (GetName == "所有风格") 
		{
			foreach (GameObject nowObj in GetText()) 
			{
				if (nowObj.GetComponent<TextStyleMessage> ().UseBasicStyle) 
				{
                    AddtextManager(nowObj);
                }	
			}
			return;
		}

		foreach(GameObject nowObj in GetText())
		{ 
			if(nowObj.GetComponent<TextStyleMessage>().DemoTag.TextType==GetName)
			{
                AddtextManager(nowObj);
            } 
		} 
	}
		

	private bool CheckName(string GetName)
	{
		if (GetName == NowTypename)
			return true;
		if (AllName.Contains (GetName))
			return false;
		return true;
	}

	public bool AcitonBool;
	public TextGroup EditorGroup;
	private string lastNub;
	private bool IsEditor;
	private bool SizeLock=true;
	private string LockName="解锁位置信息";
    private bool EditorLock;

    public void UpdateMessage()
    {
        UpdateType(SaveNub);
        UpdateSytleMessage(AllName[SaveNub]);
        GetObjType(AllName[SaveNub]);
        lastNub = AllName[SaveNub];
        if (MyText == null)
            TypeStyleName = "所有风格";
        else
            TypeStyleName = MyText.TextMark;
    }

    private Vector3 MoveVector;

	void OnGUI()
	{
        if (_serializedObject == null)
			return;
       
        if (TypeStyleName == "自定义风格" || TypeStyleName == "所有风格")
        {
            EditorLock = true;
        }
        else
        {
            EditorLock = false;
        }

		if (AllName.Count <= 0)
			return;
		if (SaveText.Count <= 0)
			return;

			GUILayout.Space (20);

			_serializedObject.Update ();
        Text_serializedObject.Update();
        EditorGUI.BeginChangeCheck ();

       
        EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("创建一个新的text风格", GUILayout.Width (150), GUILayout.Height (30))) {
				EditorGUILayoutPopup.ShowCreatWindow ();
			}
			if (GUILayout.Button ("刷新", GUILayout.Width (150), GUILayout.Height (30))) {
				UpdateTag ();
				EditorApplication.update();
			}
			if (GUILayout.Button ("删除此类风格", GUILayout.Width (150), GUILayout.Height (30))) {
				
			}
			if (GUILayout.Button (LockName, GUILayout.Width (150), GUILayout.Height (30))) {
				SizeLock = !SizeLock;
				LockName=SizeLock?"解锁位置信息":"锁定位置信息";

            }
		}
		EditorGUILayout.EndHorizontal ();
		GUILayout.Space (10);
        if (IsEditor)
        {
            Debug.Log(TypeStyleName);
			EditorGUILayout.PropertyField (TypeNameProperty);
         }
        EditorGUI.BeginDisabledGroup (AcitonBool);
		SaveNub = EditorGUILayout.Popup (SaveNub, AllName.ToArray());
		EditorGUI.EndDisabledGroup ();
		EditorGroup = (TextGroup)SaveNub;
		if (AllName[SaveNub] != lastNub) 
		{
            UpdateMessage();

        }

        EditorGUI.BeginDisabledGroup(EditorLock);
        EditorGUILayout.BeginHorizontal ();
			{
				GUI.color = Color.green;
				EditorGUILayout.PropertyField (_OutLineProperty, true);
				EditorGUILayout.PropertyField (_ShadowProperty, true);
				EditorGUILayout.PropertyField (_GradientProperty, true);
				GUI.color = Color.white;
			}
			EditorGUILayout.EndHorizontal ();


		GUILayout.Space (20);

		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.PropertyField (bool_FontColorProperty, true);
			EditorGUILayout.PropertyField (_FontColorProperty, true);
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.PropertyField (bool_FontSizeProperty, true);
			EditorGUILayout.PropertyField (_FontSizeProperty, true);
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.PropertyField (bool_FontProperty, true);
			EditorGUILayout.PropertyField (_FontProperty, true);
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.PropertyField (bool_RaycastProperty, true);
			EditorGUILayout.PropertyField (_RaycastProperty, true);
		}
		EditorGUILayout.EndHorizontal ();
        GUILayout.Space(10);
        EditorGUI.BeginDisabledGroup(SizeLock);
        EditorGUILayout.BeginHorizontal ();
		{			
			EditorGUILayout.PropertyField (bool_SizeProperty, true);			
		    EditorGUILayout.PropertyField (_SizeProperty, true);
		}
		EditorGUILayout.EndHorizontal ();

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.PropertyField (bool_PositionProperty, true);
		    EditorGUILayout.PropertyField (_PositionProperty, true);
		}
		EditorGUILayout.EndHorizontal ();

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("偏移组件",GUILayout.Width(150),GUILayout.Height(30)))
            {
                MovePosition();
            }
            MoveVector = EditorGUILayout.Vector3Field("输入偏移量:", MoveVector);

        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        if (_OutLineProperty.boolValue) 
			{
				GUILayout.Space (20);
				GUILayout.Box ("描边特效");
				EditorGUILayout.PropertyField (_OutLine_StartColorProperty, true);
				EditorGUILayout.PropertyField (_EffectSizeProperty, true);
				EditorGUILayout.PropertyField (_ShadowVectorProperty, true);
			}
			if (_ShadowProperty.boolValue) 
			{
				GUILayout.Space (20);
				GUILayout.Box ("阴影特效");
				EditorGUILayout.PropertyField (_Shadow_StartColorProperty, true);
				EditorGUILayout.PropertyField (_ShadowEffectSizeProperty, true);
			}
			if (_GradientProperty.boolValue) 
			{
				GUILayout.Space (20);
				GUILayout.Box ("渐变色特效");
				EditorGUILayout.PropertyField (_GradientTypeProperty,true);
				if ((GradientType)_GradientTypeProperty.enumValueIndex == GradientType.Horizontal) {
					EditorGUILayout.PropertyField (_StartColorProperty, true);
					EditorGUILayout.PropertyField (_EndColorProperty, true);
				}
				if ((GradientType)_GradientTypeProperty.enumValueIndex == GradientType.Vertica) {
					EditorGUILayout.PropertyField (_StartColorProperty, true);
					EditorGUILayout.PropertyField (_EndColorProperty, true);
				}
				if ((GradientType)_GradientTypeProperty.enumValueIndex == GradientType.HorizontalMore) {
					EditorGUILayout.PropertyField (_StartColorProperty, true);
					EditorGUILayout.PropertyField (_CenterColorProperty, true);
					EditorGUILayout.PropertyField (_EndColorProperty, true);
				}
			}


			GUILayout.Space (20);
			EditorGUILayout.BeginHorizontal ();
			{

			if (!IsEditor)
			{
				EditorGUILayout.BeginVertical ();
				{
					if (GUILayout.Button ("设置此类text风格", GUILayout.Width (250), GUILayout.Height (30))) {

						if (EditorUtility.DisplayDialog ("提示消息", "确实要覆盖此类字体的样式吗", "确定","取消")) 
						{
							SetActionStyle();
							//EditorApplication.SaveScene ();
						}
					}

					GUILayout.Space (10);

					if (GUILayout.Button ("编辑此类text风格", GUILayout.Width (250), GUILayout.Height (30))) {
			
						AcitonBool = true;
						if (EditorUtility.DisplayDialog ("编辑字体之前请保存当前场景的设置", "是否要保存当前场景设置？", "确定","取消")) 
						{
							EditorApplication.SaveScene ();
						}
						EditorText.Clear ();
						IsEditor = true;
						LastSceneName = EditorApplication.currentScene;

						EditorApplication.NewScene ();
						if (EditorDemoText == null) {
							Canvas[] GetDemotext = Resources.LoadAll<Canvas> ("");
							foreach (Canvas child in GetDemotext) {
								if (child.name == "DemoSytleEditor") {
									GameObject textdemo = GameObject.Instantiate (child.gameObject);
									EditorDemoText = textdemo.GetComponentInChildren<Text> ();
									DemoGradient = EditorDemoText.GetComponent<TextStyleMessage> ();
								
									break;
								}
							}
							Camera.main.clearFlags = CameraClearFlags.Color;
							Camera.main.backgroundColor = Color.white;
                            TypeStyleName = MyText.TextMark;                         
							LastTypeName = TypeStyleName;
							NowTypename = TypeStyleName;
						}

						return;
					}
                    GUILayout.Space(10);

                }

				EditorGUILayout.EndVertical ();
                EditorGUI.EndDisabledGroup();
                GUILayout.Space (20);
				EditorGUILayout.BeginVertical ();
				{
					if (GUILayout.Button ("刷新", GUILayout.Width (250), GUILayout.Height (30)))
						GetObjType (lastNub);
					ScrollView = GUILayout.BeginScrollView (ScrollView);
					{
						TextList.DoLayoutList ();
					}
				}
				EditorGUILayout.EndVertical ();
				GUILayout.EndScrollView();

			} 
			else
			{           
                if (GUILayout.Button ("返回", GUILayout.Width (250), GUILayout.Height (30))) 
				  {

                    if (!CheckName(TypeStyleName))
                    {
                        if (EditorUtility.DisplayDialog("提示消息", "名字已被使用,请重新定义风格名称", "确定"))
                        {
                            return;
                        }
                    }
                    if(TypeStyleName != string.Empty)                  
                       MyText.TextMark = TypeStyleName;                
                       AcitonBool = false;
					   IsEditor = false;
					   EditorApplication.OpenScene (LastSceneName);
					   UpdateAssetName ();
					   UpdateSytleMessage (AllName[SaveNub]);
				  }

				if (EditorDemoText != null&& DemoGradient!=null)
                {
                    SetFontDemoSytle ();
					EditorUtility.SetDirty (EditorDemoText.gameObject);
                    DemoGradient.UpdateState();

                }

            }


			}
			EditorGUILayout.EndHorizontal ();
	
		GUILayout.Space (20);
       
		if (GUILayout.Button ("关闭窗口", GUILayout.Width (150), GUILayout.Height (20))) 
		{
			this.Close ();
		}

		if (EditorGUI.EndChangeCheck ())
		{
			if(_serializedObject!=null)
			_serializedObject.ApplyModifiedProperties ();
             Text_serializedObject.ApplyModifiedProperties();
        }
	}


	void OnDisable()
	{
        if (IsEditor)
		EditorApplication.OpenScene (LastSceneName);
        IsOpne = false;
    }
		



public class EditorGUILayoutPopup : EditorWindow 
{
	protected void OnEnable()
	{
            
    }

	protected void OnDisable()
	{
           
        }
	private SerializedObject _serializedObject;
	public string AssetName;
	public string SytleName;
	public List<string> aLLName=new List<string>();

	public static void ShowCreatWindow()
	{
		EditorWindow window = GetWindow(typeof(EditorGUILayoutPopup));
		window.titleContent = new GUIContent ("创建新的字体风格");
		window.Show();          
        }


        private bool CheckName(string GetName)
	{
        StyleSetting[] AllSytleData = Resources.LoadAll<StyleSetting>("");
		foreach (StyleSetting child in AllSytleData) 
		{
			aLLName.Add (child.TextMark);
		}
		if (aLLName.Contains (GetName))
			return false;
		return true;
	}

        public string Path= "Assets/DH_TextStyle/Resources/Data";
	void OnGUI()
	{
		GUILayout.Space (30);
		AssetName = EditorGUILayout.TextField("资源名称:",AssetName);
		GUILayout.Space (20);
		SytleName = EditorGUILayout.TextField("Text风格名称:",SytleName);
		GUILayout.Space (50);
		if (GUILayout.Button ("确定创建", GUILayout.Width (250), GUILayout.Height (30))) 
		{
			if (!CheckName (SytleName)) {
				if (EditorUtility.DisplayDialog ("提示消息", "名字已被使用,请重新定义风格名称", "确定")) {
					return;
				}
			}
			var cfg = ScriptableObject.CreateInstance<StyleSetting> ();
			cfg.TextMark = SytleName;
			if (!AssetDatabase.IsValidFolder (Path)) 
			{
				AssetDatabase.CreateFolder ("Assets/DH_TextStyle/Resources", "Data");
			}
			AssetDatabase.CreateAsset (cfg,Path+"/"+AssetName+".asset");
				
			AssetDatabase.SaveAssets ();
			if (EditorUtility.DisplayDialog ("提示消息", "创建成功", "确定")) 
			{
                this.Close ();
			}
		}
	}
}

    public class EditorGUILayoutTextNow : EditorWindow
    {
        protected void OnEnable()
        {
            IsOpne = true;
        }

        protected void OnDisable()
        {
            IsOpne = false;
        }
        private SerializedObject _serializedObject;
        public string AssetName;
        public string SytleName;
        public List<string> aLLName = new List<string>();
        private static bool IsOpne = false;

        private static TextStyleMessage DemoGradient;
        private static Text EditorDemoText;
        private StyleSetting MyText;
        public static void ShowCreatWindow(TextStyleMessage NowMessage, Text GetText)
        {
            if (IsOpne)
                return;
            EditorWindow window = GetWindow(typeof(EditorGUILayoutTextNow));
            window.titleContent = new GUIContent("Save the current font style");
            window.Show();
            DemoGradient = NowMessage;
            EditorDemoText = GetText;
        }

        private bool CheckName(string GetName)
        {
            StyleSetting[] AllSytleData = Resources.LoadAll<StyleSetting>("");
            foreach (StyleSetting child in AllSytleData)
            {
                aLLName.Add(child.TextMark);
            }
            if (aLLName.Contains(GetName))
                return false;
            return true;
        }


        public void SetOutLine()
        {
            MyText.OutLine = DemoGradient.IsDrawOutline;
            MyText.OutLineStartColor = DemoGradient.Outline_effectColor;
            MyText.EffectSize = DemoGradient.Outline_effectDistance;
            MyText.ShadowVector = DemoGradient.ShadowType;

        }

        public void SetGradient()
        {
            MyText.Gradient = DemoGradient.IsDrawGradent;
            MyText.StartColor = DemoGradient.colorTop;
            MyText.CenterColor = DemoGradient.colorCenter;
            MyText.EndColor = DemoGradient.colorBottom;
            MyText.ChoseGradientType = (GradientType)DemoGradient.GradientType;
        }


        public void SetShadow()
        {
            MyText.Shadow = DemoGradient.IsDrawShadow;
            MyText.ShadowStartColor = DemoGradient.Shadow_effectColor;
            MyText.ShadowEffectSize = DemoGradient.Shadow_effectDistance;

        }

        public void SetFontDemoSytle()
        {
            MyText.FontColor = EditorDemoText.color;
            MyText.FontSize = EditorDemoText.fontSize;
            MyText.SizeData = EditorDemoText.rectTransform.sizeDelta;
            MyText.PositionData = EditorDemoText.rectTransform.localPosition;
            MyText.Font = EditorDemoText.font;
            MyText.Raycast = EditorDemoText.raycastTarget;

            SetOutLine();
            SetGradient();
            SetShadow();
        }

        public string Path = "Assets/DH_TextStyle/Resources/Data";
        void OnGUI()
        {
            GUILayout.Space(30);
            AssetName = EditorGUILayout.TextField("AssetsName:", AssetName);
            GUILayout.Space(20);
            SytleName = EditorGUILayout.TextField("StyleName:", SytleName);
            GUILayout.Space(50);
            if (GUILayout.Button("OK", GUILayout.Width(250), GUILayout.Height(30)))
            {
                if (!CheckName(SytleName))
                {
                    if (EditorUtility.DisplayDialog("Message", "The name has been used, please redefine the style name", "OK"))
                    {
                        return;
                    }
                }
                var cfg = ScriptableObject.CreateInstance<StyleSetting>();
                MyText = (StyleSetting)cfg;
                SetFontDemoSytle();
                cfg.TextMark = SytleName;
                if (!AssetDatabase.IsValidFolder(Path))
                {
                    AssetDatabase.CreateFolder("Assets/DH_TextStyle/Resources", "Data");
                }
                AssetDatabase.CreateAsset(cfg, Path + "/" + AssetName + ".asset");

                AssetDatabase.SaveAssets();
                if (EditorUtility.DisplayDialog("Message", "save successfully", "OK"))
                {
                    DemoGradient.ChangState(SytleName);
                    this.Close();
                }
            }
        }




    }

} 