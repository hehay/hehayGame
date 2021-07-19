using System.Collections.Generic;
using Protocols.dto;

#region AccountHandler

public delegate void LoginEvent(int i);

public delegate void RegEvent(int i);

public delegate void ModifyEvent(int i);
#endregion
    #region UserHandler
    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="userDtos"></param>
    public delegate void GetRoleListEvent(List<UserDTO> userDtos );
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="i"></param>
    public delegate void CreateRoleEvent(int i);
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="i"></param>
    public delegate void DeleteRoleEvent(int i);
    /// <summary>
    /// 角色上线
    /// </summary>
    /// <param name="i"></param>
    public delegate void OnLineEvent(UserDTO i);
    public delegate void GetUserDtoEvent(UserDTO userDto);
    #endregion  
    

       #region MapHandler

public delegate void EnterSceneEvent(List<AbsRoleModel> userDtos);

public delegate void EnterSceneBROEvent(UserDTO userDto);

public delegate void LeaveSceneBROEvent(int userDtoId);

public delegate void LeaveSceneEvent();

public delegate void MoveEvent(MoveDto moveDto);

public delegate void AttackBROEvent(AttackDTO attackDto);

public delegate void SkillBROEvent(SkillAttackDTO skillAttackDto);
public delegate void DamageBROEvent(DamageDTO damageDto);

public delegate void UseInventoryEvent(InventoryItemDTO inventoryItemDto);
public delegate void UnUseInventoryEvent(InventoryItemDTO itemDto);
public delegate void TalkEvent(TalkDTO talkDto);
public delegate void TalkBROEvent(TalkDTO talkDto);

public delegate void KillEvent(UserDTO userDto);
public delegate void ReviveBROEvent(int id);
    #endregion

#region PosHandler

public delegate void GetPosEvent(PosDto posDto);


#endregion

#region SkillHandler

public delegate void GetSkillListEvent(List<SkillDTO> skillDtos);

public delegate void UpdateSkillEvent(SkillDTO skillDto);

public delegate void SkillUpEvent(SkillDTO skillDto);
#endregion


#region inventoryHandler

public delegate void GetInventoryEvent(List<InventoryItemDTO> itemDtos);

public delegate void AddInventoryEvent(InventoryItemDTO itemDto);

public delegate void DeleteInventoryEvent(InventoryItemDTO itemDto);

public delegate void UpdateInventoryEvent(InventoryItemDTO itemDto);


#endregion
public delegate void OnUserDtoChangedEvent(ChangedType changedType);
