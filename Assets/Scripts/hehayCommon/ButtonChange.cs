using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum BtnStyle
{
    Default=0,
    HideBk,
    Scale,
    ChangeBk,
    ChangeColor
}
public enum DragState
{
    Disable,
    Ctrlable,
    Waiting,
    Moving,
    Darg,
    EndDrag,
    Rotate
}
public class ButtonChange : Button, IPointerEnterHandler, IPointerExitHandler/*, IPointerUpHandler*/
{
    protected UIBase uiBase;
    public UIBase GetBase() 
    {
        return uiBase;
    }
    [Header("当前UI的样式")]//直接显示汉字在面板上
    public BtnStyle style;
    [Header("背景显示隐藏样式组")]//直接显示汉字在面板上
    public GameObject backGroundImg;
    [Header("缩放")]
    public GameObject scaleTarget;
    public Vector3 targetSize;
    [Header("切换图片样式组")]//直接显示汉字在面板上
    public Image changedBkTarget;
    public Sprite normalImg;
    public Sprite highlightedImg;
    [Header("改变按钮颜色")]
    public Color normalColor;
    public Color highlightColor;
    public DragState state;
    public bool isWorking=false;
    public BtnSound sound;
    protected override void Awake()
    {
        base.Awake();
        FindUIBase(transform,out uiBase);
        state = DragState.Ctrlable;
        sound = GetComponent<BtnSound>();
    }
    public void FindUIBase(Transform tran,out UIBase ui)
    {
        ui = tran.GetComponentInParent<UIBase>();
        if (!ui)
        {
            Transform p = transform.parent;
            FindUIBase(p,out ui);
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!uiBase.handleAble||state!=DragState.Ctrlable) return;
        switch (style)
        {
            case BtnStyle.HideBk:
                backGroundImg.SetActive(false);
                UIManager.Ins.ShowHyalineMask(true);
                break;
            case BtnStyle.Scale:
                Debug.Log(transform.name);
                scaleTarget.transform.localScale = targetSize;
                break;
            case BtnStyle.ChangeBk:
                changedBkTarget.sprite = highlightedImg;
                break;
            case BtnStyle.ChangeColor:
                changedBkTarget.color = highlightColor;
                break;
            default:
                break;
        }
       
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!uiBase.handleAble || state != DragState.Ctrlable) return;
        switch (style)
        {
            case BtnStyle.HideBk:
                backGroundImg.SetActive(true);
                UIManager.Ins.ShowHyalineMask(false);
                break;
            case BtnStyle.Scale:
                scaleTarget.transform.localScale = Vector3.one;
                break;
            case BtnStyle.ChangeBk:
                changedBkTarget.sprite = normalImg;
                break;
            case BtnStyle.ChangeColor:
                changedBkTarget.color = normalColor;
                break;
            default:
                break;
        }

    }
    public void Resume()
    {
        switch (style)
        {
            case BtnStyle.ChangeBk:
                changedBkTarget.sprite = normalImg;
                break;
            case BtnStyle.ChangeColor:
                changedBkTarget.color = normalColor;
                break;
        }
    }
    /*void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!uiBase._handleAble || !ctrlAble) return;
        switch (style)
        {
            case BtnStyle.HideBk:
                backGroundImg.SetActive(false);
                break;
            case BtnStyle.Scale:
                scaleTarget.transform.localScale = Vector3.one;
                break;
            case BtnStyle.ChangeBk:
                changedBkTarget.sprite = normalImg;
                break;
            default:
                break;
        }

    }*/
}
