// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：交易中心窗口组件
// 作成時間：2018-07-27
// 類を作る：Transform_BusinessCenter.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class InsGroup
{
	public Transform Parent;
	public GameObject listobj;
}

public class Transform_BusinessCenter : MonoBehaviour {

	[SerializeField]
	private ButtonChangeGroup btngroup;

	[SerializeField]
	private Button btn_guashou, btn_qiugou, btn_fabu,OpenBtn,
	btn_ok,btn_lost,btn_suremoney;

	[SerializeField]
	HttpModel http_fabu,http_qiugou,
	http_guashou_list,
	http_qiugou_list,
	http_qiugouu_pipei,
	http_guashou_pipei,
	http_guashou_listmine,
	http_qiugou_listmine,
	http_jy,
	http_dingdan,
	http_pipei,
	Http_dakuan
	;
	public Texture2D img;
	public InsGroup gs_list,jiaoyo_list,pipei_list;

	public Transform Ppwindow,gs,qg,makesure,dakuan;

	private DataDic<DataItem.gs_Data> _data;

    private void Start()
    {
		DataManager.GetDataManager.businessCenter.EventObj.Addlistener(UpdateData_GuaShou);
		DataManager.GetDataManager.businessCenterqiugou.EventObj.Addlistener(UpdateData_Qiugou);
		DataManager.GetDataManager.businessCenterqiugou.EventObj.Addlistener(Update_JiaoYi);
		//DataManager.GetDataManager.businessCenter.EventObj.Addlistener(Update_JiaoYi);
		DataManager.GetDataManager.businessMyfb.EventObj.Addlistener(Update_fabu);
		DataManager.GetDataManager.businessdingdan.EventObj.Addlistener(Update_dingdan);
		DataManager.GetDataManager.businesspipei.EventObj.Addlistener(Update_pipei);

		//************

		for (int i = 0; i < btngroup.all.Count - 1; i++) 
		{
			btn_ok.gameObject.SetActive (false);
			btn_lost.gameObject.SetActive (false);
			//btngroup.all [i].btn.onClick.RemoveAllListeners();
			if (i == 0||i==1)
			{
				btngroup.all [i].btn.onClick.AddListener (delegate() 
					{
						btn_fabu.gameObject.SetActive (true);
						btn_guashou.gameObject.SetActive (false);
						btn_qiugou.gameObject.SetActive (false);
					});

			} 

			if(i==2)
			{
				btngroup.all [i].btn.onClick.AddListener (delegate()
					{
						btn_fabu.gameObject.SetActive (false);
						btn_guashou.gameObject.SetActive (true);
						btn_qiugou.gameObject.SetActive (true);
					});
			}
			if(i==3)
			{
				btngroup.all [i].btn.onClick.AddListener (delegate()
					{
						btn_fabu.gameObject.SetActive (false);
						btn_guashou.gameObject.SetActive (false);
						btn_qiugou.gameObject.SetActive (false);
					});
			}

		}




		btngroup.all [0].btn.onClick.AddListener (delegate() 
			{
				btn_suremoney.gameObject.SetActive(false);
				Debug.Log("0");
				http_guashou_list.Data.AddData ("tp", "eth");
				http_guashou_list.Get ();
				btn_fabu.onClick.RemoveAllListeners();
				btn_fabu.onClick.AddListener (delegate() 
					{				
						GameManager.GetGameManager.OpenWindow(gs);
						Button btn=gs.GetComponentInChildren<Button>();
						btn.onClick.RemoveAllListeners();
						btn.onClick.AddListener(delegate() 
							{
								http_fabu.Get();	
								http_fabu.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage obj) 
									{
										if(obj.Code==HttpCode.SUCCESS)
										{
											http_guashou_list.Data.AddData ("tp", "eth");
											http_guashou_list.Get ();	
										}
									});
							});				
					});
			});

		btngroup.all [1].btn.onClick.AddListener (delegate() 
			{
				Debug.Log("1");
				btn_suremoney.gameObject.SetActive(false);
				http_qiugou_list.Data.AddData ("tp", "eth");
				http_qiugou_list.Get ();
				btn_fabu.onClick.RemoveAllListeners();
				btn_fabu.onClick.AddListener (delegate() 
					{
						GameManager.GetGameManager.OpenWindow(qg);
						Button btn=qg.GetComponentInChildren<Button>();
						btn.onClick.AddListener(delegate() 
							{
								http_qiugou.Get();
								http_qiugou.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage obj) 
									{
										if(obj.Code==HttpCode.SUCCESS)
										{
											http_qiugou_list.Data.AddData ("tp", "eth");
											http_qiugou_list.Get ();	
										}
									});	
							});							
					});

			});

		btngroup.all [2].btn.onClick.AddListener (delegate() 
			{
				btn_suremoney.gameObject.SetActive(false);
				//http_guashou_listmine.Data.AddData ("tp", "sb");
				http_guashou_listmine.Get();				
			});

		btngroup.all [3].btn.onClick.AddListener (delegate() 
			{
				btn_suremoney.gameObject.SetActive(false);
				http_dingdan.Get();

			});

		btngroup.all [4].btn.onClick.AddListener (delegate() 
			{
				btn_lost.gameObject.SetActive (false);
				btn_ok.gameObject.SetActive (false);
				btn_suremoney.gameObject.SetActive(false);
				StateType=0;
				http_guashou_listmine.Get();
			});

		btngroup.all [5].btn.onClick.AddListener (delegate() 
			{
				btn_lost.gameObject.SetActive (false);
				btn_ok.gameObject.SetActive (false);
				btn_suremoney.gameObject.SetActive(false);
				StateType=1;
				http_qiugou_listmine.Get();
			});
		btngroup.all [6].btn.onClick.AddListener (delegate() 
			{			
				btn_suremoney.gameObject.SetActive(false);
				btn_fabu.gameObject.SetActive (false);
				btn_guashou.gameObject.SetActive (false);
				btn_qiugou.gameObject.SetActive (false);
				btn_ok.gameObject.SetActive (false);
				btn_lost.gameObject.SetActive (false);
				StateType=1;
				http_pipei.Get();
			});
		
		//***************


		OpenBtn.onClick.RemoveAllListeners ();
		OpenBtn.onClick.AddListener (delegate() 
		{

				//btngroup.SetFist();
				btn_fabu.gameObject.SetActive (true);
				btn_guashou.gameObject.SetActive (false);
				btn_qiugou.gameObject.SetActive (false);
				btngroup.SetFist();
				//请求收购列表
				http_guashou_list.Data.AddData ("tp", "eth");
				http_guashou_list.Get ();		
				btn_suremoney.onClick.AddListener(delegate() 
				{
					GameManager.GetGameManager.OpenWindow(dakuan);
						//LoadImage.GetLoadIamge.SendImage(Static.Instance.URL+"upimage",img);
				});

		});
				
    }
		

	private int StateType=0;
	private void SetFlase()
	{
		btn_ok.gameObject.SetActive (false);
		btn_lost.gameObject.SetActive (false);
	}
		
    private void UpdateData_GuaShou(object data)
    {
		Debug.Log ("E");
		if (!gs_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (gs_list.Parent);
		Debug.Log (_data.Data.Count+"挂售");
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			
			Debug.Log (child.sellinfo);
			string strstate=child.sellinfo ["state"].ToString ();
			if (strstate == "已取消...")
				continue;
			GameObject item = ObjectPool.GetInstance().GetObj(gs_list.listobj, gs_list.Parent);
			TransformData obj = item.GetComponent<TransformData>();
			LoadImage.GetLoadIamge.Load(child.sellinfo["fromavatar"].ToString(),new RawImage[]{obj.GetObjectValue<RawImage>("head")});
			obj.GetObjectValue<Text> ("desc").text = child.sellinfo ["state"].ToString ();
			obj.GetObjectValue<Text> ("num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();
			obj.GetObjectValue<Button> ("btn").onClick.AddListener (delegate() 
			{
					http_guashou_pipei.Data.AddData("genid",child.sellinfo["id"].ToString());
					GameManager.GetGameManager.OpenWindow(Ppwindow);
					TransformData objtransform=Ppwindow.GetComponent<TransformData>();
					objtransform.GetObjectValue<Text>("state").text=child.sellinfo["state"].ToString();
					objtransform.GetObjectValue<Text>("name").text=child.sellinfo["fromname"].ToString();
					objtransform.GetObjectValue<RawImage>("head").texture=obj.GetObjectValue<RawImage> ("head").texture;
					Button btn=Ppwindow.GetComponentInChildren<Button>();
					btn.onClick.AddListener(delegate() {
						http_guashou_pipei.Get();
						http_guashou_pipei.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage obja) 
							{
								if(obja.Code==HttpCode.SUCCESS)
								{
									http_guashou_list.Data.AddData ("tp", "eth");
									http_guashou_list.Get ();	

								}
							});	
					});

			});
		}
       
    }



	private void UpdateData_Qiugou(object data)
	{
		Debug.Log ("F");
		if (!gs_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (gs_list.Parent);
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			string strstate=child.sellinfo ["state"].ToString ();
			if (strstate == "已取消...")
				continue;
			GameObject item = ObjectPool.GetInstance().GetObj(gs_list.listobj, gs_list.Parent);
			TransformData obj = item.GetComponent<TransformData>();
			LoadImage.GetLoadIamge.Load(child.sellinfo["fromavatar"].ToString(),new RawImage[]{obj.GetObjectValue<RawImage>("head")});
			obj.GetObjectValue<Text> ("desc").text = child.sellinfo ["state"].ToString ();
			obj.GetObjectValue<Text> ("num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();

			obj.GetObjectValue<Button> ("btn").onClick.AddListener (delegate() 
				{
					http_qiugouu_pipei.Data.AddData("genid",child.sellinfo["id"].ToString());
					GameManager.GetGameManager.OpenWindow(Ppwindow);
					TransformData objtransform=Ppwindow.GetComponent<TransformData>();
					objtransform.GetObjectValue<Text>("state").text=child.sellinfo["state"].ToString();
					objtransform.GetObjectValue<Text>("name").text=child.sellinfo["fromname"].ToString();
					objtransform.GetObjectValue<RawImage>("head").texture=obj.GetObjectValue<RawImage> ("head").texture;
					Button btn=Ppwindow.GetComponentInChildren<Button>();
					btn.onClick.AddListener(delegate() {
						http_qiugouu_pipei.Get();
						http_qiugouu_pipei.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage obja) 
						{
								if(obja.Code==HttpCode.SUCCESS)
								{
		
										http_qiugou_list.Data.AddData ("tp", "eth");
										http_qiugou_list.Get ();	
								}
							});	
					});

				});
		}

	}


	//我的发布
	private void Update_fabu(object data)
	{
		Debug.Log ("D");
	
		if (!jiaoyo_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (jiaoyo_list.Parent);
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			string strstate=child.sellinfo ["state"].ToString ();
			if (strstate == "已取消...")
				continue;
			GameObject item = ObjectPool.GetInstance().GetObj(jiaoyo_list.listobj, jiaoyo_list.Parent);
			TransformData obj = item.GetComponent<TransformData>();
			obj.GetObjectValue<Text> ("Num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();
			obj.GetObjectValue<Text> ("name").text = child.sellinfo ["fromname"].ToString ();
			obj.GetObjectValue<Text> ("type").text = child.sellinfo ["style"].ToString ();
			obj.GetObjectValue<Text> ("state").text = strstate;

			obj.GetObjectValue<Button> ("btn_chose").onClick.AddListener (delegate() 
				{
					Debug.Log(child.sellinfo["state"]);
					if (strstate == "挂售中..."||strstate == "求购中...") 
					{
						btn_lost.gameObject.SetActive (true);
						Debug.Log("true");

					}
					if (strstate == "匹配中...") 
					{
						if(child.sellinfo ["style"].ToString ()=="挂售")
						{
							btn_ok.gameObject.SetActive (true);


						}
						else
						{
							btn_ok.gameObject.SetActive (false);
							btn_suremoney.gameObject.SetActive(true);
							btn_suremoney.onClick.AddListener(delegate() 
							{
								Http_dakuan.Data.URL="market/lookingfordakuan";
								Http_dakuan.Data.AddData("genid",child.sellinfo ["id"].ToString ());
							});
						}
						btn_lost.gameObject.SetActive (false);
					}
					else
						btn_suremoney.gameObject.SetActive(false);
					btn_ok.onClick.RemoveAllListeners();
					btn_ok.onClick.AddListener(delegate() 
						{
							GameManager.GetGameManager.OpenWindow(makesure);
							TransformData makeobj=makesure.GetComponent<TransformData>();					

							if(child.sellinfo ["style"].ToString ()=="挂售")
							{
								http_jy.Data.URL="market/hangsellyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项挂售订单吗";
							}
							else
							{
								http_jy.Data.URL="market/lookingforyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项求购订单吗";
							}
							makeobj.GetObjectValue<Button>("btn_ok").onClick.RemoveAllListeners();
							makeobj.GetObjectValue<Button>("btn_ok").onClick.AddListener(delegate() 
								{									
									http_jy.Data.AddData("genid",child.sellinfo ["id"].ToString ());
									http_jy.Get();
									http_jy.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage o) 
										{
											if(o.Code==HttpCode.SUCCESS)
											{
												if(child.sellinfo ["style"].ToString ()=="挂售")
													http_guashou_listmine.Get();
												else
													http_qiugou_listmine.Get();
												SetFlase();
											}
										});

								});						
						});

					btn_lost.onClick.RemoveAllListeners();
					btn_lost.onClick.AddListener(delegate() 
						{
							GameManager.GetGameManager.OpenWindow(makesure);
							TransformData makeobj=makesure.GetComponent<TransformData>();					
							if(child.sellinfo ["style"].ToString ()=="挂售")
							{
								http_jy.Data.URL="market/hangsellno";
								makeobj.GetObjectValue<Text>("desc").text="您要确取消此项挂售订单吗";
							}
							else
							{
								http_jy.Data.URL="market/lookingforno";
								makeobj.GetObjectValue<Text>("desc").text="您要确取消此项求购订单吗";
							}
							makeobj.GetObjectValue<Button>("btn_ok").onClick.RemoveAllListeners();
							makeobj.GetObjectValue<Button>("btn_ok").onClick.AddListener(delegate() 
								{

									http_jy.Data.AddData("genid",child.sellinfo ["id"].ToString ());
									http_jy.Get();	
									http_jy.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage o) 
										{
											if(o.Code==HttpCode.SUCCESS)
											{
												if(o.Code==HttpCode.SUCCESS)
												{
													if(child.sellinfo ["style"].ToString ()=="挂售")
														http_guashou_listmine.Get();
													else
														http_qiugou_listmine.Get();
													SetFlase();
												}
											}
										});
								});
						});

				});
		}


	}


	//我的发布
	private void Update_JiaoYi(object data)
	{
		Debug.Log ("C");
		if (!jiaoyo_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (jiaoyo_list.Parent);
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			string strstate=child.sellinfo ["state"].ToString ();
			if (strstate == "已取消...")
				continue;
			GameObject item = ObjectPool.GetInstance().GetObj(jiaoyo_list.listobj, jiaoyo_list.Parent);
			TransformData obj = item.GetComponent<TransformData>();
			obj.GetObjectValue<Text> ("Num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();
			obj.GetObjectValue<Text> ("name").text = child.sellinfo ["fromname"].ToString ();
			obj.GetObjectValue<Text> ("type").text = child.sellinfo ["style"].ToString ();
			obj.GetObjectValue<Text> ("state").text = strstate;

			obj.GetObjectValue<Button> ("btn_chose").onClick.AddListener (delegate() 
			{
					btn_suremoney.gameObject.SetActive(false);
					Debug.Log(child.sellinfo["state"]);
					if (strstate == "挂售中..."||strstate == "求购中...") 
					{
						btn_lost.gameObject.SetActive (true);
						Debug.Log("true");

					}
					if (strstate == "匹配中...") 
					{
						if(child.sellinfo ["style"].ToString ()=="挂售")
							btn_ok.gameObject.SetActive (true);
						else
						{
							btn_ok.gameObject.SetActive (false);
							btn_suremoney.gameObject.SetActive(true);
							btn_suremoney.onClick.AddListener(delegate() 
								{
									Http_dakuan.Data.URL="market/lookingfordakuan";
									Http_dakuan.Data.AddData("genid",child.sellinfo ["id"].ToString ());
								});
						}
						btn_lost.gameObject.SetActive (false);

					}
					else
						btn_suremoney.gameObject.SetActive(false);

					btn_ok.onClick.RemoveAllListeners();
					btn_ok.onClick.AddListener(delegate() 
					{
							GameManager.GetGameManager.OpenWindow(makesure);
							TransformData makeobj=makesure.GetComponent<TransformData>();					

							if(child.sellinfo ["style"].ToString ()=="挂售")
							{
								http_jy.Data.URL="market/hangsellyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项挂售订单吗";
							}
							else
							{
								http_jy.Data.URL="market/lookingforyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项求购订单吗";
							}
							makeobj.GetObjectValue<Button>("btn_ok").onClick.RemoveAllListeners();
							makeobj.GetObjectValue<Button>("btn_ok").onClick.AddListener(delegate() 
							{									
									
								
									http_jy.Data.AddData("genid",child.sellinfo ["id"].ToString ());
									http_jy.Get();
								
							});						
					});
							
					btn_lost.onClick.RemoveAllListeners();
					btn_lost.onClick.AddListener(delegate() 
					{
							GameManager.GetGameManager.OpenWindow(makesure);
							TransformData makeobj=makesure.GetComponent<TransformData>();					
							if(child.sellinfo ["style"].ToString ()=="挂售")
							{
								http_jy.Data.URL="market/hangsellno";
								makeobj.GetObjectValue<Text>("desc").text="您要确取消此项挂售订单吗";
							}
							else
							{
								http_jy.Data.URL="market/lookingforno";
								makeobj.GetObjectValue<Text>("desc").text="您要确取消此项求购订单吗";
							}
							makeobj.GetObjectValue<Button>("btn_ok").onClick.RemoveAllListeners();
							makeobj.GetObjectValue<Button>("btn_ok").onClick.AddListener(delegate() 
							{
										
									http_jy.Data.AddData("genid",child.sellinfo ["id"].ToString ());
									http_jy.Get();	
							});
					});
				
			});
		}

		
	}



	private void Update_dingdan(object data)
	{
		Debug.Log ("B");
		if (!jiaoyo_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (jiaoyo_list.Parent);
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			string strstate = child.sellinfo ["state"].ToString ();
//			if (strstate == "已取消...")
//				continue;
			GameObject item = ObjectPool.GetInstance ().GetObj (jiaoyo_list.listobj, jiaoyo_list.Parent);
			TransformData obj = item.GetComponent<TransformData> ();
			obj.GetObjectValue<Text> ("Num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();
			obj.GetObjectValue<Text> ("name").text = child.sellinfo ["fromname"].ToString ();
			obj.GetObjectValue<Text> ("state").text = strstate;
			obj.GetObjectValue<Text> ("type").text = child.sellinfo ["style"].ToString ();
		}
	}

	private void Update_pipei(object data)
	{
		Debug.Log ("A");
		if (!jiaoyo_list.Parent.gameObject.activeInHierarchy)
			return;
		_data = data as DataDic<DataItem.gs_Data>;
		GameManager.GetGameManager.ClearChild (jiaoyo_list.Parent);
		foreach (DataItem.gs_Data child in _data.Data) 
		{
			string strstate = child.sellinfo ["state"].ToString ();
			//			if (strstate == "已取消...")
			//				continue;
			GameObject item = ObjectPool.GetInstance ().GetObj (jiaoyo_list.listobj, jiaoyo_list.Parent);
			TransformData obj = item.GetComponent<TransformData> ();
			obj.GetObjectValue<Text> ("Num").text = child.sellinfo ["num"].ToString ();
			obj.GetObjectValue<Text> ("price").text = child.sellinfo ["price"].ToString ();
			obj.GetObjectValue<Text> ("name").text = child.sellinfo ["fromname"].ToString ();
			obj.GetObjectValue<Text> ("type").text = child.sellinfo ["style"].ToString ();
			obj.GetObjectValue<Text> ("state").text = strstate;
			obj.GetObjectValue<Button> ("btn_chose").onClick.RemoveAllListeners (); 
			obj.GetObjectValue<Button> ("btn_chose").onClick.AddListener (delegate() 
			{
					btn_suremoney.gameObject.SetActive(false);
					if (strstate == "匹配中...") 
					{
						if(child.sellinfo ["style"].ToString ()=="求购")
						{
							btn_ok.gameObject.SetActive (true);
						}
						else
						{
							btn_ok.gameObject.SetActive (false);
							btn_suremoney.gameObject.SetActive(true);
							btn_suremoney.onClick.AddListener(delegate() 
								{
									Http_dakuan.Data.URL="market/hangselldakuan";
									Http_dakuan.Data.AddData("genid",child.sellinfo ["id"].ToString ());						
								});
						}
						btn_lost.gameObject.SetActive (false);
					}
					else
						btn_suremoney.gameObject.SetActive(false);

					btn_ok.onClick.RemoveAllListeners();
					btn_ok.onClick.AddListener(delegate() 
						{
							GameManager.GetGameManager.OpenWindow(makesure);
							TransformData makeobj=makesure.GetComponent<TransformData>();					

							if(child.sellinfo ["style"].ToString ()=="挂售")
							{
								http_jy.Data.URL="market/hangsellyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项挂售订单吗";
							}
							else
							{
								http_jy.Data.URL="market/lookingforyes";
								makeobj.GetObjectValue<Text>("desc").text="您要确定此项求购订单吗";
							}
							makeobj.GetObjectValue<Button>("btn_ok").onClick.RemoveAllListeners();
							makeobj.GetObjectValue<Button>("btn_ok").onClick.AddListener(delegate() 
								{									
									http_jy.Data.AddData("genid",child.sellinfo ["id"].ToString ());
									http_jy.Get();
									http_jy.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage o) 
									{
											if(o.Code==HttpCode.SUCCESS)
											{
												http_pipei.Get();
												SetFlase();
											}
									});

								});						
						});
					

				});
		}
	}
}