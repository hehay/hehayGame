using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipInfo : MonoBehaviour
{

    public static EquipInfo _instance;
    [SerializeField] private Image image;
    [SerializeField] private Text _name;
    [SerializeField] private Text state;
    [SerializeField] private Text att;
    [SerializeField] private Text def;
    [SerializeField] private Text arm;
    [SerializeField] private Text crit;
    [SerializeField] private Text exeCrit;
    [SerializeField] private Text speed;
    [SerializeField] private Text sell;
    [SerializeField] private Text info;
    [SerializeField] private Text starLevel;
    [SerializeField] private Text quality;
    private InventoryItemDTO itemDto;
    [SerializeField] private Text buttonText;
    private InventoryGridUi gridUi;
    private EquipGrid equipGrid;
	void Awake ()
	{
	    _instance = this;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInfo(InventoryItemDTO itemDto,InventoryGridUi gridUi,EquipGrid equipGrid,bool isEquip=false)
    {
        gameObject.SetActive(true);
        InventoryInfo._instance.gameObject.SetActive(false);
        transform.SetAsLastSibling();
        this.itemDto = itemDto;
        image.sprite = Resources.Load<Sprite>("Ui/Inventory/" + itemDto.inventory.icon);
        _name.text = itemDto.inventory.name;
        if (isEquip)
        {
            this.equipGrid = equipGrid;
            state.text = "【已装备】";
            buttonText.text = "卸下";
        }
         else
        {
            this.gridUi = gridUi;
            state.text = "【未装备】";
            buttonText.text = "装备";

        }
        att.text = "攻击+"+itemDto.attack;
        def.text = "防御+" + itemDto.def;

        arm.text = "穿甲+" + itemDto.armour;

        exeCrit.text = "免暴+" + itemDto.exemptCrit;

        crit.text = "暴击+" + itemDto.crit;

        speed.text = "速度+" + itemDto.speed;
        sell.text = "出售 " + itemDto.inventory.sell;
        quality.text = "品质 " + itemDto.quality;
        starLevel.text = "星级 " + itemDto.starLevel;
        info.text = itemDto.inventory.info;

    }

    public void Close()
    {
        itemDto = null;
        gameObject.SetActive(false);

    }

    public void OnButtonClick()
    {
        if (itemDto.id > 0 && itemDto != null)
        {
            if (buttonText.text == "装备")
            {
                for (int i = 0; i < Knapsack._instance.EquipGrids.Count; i++)
                {
                    if (Knapsack._instance.EquipGrids[i].EquipType == itemDto.inventory.equipType)
                    {
                        Knapsack._instance.EquipGrids[i].SetInfo(itemDto);
                        gridUi.CleraInfo();
                        break;
                    }
                }

            }
            else
            {
                bool isFull = false;
                for (int i = 0; i < Knapsack._instance.equipinventoryGridUis.Count; i++)
                {
                    if (Knapsack._instance.equipinventoryGridUis[i].num <= 0)
                    {
                        isFull = true;
                        break;
                    }
                }

                if (isFull)
                {
                    NetIO.Instance.Write(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.UnUseInventory_CREQ, itemDto.id);
                    equipGrid.Clear();
                    GameData.SetEquips((int)itemDto.inventory.equipType,0);
                }
                else
                {
                    WarrningManager.warringList.Add(new WarringModel("背包已满！", null, 2));
                }

            }
            gameObject.SetActive(false);

        }
    }

}
