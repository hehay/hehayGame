using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;

public class UserHandler : MonoBehaviour,IHandler {


    public void MessageReceive(SocketModel message)
    {

        switch (message.command)
        {
            case UserProtocol.GetRoleList_SRES:
                GetRoleList(message.GetMessage<List<UserDTO>>());
                break;
            case UserProtocol.CreateRole_SRES:
                CreateRole(message.GetMessage<int>());
                break;
            case UserProtocol.DeleteRole_SRES:
                DeleteRole(message.GetMessage<int>());
                break;
            case UserProtocol.OnLine_SRES:
                OnLine(message.GetMessage<UserDTO>());
                break;
            case UserProtocol.GetUserDt_SRES:
                GetUserDto(message.GetMessage<UserDTO>());
                break;
            case UserProtocol.Battle_SRES:
                BattleReceive(message.GetMessage<List<MatchDTO>>());
                break;
            case UserProtocol.Match_SRES:
                MatchReceive(message.GetMessage<List<MatchDTO>>());
                break;
        }
    }

    public GetRoleListEvent GetRoleList;
    public CreateRoleEvent CreateRole;
    public DeleteRoleEvent DeleteRole;
    public OnLineEvent OnLine;
    public GetUserDtoEvent GetUserDto;
    public CallBack<List<MatchDTO>> BattleReceive;
    public CallBack<List<MatchDTO>> MatchReceive;
    public CallBack<int> StartQualify;
}
