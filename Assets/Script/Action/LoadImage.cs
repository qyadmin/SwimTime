// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：加载头像类
// 作成時間：2018-07-30
// 類を作る：LoadImage.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadImage : MonoBehaviour
{

	public static LoadImage GetLoadIamge;
	void Awake()
	{
		GetLoadIamge = this;
	}
    private Dictionary<string, Texture2D> LoadedIamge = new Dictionary<string, Texture2D>();

    public void Load(string url, RawImage[] image)
    {
        Texture2D cuuretimage = null;
        bool IsGet = LoadedIamge.TryGetValue(url, out cuuretimage);
        if (IsGet)
        {
            foreach (var item in image)
            {
				if(item!=null)
                item.texture = cuuretimage;
            }
        }
        else
            StartCoroutine(GetMessage(url, image));
    }


    private IEnumerator GetMessage(string url, RawImage[] image)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            foreach (var item in image)
            {
				if (item != null)
					item.texture = www.texture;
				else
					Debug.Log ("目标RawImage已被摧毁");
            }
            if (!LoadedIamge.ContainsKey(url))
                LoadedIamge.Add(url, www.texture);
        }
        else
        {
            MessageManager._Instantiate.Show("获取头像失败");
        }
    }


	public void SendImage(string url,Texture2D img)
	{
		StartCoroutine (UploadTexture(url,img.EncodeToPNG()));
	}



	IEnumerator UploadTexture(string url,byte[] GetTex)
	{
		WWWForm form = new WWWForm();
		form.AddBinaryData ("post", GetTex);

		WWW www = new WWW(url, form);
		yield return www;
		if (www.error != null)
			print(www.error);
		else
		{
			Debug.Log (www.text);
		}
}

}
