using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowOrHit : MonoBehaviour {

    public void Show(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    public void ShowDealet(GameObject Obj)
    {
        StartCoroutine(ShowDealetie(Obj));
    }
    IEnumerator ShowDealetie(GameObject Obj)
    {
        yield return new WaitForSeconds(1);
        Obj.SetActive(true);
    }

    public void Hide(GameObject Obj)
    {
        Obj.SetActive(false);
    }

    public void SettextNull(Text GetText)
    {
        GetText.text = string.Empty;
    }

    public void SettextInPutNull(InputField GetText)
    {
        GetText.text = string.Empty;
    }

	//mm

	public void StopYzm(GameObject Obj)
	{
		Obj.SetActive(false);
		StopCoroutine("Hit");
		if(ss!=null)
		ss.text = "获取验证码";
	}
    public void StopjyYzm(GameObject Obj)
    {
        Obj.SetActive(false);
        StopCoroutine("Hitjy");
        if (sss != null)
            sss.text = "获取验证码";
    }
    public void StopZCYzm(GameObject Obj)
    {
        Obj.SetActive(false);
        StopCoroutine("HitZC");
        if (s != null)
            s.text = "获取验证码";
    }

    public void ShowWaitHit(GameObject Obj)
    {
        Obj.SetActive(true);
		StartCoroutine("Hit",Obj);
    }
		
    private int Nub;
    IEnumerator Hit(GameObject obj)
    {
        Nub = 60;
        while (Nub > 0)
        {
            Nub--;
            if (ss != null)
                ss.text = Nub.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        obj.SetActive(false);
        if (ss != null)
            ss.text = "获取验证码";
    }
    private Text ss;
    public void ShowTime(Text aaa)
    {
        ss = aaa;
    }
	//mm

	//jymm
	public void ShowWaitHitjy(GameObject Obj)
	{
		Obj.SetActive(true);
		StartCoroutine("Hitjy",Obj);
	}

	private int jyNub;
	IEnumerator Hitjy(GameObject obj)
	{
		jyNub = 60;
		while (jyNub > 0)
		{
			jyNub--;
			if (sss != null)
				sss.text = jyNub.ToString() + "s";
			yield return new WaitForSeconds(1);
		}
		obj.SetActive(false);
		if (sss != null)
			sss.text = "获取验证码";
	}
	private Text sss;
	public void ShowTimejy(Text aaa)
	{
		sss = aaa;
	}
    //jymm
    public void ShowWaitHitZC(GameObject Obj)
    {
        Obj.SetActive(true);
        StartCoroutine("HitZC",Obj);
    }

    private int ZCNub;
    IEnumerator HitZC(GameObject obj)
    {
        ZCNub = 60;
        while (ZCNub > 0)
        {
            ZCNub--;
            if (s != null)
                s.text = ZCNub.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        obj.SetActive(false);
        if (s != null)
            s.text = "获取验证码";
    }
    private Text s;
    public void ShowTimeZC(Text aaa)
    {
        s = aaa;
    }


    public void ShowTimeFive(Text ttt)
    {
        StartCoroutine(hitoh(ttt));
    }

    IEnumerator hitoh(Text ttt)
    {
        yield return new WaitForSeconds(5);
        ttt.text = string.Empty;
    }


    public void SetJiFen(Text ShowJiFen)
    {
        ShowJiFen.text = Static.Instance.GetValue("jifen");
    }

    private bool open = true;
    public Sprite Open, close;

    public void SuoXiao(Transform aaa)
    {
        aaa.localScale = new Vector3(0, 0, 0);
    }

    public void ChangeText(Text showyzm)
    {
        StopAllCoroutines();
        showyzm.text = "获取验证码";
    }

	[SerializeField]
	private Text PageNubText;
	public void PageChange(int PageNub)
	{
		if (aLLlIST.transform.childCount <= 0)
			return;
		lASTpage = PageNubText.text;
		Nub = int.Parse (PageNubText.text)+PageNub;
		Nub = Nub > 0 ? Nub : 1;
		PageNubText.text = Nub.ToString ();
	}

	private string lASTpage="1";

	public void UpdatePageFL()
	{
		PageNubText.text = lASTpage;
	}

	[SerializeField]
	private Text PageNubShowText;
	public void ShowPAGE()
	{
		PageNubShowText.text = PageNubText.text;
	}


	public Text TypeValue;

	public void SetPageStart(Dropdown getD)
	{
		TypeValue.text = GetTyPEid(getD.value.ToString());
		PageNubText.text = "1";
	}

	Dictionary<string,string> DropList=new Dictionary<string,string>();
	public Dictionary<string ,string> TypeMessage=new Dictionary<string, string>();//ID,NAME
	string GetTyPEid(string Name)
	{
		string id = string.Empty;
		TypeMessage.TryGetValue (Name, out id);
		return id;
	}

	public void SetTypeMessage()
	{
		
	}
		
	public Dropdown GetD;
	public void AddOpelns(Transform AllType)
	{
		TypeMessage.Clear ();
		DropList.Clear ();
		int i = 0;

		foreach (Transform child in AllType) 
		{
			TypeMessage.Add (i.ToString(), child.GetChild (0).name);
			if (DropList.ContainsKey (child.GetChild (1).name))
				DropList.Remove (child.GetChild (1).name);
			DropList.Add (child.GetChild (1).name,child.GetChild (0).name);
			i++;
		}
		GetD.options.Clear ();
	
		List<string> allname = new List<string> ();
		foreach (string child in DropList.Keys)
			allname.Add (child);
		GetD.AddOptions (allname);

	}

	public void ClearFather(Transform AllType)
	{
		foreach (Transform child in AllType)
		{
			Destroy(child.gameObject);
		}

	}
		
	[SerializeField]
	HttpModel listSHop;
	public void Search()
	{
		if (aLLlIST.transform.childCount > 0)
			listSHop.Get ();
	}

	public Transform aLLlIST;
	public void SetAll()
	{
		foreach (Transform child in aLLlIST)
			Destroy (child.gameObject);
	}


}
