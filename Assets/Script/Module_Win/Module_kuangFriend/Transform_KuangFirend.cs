// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：矿友窗口组件
// 作成時間：2018-07-27
// 類を作る：Transform_KuangFirend.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class Transform_KuangFirend : MonoBehaviour
{
    private DataDic<DataItem.KuangFirendData> _data;
	public InsGroup Friend;
	public HttpModel HTTP_Friend;
	public Button BTN_OPEN,BTN_ONE,BTN_TWO;
	public ButtonChangeGroup fd;
    public HttpModel http_getTeamTouzi;
    public Text teamTouziTxt;
    void Start()
    {
        DataManager.GetDataManager.kuangFirendList.EventObj.Addlistener(UpdateData);
        BTN_OPEN.onClick.AddListener(delegate ()
       {
           http_getTeamTouzi.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
           {
               if (msg.Data["ok"].ToString() == "ok")
               {
                   teamTouziTxt.text = msg.Data["teamtouzi"].ToString();
               }
           });
            http_getTeamTouzi.Get();

                BTN_ONE.onClick.AddListener(delegate() 
				{
						HTTP_Friend.Data.DataName="myfriendone";
						HTTP_Friend.Data.NeedReplayName=true;
						HTTP_Friend.Data.ReplayName="myfriend";
						HTTP_Friend.Data.URL="bind/loadone";
						HTTP_Friend.Get();
				});
				BTN_TWO.onClick.AddListener(delegate() 
				{
						HTTP_Friend.Data.DataName="myfriendtwo";
						HTTP_Friend.Data.NeedReplayName=true;
						HTTP_Friend.Data.ReplayName="myfriend";
						HTTP_Friend.Data.URL="bind/loadtwo";
						HTTP_Friend.Get();
				});
				
				HTTP_Friend.Data.DataName="myfriendone";
				HTTP_Friend.Data.NeedReplayName=true;
				HTTP_Friend.Data.ReplayName="myfriend";
				HTTP_Friend.Data.URL="bind/loadone";
				HTTP_Friend.Get();
				fd.SetFist();
			
		});
        
    }
    private void UpdateData(object data)
    {

		GameManager.GetGameManager.ClearChild(Friend.Parent);
		_data = data as DataDic<DataItem.KuangFirendData>;

        foreach (DataItem.KuangFirendData item in _data.Data)
        {
            GameObject go = ObjectPool.GetInstance().GetObj(Friend.listobj, Friend.Parent);
            TransformData obj = go.GetComponent<TransformData>();
            LoadImage.GetLoadIamge.Load(item.userinfo["avatar"].ToString(), new RawImage[] { obj.GetObjectValue<RawImage>("head") });
            obj.GetObjectValue<Text>("name").text = item.userinfo["nickname"].ToString();
            obj.GetObjectValue<Text>("id").text = item.userinfo["id"].ToString();
            obj.GetObjectValue<Text>("lvl").text = GlobalData.GameConstConfig.GetGuojikuangyouUserLevel(int.Parse(item.userinfo["level"].ToString()));
        }
    }
}
