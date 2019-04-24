using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class report_Event : MonoBehaviour
{

    [HideInInspector]
    public List<string> X_Value = new List<string>();
    [HideInInspector]
    public List<Vector2> Y1_Value = new List<Vector2>();
    [HideInInspector]
    public List<Vector2> Y2_Value = new List<Vector2>();

    [SerializeField]
    WMG_Axis_Graph Axis;
    [SerializeField]
    WMG_Series y_Axis, y2_Axis;

    WMG_Axis X_Axis;
    WMG_Axis Y_Axis;

    JsonData mins;
    JsonData maxs;

    private void Awake()
    {
        X_Axis = Axis.xAxis;
        Y_Axis = Axis.yAxis;
    }


    public void Succece()
    {
        X_Axis.axisLabels.SetList(X_Value);

        y_Axis.pointValues.SetList(Y1_Value);

        y2_Axis.pointValues.SetList(Y2_Value);

        Axis.yAxis.AxisMaxValue = GetMaxs();
    }


    public void GetJsonDate(JsonData data)
    {
        X_Value.Clear();
        Y1_Value.Clear();
        Y2_Value.Clear();

        if (data == null) return;
        if (!data.Keys.Contains("mins")) return;
        if (!data.Keys.Contains("maxs")) return;

        if (data["mins"] != null)
            mins = data["mins"];
        if (data["maxs"] != null)
            maxs = data["maxs"];

        int mun1 = 0;
        foreach (var child in mins)
        {
            string str = child.ToString();
            str = str.Replace("[", "").Replace("]", "");
            string[] strgroup = str.Split(',');
            X_Value.Add(strgroup[0]);
            Y1_Value.Add(new Vector2(mun1, float.Parse(strgroup[1])));
            mun1++;
        }
        int mun2 = 0;
        foreach (var child in maxs)
        {
            string str = child.ToString();
            str = str.Replace("[", "").Replace("]", "");
            string[] strgroup = str.Split(',');
            Y2_Value.Add(new Vector2(mun2, float.Parse(strgroup[1])));
            mun2++;
        }
        Succece();
    }


    float GetMaxs()
    {
        float max = 0;

        foreach (Vector2 i in Y1_Value)
        {
            if (i.y >= max)
                max = i.y;
        }
        foreach (Vector2 i in Y2_Value)
        {
            if (i.y >= max)
                max = i.y;
        }

        return max;
    }

}
