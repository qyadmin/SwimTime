using UnityEngine;
using DataItem;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public class DataManager : MonoBehaviour
{
    public static DataManager GetDataManager;

    private void Awake()
    {
        GetDataManager = this;
    }
    public GameObject OnResponesObj { get { return this.gameObject; } }

    #region Data创建区域
    //列表数据
    public DataDic<TaskData> task = new DataDic<TaskData>("task");
    public DataDic<RankData> rank = new DataDic<RankData>("list");
    public DataDic<KuangFirendData> kuangFirendList = new DataDic<KuangFirendData>("myfriend");
	public DataDic<gs_Data> businessCenter = new DataDic<gs_Data>("hangsell");
	public DataDic<gs_Data> businessdingdan = new DataDic<gs_Data>("dingdan");
	public DataDic<gs_Data> businessCenterqiugou = new DataDic<gs_Data>("lookingfor");
	public DataDic<gs_Data> businesspipei= new DataDic<gs_Data>("pipei");
	public DataDic<gs_Data> businessMyfb= new DataDic<gs_Data>("fabu");
    public DataDic<ShopData> shop = new DataDic<ShopData>("Shop");
    public DataDic<MachineInfoData> machineInfo = new DataDic<MachineInfoData>("miners");
    public DataDic<NoticeData> notice = new DataDic<NoticeData>("data");
    //单体数据
    public DataInfo<UserData> user = new DataInfo<UserData>("userdata");
    public DataInfo<PaomaDengData> paomaDeng = new DataInfo<PaomaDengData>("PaomaDengData");
    #endregion //data创建区域

    #region 接收消息区域
    

    //任务列表
    public void Receive_Data(object[] data)
    {
		if (data [3].ToString () == "userdata") 
		{
			JsonData jd = (JsonData)data [0];
			jd ["phone"] = jd ["phone"].ToString ();
			jd ["superior"] = jd ["superior"].ToString ();
			data [0] = jd;
			Debug.Log (JsonMapper.ToJson(jd));
		}
        DataPool.GetInstance().SendDataMessage.Send(data);
    }
		
    #endregion //********************接收消息区域

	public void UpdateSb(string jd)
	{
		user.Data.sb = double.Parse(jd);
		user.SyncData ();
	}
}