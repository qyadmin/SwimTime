using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using UnityEngine;
using TypeClass;
[CustomEditor(typeof(HttpModel))]
public class TestListInspector : Editor
{
    private ReorderableList m_NameList;
	private ReorderableList MessageList;
	private ReorderableList GetDataList;
	private ReorderableList SetDataList;
	private ReorderableList ListDataList;
	private ReorderableList ListSingleList;
	private ReorderableList ListSingleType;
    private ReorderableList ShareHttpList;

    private SerializedProperty GetMessageShow;
	private SerializedProperty GetBaseDataShow;
	private SerializedProperty GetListDataShow;
    

    private int TypeNub=0;
	private bool ShowTypeNub = false;
	HttpModel myModel;
    private void OnEnable()
    {
		myModel = (HttpModel)target;  
        m_NameList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("SendData"),
            true, true, true, true);

        ShareHttpList = new ReorderableList(serializedObject,
            serializedObject.FindProperty("Data").FindPropertyRelative("ShareModel"),
            true, true, true, true);

        MessageList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("MsgList"),
			true, true, true, true);

		GetDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("GetDataList"),
          true, true, true, true);

		SetDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("BackDataGet"),
			true, true, true, true);

		ListDataList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("NameList"),
			true, true, true, true);

		ListSingleList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("NameSingleList"),
			true, true, true, true);
		ListSingleType = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("OffectNubList"),
			true, true, true, true);
		GetDataList.drawElementCallback += DrawNameElementObj;

        m_NameList.drawElementCallback += DrawNameElement;

		SetDataList.drawElementCallback += DrawNameElementSetBack;

		ListDataList.drawElementCallback += DrawNameElementList;

		ListSingleList.drawElementCallback += DrawNameSingleList;

		MessageList.drawElementCallback += DrawMessageList;

        ShareHttpList.drawElementCallback += HttpModeDraw;

        ShareHttpList.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "分享到其他Model处理");
        };


        MessageList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "接收返回消息的列表");
		};


		ListSingleList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "添加获取返回数据的列表");
		};

        m_NameList.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "添加请求数据的列表");
        };

		GetDataList.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "返回数据列表");
        };

		SetDataList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "返回数据赋值列表");
		};

		ListDataList.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "需要取得的列表数据");
		};

		ListSingleType.drawHeaderCallback = (Rect rect) =>
		{
			GUI.Label(rect, "状态执行列表");
		};



		ListDataList.onSelectCallback = (ReorderableList list) =>
		{
			ListSingleType.drawElementCallback -= DrawTypeElementList;
			if(myModel.Data.MyListMessage.NameList[list.index].MySaveType == SaveNameType.Chosetype)
			{
				myModel.Data.MyListMessage.SetOffectList(list.index);
				TypeNub=list.index;
				ShowTypeNub=true;
			}
			else
			{
				ShowTypeNub=false;
			}
			ListSingleType.drawElementCallback += DrawTypeElementList;
		};

    }

    private void HttpModeDraw(Rect rect, int index, bool selected, bool focused)
    {
        SerializedProperty element = ShareHttpList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(rect, element, GUIContent.none);
    }


    private void DrawTypeElementList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ListSingleType.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Nub"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.width - 300, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AddToListValue"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.width-200, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SendModel"), GUIContent.none);
		//EditorGUI.PropertyField (new Rect(rect.width-100, rect.y, 10, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("AddButtonEvent"), GUIContent.none);
		if (myModel.Data.MyListMessage.OffectNubList.Count <= index)
			return;
		myModel.Data.MyListMessage.OffectNubList [index].SaveNub = EditorGUI.Popup (new Rect (rect.x + 120, rect.y, 60, EditorGUIUtility.singleLineHeight), myModel.Data.MyListMessage.OffectNubList [index].SaveNub, 
		myModel.Data.MyListMessage.GetNameArray(TypeNub));
	}



	private void DrawMessageList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = MessageList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.width-rect.width/8, rect.y,rect.width/6, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Mytype"), GUIContent.none);

		EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width/4f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);
		MsgType ShowType =(MsgType) element.FindPropertyRelative ("Mytype").enumValueIndex;
		if (ShowType == MsgType.Text) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4f, rect.y, rect.width / 4f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowInText"), GUIContent.none);
		}
		if (ShowType == MsgType.InputText) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4f, rect.y, rect.width / 4f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowIn_InputText"), GUIContent.none);
		}
		if (ShowType == MsgType.AcitonEvent) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4f * 2, rect.y, rect.width / 4f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventName"), GUIContent.none);
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4f, rect.y, rect.width / 4f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("MessageObj"), GUIContent.none);
		}
	}
		

	private void DrawNameSingleList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ListSingleList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(new Rect(rect.width - rect.width / 5 - 80, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsDis"), GUIContent.none);
        if(element.FindPropertyRelative("IsDis").boolValue)
        EditorGUI.PropertyField(new Rect(rect.width - rect.width / 5-40, rect.y, 40, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("DisNub"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.width-rect.width/5, rect.y, rect.width/8, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);

		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 15, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsSave"), GUIContent.none);

		EditorGUI.PropertyField(new Rect(rect.x+16, rect.y, rect.width/3.6f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);

		GetBackType ShowType =(GetBackType) element.FindPropertyRelative ("MyType").enumValueIndex;

		if (ShowType == GetBackType.Text) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width / 3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Showtext"), GUIContent.none);
			EditorGUI.LabelField (new Rect(rect.width-10, rect.y, 40, EditorGUIUtility.singleLineHeight),"ToInt");
			EditorGUI.PropertyField(new Rect(rect.width-25, rect.y, 10, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsInt"),GUIContent.none);
		}
		if (ShowType == GetBackType.InputText) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width / 3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowInputtext"), GUIContent.none);
			EditorGUI.LabelField (new Rect(rect.width-10, rect.y, 40, EditorGUIUtility.singleLineHeight),"ToInt");
			EditorGUI.PropertyField(new Rect(rect.width-25, rect.y, 10, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsInt"),GUIContent.none);
		}
		if(ShowType==GetBackType.Iamge)
			EditorGUI.PropertyField (new Rect(rect.x+rect.width/3.5f, rect.y, rect.width/3.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("ShowTexture"), GUIContent.none);
		if (ShowType == GetBackType.AcitonEvent) {
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 3.5f, rect.y, rect.width /6, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventObj"), GUIContent.none);
			EditorGUI.PropertyField (new Rect (rect.x + rect.width / 4*2, rect.y, rect.width /6, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventName"), GUIContent.none);
		}
		//EditorGUI.PropertyField(rect, element, GUIContent.none);
	}

	private void DrawNameElementList(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = ListDataList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Name"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.x+90, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("MyObjeType"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.width-50, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("MySaveType"), GUIContent.none);
		//HttpModel myModel = (HttpModel)target;  
		myModel.Data.MyListMessage.NameList[index].index=EditorGUI.Popup(new Rect(rect.x+150, rect.y, 60, EditorGUIUtility.singleLineHeight),myModel.Data.MyListMessage.NameList[index].index,myModel.Data.MyListMessage.GetNameArray(index));

		if ((SaveNameType)element.FindPropertyRelative ("MySaveType").enumValueIndex == SaveNameType.SaveOtherName) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-150, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("OtherName"), GUIContent.none);
		}
		if ((SaveNameType)element.FindPropertyRelative ("MySaveType").enumValueIndex == SaveNameType.ActionEvent) 
		{
			EditorGUI.PropertyField (new Rect(rect.width-180, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("EventName"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.width - 80, rect.y,30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("NotString"), GUIContent.none);
        }
		if ((SaveNameType)element.FindPropertyRelative ("MySaveType").enumValueIndex == SaveNameType.Chosetype) 
		{
			
		}
		//EditorGUI.PropertyField(rect, element, GUIContent.none);
	}



    private void DrawNameElementObj(Rect rect, int index, bool selected, bool focused)
    {
		SerializedProperty element = GetDataList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(rect, element, GUIContent.none);
//        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SaveButtonEvent"), GUIContent.none);
//        EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("GetGasValve"), GUIContent.none);
    }

	private void DrawNameElementSetBack(Rect rect, int index, bool selected, bool focused)
	{
		SerializedProperty element = SetDataList.serializedProperty.GetArrayElementAtIndex(index);

		rect.y += 2;
		rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
		EditorGUI.PropertyField (new Rect(rect.width-20, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("IsSave"), GUIContent.none);

		GetBackTypeValue ShowType =(GetBackTypeValue) element.FindPropertyRelative ("MyType").enumValueIndex;
		if ( ShowType== GetBackTypeValue.GetValue) 
		{
			SetDataList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"获取的值赋给Text",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetText"), GUIContent.none);
		}
		if (ShowType == GetBackTypeValue.NoGetValue) 
		{
			SetDataList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"获取的值保存到列表",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
		}
	}






    private void DrawNameElement(Rect rect, int index, bool selected, bool focused)
    {
        SerializedProperty element = m_NameList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MyType"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.width-100, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Rule"), GUIContent.none);
        EditorGUI.PropertyField (new Rect(rect.width-20, rect.y, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("IsSave"), GUIContent.none);
		GetTypeValue ShowType =(GetTypeValue) element.FindPropertyRelative ("MyType").enumValueIndex;
		if ( ShowType== GetTypeValue.GetFromValue) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从输入的值中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetValue"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFormText) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"拖入的Text中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetText"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromInputField) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从输入Text中获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("SetInputField"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromList) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从储存列表中按自己的的名字获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
		}
		if (ShowType == GetTypeValue.GetFromListOther) 
		{
			m_NameList.elementHeight = 40;
			EditorGUI.HelpBox(new Rect(rect.x+110, rect.y, rect.width/4, EditorGUIUtility.singleLineHeight),"从储存列表中按输入的名字获取",MessageType.None);
			EditorGUI.PropertyField (new Rect(rect.x, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (new Rect(rect.width-100, rect.y+20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("OtherName"), GUIContent.none);
		}
        EditorGUI.PropertyField(new Rect(rect.x + 120, rect.y + 20, 20, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("IsCheck"), GUIContent.none);
        if (element.FindPropertyRelative("IsCheck").boolValue)
        {
            CheckType Rule = (CheckType)element.FindPropertyRelative("Rule").enumValueIndex;
            if (Rule == CheckType.isEqual)
            {
                EditorGUI.PropertyField(new Rect(rect.x + 150, rect.y + 20, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("InputText"), GUIContent.none);
               
            }
            if (Rule == CheckType.rule)
            {
                EditorGUI.PropertyField(new Rect(rect.x + 150, rect.y + 20, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("RuleFromat"), GUIContent.none);
            }
            EditorGUI.PropertyField(new Rect(rect.x + 250, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("WarmMessage"), GUIContent.none);
        }
        //EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ShowError"), GUIContent.none);
        //EditorGUI.PropertyField(rect, itemData, GUIContent.none);
    }




    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GetMessageShow = serializedObject.FindProperty("Data");
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("DebugData"));
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("ErrorCode"));
        m_NameList.DoLayoutList();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("IsLock"));     
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("Action"));
		EditorGUI.BeginDisabledGroup (GetMessageShow.FindPropertyRelative("Action").boolValue);

        EditorGUILayout.EndHorizontal ();
		EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("CutCount"));
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("HeaderName"));
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("DataName"));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("NeedReplayName"));
        if(GetMessageShow.FindPropertyRelative("NeedReplayName").boolValue)
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("ReplayName"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isLoacl"));
        if (serializedObject.FindProperty("isLoacl").boolValue)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("LocalHttp"));
        else
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("URL"));

		EditorGUI.EndDisabledGroup (); 
		MessageList.DoLayoutList();
        ShareHttpList.DoLayoutList();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("IsShell"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DataType"));
		if ((TypeGo)(serializedObject.FindProperty ("DataType").enumValueIndex) ==TypeGo.GetTypeB) 
		{
			GUI.backgroundColor = Color.green;
			GetListDataShow = GetMessageShow.FindPropertyRelative ("MyListMessage");
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("FatherObj"));
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("InsObj"));

			ListDataList.DoLayoutList ();
			GUI.backgroundColor = Color.white;

				if (ShowTypeNub) {
					ListSingleType.DoLayoutList ();
					GUILayout.Space (5);
					if (GUILayout.Button ("保存配置信息")) {
						myModel.Data.MyListMessage.GetOffectList (TypeNub);
					}
				}
		}
		if ((TypeGo)(serializedObject.FindProperty ("DataType").enumValueIndex) == TypeGo.GetTypeC) 
		{
			GUI.backgroundColor = Color.yellow;
			ListSingleList.DoLayoutList ();
			GUI.backgroundColor = Color.white;
		}
		if ((TypeGo)(serializedObject.FindProperty ("DataType").enumValueIndex) == TypeGo.GetTypeD) 
		{
			GUI.backgroundColor = Color.blue;
			GetListDataShow = GetMessageShow.FindPropertyRelative ("MyListMessage");
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("MessageObj"));
			EditorGUILayout.PropertyField (GetListDataShow.FindPropertyRelative ("ActionName"));
			GUI.backgroundColor = Color.white;
		}
        if ((TypeGo)(serializedObject.FindProperty("DataType").enumValueIndex) == TypeGo.GetTypeE)
        {
			EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("Receivemodel"));
			//if(GetMessageShow.FindPropertyRelative("Receivemodel").enumValueIndex!=0)
			EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("AddTag"));
			EditorGUILayout.LabelField("捕捉DataName");
            //GUI.backgroundColor = Color.green;
            //GetListDataShow = GetMessageShow.FindPropertyRelative("MyListMessage");
            //EditorGUILayout.PropertyField(GetListDataShow.FindPropertyRelative("FatherObj"));
            //EditorGUILayout.PropertyField(GetListDataShow.FindPropertyRelative("InsObj"));
            //EditorGUILayout.PropertyField(GetListDataShow.FindPropertyRelative("ActionEventName"));
        }
        EditorGUILayout.PropertyField(GetMessageShow.FindPropertyRelative("ShowMessage"));
       
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Suc"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("Fal"));
		GUI.backgroundColor = Color.red;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("DoAction"));
		GUI.backgroundColor = Color.white;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("NoShow"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("HideMessage"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("OnlyError"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("WaitTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("IsAdd"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ShowAddMessage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NoldIcon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("IsWait"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NoSus"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("GetTWO"));



        serializedObject.ApplyModifiedProperties();
    }




}

//[CustomEditor(typeof(HttpModel))]
//public class EditorGUILayoutPopup : EditorWindow 
//{
//	public static HttpModel NowModel;
//	private ReorderableList ListDataList;
//
//	protected SerializedObject _serializedObject;
//
//	protected void OnEnable()
//	{
//		_serializedObject = new SerializedObject(NowModel);
//
//		ListDataList = new ReorderableList(_serializedObject,
//			_serializedObject.FindProperty("Data").FindPropertyRelative("MyListMessage").FindPropertyRelative("NameList").GetArrayElementAtIndex(0).FindPropertyRelative("NubList"),
//			true, true, true, true);
//		ListDataList.drawElementCallback += DrawNameElementList;
//		ListDataList.drawHeaderCallback = (Rect rect) =>
//		{
//			GUI.Label(rect, "状态执行列表");
//		};
//	}
//
//	private void DrawNameElementList(Rect rect, int index, bool selected, bool focused)
//	{
//		SerializedProperty element = ListDataList.serializedProperty.GetArrayElementAtIndex(index);
//
//		rect.y += 2;
//		rect.height = EditorGUIUtility.singleLineHeight;
//		EditorGUI.PropertyField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Nub"), GUIContent.none);
//		EditorGUI.PropertyField (new Rect(rect.x+90, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative ("Onoff"), GUIContent.none);
//
//		NowModel.Data.MyListMessage.NameList [0].NubList [index].SaveNub = EditorGUI.Popup (new Rect (rect.x + 240, rect.y, 60, EditorGUIUtility.singleLineHeight), NowModel.Data.MyListMessage.NameList [0].NubList [index].SaveNub, options);
//
//		EditorGUI.PropertyField(rect, element, GUIContent.none);
//	}
//		
//	public  static string[] options;
//
//	//List<string> options=new List<string>();
//	public int index = 0;
//
//	//[MenuItem("Examples/Editor GUILayout Popup usage")]
//	public static void ShowEditorList(List<string> Getoptions )
//	{
//		options = Getoptions.ToArray ();
//		EditorWindow window = GetWindow(typeof(EditorGUILayoutPopup));
//		window.Show();
//	}
//
//
//	void OnGUI()
//	{
//		_serializedObject.Update ();
//		ListDataList.DoLayoutList ();
//		index = EditorGUILayout.Popup(index,options);
//		_serializedObject.ApplyModifiedProperties ();
//	}
//}