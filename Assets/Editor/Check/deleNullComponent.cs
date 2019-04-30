using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RemoveMissingScripts : Editor
{
    [MenuItem("Tools/移除丢失的脚本")]
    public static void RemoveMissingScript()
    {
        var gos = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var item in gos)
        {
            Debug.Log(item.name);
            SerializedObject so = new SerializedObject(item);
            var soProperties = so.FindProperty("m_Component");
            var components = item.GetComponents<Component>();
            int propertyIndex = 0;
            foreach (var c in components)
            {
                if (c == null)
                {
                    soProperties.DeleteArrayElementAtIndex(propertyIndex);
                }
                ++propertyIndex;
            }
            so.ApplyModifiedProperties();
        }

        AssetDatabase.Refresh();
        Debug.Log("清理完成!");
        //Debug.Log(gos.Length);
        //var r= Resources.FindObjectsOfTypeAll<GameObject>();
        //foreach (var item in r)
        //{
        //    Debug.Log(item.name);
        //}
        //Debug.Log(r.Length);

    }

}