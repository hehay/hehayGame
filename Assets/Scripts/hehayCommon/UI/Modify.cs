using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols.dto;
using Protocols;

public class Modify : UIBase
 {
    public InputField username;
    public InputField password;
    public InputField confirmPassword;
    public GameObject confirmBtn;
    public GameObject concelBtn;

     public override void OnEnter()
    {
        base.OnEnter();
        confirmBtn.BtnAddAction(ClickConfirm,SoundType.Click);
     
    }
    public void ClickConfirm()
    {
        if (username.text.Length <= 3) 
        {
            WarrningManager.warringList.Add(new WarringModel("账号长度太短", null, 2));
            return;
        }
       
        if (password.text.Length <= 3)
        {
            
            WarrningManager.warringList.Add(new WarringModel("密码长度太短", null, 2));
            return;
        }
        if (password.text != confirmPassword.text) 
        {
            WarrningManager.warringList.Add(new WarringModel("密码与确认密码不一致", null, 2));
            return;
        }
        // 向服务器发送登录请求
        AccountDTO accountDto = new AccountDTO
        {
            account = username.text,
            password = password.text
        };


        NetIO.Instance.Write(Protocol.Accaount, 0, AccountProtocol.Modify_CREQ, accountDto);

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
