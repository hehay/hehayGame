using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public SkillDTO SkillDto;
    [SerializeField] private Image icon;
    [SerializeField] private Image mask;
    [SerializeField] private Text skillName;
    [SerializeField] private Text info;

    [SerializeField] private Text nextlevel;
    [SerializeField]
    private Text level;
    [SerializeField] private Text range;

    [SerializeField] private Text type;

    [SerializeField] private Text cold;

    [SerializeField] private Text mp;
    [SerializeField] private GameObject button;

	// Use this for initialization
	void Awake () {
        GameData.userDtoChanged += userDtoChanged;

	}

    void OnDestroy()
    {
        GameData.userDtoChanged -= userDtoChanged;

    }
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInfo(SkillDTO skillDto)
    {
        this.SkillDto = skillDto;
        icon.sprite = Resources.Load<Sprite>("Ui/Skill/" + skillDto.SkillModelDto.icon_name);
        mask.sprite = icon.sprite;
        if (GameData.UserDto.level >= skillDto.nextLevel)
        {
            button.SetActive(true);
        }
        if (skillDto.level > 0)
        {
            mask.gameObject.SetActive(false);

        }
        skillName.text = skillDto.SkillModelDto.name;
        range.text = "技能范围：" + skillDto.dis+"米";
        nextlevel.text = "升级需玩家等级：" + skillDto.nextLevel;
        cold.text = "CD：" + skillDto.coldTime;
        mp.text = "mp：" + skillDto.mp;
        level.text = "lv：" + skillDto.level;
        switch (skillDto.SkillModelDto.applyType)
        {
                case ApplyType.Buff:
                type.text = "增强";
                break;
                case ApplyType.MultiTarget:
                type.text = "群体伤害";
                info.text = skillDto.SkillModelDto.info + GameData.UserDto.attack + "(+/" + skillDto.applyValue + "%)伤害";
                break;
                case ApplyType.Passive:
                type.text = "增益";
                break;
                case ApplyType.SingleTarget:
                type.text = "单体伤害";
                info.text = skillDto.SkillModelDto.info + GameData.UserDto.attack + "(+/" + skillDto.applyValue + "%)伤害";
                break;
        }        
    }

    public void userDtoChanged(ChangedType changedType)
    {
        if (changedType == ChangedType.LEVEL)
        {
            if (SkillDto == null || SkillDto.id < 0) return;
            if (GameData.UserDto.level >= SkillDto.nextLevel)
            {
                button.SetActive(true);
            }
        }
    }
    public void Button()
    {
        NetIO.Instance.Write(Protocol.Skill,0,SkillProtocol.SkillUp_CREQ,SkillDto.id);
        button.SetActive(false);
    }
}
