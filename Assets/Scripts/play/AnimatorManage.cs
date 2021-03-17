using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AnimatorManage:MonoBehaviour
{
    private Animator myAnim;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
    }


    public void SetInt(string name, int value)
    {
        myAnim.SetInteger(name,value);
    }

    public void SetTrigger(string name)
    {
        myAnim.SetTrigger(name);
    }

    public void SetFloat(string name, float value)
    {
        myAnim.SetFloat(name,value);
    }

    public void SetBool(string name, bool value)
    {
        myAnim.SetBool(name,value);
    }
}

