using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PalyButtonMusic : MonoBehaviour {

    public Sprite OpenImage,CloseIamge;
    private bool State = false;
    public Image _iamge;
    [SerializeField]
    private bool IsBack;
     void Start()
    {
       _iamge = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(ChangeSatte);
        if (IsBack)
            State = Static.Instance.MusicSwich;
        else
        State = Static.Instance.MusicSwichButton;
        SetState();
    }

    void ChangeSatte()
    {
        State = !State;
        SetState();
    }

    void SetState()
    {
        if (State)
        {
            _iamge.sprite = OpenImage;
            if(IsBack)
                Static.Instance.MusicSwich = true;
            else
            Static.Instance.MusicSwichButton = true;
        }
        else
        {
            _iamge.sprite = CloseIamge;
            if (IsBack)
                Static.Instance.MusicSwich = false;
            else
                Static.Instance.MusicSwichButton = false;
        }
    }

}
