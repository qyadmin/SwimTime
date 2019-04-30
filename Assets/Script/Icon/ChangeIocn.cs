using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class ChangeIocn : MonoBehaviour {

    [SerializeField]
    private Sprite[] icon_three;
    [SerializeField]
    private Image icon_iamge;
    [SerializeField]
    private Text nametext;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image HeadIcon;


    public void Addname(string GetData)
    {
        JsonData GETDATA= JsonMapper.ToObject(GetData);
        ConfigManager.GetConfigManager.SetSmallIamge(HeadIcon,int.Parse(GETDATA["avatar"].ToString()));
        nametext.text= GETDATA["name"].ToString();
    }


    public void ChangeIcon(string GetData)
    {
        int nub = int.Parse(GetData);
        if (nub <= 3)
        {
            icon_iamge.sprite = icon_three[nub - 1];
            ScoreText.gameObject.SetActive(false);
        }
        else
        {
            ScoreText.text = GetData;
            icon_iamge.rectTransform.sizeDelta = new Vector2(0, 0);
        }
    }

}
