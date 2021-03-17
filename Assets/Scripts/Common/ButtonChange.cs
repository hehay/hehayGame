using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum 样式
{
    背景显示隐藏 = 0,
    缩放 = 1,
    切换图片 = 2
}
public class ButtonChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [Header("当前UI的样式")]//直接显示汉字在面板上
    public 样式 切换样式;
    [Header("背景显示隐藏样式组")]//直接显示汉字在面板上
    public GameObject backGroundImg;
    [Header("切换图片样式组")]//直接显示汉字在面板上
    public Image 切换图片的Image;
    public Sprite normalImg;
    public Sprite highlightedImg;
   
    public void OnPointerEnter(PointerEventData eventData)
    {

        switch (切换样式)
        {
            case 样式.背景显示隐藏:
                backGroundImg.SetActive(true);
                break;
            case 样式.缩放:
                break;
            case 样式.切换图片:
                切换图片的Image.sprite = highlightedImg;
                break;
            default:
                break;
        }
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (切换样式)
        {
            case 样式.背景显示隐藏:
                backGroundImg.SetActive(false);
                break;
            case 样式.缩放:
                break;
            case 样式.切换图片:
                切换图片的Image.sprite = normalImg;
                break;
            default:
                break;
        }

    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        switch (切换样式)
        {
            case 样式.背景显示隐藏:
                backGroundImg.SetActive(false);
                break;
            case 样式.缩放:
                break;
            case 样式.切换图片:
                切换图片的Image.sprite = normalImg;
                break;
            default:
                break;
        }

    }
}
