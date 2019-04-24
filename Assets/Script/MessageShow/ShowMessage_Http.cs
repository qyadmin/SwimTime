using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowMessage_Http : MonoBehaviour
{

    [SerializeField]
    private Text ShowMessage;
    Coroutine OpenM;
    private float waittime;
    public void SetMessage(string Message)
    {
        waittime = 0;
        ActionMesaaage(Message);
    }

    public void SetMessage(string Message, float wait)
    {
        waittime = wait;
        ActionMesaaage(Message);
    }

    private void ActionMesaaage(string Message)
    {
        if (Message == "(-1)")
            return;
        if (OpenM != null)
        {
            StopCoroutine(OpenM);
        }
        ShowMessage.text = Message;
        transform.localScale = new Vector3(0, 0, 0);
        OpenM = StartCoroutine("Open");
    }

    IEnumerator Open()
    {
        while (transform.localScale.y <= 1)
        {
            transform.localScale += new Vector3(0, 0.2f, 0);
            yield return 0;
        }
        transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(1.5f + waittime);

        while (transform.localScale.y > 0)
        {
            transform.localScale -= new Vector3(0, 0.2f, 0);
            yield return 0;
        }
        transform.localScale = new Vector3(0, 0, 0);
        ShowMessage.text = string.Empty;
    }
}
