using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;

public class PosHandler : MonoBehaviour,IHandler {



    public void MessageReceive(SocketModel message)
    {
        switch (message.command)
        {
            case PosProtocol.GetPos_SRES:
                GetPos(message.GetMessage<PosDto>());
                break;
        }
    }


    public GetPosEvent GetPos;
}
