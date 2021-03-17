using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum Style
{
    BkHide = 0,
    Scale,
    ChangeImg,
    ChangeColor
}
public class ButtonChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [Header("CurStyle")]//直接显示汉字在面板上
    public Style style;
    [Header("BkHide")]//直接显示汉字在面板上
    public GameObject backGroundImg;
    [Header("ChangeImg")]//直接显示汉字在面板上
    public Image targetImg;
    public Sprite normalImg;
    public Sprite selectedImg;
    [Header("ChangeColor")]
    public Text targetText;
    public Color normalColor;
    public Color selectedColor;
   
    public void OnPointerEnter(PointerEventData eventData)
    {

        switch (style)
        {
            case Style.BkHide:
                backGroundImg.SetActive(true);
                break;
            case Style.Scale:
                break;
            case Style.ChangeImg:
                targetImg.sprite = selectedImg;
                break;
            case Style.ChangeColor:
                targetText.color = selectedColor;
                break;
            default:
                break;
        }
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (style)
        {
            case Style.BkHide:
                backGroundImg.SetActive(false);
                break;
            case Style.Scale:
                break;
            case Style.ChangeImg:
                targetImg.sprite = normalImg;
                break;
            case Style.ChangeColor:
                targetText.color = normalColor;
                break;
            default:
                break;
        }

    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        switch (style)
        {
            case Style.BkHide:
                backGroundImg.SetActive(false);
                break;
            case Style.Scale:
                break;
            case Style.ChangeImg:
                targetImg.sprite = normalImg;
                break;
            case Style.ChangeColor:
                targetText.color = normalColor;
                break;
            default:
                break;
        }

    }
}
