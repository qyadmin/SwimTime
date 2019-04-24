using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarMove : MonoBehaviour
{

    private Animator GetAnimator;
    AnimatorStateInfo info;
    private bool Islock;
    private RawImage my;
    private Vector3 scl;
    // Use this for initialization
    void Start()
    {
        my = GetComponent<RawImage>();
        GetAnimator = GetComponent<Animator>();
        GetAnimator.speed = 0;
        ChangePoint();
        scl = transform.localScale;
    }

    public void ChangePoint()
    {
        Islock = true;
        int mun = Random.Range(1, 15);
        GetAnimator.SetBool("T0" + mun.ToString(), true);
        float muns = Random.Range(0.1f, 5.0f);
        Invoke("Move", muns);
    }

    void Move()
    {
        GetAnimator.speed = 1;
        Islock = false;
    }

    private Vector3 last_pos;
    private bool IsGo = false;
    void Update()
    {
        info = GetAnimator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1.0f && !Islock)
        {
            ChangePoint();
        }
        if (last_pos != transform.position)
        {
            if (last_pos.x > transform.position.x)
            {
                IsGo = true;
                transform.localScale = new Vector3(scl.x, scl.y, scl.z);
                //Debug.Log("左");
            }
            else
            {
                transform.localScale = new Vector3(-scl.x, scl.y, scl.z);
                //Debug.Log("右");
                IsGo = false;
            }
        }
        else
        {
          //Debug.Log("STOP");
            //Debug.Log("STOP" + "放电");
        }
        last_pos = transform.position;
    }
}
