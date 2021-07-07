using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSound : MonoBehaviour
{
    public SoundType type;
    public void ClickSound(ButtonChange bc)
    {
        if (!bc.GetBase().handleAble) return;
        if (bc.isWorking) return;
        switch (type)
        {

            case SoundType.Click:
                SoundMgr.Instance.Play("btnClick");
                //SoundMgr.Instance.Play("act_rotate");
                break;
            case SoundType.Close:
                Debug.Log("关闭音效");
                break;
            case SoundType.ActRotate:
                SoundMgr.Instance.Play("act_rotate");
                break;
        }
    }
    private void Awake()
    {

    }
}
public enum SoundType
{
    Click,
    Close,
    ActRotate
}