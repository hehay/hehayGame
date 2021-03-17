using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;

public class InventoryHandler : MonoBehaviour,IHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MessageReceive(SocketModel message)
    {
        switch (message.command)
        {
            case InventoryProtocol.GetInventory_SRES:
                GetInventory(message.GetMessage<List<InventoryItemDTO>>());
                break;
            case InventoryProtocol.AddInventory_SRES:
                AddInventory(message.GetMessage<InventoryItemDTO>());
                break;
            case InventoryProtocol.DeleteInventory_SRES:
                break;

        }
    }

    public GetInventoryEvent GetInventory;
    public AddInventoryEvent AddInventory;
    public DeleteInventoryEvent DeleteInventory;
    public UpdateInventoryEvent UpdateInventory;

}
