using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 移动设备输入框的自适应组件
/// </summary>
public class ChatViewAdaptMobileKeyBoard : MonoBehaviour
{
    InputField _inputField = null;
    [SerializeField]
    Text dfd;
    bool Lock = false;

    float TargetHeight;
    /// <summary>
    /// 自适应（弹出输入框后整体抬高）的面板的初始位置
    /// </summary>
    private Vector3 _adaptPanelOriginPos;
    [SerializeField]
    RectTransform _adaptPanelRt;
    private float RESOULUTION_HEIGHT = 1280F;

    private void Update()
    {
        if (_inputField!= null && _inputField.isFocused)
        {
            if (!Lock)
                TargetHeight = _inputField.transform.position.y;
            if (Application.platform == RuntimePlatform.Android)
            {
//                float ChaZhi = AndroidGetKeyboardHeight()+ 160f - TargetHeight;
//                float keyboardHeight = ChaZhi * RESOULUTION_HEIGHT / Screen.height;
//
//                dfd.text = AndroidGetKeyboardHeight().ToString()+"    "+ TargetHeight+"     " + keyboardHeight.ToString() +"    "+ ChaZhi;
//                Debug.LogFormat("安卓平台检测到InputField.isFocused为真，获取键盘高度：{0}, Screen.height：{1}", keyboardHeight, Screen.height);
//                if(ChaZhi>0)
//                _adaptPanelRt.anchoredPosition = Vector3.up * (keyboardHeight);
				float ChaZhi = AndroidGetKeyboardHeight() + (150*Screen.height/1024) - TargetHeight;
				float keyboardHeight = ChaZhi;
				
				dfd.text = AndroidGetKeyboardHeight().ToString()+"    "+ TargetHeight+"     " + keyboardHeight.ToString() +"    "+ ChaZhi;
				Debug.LogFormat("安卓平台检测到InputField.isFocused为真，获取键盘高度：{0}, Screen.height：{1}", keyboardHeight, Screen.height);
				if(ChaZhi>0)
				_adaptPanelRt.transform.position = new Vector3 (_adaptPanelOriginPos.x,_adaptPanelOriginPos.y + keyboardHeight,_adaptPanelOriginPos.z);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
				float ChaZhi = IOSGetKeyboardHeight()  + (150*Screen.height/1024) - TargetHeight;
				float keyboardHeight = ChaZhi;
                //Debug.Log("IOS平台检测到键盘高度：{0},Screen.height: {1}", keyboardHeight, Screen.height);
				if(ChaZhi>0)
				_adaptPanelRt.transform.position = new Vector3 (_adaptPanelOriginPos.x,_adaptPanelOriginPos.y + keyboardHeight,_adaptPanelOriginPos.z);
            }
            else
            {
//				float ChaZhi =500f+100f - TargetHeight;
//				float keyboardHeight = ChaZhi;
//
//				dfd.text = "600"+"    "+ TargetHeight+"     " + keyboardHeight.ToString() +"    "+ ChaZhi;
//				Debug.LogFormat("安卓平台检测到InputField.isFocused为真，获取键盘高度：{0}, Screen.height：{1}", keyboardHeight, Screen.height);
//				if (ChaZhi > 0) {
//					_adaptPanelRt.transform.position = new Vector3 (_adaptPanelOriginPos.x,_adaptPanelOriginPos.y + keyboardHeight,_adaptPanelOriginPos.z);
//					//_adaptPanelRt.anchoredPosition = Vector3.up * (keyboardHeight);
//				}
            }
            Lock = true;
        }
    }

    private void OnValueChanged(string arg0) {
        
    }


    /// <summary>
    /// 结束编辑事件，TouchScreenKeyboard.isFocused为false的时候
    /// </summary>
    /// <param name="currentInputString"></param>
    private void OnEndEdit(string currentInputString)
    {
        //Debuger.LogFormat("OnEndEdit, 输入内容：{0}, 结束时间：{1}", currentInputString, Time.realtimeSinceStartup);
        Lock = false;
        _inputField.onEndEdit.RemoveListener(OnEndEdit);
        _inputField.onValueChanged.RemoveListener(OnValueChanged);
        _inputField = null;
		_adaptPanelRt.transform.position = _adaptPanelOriginPos;
    }

    /// <summary>
    /// 获取安卓平台上键盘的高度
    /// </summary>
    /// <returns></returns>
    public int AndroidGetKeyboardHeight()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").
                Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);
                return Screen.height - Rct.Call<int>("height");
            }
        }
    }


    public float IOSGetKeyboardHeight()
    {
        return TouchScreenKeyboard.area.height;
    }

    public void InputFieldClick(InputField self)
    {
		//ResetScreen ();
		//_adaptPanelOriginPos = _adaptPanelRt.transform.position;
  //      _inputField = self;
  //      _inputField.onEndEdit.AddListener(OnEndEdit);
  //      _inputField.onValueChanged.AddListener(OnValueChanged);
    }
	void ResetScreen()
	{
		Lock = false;
		if (_inputField == null)
			return;
		_inputField.onEndEdit.RemoveListener(OnEndEdit);
		_inputField.onValueChanged.RemoveListener(OnValueChanged);
		_inputField = null;
		_adaptPanelRt.transform.position = _adaptPanelOriginPos;
	}

	public void ChangeAdapat(RectTransform adapat)
	{
		ResetScreen ();
		_adaptPanelRt = adapat;
	}
}