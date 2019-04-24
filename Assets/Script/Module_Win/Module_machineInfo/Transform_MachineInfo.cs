// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：矿机信息窗口组件
// 作成時間：2018-07-30
// 類を作る：Transform_MachineInfo.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;
using DataItem;
using System.Collections;

public class Transform_MachineInfo : MonoBehaviour
{

    DataDic<MachineInfoData> data;
    public InsGroup Mhe_list;
    public InsGroup MheScreen_list;
    public Transform AllCar;
    private int[] localNumGroup = new int[4];

    public HttpModel http_yijianwakuang;
    public Button yijianwakuang_btn;
    private Coroutine StartIntermittenceShow;

    public Transform WindowBASE_kuangchetixian;
    public Transfrom_MachineTixian transfrom_MachineTixian;
    void Start()
    {
        DataManager.GetDataManager.machineInfo.EventObj.Addlistener(UpdataData);
    }

    private void UpdataData(object dataObj)
    {
        data = dataObj as DataDic<MachineInfoData>;
        if (data.Data == null) return;
        yijianwakuang_btn.onClick.RemoveAllListeners();
        yijianwakuang_btn.onClick.AddListener(() =>
        {
            if (StartIntermittenceShow != null)
                StopCoroutine(StartIntermittenceShow);
            StartIntermittenceShow = StartCoroutine(GetStartAll());
        });
        GameManager.GetGameManager.ClearChild(MheScreen_list.Parent);
        GameManager.GetGameManager.ClearChild(Mhe_list.Parent);
        localNumGroup = new int[4];
        foreach (MachineInfoData item in data.Data)
        {

            //我的车库
            GameObject go = ObjectPool.GetInstance().GetObj(Mhe_list.listobj, Mhe_list.Parent);
            TransformData Transformdata = go.GetComponent<TransformData>();
            Transformdata.GetObjectValue<Text>("name").text = GlobalData.GameConstConfig.GetCarName(item.tp);
            //int muncar = int.Parse(item.tp.Replace("sb", "")) / 500 - 1;
            int muncar = GlobalData.GameConstConfig.GetCarIndex(item.tp);
            ConfigManager.GetConfigManager.SetCarImage(Transformdata.GetObjectValue<RawImage>("head"), muncar);
            Button tixianBtn = Transformdata.GetObjectValue<Button>("tixianBtn");
            tixianBtn.onClick.RemoveAllListeners();
            tixianBtn.onClick.AddListener(() =>
            {
                GameManager.GetGameManager.OpenWindow(WindowBASE_kuangchetixian);
                transfrom_MachineTixian.TixianTask(item);
            });
            //我的场景
            GameObject goScreen = ObjectPool.GetInstance().GetObj(MheScreen_list.listobj, MheScreen_list.Parent);
            ConfigManager.GetConfigManager.SetCarImage(goScreen.GetComponent<RawImage>(), muncar);
            localNumGroup[muncar]++; 
        }
        for (int i = 0; i < localNumGroup.Length; i++)
        {
            AllCar.GetChild(i).GetComponent<TransformData>().GetObjectValue<Text>("Num").text = localNumGroup[i].ToString();
        }
    }
    private IEnumerator GetStartAll()
    {
        if (data.Data.Count == 0)
        {
            StopCoroutine(StartIntermittenceShow);
            yield return null;
        }
        bool islock = true;
        bool Sus = true;
        string msgcode = GlobalData.NETWORK_REQUEST_FAILED;
        yield return new WaitForSeconds(.1f);
        http_yijianwakuang.HideMessage = true;
        http_yijianwakuang.NoldIcon = true;
        foreach (MachineInfoData item in data.Data)
        {
            http_yijianwakuang.Data.AddData("phone", Static.Instance.GetValue("phone"));
            http_yijianwakuang.Data.AddData("id", item.id.ToString());
            http_yijianwakuang.HttpSuccessCallBack.ClearAllEevnt();
            http_yijianwakuang.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
            {
                if (msg.Code == HttpCode.FAILED)
                {
                    Sus = false;
                    msgcode = GlobalData.THE_MINER_HAS_BEEN_GET_GAINS_TODAY;
                }
                if (msg.Code == HttpCode.SUCCESS)
                {
                    if (Sus)
                        msgcode = http_yijianwakuang.Data.ErrorMsg;
                }


                islock = false;
            });
            http_yijianwakuang.Get();
            islock = true;
            while (islock)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(.1f);
        }
        http_yijianwakuang.NoldIcon = false;
        http_yijianwakuang.HideMessage = false;
        MessageManager._Instantiate.Show(msgcode);
    }
}
