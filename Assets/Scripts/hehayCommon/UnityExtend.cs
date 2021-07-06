using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UnityExtend
{
    public static void DestoryChildren(this GameObject obj)
    {
        Debug.Log(obj.name + obj.transform.childCount);
        for (int i = obj.transform.childCount - 1; i >= 0; i--)
        {
            Debug.Log(obj.transform.GetChild(i).name);
            GameObject.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }
    }
    public static void BtnAddAction(this GameObject obj, UnityAction act,SoundType type)
    {
        /*if (obj.GetComponent<ButtonChange>())
        {
            obj.GetComponent<ButtonChange>().onClick.RemoveAllListeners();
        }
        else
        {
            obj.AddComponent<ButtonChange>();
        }
        obj.GetComponent<ButtonChange>().onClick.AddListener(act);*/
        ButtonChange bc = obj.GetComponent<ButtonChange>();
        if (bc)
        {
            BtnSound bs = obj.AddComponent<BtnSound>();
            bs.type = type;
            bc.onClick.AddListener(delegate { bs.ClickSound(bc); });
            ///obj.GetComponent<ButtonChange>().onClick.AddListener(bs.ClickSound);
            bc.onClick.AddListener(act);
            bc.onClick.AddListener(bc.Resume);
            //obj.GetComponent<ButtonChange>().sound = bs;
        }
        else
        {
            bc = obj.AddComponent<ButtonChange>();
            BtnSound bs = obj.AddComponent<BtnSound>();
            bs.type = type;
            ///obj.GetComponent<ButtonChange>().onClick.AddListener(bs.ClickSound);
            bc.onClick.AddListener(delegate { bs.ClickSound(bc); });
            bc.onClick.AddListener(act);
            //按钮回弹
            bc.onClick.AddListener(bc.Resume);
            //obj.GetComponent<ButtonChange>().sound = bs;

        }
    }
    public static void ObjAddBoxCollider(this GameObject obj, Vector3 size, string tag)
    {
        if (!obj.GetComponent<BoxCollider>())
        {
            obj.AddComponent<BoxCollider>();

        }
        obj.GetComponent<BoxCollider>().size = new Vector3(size.x, size.y, 0.1f);
        if (!string.IsNullOrEmpty(tag))
        {
            obj.tag = tag;
        }

    }
    public static long GetUTCTime(this GameObject obj, string time)
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
    public static List<Transform> GetChildren(this Transform tran)
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < tran.childCount; i++)
        {
            Transform t = tran.GetChild(i);
            children.Add(t);
        }
        return children;
    }
}
