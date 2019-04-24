using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddNub : MonoBehaviour {


    [SerializeField]
    private Button Add_Button;
    [SerializeField]
    private Button Dis_Button;

    public InputField Body;

    [SerializeField]
    private int AddCount;
    private int Nub;

    private void Start()
    {
        Body.text = "1";
;        Add_Button.onClick.AddListener(delegate ()
        {
            Add(AddCount);
        });

        Dis_Button.onClick.AddListener(delegate ()
        {
            Add(-AddCount);
        });
    }

    private void Add(int addnub)
	{
		if (Body.text != string.Empty&& Body.text!="0")
            Nub = int.Parse (Body.text);
		else
            Nub = 1;
        Nub = (Nub + addnub);
        Nub = Nub < 1 ? 1 : Nub;
        Body.text = Nub.ToString() ;
	}
}
