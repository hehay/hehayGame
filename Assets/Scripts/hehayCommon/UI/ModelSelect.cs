using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols;
using Protocols.dto;
using System;

public class ModelSelect : UIBase
 {
    public Text goldText;
    public Text timeText;
    private UserHandler userHandler;
     public override void OnEnter()
    {
        base.OnEnter();
        NetIO.Ins.Send(Protocol.User,0,UserProtocol.GetUserDt_CREQ,null);
        userHandler = GameObject.Find("net").GetComponent<UserHandler>();
        userHandler.GetUserDto += GetUserDto;
    }
    public void GetUserDto(UserDTO userData) 
    {
        goldText.text = userData.gold.ToString();
    }
    public void SetTimeText()
    {
        timeText.text = new TimeSpan(0, 0, (int)(DateTime.Today.AddHours(24) - DateTime.Now).TotalSeconds).ToString();
    }
    private void Update()
    {
        SetTimeText();
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
