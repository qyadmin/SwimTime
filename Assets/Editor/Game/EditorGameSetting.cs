using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
public class SettingGame : EditorWindow
{

    protected SerializedObject serializedObject;
    protected SerializedProperty _assetLstProperty;
    protected SerializedProperty _assetLstPropertyURL;
    private GameSetting GameData;

    private List<GameObject> AllObj = new List<GameObject>();

    protected void OnDisable()
    {
        GameData.OpneIamge = this.OpneIamge;
        GameData.CloseIamge = this.CloseIamge;
        GameData.MessageObj = this.MessageObj;
        GameData.LoadObj = this.LoadObj;
    }

    protected void OnEnable()
    {
        var database = Resources.LoadAll<GameSetting>("")[0];
        GameData = (GameSetting)database;
        if (database)
        {
            serializedObject = new SerializedObject(database);
            this.OpneIamge = GameData.OpneIamge;
            this.CloseIamge = GameData.CloseIamge;
            this.MessageObj = GameData.MessageObj;
            this.LoadObj = GameData.LoadObj;
            this.Button_BackMusic = GetTypeObj<Button>("PalyBackMusic");
        }
        _assetLstProperty = serializedObject.FindProperty("ImageList");
        _assetLstPropertyURL = serializedObject.FindProperty("URLList");
    }


    public T GetTypeObj<T>(string TypeName)
    {
        object oBJ = null;
        List<GameObject> allObject = GetAllObjectsInScene();
        foreach (GameObject nowObj in allObject)
        {
            if (nowObj.GetComponent(TypeName))
                oBJ = nowObj.GetComponent<T>();
        }
        return (T)oBJ;
    }

    public List<GameObject> GetTypeObj(string TypeName)
    {
        object oBJ = null;
        List<GameObject> allObject = GetAllObjectsInScene();
        List<GameObject> GetAllObj = new List<GameObject>();
        foreach (GameObject nowObj in allObject)
        {
            if (nowObj.GetComponent(TypeName))
                GetAllObj.Add(nowObj);
        }
        return GetAllObj;
    }



    public GameObject InstanceSoundObj(string Name, AudioClip GetClip, bool AwakeOR, bool LoopOR)
    {
        GameObject SoundObj = GameObject.Find(Name);
        if (SoundObj)
        {
            DestroyImmediate(SoundObj);
        }
        SoundObj = new GameObject(Name);
        SoundObj.AddComponent<AudioSource>().clip = GetClip;
        SoundObj.GetComponent<AudioSource>().playOnAwake = AwakeOR;
        SoundObj.GetComponent<AudioSource>().loop = LoopOR;

        return SoundObj;
    }

    public void InstatceMessageObj(string Name, GameObject INToBJ)
    {
        if (MessageObj == null)
            Debug.Log("没有添加对象物体");

        GameObject MessageObjGet = GameObject.Find(Name);
        if (MessageObjGet != null)
            DestroyImmediate(MessageObjGet);

        Transform FatherObj = GetTypeObj<Canvas>("Canvas").transform;
        MessageObjGet = Instantiate(INToBJ);
        MessageObjGet.name = Name;
        MessageObjGet.transform.SetParent(FatherObj);
        MessageObjGet.transform.localScale = new Vector3(0, 0, 0);
        MessageObjGet.transform.localPosition = new Vector3(0, 0, 0);
    }



    [MenuItem("GameSetting/EditorGameSetting")]
    public static void ShowEditorList()
    {
        EditorWindow window = GetWindow(typeof(SettingGame));
        window.Show();
    }

    private Button Button_BackMusic;
    public Sprite OpneIamge;
    public Sprite CloseIamge;
    private GameObject ButtonSource;
    private GameObject BackSource;
    private GameObject MessageObj;
    private GameObject LoadObj;

    public bool CheckObj()
    {
        if (Button_BackMusic == null)
        {
            ShowMessage("没有找到:" + "背景音乐按钮");
            return false;
        }
        if (OpneIamge == null)
        {
            ShowMessage("没有找到:" + "音乐图标");
            return false;
        }
        if (CloseIamge == null)
        {
            ShowMessage("没有找到:" + "音乐图标");
            return false;
        }
        if (GameData.ImageList.Count <= 0)
        {
            ShowMessage("没有添加图片");
            return false;
        }
        //		if (BackSource == null) 
        //		{
        //			ShowMessage ("没有找到:"+"背景音乐");
        //			return false;
        //		}
        if (MessageObj == null)
        {
            ShowMessage("没有找到:" + "消息框");
            return false;
        }
        if (LoadObj == null)
        {
            ShowMessage("没有找到:" + "错误提示框");
            return false;
        }

        return true;
    }

