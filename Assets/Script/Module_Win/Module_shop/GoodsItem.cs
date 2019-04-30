// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：在这里修改脚本说明
// 作成時間：#CreateTime#
// 類を作る：GoodsItem.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class GoodsItem : MonoBehaviour {

    public Text _nameTxt, _priceTxt, _showMsgTxt;
    public Image _typeIcon, _msgMask;
    public Button _chooseBtn, _showMsgBtn;
    [HideInInspector]
    public bool _lock = false;
}
