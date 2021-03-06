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
    public InputField oldPassword;
    public InputField newPassword;
    public InputField confirmPassword;
    public GameObject modifyBtn;
    public GameObject concelBtn;
    private AccountHandler accountHandler;

     public override void OnEnter()
    {
        base.OnEnter();
        modifyBtn.BtnAddAction(ClickModify,SoundType.Click);
        concelBtn.BtnAddAction(ClickConcel,SoundType.Click);
        accountHandler = GameObject.Find("net").GetComponent<AccountHandler>();
        accountHandler.Modify += ModifyReceive;
     
    }
    public void ClickModify()
    {
        if (username.text.Length <= 3) 
        {
            WarrningManager.warringList.Add(new WarringModel("账号长度太短", null, 2));
            return;
        }
       
        if (oldPassword.text.Length <= 3)
        {
            
            WarrningManager.warringList.Add(new WarringModel("旧密码长度太短", null, 2));
            return;
        }
        if (newPassword.text.Length <= 3) 
        {
            WarrningManager.warringList.Add(new WarringModel("新密码长度太短", null, 2));
            return;

        }
        if (newPassword.text != confirmPassword.text) 
        {
            WarrningManager.warringList.Add(new WarringModel("密码与确认密码不一致", null, 2));
            return;
        }
        // 向服务器发送登录请求
        ModifyDTO modifyDto = new ModifyDTO
        {
            account = username.text,
            oldPassword = oldPassword.text,
            newPassword =newPassword.text
        };
        NetIO.Ins.Send(Protocol.Account, 0, AccountProtocol.Modify_CREQ, modifyDto);

    }
    public void ModifyReceive(int i) 
    {
        switch (i) 
        {
            case -1:
                WarrningManager.warringList.Add(new WarringModel("账号密码格式错误", null, 2));
                break;
            case -2:
                WarrningManager.warringList.Add(new WarringModel("没有此账号", null, 2));
                break;
            case -3:
                WarrningManager.warringList.Add(new WarringModel("修改失败", null, 2));
                break;
            case 1:
                WarrningManager.warringList.Add(new WarringModel("密码修改成功", null, 2));
                UIManager.Ins.OpenUI(EUITYPE.Login);
                UIManager.Ins.CloseUI(this);
                break;
        }
    
    }
    public void ClickConcel() 
    {
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
