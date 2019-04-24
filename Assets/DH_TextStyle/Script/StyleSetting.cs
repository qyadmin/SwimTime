using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "GameConfig.asset")]
public class StyleSetting :ScriptableObject  {

	public string TextMark;
	public Color FontColor=Color.black;
	public int FontSize=25;
	public Font Font;
	public bool Raycast=false;
	public Vector2 SizeData=new Vector2(200,50);
	public Vector3 PositionData=Vector3.zero;


    //控制元素
    public bool Color_Bool=true;
	public bool Font_Bool= false;
	public bool FontSize_Bool= false;
	public bool Raycast_Bool= false;
	public bool SizeData_Bool= false;
	public bool PositionData_Bool=false;
	//控制元素


	public void SetBool(bool GetColor_Bool,bool GetFont_Bool,bool GetFontSize_Bool,bool GetRaycast_Bool,bool GetSizeData_Bool,bool GetPositionData_Bool)
	{
		this.Color_Bool = GetColor_Bool;
		this.Font_Bool = GetFont_Bool;
		this.FontSize_Bool = GetFontSize_Bool;
		this.Raycast_Bool = GetRaycast_Bool;
		this.SizeData_Bool = GetSizeData_Bool;
		this.PositionData_Bool = GetPositionData_Bool;
	}


	public void SetFontStyle(Color GetColor,Font GetFont,Vector2 GetSizeData,Vector3 GetPositionData,int GetSize,bool GetRaycast )
	{
		this.FontColor = GetColor;
		this.FontSize = GetSize;
		this.Font = GetFont;
		this.Raycast = GetRaycast;
		this.SizeData = GetSizeData;
		this.PositionData = GetPositionData;
	}

	public bool OutLine=false;
	public bool Shadow=false;
	public bool Gradient=false;

	public void SetBool(bool GetOutLine,bool GetShadow,bool GetGradient)
	{
		this.OutLine = GetOutLine;
		this.Shadow = GetShadow;
		this.Gradient = GetGradient;
	}

	public Color StartColor=Color.black;
	public Color CenterColor=Color.grey;
	public Color EndColor=Color.white;

	public void SetGradientStyle(Color St,Color Cneter,Color End)
	{
		this.StartColor = St;
		this.CenterColor = Cneter;
		this.EndColor = End;
	}


	public Color ShadowStartColor=Color.black;
	public void SetShadow(Color GetColor,Vector2 Size)
	{
		this.ShadowStartColor = GetColor;
		this.ShadowEffectSize = Size;
	}

	public Color OutLineStartColor=Color.black;
	public void SetOutine(Color GetColor,Vector2 Size)
	{
		this.OutLineStartColor = GetColor;
		this.EffectSize = Size;
	}

	public Vector2 EffectSize=new Vector2(2,2);
	public Vector2 ShadowEffectSize=new Vector2(2,-2);

	public GradientType ChoseGradientType;
	public TextGroup AllGroup;
    public OutLineVector ShadowVector;
    public void SetType(GradientType ChoseGradientType, OutLineVector ShadowVector)
    {
        this.ChoseGradientType = ChoseGradientType;
        this.ShadowVector = ShadowVector;
    }
}
