using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols.dto;
using Protocols;
using UnityEngine.SceneManagement;

public class Login : UIBase
 {
    public InputField username;
    public InputField password;
    public GameObject loginBtn;
    public GameObject modifyBtn;
    public GameObject regBtn;
    private AccountHandler accountHandler;
     public override void OnEnter()
    {
        base.OnEnter();
        loginBtn.BtnAddAction(ClickLogin,SoundType.Click);
        modifyBtn.BtnAddAction(ClickModify,SoundType.Click);
        regBtn.BtnAddAction(ClickReg,SoundType.Click);
        accountHandler = GameObject.Find("net").GetComponent<AccountHandler>();
        accountHandler.Login += LoginReceive;
    }
    public void ClickLogin() 
    {
        string account = username.text;
        string pass = password.text;
        // 向服务器发送注册消息
        AccountDTO accountDto = new AccountDTO
        {
            account = account,
            password = pass
        };
        NetIO.Ins.Send(Protocol.Account, 0, AccountProtocol.Login_CREQ, accountDto);

    }
    public void ClickReg() 
    {
        UIManager.Ins.OpenUI(EUITYPE.RegUI);
        UIManager.Ins.CloseUI(this);
    
    }
    public void ClickModify() 
    {
        UIManager.Ins.OpenUI(EUITYPE.Modify);
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public void LoginReceive(int i)
    {
        switch (i)
        {
            case -1://账号密码格式错误
                WarrningManager.warringList.Add(new WarringModel("账号密码格式错误", null, 2));
                break;
            case -2://账号不存在
                WarrningManager.warringList.Add(new WarringModel("账号不存在", null, 2));
                break;
            case -3://密码不匹配
                WarrningManager.warringList.Add(new WarringModel("密码不匹配", null, 2));
                break;
            case -4://账号已登陆
                WarrningManager.warringList.Add(new WarringModel("账号已登陆", null, 2));
                break;
            case 1://成功
                // 跳转登录成功界面
                Debug.Log("登陆成功！");
                UIManager.Ins.OpenUI(EUITYPE.ModelSelect);
                UIManager.Ins.CloseUI(this);
                UIManager.Ins.CloseUI(EUITYPE.StartUI);
                break;
        }

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
