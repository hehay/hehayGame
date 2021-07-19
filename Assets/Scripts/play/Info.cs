using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;


public class AnimState
{
    public const int Idle = 1;
    public const int  Run = 2;
     public const int Attack = 3;
    public const int  Skill1 = 4;
    public const int  Skill2 = 5;
    public const int  Skill3 = 6;
    public const int  Die = 7;
    public const int Control = 8;
}
public class Info : MonoBehaviour
{


    public int id;

    public Transform[] list;

    private AnimatorManage animatorManage;

    public int state = AnimState.Idle;
    public float speed;

    public AbsRoleModel UserDto;
    public SkillAttackDTO SkillAttackDto;

    [SerializeField] public Transform fallbloodSpawn;

    void Awake()
    {
        animatorManage = GetComponent<AnimatorManage>();

    }
    public void Attack(Transform[] _list)
    {
        list = _list;
        state = AnimState.Attack;
        SkillAttackDto = null;
        animatorManage.SetInt("state", (int)state);

    }

    public void Skill(Transform[] _list,SkillAttackDTO skillAttackDto)
    {
        list = _list;
        this.SkillAttackDto = skillAttackDto;
        switch (skillAttackDto.skillDto.SkillModelDto.aniname)
        {
            case AnimState.Skill1:
                state = AnimState.Skill1;
                break;
            case AnimState.Skill2:
                state = AnimState.Skill2;
                break;
            case AnimState.Skill3:
                state = AnimState.Skill3;
                break;
        }
        animatorManage.SetInt("state", (int)state);

    }
    public void Attacked()
    {
        state = AnimState.Idle;
        animatorManage.SetInt("state", (int)state);
        if (id != GameData.UserDto.id) return;//如果不是自己打出伤害，则返回
        // 发送伤害
        if (list.Length > 0)
        {
            List<int[]> idListList=new List<int[]>();
            List<int> idList =new List<int>();
            DamageDTO damageDto = new DamageDTO();
            damageDto.userid = id;
            if (SkillAttackDto != null&&SkillAttackDto.skillDto.id>0)
            {
                damageDto.skill = SkillAttackDto.skillDto.id;
            }
            else
            {
                damageDto.skill = -1;

            }
            for (int i = 0; i < list.Length; i++)
            {
                idList.Add(list[i].GetComponent<Info>().id); 
                idListList.Add(idList.ToArray());
            }
            damageDto.targets = idListList.ToArray();
            NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Damage_CREQ, damageDto);
        }

    }

    public void Hurted()
    {
        state = AnimState.Idle;
        animatorManage.SetInt("state", (int)state);
    }
    public void Init(int id,AbsRoleModel userDto)
    {
        this.id = id;
        state = AnimState.Idle;
        list = null;
        UserDto = userDto;
        speed = userDto.speed;
    }
}
