// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：射线检测
// 作成時間：2018-07-30
// 類を作る：HitcastEvent.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.EventSystems;

public class HitcastEvent : MonoBehaviour
{
    private RaycastHit hitInfo;

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if UNITY_IOS || UNITY_ANDROID
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                RadiographicConditionDetection();
#endif
            if (!EventSystem.current.IsPointerOverGameObject())
                RadiographicConditionDetection();
        }
    }

    /// <summary>
    /// 射线条件检测
    /// </summary>
    protected virtual void RadiographicConditionDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("Machine")))
        {
            hitInfo.transform.GetComponent<Model3DButton>().onClick.Send();
        }
    }
}