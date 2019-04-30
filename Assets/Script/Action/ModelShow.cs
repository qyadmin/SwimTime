// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：模型旋转
// 作成時間：2018-08-09
// 類を作る：ModelShow.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;

public class ModelShow : MonoBehaviour {
    [SerializeField]
    private float speed = 0f;
    private void Update()
    {
        transform.eulerAngles += Vector3.back * speed;
    }
}
