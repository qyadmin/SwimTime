using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textView : MonoBehaviour {

    [SerializeField]
    Transform father;
    [SerializeField]
    Text count;

    public void resetcount(string obj)
    {
        for (int i = 0; i < father.childCount; i++)
        {
            Destroy(father.GetChild(i).gameObject);
        }

        Text newtext = Instantiate(count);
        newtext.gameObject.SetActive(true);
        newtext.text = obj;
        newtext.transform.parent = father;
        newtext.transform.localScale = new Vector3(1, 1, 1);
    }

}
