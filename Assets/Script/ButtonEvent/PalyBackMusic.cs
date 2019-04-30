using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PalyBackMusic : MonoBehaviour {

	public bool IsOpen=true;
	public Sprite OpenTexture;
	public Sprite CloseTexture;
	public AudioSource PlaySound;
	private Image AduioImage;
    [SerializeField]
    private bool NoButton=false;
    private void OnEnable()
    {
        IsOpen = Static.Instance.MusicSwich;
        AduioImage = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(PlayeBack);
        SetMusic();
    }

	public void PlayeBack()
	{
		IsOpen = !IsOpen;
		SetMusic ();
	}

	public void SetMusic()
	{
        Debug.Log(IsOpen);
		PlaySound.enabled = IsOpen;
		Static.Instance.MusicSwich = IsOpen;
		AduioImage.sprite = IsOpen ? OpenTexture : CloseTexture;
	}


}
