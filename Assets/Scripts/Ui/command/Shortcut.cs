using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Type
{
    None,
    Attack,
    Skill,
    Inventory
}
public class Shortcut : MonoBehaviour
{
    public int shortcutId;
    public Type type = Type.Attack;
    public float dis=3;
    public float range=60;

    public SkillDTO SkillDto;
    public InventoryItemDTO InventoryItemDto;

    [SerializeField] private Image image;
    [SerializeField] private Image mask;
    private float currentCold = 0;
    private float maxCold=0;
    public bool isCold = false;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	    if (isCold)
	    {
	        currentCold -= Time.deltaTime;
	        mask.fillAmount = currentCold/maxCold;
	        if (currentCold <= 0)
	        {
                mask.gameObject.SetActive(false);
	            currentCold = 0;
	            isCold = false;
	            GetComponent<Button>().enabled = true;
	        }
	    }
	}

    public void SetCold()
    {
        if (type == Type.Skill)
        {
            if (SkillDto != null)
            {
                mask.gameObject.SetActive(true);
                currentCold = SkillDto.coldTime;
                maxCold = SkillDto.coldTime;
                isCold = true;
                GetComponent<Button>().enabled = false;
            }
        }
        else if (type == Type.Inventory)
        {
            if (InventoryItemDto != null)
            {
                mask.gameObject.SetActive(true);
                currentCold = 10;
                maxCold = 10;
                isCold = true;
                GetComponent<Button>().enabled = false;
            }
        }
       
    }

    public void SetInfo(SkillDTO skillDto)//技能
    {

        type = Type.Skill;
        image.gameObject.SetActive(true);
        image.sprite = Resources.Load<Sprite>("Ui/Skill/" + skillDto.SkillModelDto.icon_name);
        mask.sprite = image.sprite;
        dis = skillDto.dis;
        range = skillDto.range;
        if (SkillDto != null&&SkillDto.shortcutId>0)
        {
            SkillDto.shortcutId = 0;
            NetIO.Ins.Send(Protocol.Skill, 0, SkillProtocol.Updateskill_CREQ, SkillDto);
        }
        else if (SkillDto != null && SkillDto.shortcutId > 0)
        {
            InventoryItemDto.shortcutid = 0;
            NetIO.Ins.Send(Protocol.Inventory, 0, InventoryProtocol.UpdateInventory_CREQ, InventoryItemDto);
        }
        ClearLastGridInfo(skillDto);

        skillDto.shortcutId = shortcutId;
        SkillDto = skillDto;
        NetIO.Ins.Send(Protocol.Skill, 0, SkillProtocol.Updateskill_CREQ, SkillDto);

    }

    public void SetInfo(InventoryItemDTO itemDto)//物品
    {
        type=Type.Inventory;
        image.gameObject.SetActive(true);
        image.sprite = Resources.Load<Sprite>("Ui/Inventory/" + itemDto.inventory.icon);
        mask.sprite = image.sprite;
        dis = 0;
        range = 0;
        if (SkillDto != null && SkillDto.shortcutId > 0)
        {
            SkillDto.shortcutId = 0;
            NetIO.Ins.Send(Protocol.Skill, 0, SkillProtocol.Updateskill_CREQ, SkillDto);
        }
        else if (SkillDto != null && SkillDto.shortcutId > 0)
        {
            InventoryItemDto.shortcutid = 0;
            NetIO.Ins.Send(Protocol.Inventory, 0, InventoryProtocol.UpdateInventory_CREQ, InventoryItemDto);
        }
        ClearLastGridInfo(InventoryItemDto);

        itemDto.shortcutid = shortcutId;
        InventoryItemDto = itemDto;
        NetIO.Ins.Send(Protocol.Inventory, 0, InventoryProtocol.UpdateInventory_CREQ, InventoryItemDto);
    }
    public void OnClick()
    {
        Info info = GameData.play.GetComponent<Info>();
        if(info.state==AnimState.Die||info.state==AnimState.Control)return;
        if (type == Type.Attack)
        {
            NetIO.Ins.Send(Protocol.Map,SceneManager.GetActiveScene().buildIndex,MapProtocol.Attack_CREQ,GetEmeny(dis,range));
        }
        else if (type == Type.Skill)
        {
            if (GameData.UserDto.mp >= SkillDto.mp)
            {
                SkillAttackDTO skillAttackDto = new SkillAttackDTO();
                skillAttackDto.targetsId = GetEmeny(dis, range);
                skillAttackDto.skillDto = SkillDto;
                NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Skill_CREQ, skillAttackDto);
            }
            else
            {
                WarrningManager.warringList.Add(new WarringModel("能量不足",null,2));
            }
        }
        else if (type == Type.Inventory)
        {
            switch (InventoryItemDto.inventory.infoType)
            {
                    case InfoType.Hp:
                    if (GameData.UserDto.hp == GameData.UserDto.maxHp)
                    {
                        WarrningManager.warringList.Add(new WarringModel("你已经很健康了！", null, 2));
                        return;
                    }
                    break;
                    case InfoType.Mp:
                    if (GameData.UserDto.mp == GameData.UserDto.maxMp)
                    {
                        WarrningManager.warringList.Add(new WarringModel("你的能量很充足呀！", null, 2));
                        return;
                    }
                    break;
            }

            if (InventoryItemDto.id > 0 && InventoryItemDto != null&&isCold==false)
            {
                NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.UseInventory_CREQ, InventoryItemDto.id);
                SetCold();
            }
            else
            {
                WarrningManager.warringList.Add(new WarringModel("正在冷却中...", null, 2));
            }
        }
    }

    int[] GetEmeny(float _dis,float _range)
    {
        GameObject userModel = GameData.play;
        List<GameObject> modeList = GameData.ModeList;
        List<int> modelId=new List<int>();
        modeList.Remove(userModel);
        for (int i = 0; i < modeList.Count; i++)
        {
            if (modeList[i].GetComponent<Info>().UserDto.hp <= 0)
            {
                continue;
            }
            if (Vector3.Distance(userModel.transform.position, modeList[i].transform.position) > _dis)
            {
                continue;
            }
            if (_range > 0)
            {
                if ( Vector3.Angle(userModel.transform.forward,
                        modeList[i].transform.position - userModel.transform.position) > _range)
                {
                    continue;
                }
            }
            
                modelId.Add(modeList[i].GetComponent<Info>().id);
            
        }
        return modelId.ToArray();

    }

    void ClearLastGridInfo(SkillDTO skillDto)//技能清除
    {
        Shortcut[] shortcuts = transform.parent.GetComponentsInChildren<Shortcut>();
        for (int i = 0; i < shortcuts.Length; i++)
        {
            if (shortcuts[i].SkillDto == skillDto && shortcuts[i] != this&&shortcuts[i].shortcutId>0)
            {
                shortcuts[i].Clear();
                break;
            }
        }
    }
    void ClearLastGridInfo(InventoryItemDTO itemDto)//物品清除
    {
        Shortcut[] shortcuts = transform.parent.GetComponentsInChildren<Shortcut>();
        for (int i = 0; i < shortcuts.Length; i++)
        {
            if (shortcuts[i].InventoryItemDto == itemDto && shortcuts[i] != this)
            {
                shortcuts[i].Clear();
                break;
            }
        }
    }
    public void Clear()
    {
        type = Type.None;
        dis = 0;
        range = 0;
        SkillDto = null;
        isCold = true;
        currentCold = 0;
        maxCold = 0;
        GetComponent<Button>().enabled = true;
        image.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
    }
}
