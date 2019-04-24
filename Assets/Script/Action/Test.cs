using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test : MonoBehaviour
{
    public void OnCopy(Text input)
    {
        AndroidJavaObject androidObject = new AndroidJavaObject("com.Company.RunHourse.RunHorseActivity");
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        if (activity == null)
            return;
        androidObject.Call("copyTextToClipboard", activity, input.text);
    }

    public void Paste(Text input)
    {
        AndroidJavaObject androidObject = new AndroidJavaObject("com.Company.RunHourse.RunHorseActivity");
        input.text = androidObject.Call<string>("getTextFromClipboard");
    }
}
