using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using LitJson;
using Utils;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public static GameManager GetGameManager;

    public void Awake()
    {
        GetGameManager = this;
        //清除对象池缓存
        ObjectPool.GetInstance().ClearAll();
    }

    [SerializeField]
    private Transform WindownBody;


    public UnityEvent ActionEvent;


    [SerializeField]
    private Transform[] AllWindowBody;
    [SerializeField]
    private Transform[] NoEffectWindowBody;

    private List<Transform> NoEffectObj = new List<Transform>();

    [SerializeField]
    private bool NoManiScene;

    private void Update()
    {
        Timer.Instance.DoUpdate();
        Timer.Instance.DoFixUpdate();
    }

    private void Start()
    {
        //配置游戏信息
        if (!NoManiScene)
        {
            ConfigManager.GetConfigManager.LoadConfig();

            foreach (Transform child in WindownBody)
            {
                child.localScale = new Vector3(0, 0, 0);
                child.GetComponent<Image>().color = new Color(0, 0, 0, .6f);
                child.gameObject.SetActive(false);
                //AllWindown.Add(child,child.localPosition);
                // child.transform.localPosition = new Vector3(0, 100000, 0);
            }
        }
        if (AllWindowBody.Length > 0)
        {
            foreach (Transform child in AllWindowBody)
            {
                child.localScale = new Vector3(1, 1, 1);
                child.gameObject.SetActive(false);
                // AllWindown.Add(child, child.localPosition);
                // child.transform.localPosition = new Vector3(0, 100000, 0);
            }
        }

        if (NoEffectWindowBody.Length > 0)
        {
            foreach (Transform child in NoEffectWindowBody)
            {
                child.localScale = new Vector3(1, 1, 1);
                child.gameObject.SetActive(false);
                NoEffectObj.Add(child);
            }
        }

        ActionEvent.Invoke();

        Invoke("callback", 5f);

    }
    public List<Transform> ClearEffect = new List<Transform>();

    //储存窗口坐标信息
    Dictionary<Transform, Vector3> AllWindown = new Dictionary<Transform, Vector3>();
    //获取当前显示的窗口坐标信息

    /// <summary>
    /// 窗口跳转设置
    /// </summary>
    /// <param name="willOpenWin">待开启窗口</param>
    /// <param name="willCloseWin">待关闭窗口</param>
    public void ThisWindowStateSetting(Transform willOpenWin, Transform willCloseWin)
    {
        OpenWindow(willOpenWin);
        CloseWindow(willCloseWin);
    }

    public void OpenWindow(Transform key)
    {
        //AddRffectNub(key);
        //key.gameObject.SetActive(true);
        DotweenSetting(key.gameObject, true);
        //return;
        //}
        //IsMove = true;
        //key.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //key.gameObject.SetActive(true);
        //StartCoroutine(Up(key));
    }

    public void CloseWindow(Transform key)
    {
        //if (NoEffectObj.Contains(key))
        //{    
        //RemoveEffectNub(key);
        //key.gameObject.SetActive(false);
        // return;
        //}
        DotweenSetting(key.gameObject, false);
        //key.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //StartCoroutine(Dwon(key));
    }

    public void AddRffectNub(Transform key)
    {       
        if (!NoEffectObj.Contains(key))
        {
            ClearEffect.Add(key);
        }
    }

    public void RemoveEffectNub(Transform key)
    {
        if(ClearEffect.Contains(key))
           ClearEffect.Remove(key);

    }

    IEnumerator Up(Transform Obj)
    {
        IsMove = true;
        float addnub = 0.5f;
        while (addnub < 1.0f)
        {
            Obj.localScale = new Vector3(addnub, addnub, 1);
            yield return new WaitForSeconds(0.03f);
            addnub += 0.1f / (1 - addnub);
        }
        Obj.localScale = new Vector3(1, 1, 1);
        Obj.GetComponent<Image>().color = new Color(0, 0, 0, 0.6f);
        IsMove = false;
    }

    IEnumerator Dwon(Transform Obj)
    {
        float addnub = 1.0f;
        while (addnub > 0.5f)
        {
            Obj.localScale = new Vector3(addnub, addnub, 1);
            yield return new WaitForSeconds(0.03f);
            addnub -= 0.1f / (addnub);
        }
        Obj.localScale = new Vector3(0.5f, 0.5f, 1);
        Obj.gameObject.SetActive(false);
        IsMove = false;
    }

    //当前偷去的好友ID;
    public string fuid;
    public bool IsFriend = false;

    //好友模式需要隐藏的按钮
    public Transform[] Hide;
    //好友模式需要显示按钮
    public Transform[] ShowInFriend;
    //好友模式需要隐藏的窗口
    public Transform[] Hidewindow;
    //获取矿场位置信息隐藏
    [SerializeField]
    private Transform kdbody;
    [SerializeField]
    private Transform BackMySelf;


    public void SetState(bool isget)
    {
        IsFriend = isget;
        if (isget)
        {
            BackMySelf.gameObject.SetActive(true);
            foreach (Transform child in Hide)
                child.localScale = new Vector3(0, 0, 0);
            foreach (Transform child in kdbody)
                child.localScale = new Vector3(0, 0, 0);
            foreach (Transform child in Hidewindow)
                CloseWindow(child);
            foreach (Transform child in ShowInFriend)
                child.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            BackMySelf.gameObject.SetActive(false);
            foreach (Transform child in Hide)
                child.localScale = new Vector3(1, 1, 1);
            foreach (Transform child in kdbody)
                child.localScale = new Vector3(1, 1, 1);
            foreach (Transform child in ShowInFriend)
                child.localScale = new Vector3(0, 0, 0);
        }
    }

    [System.Serializable]
    public class CatchDataMessage
    {
        public string name;
        public HttpModel datatmodel;
    }

    public bool IsMove = false;

    //升级解锁
    public void Levle_UnLock(int nub)
    {
       // int maxlvl = int.Parse(Static.Instance.GetValue("lvl"));
        if (nub % 5 != 1 || nub == 1)
        {
            MessageManager._Instantiate.Show("升级成功！");
            return;
        }
        string getvalue = ((nub - 1) / 5).ToString();
        switch (getvalue)
        {
            case "1":
                MessageManager._Instantiate.Show("升级成功！恭喜你，你已解锁黄色道具", 1);
                break;
            case "2":
                MessageManager._Instantiate.Show("升级成功！恭喜你，你已解锁绿色道具", 1);
                break;
            case "3":
                MessageManager._Instantiate.Show("升级成功！恭喜你，你已解锁红色道具", 1);
                break;
        }
    }


    //特效切换指令
    public EventPatcher<Effect> SyncEffectStatus = new EventPatcher<Effect>();

    public void SyncEffect(Effect tag)
    {
        SyncEffectStatus.Send(tag);
    }

    public List<GameObject> RecallBody = new List<GameObject>();
    public void AddItem(GameObject item)
    {
        RecallBody.Add(item);
    }
    void callback()
    {
        if (RecallBody.Count > 20)
        {
            ObjectPool.GetInstance().GetObj(RecallBody[0]);
            RecallBody.RemoveAt(0);
        }
    }


    //设置窗口动画
    public WindowDotweeCtrl[] WindowType;
    public void DotweenSetting(GameObject go, bool isShow)
    {
        foreach (WindowDotweeCtrl child in WindowType)
        {
            if (child.WindowList.Contains(go))
            {
                if (isShow)
                {
                    child.OpenByDotween(go);
                }
                else
                {
                    child.CloseByDotween(go);
                }
                break;
            }
        }
    }

	public void Quite()
	{
        DataPool.GetInstance().SendDataMessage.ClearAllEevnt();
        Static.Instance.DeleteFile(Application.persistentDataPath, "jwt.txt");
        SceneManager.LoadScene("mainmeun");
    }


	    public void ClearChild(Transform parent)
	    {
		if(parent.childCount>0)
	        foreach (Transform childin in parent)
	            GameObject.Destroy(childin.gameObject);
	    }
}
