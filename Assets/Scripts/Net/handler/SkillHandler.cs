using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocols;
using Protocols.dto;
using UnityEngine;


    public class SkillHandler : MonoBehaviour,IHandler
    {
        public void MessageReceive(SocketModel message)
        {
            switch (message.command)
            {
                case SkillProtocol.GetskillList_SRES:
                    GetSkillList(message.GetMessage<List<SkillDTO>>());
                    break;
                case SkillProtocol.Updateskill_SRES:
                    UpdateSkill(message.GetMessage<SkillDTO>());
                    break;
                case SkillProtocol.SkillUp_SRES:
                    SkillUp(message.GetMessage<SkillDTO>());
                    break;
            }
        }

        public GetSkillListEvent GetSkillList;
        public UpdateSkillEvent UpdateSkill;
        public SkillUpEvent SkillUp;
    }

