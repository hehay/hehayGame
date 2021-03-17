using UnityEngine;
using System.Collections;
using Protocols.dto;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItemDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler {

    private GameObject dragedIcon;
    private RectTransform myRectTransform;
    private SkillDTO skillDto ;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.parent.parent.localScale = Vector3.zero;
        skillDto = transform.parent.GetComponent<SkillItem>().SkillDto;
        //生成拖拽图片，跟添加属性和组件
        dragedIcon = new GameObject("icon");
        dragedIcon.transform.SetParent(transform.root.transform, false);
        myRectTransform = dragedIcon.AddComponent<RectTransform>();
        dragedIcon.AddComponent<Image>();
        dragedIcon.GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
        dragedIcon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        dragedIcon.AddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 followVector;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out followVector))
        {
            myRectTransform.position = followVector;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.parent.parent.parent.localScale = Vector3.one;

        //销毁图标
        if (dragedIcon != null)
        {
            Destroy(dragedIcon.gameObject);
        }

        GameObject go = eventData.pointerEnter;
        if (go != null)
        {
            if (go.tag == TAGS.Shortcut)
            {
                go.GetComponent<Shortcut>().SetInfo(skillDto);
            }
            else if (go.tag == TAGS.ShortcutIcon)
            {
                go.GetComponentInParent<Shortcut>().SetInfo(skillDto);
            }
        }
    }

   
}
