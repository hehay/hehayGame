using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Protocols;
using Protocols.dto;
using UnityEngine.UI;

public class Knapsack : MonoBehaviour
{

    public static Knapsack _instance;
    private InventoryHandler inventoryHandler;
    #region inventoryBG
    [SerializeField]public List<InventoryGridUi> equipinventoryGridUis=new List<InventoryGridUi>();
    [SerializeField]public List<InventoryGridUi> restinventoryGridUis = new List<InventoryGridUi>();
    [SerializeField]public List<InventoryGridUi> druginventoryGridUis = new List<InventoryGridUi>();

    [SerializeField] private GameObject itemprefab;


    [SerializeField]public Stack<GameObject> ItemprefabPool =new Stack<GameObject>();
#endregion


    #region equipBG
        public List<EquipGrid> EquipGrids=new List<EquipGrid>();
    [SerializeField] private Image head;
    [SerializeField] private Text att;
    [SerializeField] private Text def;
    [SerializeField] private Text arm;
    [SerializeField] private Text crit;
    [SerializeField] private Text exeCrit;
    [SerializeField] private Text speed;
    [SerializeField] private Text level;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text expText;

    void SetHeadInfo(UserDTO userDto)
    {
        head.sprite = Resources.Load<Sprite>("Ui/Head/" + userDto.modelName);
        //int attint = 0;
        //int defint = 0; int armint = 0;
        //int critint = 0; int execritint = 0;
        //float speedint = 0;
        //for (int i = 0; i < EquipGrids.Count; i++)
        //{
        //    if (EquipGrids[i].ItemDto != null && EquipGrids[i].ItemDto.id!=0)
        //    {
        //        attint += EquipGrids[i].ItemDto.attack;
        //        defint += EquipGrids[i].ItemDto.def;
        //        armint += EquipGrids[i].ItemDto.armour;
        //        critint += EquipGrids[i].ItemDto.crit;
        //        execritint += EquipGrids[i].ItemDto.exemptCrit;
        //        speedint += EquipGrids[i].ItemDto.speed;
        //    }
        //}
        att.text = "攻击 " + (userDto.attack );
        def.text = "防御 " + (userDto.def);
        arm.text = "穿甲 " + (userDto.armour);
        crit.text = "暴击 " + (userDto.crit);
        exeCrit.text = "免暴 " + (userDto.exemptCrit);
        speed.text = "速度 " + (userDto.speed);
        level.text =  userDto.level.ToString();
        moneyText.text = userDto.money.ToString();
        int total_exp = 100 + userDto.level * 30;
        expSlider.value = (float)userDto.exp/total_exp;
        expText.text = userDto.exp+"/" + total_exp; ;
    }
    #endregion

    [SerializeField]private Text moneyText;
    void Awake ()
    {

        _instance = this;
        GameData.userDtoChanged += userDtoChanged;

        GameObject net = GameObject.Find("net");
        inventoryHandler = net.GetComponent<InventoryHandler>();
        inventoryHandler.GetInventory += GetInventory;
        inventoryHandler.AddInventory += AddInventory;
        inventoryHandler.DeleteInventory += DeleteInventory;
        inventoryHandler.UpdateInventory += UpdateInventory;
        transform.localScale=Vector3.zero;
        NetIO.Ins.Send(Protocol.Inventory,0,InventoryProtocol.GetInventory_CREQ,null);
        if (GameData.UserDto != null)
        {
            SetHeadInfo(GameData.UserDto);
        }
	}



    void OnDestroy()
    {
        GameData.userDtoChanged -= userDtoChanged;

        inventoryHandler.GetInventory -= GetInventory;
        inventoryHandler.AddInventory -= AddInventory;
        inventoryHandler.DeleteInventory -= DeleteInventory;
        inventoryHandler.UpdateInventory -= UpdateInventory;

    }

