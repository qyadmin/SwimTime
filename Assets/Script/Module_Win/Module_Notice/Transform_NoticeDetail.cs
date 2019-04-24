using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transform_NoticeDetail : MonoBehaviour
{
    public Text contentTxt;

    public void GetDetail(LitJson.JsonData data)
    {
        contentTxt.text = data["text"].ToString();
        StartCoroutine(DelayAdaptation());
    }

    private IEnumerator DelayAdaptation()
    {
        contentTxt.transform.parent.gameObject.GetComponent<ContentSizeFitter>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        contentTxt.transform.parent.gameObject.GetComponent<ContentSizeFitter>().enabled = true;
    }
}