    Vector2 ScrollView;
    void OnGUI()
    {
        ScrollView = GUILayout.BeginScrollView(ScrollView);
        {
            GUILayout.Space(10);
            serializedObject.Update();
            GUILayout.Box("音乐设置");
            Button_BackMusic = EditorGUILayout.ObjectField("音乐按钮", Button_BackMusic, typeof(Button), true) as Button;
            OpneIamge = EditorGUILayout.ObjectField("开启音乐图标", OpneIamge, typeof(Sprite), true) as Sprite;
            CloseIamge = EditorGUILayout.ObjectField("关闭音乐图标", CloseIamge, typeof(Sprite), true) as Sprite;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BackMusic"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ButtonMusic"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("KaikenMusic"));
            GUILayout.Box("URL设置列表");
            EditorGUILayout.PropertyField(_assetLstPropertyURL, true);
            GUILayout.Box("版本号设置");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Level"));
            GUILayout.Box("MD5设置");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Md5Losck"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("KeyValue"));
            GUILayout.Box("字体设置");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SetFont"));
            GUILayout.Box("储存token设置");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SaveName"));
            GUILayout.Box("提示消息面板设置");
            MessageObj = EditorGUILayout.ObjectField("网络返回消息面板", MessageObj, typeof(GameObject), true) as GameObject;
            LoadObj = EditorGUILayout.ObjectField("网络加载对象", LoadObj, typeof(GameObject), true) as GameObject;

            GUILayout.Box("头像设置列表");
            EditorGUILayout.PropertyField(_assetLstProperty, true);
            GUILayout.Space(20);
            if (GUILayout.Button("覆盖数据"))
            {
                //if (!CheckObj ())
                //	return;
                //InstatceMessageObj ("MessageObj", MessageObj);
                //InstatceMessageObj ("HttpBack", LoadObj);
                //ButtonSource = InstanceSoundObj ("PlayButtonMusic", GameData.ButtonMusic, false,false);
                //BackSource = InstanceSoundObj ("PlayBackMusic", GameData.BackMusic, true,true);
                OverButtonEvent();
                //PlayBackMusicOver ();
                //OverTextEvent ();
                //OverIamge ();
                AssetDatabase.SaveAssets();
                EditorApplication.SaveScene();
                //EditorApplication.OpenScene (Application.loadedLevelName);
            }
            GUILayout.Space(50);
            if (GUILayout.Button("关闭窗口", GUILayout.Width(150), GUILayout.Height(20)))
            {
                this.Close();
            }
            serializedObject.ApplyModifiedProperties();
        }
        GUILayout.EndScrollView();
    }


    public void OverIamge()
    {
        foreach (GameObject child in GetTypeObj("ChoseIamge"))
        {
            for (int i = 0; i < GameData.ImageList.Count; i++)
                child.transform.GetChild(i).GetComponent<Image>().sprite = GameData.ImageList[i];
        }
    }



    public void PlayBackMusicOver()
    {
        //if (!Button_BackMusic.gameObject.GetComponent<PalyBackMusic> ()) 
        //	Button_BackMusic.gameObject.AddComponent<PalyBackMusic> ().SetData(OpneIamge,CloseIamge ,BackSource.GetComponent<AudioSource> ());
        //else
        //	Button_BackMusic.gameObject.GetComponent<PalyBackMusic> ().SetData(OpneIamge,CloseIamge ,BackSource.GetComponent<AudioSource> ());
    }


    public void OverTextEvent()
    {
        List<GameObject> allObject = GetAllObjectsInScene();
        foreach (GameObject nowObj in allObject)
        {
            if (nowObj.GetComponent<Text>())
            {
                nowObj.GetComponent<Text>().font = GameData.SetFont;
            }
        }
    }


    public void OverButtonEvent()
    {
        List<GameObject> allObject = GetAllObjectsInScene();
        foreach (GameObject nowObj in allObject)
        {
            if (nowObj.GetComponent<Button>())
            {
                if (!nowObj.GetComponent<ButtonEventBase>())
                    nowObj.AddComponent<ButtonClickAction>();//.SetMusic(ButtonSource.GetComponent<AudioSource>());
                else
                    nowObj.GetComponent<ButtonEventBase>();//.SetMusic(ButtonSource.GetComponent<AudioSource>());
                                                           //if(nowObj.GetComponent<PalyBackMusic> ())
                                                           //DestroyImmediate(nowObj.GetComponent<PalyBackMusic> ());
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




    public void ShowMessage(string GetMessage)
    {
        if (EditorUtility.DisplayDialog("未完成的设置参数", GetMessage, "确定"))
        {

        }
    }
}