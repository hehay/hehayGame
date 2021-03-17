using UnityEngine;
using Protocols;


public class AccountHandler : MonoBehaviour ,IHandler{


    public void MessageReceive(SocketModel message)
    {
        UiMananager._instance.SetButtonState(true);
        switch (message.command)
        {
            case AccountProtocol.Login_SRES:
                int login = message.GetMessage<int>();
                Login(login);
                break;
            case AccountProtocol.Reg_SRES:
                int reg = message.GetMessage<int>();
                Reg(reg);
                break;
        }
    }

    public LoginEvent Login;
    public RegEvent Reg;
   
}
