using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols.dto;
using Protocols;

public class RegUI : UIBase
 {
    public InputField username;
    public InputField password;
    public InputField confirmPassword;
    public GameObject regBtn;
    public GameObject exitBtn;
    private AccountHandler accountHandler;
     public override void OnEnter()
    {
        base.OnEnter();
        regBtn.BtnAddAction(ClickReg,SoundType.Click);
        accountHandler = GameObject.Find("net").GetComponent<AccountHandler>();
        accountHandler.Reg += RegReceive;
    }
    public void ClickReg() 
    {
        if (password.text != confirmPassword.text)
        {
            // 提示弹窗密码和确认密码不一致
            WarrningManager.warringList.Add(new WarringModel("密码和确认密码不一致", null, 2));
            return;
        }
        if (username.text.Length <= 3 || password.text.Length <= 3)
        {
            // 提示弹窗账号密码格式错误
            WarrningManager.warringList.Add(new WarringModel("账号密码格式错误", null, 2));
            return;
        }
        // 向服务器发送注册消息
        AccountDTO accountDto = new AccountDTO
        {
            account = username.text,
            password = password.text
        };

        NetIO.Ins.Send(Protocol.Account, 0, AccountProtocol.Reg_CREQ, accountDto);
    }
    public void RegReceive(int i) 
    {
        switch (i)
        {
            case -1://账号密码格式错误
                WarrningManager.warringList.Add(new WarringModel("账号密码格式错误", null, 2));
                break;
            case -2://账号已存在
                WarrningManager.warringList.Add(new WarringModel("账号已存在", null, 2));
                break;
            case 1://成功
                // 跳转登录
                WarrningManager.warringList.Add(new WarringModel("注册成功，即将登陆", null, 2));
                AccountDTO accountDto = new AccountDTO
                {
                    account = username.text,
                    password = password.text
                };
                NetIO.Ins.Send(Protocol.Account, 0, AccountProtocol.Login_CREQ, accountDto);
                UIManager.Ins.OpenUI(EUITYPE.RoleSelect);
                UIManager.Ins.CloseUI(this);
                UIManager.Ins.CloseUI(EUITYPE.StartUI);
                break;
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
