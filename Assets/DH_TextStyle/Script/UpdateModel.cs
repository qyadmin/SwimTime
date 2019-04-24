using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateModle
{
    public static void UpdateState(Transform Obj)
    {
        Obj.localPosition += new Vector3(0,1,0);
        Obj.localPosition += new Vector3(0, -1, 0);
    }

    public static List<string> GetStyleGroup()
    {
        List<string> AllName = new List<string>();
        AllName.Clear();
        StyleSetting[] AllSytleData = Resources.LoadAll<StyleSetting>("");

        foreach (StyleSetting child in AllSytleData)
        {
            AllName.Add(child.TextMark);
        }
        AllName.Add("自定义风格");

        return AllName;
    }
}

public enum TypeHV
{
    Horizontal,
    Vertica,
    VerticaThreeColor

}

public enum OutLineVector
{
    Shadow_4,
    Shadow_8,
    Shadow_16
}
