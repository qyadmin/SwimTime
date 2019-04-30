// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：等待图标旋转
// 作成時間：2018-07-26
// 類を作る：WaitIcon.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;

public class WaitIcon : MonoBehaviour {
	void Update () {
        transform.eulerAngles += Vector3.back * 3.3f;
    }
}
