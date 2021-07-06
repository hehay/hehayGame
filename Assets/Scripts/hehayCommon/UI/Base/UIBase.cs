using komal.puremvc;
using UnityEngine;

public abstract class UIBase : ComponentEx
{
    public EUITYPE UIType;
    public EUILEVELTYPE UILevelType;
    public EUIOUTTYPE UIOutType;
    public int SiblingIndex;
    public bool handleAble;
    protected AudioClip _btnClip;

    public virtual void OnEnter()
    {
        this.gameObject.SetActive(true);
        handleAble = true;
    }

    public virtual void OnResume()
    {
        handleAble = true;
    }

    public virtual void OnPause()
    {
        handleAble = false;
    }

    public virtual void OnExit()
    {
        handleAble = false;
    }

    protected void PlayClickAudio()
    {

    }
}
