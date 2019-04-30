using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ButtonGroupData
{
    private List<Button> Group=new List<Button>();
    public Color Normal;
    public Color PressDown;

    public void SetButton(Transform ButtonFather)
    {
        foreach (Transform child in ButtonFather)
        {
            Button item = child.GetComponent<Button>();
            Group.Add(item);
            item.onClick.AddListener(delegate ()
            {
                ChangeStatus();
                Text[] childtexts = child.GetComponentsInChildren<Text>();
                foreach(Text childtext in childtexts)
                    childtext.GetComponent<Text>().color = PressDown;
            });
        }
    }
    public void ChangeStatus()
    {
        foreach (Button child in Group)
        {
            Text[] childtexts = child.GetComponentsInChildren<Text>();
            foreach (Text childtext in childtexts)
                childtext.GetComponent<Text>().color = Normal;
        }
    }
}

public class ButtonGroup : MonoBehaviour {

    [SerializeField]
    private ButtonGroupData Group;
    private void Start()
    {
        Group.SetButton(this.transform);
    }

}
