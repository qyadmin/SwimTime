using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlOPC : MonoBehaviour
{
    private long startSec;

    private void Start()
    {
        if (TimeBtn != null)
        {
            TimeBtn.onClick.AddListener(Wait);
        }
    }

    public void Open(Transform obj)
    {
        GameManager.GetGameManager.OpenWindow(obj);
    }

    public void Close(Transform obj)
    {
        GameManager.GetGameManager.CloseWindow(obj);

    }

    [SerializeField]
    private Button TimeBtn;
    public void Wait()
    {
        GetComponent<HttpModel>().HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            Debug.Log(LitJson.JsonMapper.ToJson(msg.Data));
            if (msg.Data["code"].ToString() == "1")
            {
                startSec = System.DateTime.Now.Second + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Day * 86400;
                StopAllCoroutines();
                StartCoroutine(Loop(TimeBtn));
            }
        });

    }


    [SerializeField]
    private GameObject mark;
    IEnumerator Loop(Button obj)
    {
        //Debug.Log(startSec);
        obj.interactable = false;
        Text a = obj.GetComponentInChildren<Text>();
        while (System.DateTime.Now.Second + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Day * 86400 - startSec <= 60)
        {
            a.text = (60 - (System.DateTime.Now.Second + System.DateTime.Now.Minute * 60 + System.DateTime.Now.Hour * 3600 + System.DateTime.Now.Day * 86400 - startSec)).ToString() + "秒";
            yield return new WaitForSeconds(1);
        }

        a.text = "获取验证码";
        obj.interactable = true;
    }


    public void Restart(Text obj)
    {
        mark.SetActive(false);
    }
}
