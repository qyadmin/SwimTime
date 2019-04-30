// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：绑定上级窗口组件
// 作成時間：2018-08-07
// 類を作る：Transform_BindingSuperior.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class Transform_BindingSuperior : MonoBehaviour {
    public Button submitBtn;
    public InputField iDInput;
    public HttpModel http_bindingSuperior;

    private void Start()
    {
        BindingSuperiorEvent();
    }

    private void BindingSuperiorEvent()
    {
        submitBtn.onClick.AddListener(() =>
        {
            http_bindingSuperior.Data.AddData(GlobalData.tophone, iDInput.text.Trim());
            http_bindingSuperior.Get();
            http_bindingSuperior.HttpSuccessCallBack.Addlistener((ReturnHttpMessage rhMsg) =>
            {
                if (rhMsg.Code == HttpCode.SUCCESS)
                {
                    MessageManager._Instantiate.Show(GlobalData.BINGING_SUCCESS);
                }
            });
        });
    }
}
