using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ProcessingData : MonoBehaviour {


	//GetMessageShopList
	public void ProcessingShop(JsonData jd)
	{
		JsonData GetData = jd ["data"];
		
		List<string> NameGround = new List<string>();
		Static.Instance.SaveShopType.Clear();


		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++)
			{
				foreach (string name in GetData[i].Keys)
				{
					NameGround.Add(name);
				}
				Dic GroundData = new Dic();
				for (int j = 0; j < GetData[i].Count; j++)
				{
					if (GetData[i][j] != null)
						GroundData.DataDic.Add(NameGround[j], GetData[i][j].ToString());
				}
				string aa = GetData[i]["id"].ToString();
				Static.Instance.AddShopList(aa, GroundData);

				NameGround.Clear();
			}
	}

	//GetMessageShopTypeList
	public void ProcessingShopType(JsonData jd)
	{
		JsonData GetData = jd ["data"];
		List<string> NameGround = new List<string>();
		Static.Instance.SaveShopType.Clear();
		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++)
			{
					foreach (string name in GetData[i].Keys)
				{
					NameGround.Add(name);
				}
				Dic GroundData = new Dic();
					for (int j = 0; j < GetData[i].Count; j++)
				{
						if (GetData[i][j] != null)
							GroundData.DataDic.Add(NameGround[j], GetData[i][j].ToString());
				}
					string aa = GetData[i]["id"].ToString();
				Static.Instance.AddShopType(aa, GroundData);

				NameGround.Clear();
			}
	}



	///TradingMessageInfo

	public void TradingMessageInfo(JsonData jd)
	{
		JsonData GetData=jd["data"];
		List<string> NameGround = new List<string> ();

		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++) {
				foreach (string name in GetData[i].Keys) {
					NameGround.Add (name);
				}
				Dic GroundData = new Dic ();
				for (int j = 0; j < GetData [i].Count; j++) {
					GroundData.DataDic.Add (NameGround [j], GetData [i] [j].ToString ());
				}
				Static.Instance.SaveTradingInfo.Add (GetData [i] ["id"].ToString (), GroundData);
				NameGround.Clear ();
			}
	}


	//GetMessageGrown
	public void GetMessageGrown(JsonData jd)
	{
		string total_sl = jd.Keys.Contains("total_sl") ? jd["total_sl"].ToString() : "";
		Static.Instance.AddValue("total_sl", total_sl);

		JsonData GetData=jd["data"];
		List<string> NameGround = new List<string> ();

		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++) {
				foreach (string name in GetData[i].Keys) {
					NameGround.Add (name);
				}
				Dic GroundData = new Dic ();

				for (int j = 0; j < GetData [i].Count; j++) {
					if (GetData [i] [j] != null)
						GroundData.DataDic.Add (NameGround [j], GetData [i] [j].ToString ());
					else
						GroundData.DataDic.Add (NameGround [j], string.Empty);
				}
				Static.Instance.SaveGrown (GetData [i] ["id"].ToString (), GroundData);
				NameGround.Clear ();
			}
	}

	//HttpMessageModel_E
	public void Processing_E(JsonData jd)
	{

		string td_num = jd.Keys.Contains("td_num") ? jd["td_num"].ToString() : "";
		string total_sl = jd.Keys.Contains("tj_num") ? jd["tj_num"].ToString() : "";
		Static.Instance.AddValue("td_num", td_num);
		Static.Instance.AddValue("tj_num", total_sl);

		JsonData GetData=jd["data"];
		List<string> NameGround = new List<string> ();
		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++) {
				foreach (string name in GetData[i].Keys) {
					NameGround.Add (name);
				}
				Dic GroundData = new Dic ();
				for (int j = 0; j <GetData [i].Count; j++) {
					GroundData.DataDic.Add (NameGround [j], GetData [i] [j].ToString ());
				}
				Static.Instance.AddFriend (GetData [i] ["bianhao"].ToString (), GroundData);
				NameGround.Clear ();
			}
	}

	//GetMessageTYD
	public void GetMessageTYD(JsonData jd)
	{
		JsonData GetData = jd ["data"];

		List<string> NameGround = new List<string> ();

		if (GetData != null)
			for (int i = 0; i < GetData.Count; i++) {
				foreach (string name in GetData[i].Keys) {
					NameGround.Add (name);
				}
				Dic GroundData = new Dic ();
				for (int j = 0; j < GetData [i].Count; j++) {
					if (GetData [i] [j] != null)
						GroundData.DataDic.Add (NameGround [j],GetData [i] [j].ToString ());
				}
				string aa = GetData[i] ["d_id"].ToString ();

				Static.Instance.AddTuDi (aa, GroundData);
				NameGround.Clear ();
			}
	}



	//GetMessageTYC
	public void GetMessageTYC(JsonData jd)
	{
//		float aaa = 0;
//		bool HaveInt=float.TryParse (Data.GetBase.msg,out aaa);
//		if(HaveInt)
//			Data.GetBase.msgInputtext.text = System.Math.Floor (aaa).ToString ();
	}

	//GetMessageTYB
	public void GetMessageTYB(JsonData jd)
	{
		//暂时没有逻辑
	}


	//GetMessageTYA
	public void GetMessageTYA(JsonData jd)
	{
		//暂时没有逻辑
	}

   


}
