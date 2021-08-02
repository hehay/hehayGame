using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using komal.puremvc;
using UnityEngine.UI;
using DG.Tweening;
using Protocols.dto;

public class Qualify : UIBase
 {
    private UserDTO userDt;
    private int exp;
    public Text expText;
    public Text winningStreak;
    public GameObject stars;
    public Text levelText;
    public Text danText;
    public GameObject singleBtn;
    public GameObject fiveBtn;

    protected override void Awake()
    {
        base.Awake();
        singleBtn.BtnAddAction(ClickSingle,SoundType.Click);
        fiveBtn.BtnAddAction(ClickFive,SoundType.Click);
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public void ClickSingle()
    {
        UIManager.Ins.OpenUI(EUITYPE.Waiting,0);
        UIManager.Ins.CloseUI(this);
       

    }
    public void ClickFive() 
    {
        UIManager.Ins.OpenUI(EUITYPE.Waiting,2);
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
    void SetEXP() 
    {
        expText.text = exp.ToString();
    
    }
    
    void Init(params object[] param)
    {
        userDt = (UserDTO)param[0];
        exp = userDt.exp;
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
