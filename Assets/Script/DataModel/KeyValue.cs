using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;

public class EncryptDecipherTool {

	private static string key = "ABCDEFGHABCDEFGHABCDEFGHABCDEFGH";
	//private static string keyLock = "!@#$%^&*$#";//@#$$#%^&*//@#$%^&*$#//&*$#@#$%^

	public static string GetList(string AAAA,bool Ifmake)
	{
		//return	Encrypt (AAAA, key);
		return AAAA;
	}

	public static string GetListSS(string AAAA)
	{
		//return Encrypt (AAAA, key);
		return AAAA;
	}



	public static string GetListOld(string AAAA,bool Ifmake)
	{
//		if (Ifmake)
//			return Encrypt (AAAA, key);
//		else
			return AAAA;
	}


	public static string Encrypt(string content)
	{
		return Encrypt(content, key);
	}

	//加密
	public static string Encrypt(string content, string k )
	{
		byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(k);
		RijndaelManaged rm = new RijndaelManaged();
		rm.Key = keyBytes;
		rm.Mode = CipherMode.ECB;
		rm.Padding = PaddingMode.PKCS7;
		ICryptoTransform ict = rm.CreateEncryptor();
		byte[] contentBytes = UTF8Encoding.UTF8.GetBytes(content);
		byte[] resultBytes = ict.TransformFinalBlock(contentBytes, 0, contentBytes.Length);

		string AAA=Convert.ToBase64String(resultBytes, 0, resultBytes.Length);
		Debug.Log(Decipher (AAA,key));
		return AAA;
	}

	public static string InsertFormat(string input, int interval, string value)  
	{  
		for (int i = interval; i < input.Length; i += interval + 1)  
			input = input.Insert(i, value);  
		return input;  
	} 
		
	public string GetValueGo(char getv)
	{
		string newv = null;
		int a = ((int)getv) % 3;
			newv+=key[a];
		 a = ((int)getv) % 5;
		newv+=key[a];
		a = ((int)getv) % 7;
		newv+=key[a];
		a = ((int)getv) % 9;
		newv+=key[a];

		return newv;
	}


//	public static string Decipher(string content)
//	{
//		return Decipher(content, key);
//	}

	//解密
	public static string Decipher(string content, string k)
	{
		byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(k);
		RijndaelManaged rm = new RijndaelManaged();
		rm.Key = keyBytes;
		rm.Mode = CipherMode.ECB;
		rm.Padding = PaddingMode.PKCS7;
		ICryptoTransform ict = rm.CreateDecryptor();
		byte[] contentBytes = Convert.FromBase64String(content);
		byte[] resultBytes = ict.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
		return UTF8Encoding.UTF8.GetString(resultBytes);
	}






	private static string strKey{get{ return Static.Instance.SetData.KeyValue;}}

	//MD5加密

	public static string UserMd5()
	{
//		string time =UnityEngine.Random.Range(0,10000).ToString();
//		string cl = strKey+time;
//		Debug.Log (cl);
//		string pwd = "";
//		MD5 md5 = MD5.Create();//实例化一个md5对像
//		// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
//		byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
//		// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
//		for (int i = 0; i < s.Length; i++)
//		{
//			// 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
//			pwd = pwd + s[i].ToString("x");
//		}
//		string Md5OK = "token" + "=" + pwd + "&" + "time" + "=" + time;
//		return Md5OK;

		string time =UnityEngine.Random.Range(0,10000).ToString();
		//string time =AAA;
		Debug.Log(time);
		string input = strKey+time;
		System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();  
		byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);  
		byte[] hash = md5.ComputeHash(inputBytes);  
		StringBuilder sb = new StringBuilder();  
		for (int i = 0; i < hash.Length; i++)  
		{  
			sb.Append(hash[i].ToString("x2"));//大  "X2",小"x2"    
		} 
		string Md5OK = "token" + "=" + sb.ToString() + "&" + "time" + "=" + time;
		return Md5OK;  
	}


    //public static void UseJWT()
    //{
    //    var payload = new Dictionary<string, object>
    //    {
    //       { "claim1", 0 },
    //       { "claim2", "claim2-value" }
    //    };
    //    var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

    //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
    //    IJsonSerializer serializer = new JsonNetSerializer();
    //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
    //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

    //    var token = encoder.Encode(payload, secret);
    //    Console.WriteLine(token);

    //}

}
