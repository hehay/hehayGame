using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols;
using Protocols.dto;

public enum Model 
{ 
    Single=0,
    Multi,
    Five
}
public class Match : UIBase
 {
    public Model model;
    public GameObject[] players;
    public Image[] masks;
  
    public GameObject confirmBtn;
    private bool hasConfirm;
    private UserHandler userHandler;
    private Color red = new Color(0.5f,0.2f,0.3f,0.2f);
    private Color blue = new Color(0.15f,0.3f,0.5f,0.2f);

    protected override void Awake()
    {
        confirmBtn.BtnAddAction(ClickConfirm,SoundType.Click);
        userHandler = GameObject.Find("net").GetComponent<UserHandler>();
        userHandler.MatchReceive += BattleReceive;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        
       
    }
    public void ClickConfirm() 
    {

        NetIO.Ins.Send(Protocol.User, 0, UserProtocol.Battle_CREQ, (int)model);

    }
    public void BattleReceive(List<MatchDTO> otherUsers) 
    {
        for (int i = 0; i < otherUsers.Count; i++)
        {
            MatchDTO matchDto = otherUsers[i];
            players[matchDto.index].SetActive(true);
            if (matchDto.index < (int)(otherUsers.Count / 2))
            {
                if (matchDto.hasConfirm)
                {
                    masks[matchDto.index].color = new Color(0.15f, 0.3f, 0.5f, 0.2f);
                }
                else
                {
                    masks[matchDto.index].color = new Color(0.15f, 0.3f, 0.5f, 0f);
                }
            }
            else 
            {
                if (matchDto.hasConfirm)
                {
                    masks[matchDto.index].color = new Color(0.5f, 0.2f, 0.3f, 0.2f);
                }
                else
                {
                    masks[matchDto.index].color = new Color(0.5f, 0.2f, 0.3f, 0f);
                }
            }
           
        }
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
        BattleReceive((List<MatchDTO>)param[0]);
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
