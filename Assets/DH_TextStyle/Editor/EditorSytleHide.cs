using UnityEditor;

[CustomEditor(typeof(StyleSetting))]
public class EditorStyleHide : Editor
{

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		EditorGUI.BeginDisabledGroup (true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty ("TextMark"));
		EditorGUI.EndDisabledGroup ();
		serializedObject.ApplyModifiedProperties();
	}

}