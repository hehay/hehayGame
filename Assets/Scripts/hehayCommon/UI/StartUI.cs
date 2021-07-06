using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;

public class StartUI : UIBase
 {
    public GameObject startBtn;
    public GameObject login;
     public override void OnEnter()
    {
        base.OnEnter();
        startBtn.BtnAddAction(ClickStart, SoundType.Click);
    }
    public void ClickStart()
    {
        UIManager.Instance.OpenUI(EUITYPE.Login);
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
