using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


public  class ButtonEventBase : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public Action ActionEvent;
   //[SerializeField]
    private AudioSource PlaySound;

    public void SetMusic(AudioSource GetSouse)
    {
        PlaySound = GetSouse;
    }

    public virtual void Start()
    {
        if (PlaySound == null)
            PlaySound = GameObject.Find("PlayButtonMusic").GetComponent<AudioSource>(); 
    }

    public virtual void AddListener(Action GetListener)
    {
        ActionEvent += GetListener;
    }

    public virtual  void OnPointerClick(PointerEventData eventData)
    {     
        if (ActionEvent != null)
            ActionEvent.Invoke();
        DefEvent();
    }

    //默认事件
    private void DefEvent()
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(Up(this.transform));
        }
        PlaySound.Play();
    }

    IEnumerator Up(Transform Obj)
    {
        float addnub = 1.0f;
        while (addnub < 1.1f)
        {
            Obj.localScale = new Vector3(addnub, addnub, addnub);
            yield return new WaitForSeconds(0.02f);
            addnub += 0.01f / (1.1f - addnub);
        }
        while (addnub > 1.0f)
        {
            Obj.localScale = new Vector3(addnub, addnub, addnub);
            yield return new WaitForSeconds(0.02f);
            addnub -= 0.01f / (addnub);
        }
        Obj.localScale = new Vector3(1,1,1);
    }


    private void OnDisable()
    {
        StopCoroutine(Up(this.transform));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}