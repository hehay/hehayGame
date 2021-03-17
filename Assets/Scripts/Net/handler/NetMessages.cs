using UnityEngine;
using System.Collections;
using Assets.Scripts.handler;
using Protocols;

public class NetMessages : MonoBehaviour
{


    private IHandler account;
    private IHandler user;
    private IHandler map;
    private IHandler pos;
    private IHandler skill;
    private IHandler inventory;
	void Awake ()
	{
        account = GetComponent<AccountHandler>();
	    user = GetComponent<UserHandler>();
	    map = GetComponent<MapHandler>();
	    pos = GetComponent<PosHandler>();
	    skill = GetComponent<SkillHandler>();
	    inventory = GetComponent<InventoryHandler>();
	}
	
	void Update () {
	    while (NetIO.Instance.messages.Count>0)
	    {
	        SocketModel model = NetIO.Instance.messages[0];
            StartCoroutine("MessagesReceive", model);
	        NetIO.Instance.messages.RemoveAt(0);
	    }
	   
	}

    void MessagesReceive(SocketModel model)
    {
        switch (model.type)
        {
            case Protocol.Accaount:
                account.MessageReceive(model);
                break;
            case Protocol.User:
                user.MessageReceive(model);
                break;
            case Protocol.Map:
                map.MessageReceive(model);
                break;
            case Protocol.Pos:
                pos.MessageReceive(model);
                break;
            case Protocol.Skill:
                skill.MessageReceive(model);
                break;
            case Protocol.Inventory:
                inventory.MessageReceive(model);
                break;
        }
    }
}
