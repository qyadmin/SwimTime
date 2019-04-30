using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class CopyClass : MonoBehaviour
{
#if UNITY_IPHONE
                /* Interface to native implementation */
                //[DllImport ("__Internal")]
                //private static extern void _copyTextToClipboard(string text);
#endif

    public void CopyToClipboard(Text input)
    {
//#if UNITY_ANDROID
//        GetComponent<Test>().OnCopy(input);
//#elif UNITY_IPHONE
//             _copyTextToClipboard(input.text);
//#endif
    }
}