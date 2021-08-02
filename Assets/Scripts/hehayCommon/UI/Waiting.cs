using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols;
using Protocols.dto;

public class Waiting : UIBase
 {
    public GameObject startMatchBtn;
    private Model model;
    private UserHandler userHandler;
    public Text countText;
    protected override void Awake()
    {
        base.Awake();
        userHandler = GameObject.Find("net").GetComponent<UserHandler>();
        userHandler.BattleReceive += BattleReceive;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        startMatchBtn.BtnAddAction(ClickStartMatch,SoundType.Click);
        if (model == Model.Single)
        {
            countText.text = "单人匹配";
        }
        else if (model == Model.Five)
        {
            countText.text = "五人匹配";
        }
    }
    public void ClickStartMatch() 
    {
        NetIO.Ins.Send(Protocol.User, 0, UserProtocol.Battle_CREQ, (int)model);
        startMatchBtn.SetActive(false);
    }
    public void BattleReceive(List<MatchDTO> players) 
    {
        UIManager.Ins.OpenUI(EUITYPE.Match,players);
        UIManager.Ins.CloseUI(this);
    }
    public override void OnResume()
    {
        base.OnResume();
    }

    public override void OnPause()
    {
       base.OnPause();
    }

    public override void OnExit()
    {
       base.OnExit();
    }
   
    
    void Init(params object[] param)
    {
        model = (Model)param[0];
    }

    public override string[] ListNotificationInterests()
    {
        return new string[]
            {
           
            };
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.name)
        {
           
        }
    }
}
