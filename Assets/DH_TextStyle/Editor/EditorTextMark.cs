using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CustomEditor(typeof(TextStyleMessage))]
public class EditorTextMark : Editor
{
    TextStyleMessage myModel;
    private bool DrawOutline;
    private bool DrawShadow;
    private bool DrawGradent;

    private int Lastnub;
    private bool LastState;

    List<string> AllName = new List<string>();

    void OnEnable()
	{
        UpdateTag();    
        myModel = (TextStyleMessage)target;
        EditorDemoText = myModel.GetComponent<Text>();
        DemoGradient = myModel;
        myModel.DemoTag.localnub = AllName.IndexOf(myModel.DemoTag.TextType);
    }
    
    private void UpdateTag()
    {
        AllName = UpdateModle.GetStyleGroup();
    }

    public override void OnInspectorGUI()
    {
        if (myModel.StateStyle)
        {
            UpdateTag();
            myModel.DemoTag.localnub = AllName.IndexOf(myModel.NowName);
            myModel.StateStyle = false;
            Debug.Log(myModel.NowName);
        }
        serializedObject.Update();
        GUILayout.Space(10);

        if (myModel != null)
        {
            myModel.DemoTag.localnub = EditorGUILayout.Popup(myModel.DemoTag.localnub, AllName.ToArray());
            myModel.DemoTag.TextType = AllName[myModel.DemoTag.localnub];
        }
        if (Lastnub != myModel.DemoTag.localnub)
        {
            UpdateState();
            Lastnub = myModel.DemoTag.localnub;
        }

        if (myModel.DemoTag.TextType != "自定义风格")
        {
            myModel.UseBasicStyle = true;
        }
        else
        {
            myModel.UseBasicStyle = false;

            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    DrawOutline = EditorGUILayout.Foldout(DrawOutline, "DrawOutline");
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("IsDrawOutline"));
                }
                EditorGUILayout.EndHorizontal();
                if (DrawOutline)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Outline_effectColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Outline_effectDistance"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ShadowType"));
                }
                EditorGUILayout.BeginHorizontal();
                {
                    DrawGradent = EditorGUILayout.Foldout(DrawGradent, "DrawGradent");
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("IsDrawGradent"));
                }
                EditorGUILayout.EndHorizontal();
                if (DrawGradent)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("GradientType"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("colorTop"));
                    if(serializedObject.FindProperty("GradientType").intValue==2)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("colorCenter"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("colorBottom"));
                    //EditorGUILayout.PropertyField(serializedObject.FindProperty("MultiplyTextColor"));
                }
                EditorGUILayout.BeginHorizontal();
                {
                    DrawShadow = EditorGUILayout.Foldout(DrawShadow, "DrawShadow");
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("IsDrawShadow"));
                }
                EditorGUILayout.EndHorizontal();
                if (DrawShadow)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Shadow_effectColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Shadow_effectDistance"));
                }
            }
            EditorGUILayout.EndVertical();
         
        }

        GUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Open Editor Window"))
            {
                EditorStyle.CreateWizard(Lastnub);
            }
            EditorGUI.BeginDisabledGroup(myModel.UseBasicStyle);
            if (GUILayout.Button("Create a new style based on this"))
            {
                EditorStyle.EditorGUILayoutTextNow.ShowCreatWindow(myModel,myModel.GetComponent<Text>());
            }
            EditorGUI.EndDisabledGroup();
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }

    public void UpdateState()
    {
        StyleSetting[] AllStyleData = Resources.LoadAll<StyleSetting>("");
        foreach (StyleSetting child in AllStyleData)
        {
            if (child.TextMark == AllName[myModel.DemoTag.localnub])
                MyText = child;
        }
        SetFontDemoSytle();
        UpdateModle.UpdateState(myModel.transform);
    }


    private  StyleSetting MyText;
    private TextStyleMessage DemoGradient;
    private Text EditorDemoText;

    public void SetOutLine()
    {
         DemoGradient.IsDrawOutline = MyText.OutLine;
         DemoGradient.Outline_effectColor = MyText.OutLineStartColor;
         DemoGradient.Outline_effectDistance = MyText.EffectSize;
         DemoGradient.ShadowType = MyText.ShadowVector;
    }

    public void SetGradient()
    {
        DemoGradient.IsDrawGradent = MyText.Gradient;
        DemoGradient.colorTop = MyText.StartColor;
        DemoGradient.colorCenter = MyText.CenterColor;
        DemoGradient.colorBottom = MyText.EndColor;
        DemoGradient.GradientType = (TypeHV)MyText.ChoseGradientType;
    }


    public void SetShadow()
    {
        DemoGradient.IsDrawShadow = MyText.Shadow;
        DemoGradient.Shadow_effectColor = MyText.ShadowStartColor;
        DemoGradient.Shadow_effectDistance = MyText.ShadowEffectSize;
    }

    public void SetFontDemoSytle()
    {
        if (MyText == null||myModel.DemoTag.TextType== "自定义风格")
            return;

        if (MyText.Color_Bool)
            EditorDemoText.color = MyText.FontColor;
        if (MyText.FontSize_Bool)
            EditorDemoText.fontSize = MyText.FontSize;

        if (MyText.SizeData_Bool)
            EditorDemoText.rectTransform.sizeDelta = MyText.SizeData;
        if (MyText.PositionData_Bool)
            EditorDemoText.rectTransform.localPosition = MyText.PositionData;
        if (MyText.Font_Bool)
            EditorDemoText.font = MyText.Font;
        if (MyText.Raycast_Bool)
            EditorDemoText.raycastTarget = MyText.Raycast;

        SetOutLine();
            SetGradient();
            SetShadow();
        UpdateModle.UpdateState(myModel.transform);
    }
}