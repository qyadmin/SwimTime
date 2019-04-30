using UnityEngine; 
using UnityEditor; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI;
using System.IO;

public class ChangeName : ScriptableWizard { 
 
	protected SerializedObject _serializedObject;
	protected SerializedProperty _assetLstProperty;
	[MenuItem("GameSetting/Check/改名字")] 
	static void CreateWizard () 
	{
		ScriptableWizard.DisplayWizard<ChangeName>("检查字符串", "ChangeName"); 
	} 
	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("ErrorList");
		ChangeMessage = "等待处理";
	}
		
	//*****************
	public string FileName;
	public string FileTokenName;
	public Texture2D GetTex;
	public string ChangeMessage;
	public void ToRename()
	{
		ChangeMessage = "文件正在处理...不要关闭面板";
		object[] m_objects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
		int index=0;
		foreach (Object item in m_objects)
		{
			if (Path.GetExtension(AssetDatabase.GetAssetPath(item)) != "")
			{
				string path = AssetDatabase.GetAssetPath(item);
				if (item.GetType () == typeof(Texture2D)) {
					AssetDatabase.RenameAsset (path, FileTokenName + item.name);
					ChangeMessage = item.name + "更改为" + FileTokenName + item.name;
				}
				index++;
			}
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		ChangeMessage = "文件处理完成！";
	}

	public void ToRenameAll()
	{
		ChangeMessage = "文件正在处理...不要关闭面板";
		object[] m_objects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
		int index=0;
		foreach (Object item in m_objects)
		{
			if (Path.GetExtension(AssetDatabase.GetAssetPath(item)) != "")
			{
				string path = AssetDatabase.GetAssetPath(item);
				if (item.GetType () == typeof(Texture2D)) {
					AssetDatabase.RenameAsset (path, FileName + index);
					ChangeMessage = item.name + "更改为" + FileName + index;
				}
				index++;
			}
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		ChangeMessage = "文件处理完成！";
	}
		
	void OnGUI()
	{
		GUILayout.Space (30);
		FileTokenName = EditorGUILayout.TextField("修改文件的Token:",FileTokenName);
		if (GUILayout.Button ("更改文件Token名称", GUILayout.Width (250), GUILayout.Height (30))) 
		{
			ToRename ();
		}
		GUILayout.Box (ChangeMessage);
		GUILayout.Space (50);
		FileName = EditorGUILayout.TextField("修改文件的名称:",FileName);
		if (GUILayout.Button ("更改文件名称", GUILayout.Width (250), GUILayout.Height (30))) 
		{
			ToRenameAll ();
		}

		if(GUILayout.Button("关闭窗口",GUILayout.Width(150),GUILayout.Height(20)))
		{
			this.Close();
		}

	}
} 