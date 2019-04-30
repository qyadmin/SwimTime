// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：排行榜窗口组件
// 作成時間：2018-07-26
// 類を作る：Transform_Rank.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;

public class Transform_Rank : MonoBehaviour
{
    public ToggleEventAction[] ToggleGroup;
    public Text rankTxt;
    public Transform parentObj;
    public GameObject rankItem;

    public Button rankBtn;
    public LoadImage load_iamge;

    private DataDic<DataItem.RankData> _data;

    public void init()
    {
        foreach (ToggleEventAction item in ToggleGroup)
        {
            item.toggle.isOn = false;
        }
        ToggleGroup[0].toggle.isOn = true;
    }
    private void Start()
    {
        DataManager.GetDataManager.rank.EventObj.Addlistener(UpdateData);
        foreach (ToggleEventAction item in ToggleGroup)
        {
            item.RegistEvent();
        }
        rankBtn.onClick.AddListener(init);
    }

    private void UpdateData(object data)
    {
        _data = data as DataDic<DataItem.RankData>;
        foreach (Transform item in parentObj)
        {
            ObjectPool.GetInstance().RecycleObj(item.gameObject);
        }

        Debug.Log(_data.Data.Count);
        foreach (DataItem.RankData item in _data.Data)
        {
            GameObject go = ObjectPool.GetInstance().GetObj(rankItem);
            go.transform.SetParent(parentObj);
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            RankItem rItem = go.GetComponent<RankItem>();
            rItem.rankNumObj.transform.GetChild(0).gameObject.SetActive(item.rank == 1);
            rItem.rankNumObj.transform.GetChild(1).gameObject.SetActive(item.rank == 2);
            rItem.rankNumObj.transform.GetChild(2).gameObject.SetActive(item.rank == 3);
            rItem.rankNumObj.transform.GetChild(3).gameObject.SetActive(item.rank > 3);
            if (item.rank > 3)
            {
                rItem.rankNumObj.transform.GetChild(3).GetComponent<Text>().text = item.rank.ToString();
            }

            rItem.boardObj.transform.GetChild(0).gameObject.SetActive(item.rank == 1);
            rItem.boardObj.transform.GetChild(1).gameObject.SetActive(item.rank == 2);
            rItem.boardObj.transform.GetChild(2).gameObject.SetActive(item.rank == 3);
            rItem.boardObj.transform.GetChild(3).gameObject.SetActive(item.rank > 3);


            rItem.nameTxt.text = item.userinfo[GlobalData.nickname].ToString();
            rItem.sanTxt.text = GlobalData.GameConstConfig.GetSan(int.Parse(item.userinfo[GlobalData.paragraph].ToString()));
            load_iamge.Load(item.userinfo[GlobalData.avatar].ToString(), new RawImage[] { rItem.headRawImg });
            rItem.suanliTxt.text = item.score.ToString();
        }
    }
}
