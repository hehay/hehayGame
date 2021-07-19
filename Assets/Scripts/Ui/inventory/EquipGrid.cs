using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipGrid : MonoBehaviour
{

    public InventoryItemDTO ItemDto;
    [SerializeField] private Image image;
    private Button button;
    public EquipType EquipType;
	// Use this for initialization
	void Awake () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnThisClick);
        image.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInfo(InventoryItemDTO itemDto)
    {
        if (ItemDto.id > 0 && ItemDto != null)
        {
            ItemDto.inventoryGridId = itemDto.inventoryGridId;
            itemDto.IsDressed = false;
            NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.UnUseInventory_CREQ, ItemDto.id);
        }
        image.gameObject.SetActive(true);
        ItemDto = itemDto;
        ItemDto.inventoryGridId = -1;
        image.sprite = Resources.Load<Sprite>("Ui/Inventory/" + itemDto.inventory.icon);
        if (!itemDto.IsDressed)
        {
            NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.UseInventory_CREQ, itemDto.id);    
        }
    }


    public void OnThisClick()
    {
        if (ItemDto.id > 0 && ItemDto != null)
        {
            EquipInfo._instance.SetInfo(ItemDto,null,this,true);
        }
    }

    public void Clear()
    {
        ItemDto = new InventoryItemDTO();
        image.gameObject.SetActive(false);
    }

}
