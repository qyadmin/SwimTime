using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum TextGroup
{
	AllType,
	TypeA,
	TypeB,
	TypeC,
	TypeD,
	TypeE,
	TypeF,
	TypeG,
	TypeH
}

public enum GradientType
{
	Horizontal,
	Vertica,
	HorizontalMore
}

[System.Serializable]
public class TextContainer
{
	public TextGroup TextMark;
	//public List<Text> ContainerText=new List<Text>();

	public Color FontColor;
	public int FontSize;
	public Font Font;
	public bool Raycast;

	public void SetFontSytle(Color GetColor,Font GetFont,int GetSize,bool GetRaycast )
	{
		this.FontColor = GetColor;
		this.FontSize = GetSize;
		this.Font = GetFont;
		this.Raycast = GetRaycast;
	}

	public bool OutLine;
	public bool Shadow;
	public bool Gradient;

	public void SetBool(bool GetOutLine,bool GetShadow,bool GetGradient)
	{
		this.OutLine = GetOutLine;
		this.Shadow = GetShadow;
		this.Gradient = GetGradient;
	}

	public Color StartColor;
	public Color CenterColor;
	public Color EndColor;

	public void SetGradientSytle(Color St,Color Cneter,Color End)
	{
		this.StartColor = St;
		this.CenterColor = Cneter;
		this.EndColor = End;
	}


	public Color ShadowStartColor;
	public void SetShadow(Color GetColor,Vector2 Size)
	{
		this.ShadowStartColor = GetColor;
		this.ShadowEffectSize = Size;
	}

	public Color OutLineStartColor;
	public void SetOutine(Color GetColor,Vector2 Size)
	{
		this.OutLineStartColor = GetColor;
		this.EffectSize = Size;
	}

	public Vector2 EffectSize;
	public Vector2 ShadowEffectSize;

	public GradientType ChoseGradientType;
	public TextGroup AllGroup;
	//public OutLineVector ShadowVector;
	//public void SetType(GradientType ChoseGradientType,OutLineVector ShadowVector)
	//{
	//	this.ChoseGradientType=ChoseGradientType;
	//	this.ShadowVector = ShadowVector;
	//}

	public void SetText(List<Text> GetText)
	{
		//ContainerText = GetText;
	}

}
