using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;
using UnityEngine.UI;

public class InventoryGridUi : MonoBehaviour
{
    public static InventoryGridUi _instance;
    public int gridId;

    public InventoryItemDTO inventoryItemDto=null;
    public int num = 0;
    [SerializeField]private Text numText;
    private Button button;
	// Use this for initialization
	void Awake ()
	{
	    _instance = this;
        numText = GetComponentInChildren<Text>();
	    button = GetComponent<Button>();
        button.onClick.AddListener(OnThisClick);
	}


    public void SetInfo(InventoryItemDTO _itemDto)
    {

        inventoryItemDto = _itemDto;
        num = _itemDto.count;
        if (num > 1)  ShowNum();
        if (_itemDto.inventoryGridId != gridId)
        {
            _itemDto.inventoryGridId = gridId;
            NetIO.Ins.Send(Protocol.Inventory,0,InventoryProtocol.UpdateInventory_CREQ,_itemDto);
        }

        InventoryItemUi itemUi = GetComponentInChildren<InventoryItemUi>();
        itemUi.SetInfo(_itemDto);
    }
    public void UpdateNum(int num )
    {
        inventoryItemDto.count = num;
        this.num = num;
        ShowNum();
    }
    void ShowNum()
    {
        numText.enabled = true;
        numText.transform.SetAsLastSibling();
        numText.text = this.num.ToString();
    }
    public void CleraInfo()
    {
        num = 0;
        inventoryItemDto = new InventoryItemDTO();
        numText.enabled = false;
        InventoryItemUi go = GetComponentInChildren<InventoryItemUi>();
        if (go != null)
        {
            go.gameObject.SetActive(false);
            Knapsack._instance.ItemprefabPool.Push(go.gameObject);
        }
      
    }


    public void OnThisClick()
    {
        if (inventoryItemDto != null && inventoryItemDto.id > 0)
        {
            switch (inventoryItemDto.inventory.inventoryType)
            {
                    case InventoryType.Equip:
                    EquipInfo._instance.SetInfo(inventoryItemDto,this,null, false);
                    break;
                default:
                    InventoryInfo._instance.SetInfo(inventoryItemDto);
                    break;
            }
        }

    }

}
