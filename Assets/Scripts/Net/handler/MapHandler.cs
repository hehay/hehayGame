using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocols;
using Protocols.dto;
using UnityEngine;

namespace Assets.Scripts.handler
{
    public class MapHandler : MonoBehaviour,IHandler
    {
        public void MessageReceive(SocketModel message)
        {

            switch (message.command)
            {
                case MapProtocol.EnterMap_SRES://自己进入场景,获取进入场景的用户角色
                    EnterScene(message.GetMessage<List<AbsRoleModel>>());
                    break;
                case MapProtocol.EnterMap_BRO://有别的玩家进来
                    EnterSceneBRO(message.GetMessage<UserDTO>());
                    break;
                case MapProtocol.LeaveMap_SRES://自己离开场景
                    LeaveScene();
                    break;
                case MapProtocol.LeaveMap_BRO://别人离开场景
                    LeaveSceneBRO(message.GetMessage<int>());
                    break;
                case MapProtocol.Move_BRO:
                    Move(message.GetMessage<MoveDto>());
                    break;
                case MapProtocol.Attack_BRO:
                    AttackBro(message.GetMessage<AttackDTO>());
                    break;
                case MapProtocol.Skill_BRO:
                    SkillBro(message.GetMessage<SkillAttackDTO>());
                    break;
                case MapProtocol.Damage_BRO:
                    DamageBro(message.GetMessage<DamageDTO>());
                    break;
                case MapProtocol.UseInventory_BRO:
                    UseInventory(message.GetMessage<InventoryItemDTO>());
                    break;
                case MapProtocol.UnUseInventory_SRES:
                    UnUseInventory(message.GetMessage<InventoryItemDTO>());
                    break;
                case MapProtocol.Talk_SRES:
                    Talk(message.GetMessage<TalkDTO>());
                    break;
                case MapProtocol.Talk_BRO:
                    Talk(message.GetMessage<TalkDTO>());
                    break;
                case MapProtocol.Revive_BRO:
                    ReviveBro(message.GetMessage<int>());
                    break;
                case MapProtocol.Killraward_BRO:
                    Kill(message.GetMessage<UserDTO>());
                    break;
            }
        }

        public EnterSceneEvent EnterScene;
        public LeaveSceneEvent LeaveScene;
        public EnterSceneBROEvent EnterSceneBRO;
        public LeaveSceneBROEvent LeaveSceneBRO;
        public MoveEvent Move;
        public AttackBROEvent AttackBro;
        public SkillBROEvent SkillBro;
        public DamageBROEvent DamageBro;
        public UseInventoryEvent UseInventory;
        public UnUseInventoryEvent UnUseInventory;
        public TalkEvent Talk;
        public TalkBROEvent TalkBro;
        public ReviveBROEvent ReviveBro;
        public KillEvent Kill;
    }
}
