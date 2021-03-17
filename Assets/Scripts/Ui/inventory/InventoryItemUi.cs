using UnityEngine;
using System.Collections;
using Protocols.dto;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUi : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public InventoryItemDTO ItemDto;
    [SerializeField] private Image image;

    private GameObject dragedIcon;
    private RectTransform myRectTransform;

    public void SetInfo(InventoryItemDTO _itemDto)
    {
        ItemDto = _itemDto;
        image.sprite = Resources.Load<Sprite>("Ui/Inventory/" + _itemDto.inventory.icon);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //生成拖拽图片，跟添加属性和组件
        dragedIcon = new GameObject("icon");
        dragedIcon.transform.SetParent(GameObject.Find("Canvas").transform, false);
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
        //销毁图标
        if (dragedIcon != null)
        {
            Destroy(dragedIcon.gameObject);
        }

        GameObject go = eventData.pointerEnter;

        if (go != null)
        {
            if (go.tag == TAGS.ItemGrid)//拖到格子上
            {
                InventoryGridUi oldGrid = transform.parent.GetComponent<InventoryGridUi>();
                InventoryGridUi newGrid = go.GetComponent<InventoryGridUi>();
                transform.SetParent(go.transform,false);
                newGrid.SetInfo(oldGrid.inventoryItemDto);
                oldGrid.CleraInfo();
            }
            else if (go.tag == TAGS.Item && go != transform.gameObject)//拖到物品上且不是自己
            {
                InventoryItemDTO _itemDto ;
                InventoryGridUi oldGrid = transform.parent.GetComponent<InventoryGridUi>();
                InventoryGridUi newGrid = go.transform.parent.GetComponent<InventoryGridUi>();
                _itemDto = oldGrid.inventoryItemDto;
                oldGrid.SetInfo(newGrid.inventoryItemDto);
                newGrid.SetInfo(_itemDto);
            }
            else if (go.tag == TAGS.Shortcut)//快捷栏
            {
                if (ItemDto.inventory.inventoryType == InventoryType.Drug)
                {
                    go.GetComponent<Shortcut>().SetInfo(ItemDto);
                }
            }
        }
        else
        {
           
        }
    }


}
