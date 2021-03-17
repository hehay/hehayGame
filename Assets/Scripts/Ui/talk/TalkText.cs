using UnityEngine;
using System.Collections;
using Protocols.dto;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{

    [SerializeField] private Text talkText;
    private int userid;
    private string username;
    public void SetInfo(TalkDTO talkDto)
    {
        userid = talkDto.userid;
        username = talkDto.userName;
        switch (talkDto.talkType)
        {
            case TalkType.Word:
                talkText.color=new Color(0,0.5f,1,1);
                talkText.text = "【世界】"+username+"说：" + talkDto.text;
                break;
            case TalkType.Scene:
                talkText.color = new Color(0,1,0.5f,1);
                talkText.text = "【场景】" + username + "说：" + talkDto.text;
                break;
            case TalkType.One:
                talkText.color = Color.magenta;
                if (userid != GameData.UserDto.id)
                {
                    talkText.text = "【私聊】" +username+"对你说："+ talkDto.text;
                }
                else
                {
                    talkText.text = "【私聊】你对" + username +"说："+  talkDto.text;
                }
                break;
            case TalkType.System:
                talkText.color = new Color((float)40 / 255, (float)40 / 255, (float)40 / 255, 1);
                talkText.text = "【系统】" + talkDto.text;
                break;

        }
        
    }

    public void Clear()
    {
        talkText.text = "";
        userid = -1;
    }
    public void Button()
    {
        if (userid != GameData.UserDto.id&&userid>=0)
        {
            Talk._instance.SetInput(2, userid, username );
        }
    }
}
