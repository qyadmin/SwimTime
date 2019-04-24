using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transform_Notice : MonoBehaviour
{

    public Transform noticeItemContentParent;
    public GameObject noticeItem;



    public HttpModel http_notice;
    public Button noticeBtn;
    public Transform WindowBASE_noticeDetail;
    public Transform_NoticeDetail transform_NoticeDetail;
    private DataDic<DataItem.NoticeData> _data;


    private void Start()
    {
        noticeBtn.onClick.AddListener(GetNotice);
    }


    private void GetNotice()
    {
        http_notice.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
        {
            if (msg.Data["ok"].ToString() == "ok")
            {
                GameManager.GetGameManager.ClearChild(noticeItemContentParent);
                JsonData tbl = msg.Data["tbl"];

                foreach (JsonData item in tbl)
                {
                    GameObject go = ObjectPool.GetInstance().GetObj(noticeItem, noticeItemContentParent);
                    NoticeItem nItem = go.GetComponent<NoticeItem>();
                    nItem.titleTxt.text = item["title"].ToString();
                    nItem.backgroundBtn.onClick.RemoveAllListeners();
                    nItem.backgroundBtn.onClick.AddListener(() =>
                    {
                        GameManager.GetGameManager.OpenWindow(WindowBASE_noticeDetail);
                        transform_NoticeDetail.GetDetail(item);
                    });
                }
            }
        });
        http_notice.Get();
    }
}