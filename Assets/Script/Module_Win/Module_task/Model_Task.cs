using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DataItem;

public class Model_Task : MonoBehaviour
{

    public Transform FatherObj;
    public GameObject ListObj;
    public HttpModel http_lingqu;
    public Transform WindowTask;

    DataDic<TaskData> data;
    void Start()
    {
        DataManager.GetDataManager.task.EventObj.Addlistener(UpdataData);
    }
		
    private void UpdataData(object dataObj)
    {
        data = dataObj as DataDic<TaskData>;
        foreach (Transform child in FatherObj)
            ObjectPool.GetInstance().RecycleObj(child.gameObject);
        foreach (TaskData child in data.Data)
        {
            if (child.req)
                continue;
            GameObject item = ObjectPool.GetInstance().GetObj(ListObj, FatherObj);
            Transform_Task obj = item.GetComponent<Transform_Task>();
            TaskMessage task = ConfigManager.GetConfigManager.GetTaskmessage(child.id);
            obj.icon.sprite = task.iamge;
            obj.Name.text = task.name;
            obj.desc.text = task.desc.Replace("s", child.count.ToString());

            obj.coin.text = child.coin.ToString();
            obj.jindu.text = child.current + "/" + child.count;
            if (child.complete)
            {
                obj.GoButton.GetComponentInChildren<Text>().text = "领取奖励";
                obj.GoButton.onClick.AddListener(delegate ()
               {
                   http_lingqu.Data.AddData("id", child.id);
                   http_lingqu.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage data)
                   {
                       if(data.Code==HttpCode.SUCCESS)
                       DataManager.GetDataManager.task.Remove(child.id);
                   });
                   http_lingqu.Get();

               });
            }
            else
            {
                obj.GoButton.GetComponentInChildren<Text>().text = "前往";
                obj.GoButton.onClick.AddListener(delegate ()
               {
                   if (task.TagWindow)
                       GameManager.GetGameManager.OpenWindow(task.TagWindow);

                   GameManager.GetGameManager.CloseWindow(WindowTask);
               });
            }
        }
    }
}
