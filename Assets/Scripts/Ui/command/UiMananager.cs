using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;

public class UiMananager : MonoBehaviour
{

    public static UiMananager _instance;

    [SerializeField] private GameObject BeginPanle;
    [SerializeField] private GameObject LoginPanle;
    [SerializeField] private GameObject RegPanle;

    [SerializeField] private InputField loginAccount;
    [SerializeField] private InputField loginPassword;
    [SerializeField] public InputField regAccount;
    [SerializeField] public InputField regPassword;
    [SerializeField] private InputField regCountersignPassword;

    [SerializeField] private Button loginButton;
    [SerializeField] private Button regButton;
    private AccountHandler accountHandler;
    void Awake()
    {
        _instance = this;
        accountHandler = GameObject.Find("net").GetComponent<AccountHandler>();
        accountHandler.Login += Login;
        accountHandler.Reg += Reg;
    }

    void OnDestroy()
    {
        accountHandler.Login -= Login;
        accountHandler.Reg -= Reg;
    }

#region 服务器返回
    /// <summary>
    /// 返回登陆结果
    /// </summary>
    /// <param name="i"></param>
    void Login(int i)
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
                SceneManager.LoadScene(1);
                break;
        }
    }
    /// <summary>
    /// 返回注册结果
    /// </summary>
    /// <param name="i"></param>
    void Reg(int i)
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
                WarrningManager.warringList.Add(new WarringModel("注册成功，即将登陆", LoginGame, 2));
                RegPanle.SetActive(false);
                break;
        }
    }

    void LoginGame()
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
#endregion
    #region BeginPanle

    public void OnBeginPanleStartGameBtnClick()
    {
        BeginPanle.SetActive(false);
        LoginPanle.SetActive(true);
    }

    #endregion

    #region LoginPanle

    public void OnLoginPanleLoginBtnClick()
    {
        if (loginAccount.text.Length <= 0 || loginPassword.text.Length <= 0)
        {
                    //提示弹窗账号密码格式错误
            WarrningManager.warringList.Add(new WarringModel("账号密码格式错误",null,2));
            return;
        }
        
            // 向服务器发送登录请求
        AccountDTO accountDto = new AccountDTO
        {
            account = loginAccount.text,
            password = loginPassword.text
        };

        NetIO.Instance.Write(Protocol.Accaount, 0, AccountProtocol.Login_CREQ, accountDto);

        SetButtonState(false);
    }

    public void OnLoginPanleBackBtnClcik()
    {
        loginPassword.text=String.Empty;
        LoginPanle.SetActive(false);
        BeginPanle.SetActive(true);
        SetButtonState(true);

    }

    public void OnLoginPanleRegBtnClick()
    {
        loginPassword.text = String.Empty;
        LoginPanle.SetActive(false);
        RegPanle.SetActive(true);
        SetButtonState(true);

    }


    #endregion

    #region RegPanle

    public void OnRegPanleOkBtnClick()
    {
        if (regPassword.text != regCountersignPassword.text)
        {
            // 提示弹窗密码和确认密码不一致
            WarrningManager.warringList.Add(new WarringModel("密码和确认密码不一致", null, 2));
            return;
        }
        if (regAccount.text.Length <= 3 || regPassword.text.Length <= 3)
        {
            // 提示弹窗账号密码格式错误
            WarrningManager.warringList.Add(new WarringModel("账号密码格式错误", null, 2));
            return;
        }
        // 向服务器发送注册消息
        AccountDTO accountDto = new AccountDTO
        {
            account = regAccount.text,
            password = regPassword.text
        };

        NetIO.Instance.Write(Protocol.Accaount,0,AccountProtocol.Reg_CREQ,accountDto);
        SetButtonState(false);

    }

    public void OnRegPanleBackBtnClick()
    {
        regAccount.text = String.Empty;
        regPassword.text = String.Empty;
        regCountersignPassword.text = String.Empty;
        RegPanle.SetActive(false);
        BeginPanle.SetActive(true);
        SetButtonState(true);

    }

#endregion

    public void SetButtonState(bool isOn)
    {
        loginButton.interactable = isOn;
        regButton.interactable = isOn;
    }

}
