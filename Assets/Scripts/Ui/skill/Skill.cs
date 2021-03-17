using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;

public class Skill : MonoBehaviour
{
    public static Skill _instance;

    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject prefab;
    private SkillHandler skillHandler;

	// Use this for initialization
	void Awake ()
	{
	    _instance = this;
        GameObject net = GameObject.Find("net");
	    skillHandler = net.GetComponent<SkillHandler>();
	    skillHandler.GetSkillList += GetSkillList;
	    skillHandler.SkillUp += SkillUp;
	    skillHandler.UpdateSkill += UpdateSkill;
        NetIO.Instance.Write(Protocol.Skill,0,SkillProtocol.GetskillList_CREQ,null);
        transform.localScale=Vector3.zero;
	}

    void OnDestroy()
    {
        // ReSharper disable once DelegateSubtraction
        skillHandler.GetSkillList -= GetSkillList;
        // ReSharper disable once DelegateSubtraction
        skillHandler.SkillUp -= SkillUp;
        // ReSharper disable once DelegateSubtraction
        skillHandler.UpdateSkill -= UpdateSkill;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateSkill(SkillDTO skillDto)
    {
        
    }
    public void SkillUp(SkillDTO skillDto)
    {
        SkillItem[] skillItems = grid.GetComponentsInChildren<SkillItem>();
        for (int i = 0; i < skillItems.Length; i++)
        {
            if (skillItems[i].SkillDto.id==skillDto.id)
            {
                skillItems[i].SetInfo(skillDto);
                break;
            }
        }
    }
    public void GetSkillList(List<SkillDTO> skillDtos )
    {
        for (int i = 0; i < skillDtos.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(grid.transform,false);
            go.transform.localScale=Vector3.one;
            go.transform.localPosition=Vector3.zero;
            go.GetComponent<SkillItem>().SetInfo(skillDtos[i]);
            if (skillDtos[i].shortcutId > 0)
            {
                for (int j = 0; j < CanvasManage._instance.shortcuts.Length; j++)
                {
                    if (CanvasManage._instance.shortcuts[j].shortcutId == skillDtos[i].shortcutId)
                    {
                        CanvasManage._instance.shortcuts[j].SetInfo(skillDtos[i]);
                        break;
                    }
                }               
            }
        }
    }

    public void CloseBtnClick()
    {
        transform.localScale=Vector3.zero;
    }
    public void Show()
    {
        transform.localScale = Vector3.one;
        transform.SetAsLastSibling();
    }
}
