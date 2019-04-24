using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleState : MonoBehaviour {

    [SerializeField]
    private Toggle GetToggle;
    [SerializeField]
    private GameObject AotuoButton;
    [SerializeField]
    private Image BackImage;
    private Color SelfColor;
    private Color SelfColorMark;
    [SerializeField]
    private GameObject mark;
    public void TiggerOn(string getData)
    {
        SelfColor = BackImage.color;
       // SelfColorMark=SelfColor - new Color(0, 0, 0, 0.5f);
        GetToggle.isOn = System.Convert.ToBoolean(int.Parse(getData));
        if (GetToggle.isOn)
        {
            AotuoButton.SetActive(true);
            BackImage.color = SelfColorMark;
            mark.SetActive(true);
        }
        else
        {
           // BackImage.color = SelfColor;
            AotuoButton.SetActive(false);
            mark.SetActive(false);
        }
    }
}
