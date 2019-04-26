using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class DeviceID{
    [DllImport("__Internal")]
    private static extern string GetIphoneADID();

    public static string Get() {
#if UNITY_ANDROID
        string aID = SystemInfo.deviceUniqueIdentifier;
        return "ANDROID-"+aID;
#elif UNITY_IPHONE  
        string iID = GetIphoneADID();
        return "IOS-"+iID;
#endif
        return "isnot in mobilePhone";
    }
}
