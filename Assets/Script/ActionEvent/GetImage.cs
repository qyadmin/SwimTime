using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetImage : MonoBehaviour {


    [SerializeField]
    private Image HeadIamge;
    [SerializeField]
    private Image HeadIamgeSet;
    public void GetHeadIamge(string GetData)
    {
        ConfigManager.GetConfigManager.SetIamge(HeadIamge,int.Parse(GetData));
        ConfigManager.GetConfigManager.SetIamge(HeadIamgeSet, int.Parse(GetData));
    }

}
