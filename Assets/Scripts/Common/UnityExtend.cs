using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UnityExtend
{
    public static void DestoryAllChilds(this GameObject obj)
    {
        //Debug.Log(obj.name+ obj.transform.childCount);
        for (int i = obj.transform.childCount-1; i >=  0; i--)
        {
            //Debug.Log(obj.transform.GetChild(i).name);
            GameObject.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }
    }
    public static void BtnAddListener(this GameObject obj, UnityAction act)
    {
        if (obj.GetComponent<Button>())
        {
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
            obj.AddComponent<Button>();
        }
        obj.GetComponent<Button>().onClick.AddListener(act);
    }
    public static void ObjAddBoxCollider(this GameObject obj,Vector3 size,string tag)
    {
        if (!obj.GetComponent<BoxCollider>())
        {
            obj.AddComponent<BoxCollider>();
        }
        obj.GetComponent<BoxCollider>().size =new Vector3 (size.x,size.y,0.1f);
        if(!string.IsNullOrEmpty(tag))
        {
            obj.tag = tag;
        }
       
    }
    public static long GetUTCTime(this GameObject obj,string time)
    {
        if (string.IsNullOrEmpty(time))
        {
            return (DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000;
        }
        else
        {
            DateTime dt = Convert.ToDateTime(time);
            long temp = DateTime.Now.Ticks - DateTime.UtcNow.Ticks;
            return (dt.Ticks - temp - new DateTime(1970, 1, 1).Ticks) / 10000;
        }
    }
}
