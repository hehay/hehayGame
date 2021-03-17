using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols.dto;

public enum ChangedType
{
    HPMP,
    ATTACK,
    DEF,
    ARMOUR,
    CRIT,
    EXEMPTCRIT,
    SPEED,
    EXP,
    MONEY,
    LEVEL,
    All

}
public class GameData 
{



    public static UserDTO UserDto = null;//自己
    public static List<AbsRoleModel> UserDtos = new List<AbsRoleModel>();//场景存储的对象（切换场景会更新）
    public static int lastScene = 0;
    public static GameObject play = null;
    public static List<GameObject> ModeList=new List<GameObject>();
    public static OnUserDtoChangedEvent userDtoChanged;
    public static int wantLoadScene = 0;


    public static void SetHpMp(int hp=0, int mp=0)
    {
        UserDto.hp += hp;
        UserDto.mp += mp;
        if (UserDto.hp <= 0) UserDto.hp = 0;
        if (UserDto.mp <= 0) UserDto.mp = 0;
        if (UserDto.hp >= UserDto.maxHp) UserDto.hp = UserDto.maxHp;
        if (UserDto.mp >= UserDto.maxMp) UserDto.mp = UserDto.maxMp;

        userDtoChanged(ChangedType.HPMP);
    }

    public static void SavaHpMp(int hp = 0, int mp = 0)
    {
        UserDto.hp = hp;
        UserDto.mp = mp;
        userDtoChanged(ChangedType.All);
    }
    public static void SetExp(int exp)
    {
        UserDto.exp += exp;
        int total_exp = 100 +UserDto.level * 30;
        while (UserDto.exp >= total_exp)
        {
            // 升级
           UserDto.level++;
           UserDto.exp -= total_exp;
           UserDto.hp += UserDto.level * 50;
           UserDto.maxHp += UserDto.level * 50;
           UserDto.mp += UserDto.level * 25;
           UserDto.maxMp += UserDto.level * 25;
           total_exp = 100 + UserDto.level * 30;
        }
        userDtoChanged(ChangedType.EXP);
    }

    public static void SetEquips(int type, int itemId)
    {
        UserDto.equips[type] = itemId;
        userDtoChanged(ChangedType.All);
    }
}