    public void userDtoChanged(ChangedType changedType)
    {
        if (changedType == ChangedType.ATTACK || changedType == ChangedType.EXP || changedType == ChangedType.LEVEL || changedType == ChangedType.DEF || changedType == ChangedType.ARMOUR || changedType == ChangedType.CRIT || changedType == ChangedType.EXEMPTCRIT || changedType == ChangedType.SPEED || changedType == ChangedType.MONEY || changedType == ChangedType.All)
        {
            SetHeadInfo(GameData.UserDto);
        }
    }

    
    private void GetInventory(List<InventoryItemDTO> itemdtos)
    {
        if (itemdtos==null) return;
        for (int i = 0; i < itemdtos.Count; i++)
        {
            InventoryItemDTO itemDto = itemdtos[i];
            if (itemDto.inventory.inventoryType == InventoryType.Equip)
            {
                if (GameData.UserDto.equips.Contains(itemDto.id))
                {
                    // 穿上装备
                    for (int j = 0; j < EquipGrids.Count; j++)
                    {
                        if (EquipGrids[j].EquipType == itemDto.inventory.equipType && itemDto.IsDressed)
                        {
                            EquipGrids[j].SetInfo(itemDto);
                            break;
                        }
                    }
                }
                else
                {
                    GetItemDto(equipinventoryGridUis, itemDto);
                }
            }
            else if (itemDto.inventory.inventoryType == InventoryType.Drug)
            {
                if (itemDto.shortcutid > 0)
                {
                    for (int j = 0; j < CanvasManage._instance.shortcuts.Length; j++)
                    {
                        if (CanvasManage._instance.shortcuts[j].shortcutId == itemDto.shortcutid)
                        {
                            CanvasManage._instance.shortcuts[j].SetInfo(itemDto);
                            break;
                        }
                    }           
                }
                GetItemDto(druginventoryGridUis, itemDto);
            }
            else
            {
                GetItemDto(restinventoryGridUis, itemDto);
            }
           
        }
    }

    void GetItemDto(List<InventoryGridUi> list,InventoryItemDTO itemDto)
    {
        for (int j = 0; j < list.Count; j++)
        {
            if (list[j].gridId == itemDto.inventoryGridId)
            {
                GameObject prefab;
                if (ItemprefabPool.Count > 0)
                {
                    prefab = ItemprefabPool.Pop();
                    prefab.gameObject.SetActive(true);
                }
                else
                {
                    prefab = Instantiate(itemprefab);
                }
                prefab.transform.SetParent(list[j].gameObject.transform, false);
                prefab.transform.localScale = Vector3.one;
                prefab.transform.localPosition = Vector3.zero;
                list[j].SetInfo(itemDto);
            }
        }
    }

    void DeleteInventory(InventoryItemDTO itemDto)
    {
        
    }

    void UpdateInventory(InventoryItemDTO itemDto)
    {
        
    }
    void AddItemDto(List<InventoryGridUi> list, InventoryItemDTO itemDto, InventoryGridUi gridUi)
    {

        for (int i = 0; i < list.Count; i++)
        {

            if (list[i].num == 0 || list[i].inventoryItemDto==null)
            {            

                gridUi = list[i];
                break;
            }
        }
        if (gridUi != null)
        {

            GameObject prefab;
            if (ItemprefabPool.Count > 0)
            {
                prefab = ItemprefabPool.Pop();
                prefab.gameObject.SetActive(true);
            }
            else
            {
                prefab = Instantiate(itemprefab);
            }
            prefab.transform.SetParent(gridUi.gameObject.transform, false);
            prefab.transform.localScale = Vector3.one;
            prefab.transform.localPosition = Vector3.zero;
            gridUi.SetInfo(itemDto);
        }
        else
        {
            WarrningManager.warringList.Add(new WarringModel("背包已满！", null, 2));
        }
    }
    /// <summary>
    /// 服务器返回增加物品信息
    /// </summary>
    /// <param name="itemDto"></param>
    public void AddInventory(InventoryItemDTO itemDto)//增加物品
    {
        InventoryGridUi gridUi = null;

        if (itemDto.inventory.inventoryType == InventoryType.Equip)
        {
            AddItemDto(equipinventoryGridUis,itemDto,gridUi);
            
        }
        else if (itemDto.inventory.inventoryType == InventoryType.Drug)
        {

            for (int i = 0; i < druginventoryGridUis.Count; i++)
            {
                if (druginventoryGridUis[i].inventoryItemDto.id == itemDto.id)
                {
                    gridUi = druginventoryGridUis[i];
                    break;
                }
            }
            if (gridUi != null)
            {
               gridUi.UpdateNum(itemDto.count);
            }
            else
            {
                AddItemDto(druginventoryGridUis,itemDto,gridUi);
            }
        }
        else
        {
            for (int i = 0; i < restinventoryGridUis.Count; i++)
            {
                if (restinventoryGridUis[i].inventoryItemDto.id == itemDto.id)
                {
                    gridUi = restinventoryGridUis[i];
                    break;
                }
            }
            if (gridUi != null)
            {
                gridUi.UpdateNum(itemDto.count);
            }
            else
            {
                AddItemDto(restinventoryGridUis, itemDto, gridUi);
            }
        }
    }


    public void Close()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show()
    {
        transform.localScale = Vector3.one;
        transform.SetAsLastSibling();
    }
	void Update () {
	    if (Input.GetMouseButtonDown(1))
	    {
	        NetIO.Ins.Send(Protocol.Inventory,0,InventoryProtocol.AddInventory_CREQ,2008);
	    }
	}
}
