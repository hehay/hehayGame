using UnityEngine;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour {

    public static InventoryInfo _instance;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text sell;
    [SerializeField]
    private Text info;
    private InventoryItemDTO itemDto;
    void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    public void SetInfo(InventoryItemDTO itemDto)
    {
        gameObject.SetActive(true);
        EquipInfo._instance.gameObject.SetActive(false);
        this.itemDto = itemDto;
        transform.SetAsLastSibling();

        image.sprite = Resources.Load<Sprite>("Ui/Inventory/" + itemDto.inventory.icon);
        _name.text = itemDto.inventory.name;
        sell.text = "出售 " + itemDto.inventory.sell;
        switch (itemDto.inventory.infoType)
        {
            case InfoType.Hp:
                info.text = "+HP " + itemDto.inventory.applyValue+"\n" + itemDto.inventory.info;

                break;
                case InfoType.Mp:
                info.text = "+Mp " + itemDto.inventory.applyValue + "\n" + itemDto.inventory.info;

                break;
                case InfoType.Exp:
                info.text = "+Exp " + itemDto.inventory.applyValue + "\n" + itemDto.inventory.info;

                break;
        }

    }

    public void Close()
    {
        itemDto = null;
        gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (itemDto.id > 0 && itemDto != null&&CanvasManage._instance.shortcuts[itemDto.shortcutid-1].isCold==false)
        {
            NetIO.Ins.Send(Protocol.Map,SceneManager.GetActiveScene().buildIndex,MapProtocol.UseInventory_CREQ,itemDto.id);           
        }
        else
        {
            WarrningManager.warringList.Add(new WarringModel("正在冷却中...", null, 2));
        }
        gameObject.SetActive(false);
    }
}
