using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols.dto;
using Protocols;

public class Login : UIBase
 {
    public InputField username;
    public InputField password;
    public GameObject loginBtn;
    public GameObject modifyBtn;
    public GameObject regBtn;
     public override void OnEnter()
    {
        base.OnEnter();
        loginBtn.BtnAddAction(ClickLogin,SoundType.Click);
        modifyBtn.BtnAddAction(ClickModify,SoundType.Click);
    }
    public void ClickLogin() 
    {
        string account = UiMananager._instance.regAccount.text;
        string pass = UiMananager._instance.regPassword.text;
        // 向服务器发送注册消息
        AccountDTO accountDto = new AccountDTO
        {
            account = account,
            password = pass
        };
        NetIO.Instance.Write(Protocol.Accaount, 0, AccountProtocol.Login_CREQ, accountDto);

    }
    public void ClickModify() 
    {
        UIManager.Instance.OpenUI(EUITYPE.Modify);
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
