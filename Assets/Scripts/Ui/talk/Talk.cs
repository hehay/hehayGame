using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.handler;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public static Talk _instance;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private InputField text;
    [SerializeField] private Text inputText;
    private MapHandler map;
    private int userid;
    #region Talk
    [SerializeField]
    private Text talkText;
    [SerializeField]
    private Scrollbar talkTextScrollbar;
    [SerializeField]
    private GameObject all;
    [SerializeField] private Scrollbar allScrollbar;
    private List<GameObject> allList = new List<GameObject>();

    [SerializeField] private GameObject word;
    [SerializeField] private Scrollbar wordScrollbar;
    private List<GameObject> wordList = new List<GameObject>();


    [SerializeField] private GameObject scene;
    [SerializeField] private Scrollbar sceneScrollbar;
    private List<GameObject> sceneList = new List<GameObject>();


    [SerializeField] private GameObject one;
    [SerializeField] private Scrollbar oneScrollbar;
    private List<GameObject> oneList = new List<GameObject>();


    [SerializeField] private GameObject prefab;
    public Stack<GameObject> talkPool=new Stack<GameObject>();
    #endregion

	void Awake ()
	{
	    _instance = this;
        GameObject net = GameObject.Find("net");
        map = net.GetComponent<MapHandler>();
        map.Talk += TalkSEQS;
        map.TalkBro += TalkBro;
	    transform.localScale = Vector3.zero;
	}

    void OnDestroy()
    {
        map.Talk -= TalkSEQS;
        map.TalkBro -= TalkBro;
    }

    private void TalkBro(TalkDTO talkDto)
    {
        TalkSEQS(talkDto);
    }

    private void TalkSEQS(TalkDTO talkDto)
    {
        GameObject go;
        GameObject alltemp;
        if (talkPool.Count > 0)
        {
            go = talkPool.Pop();
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(prefab);
        }
       
        switch (talkDto.talkType)
        {
            case TalkType.Word:
                go.transform.SetParent(word.transform, false);
                go.GetComponent<TalkText>().SetInfo(talkDto);
                wordList.Add(go);
                if (wordList.Count > 50)
                {
                    wordList.RemoveAt(0);
                    wordList[0].gameObject.SetActive(false);
                    wordList[0].GetComponent<TalkText>().Clear();
                    talkPool.Push(wordList[0]);
                }
                wordScrollbar.value = 0;
                talkText.text += "<color=#007DFFFF>" + "【世界】" + talkDto.userName + "说：" + talkDto.text + "</color>" + "\n";
                break;
            case TalkType.Scene:
                go.transform.SetParent(scene.transform, false);
                go.GetComponent<TalkText>().SetInfo(talkDto);
                 sceneList.Add(go);
                if (sceneList.Count > 50)
                {
                    sceneList.RemoveAt(0);
                    sceneList[0].gameObject.SetActive(false);
                    sceneList[0].GetComponent<TalkText>().Clear();
                    talkPool.Push(sceneList[0]);
                }
                sceneScrollbar.value = 0;
                talkText.text += "<color=#00FF7DFF>" + "【场景】" + talkDto.userName + "说：" + talkDto.text + "</color>" + "\n";
                break;
            case TalkType.System:
                 go.transform.SetParent(scene.transform, false);
                go.GetComponent<TalkText>().SetInfo(talkDto);
                 sceneList.Add(go);
                if (sceneList.Count > 50)
                {
                    sceneList.RemoveAt(0);
                    sceneList[0].gameObject.SetActive(false);
                    sceneList[0].GetComponent<TalkText>().Clear();
                    talkPool.Push(sceneList[0]);
                }
                sceneScrollbar.value = 0;
                talkText.text += "<color=#282828FF>" + "【系统】" + talkDto.text + "</color>" + "\n";
                break;
            case TalkType.One:
                go.transform.SetParent(one.transform, false);
                go.GetComponent<TalkText>().SetInfo(talkDto);
                    oneList.Add(go);
                if (oneList.Count > 50)
                {
                    oneList.RemoveAt(0);
                    oneList[0].gameObject.SetActive(false);
                    oneList[0].GetComponent<TalkText>().Clear();
                    talkPool.Push(oneList[0]);
                }
                oneScrollbar.value = 0;
                if (talkDto.userid == GameData.UserDto.id)
                {
                    talkText.text += "<color=#FF00FFFF>" + "【私聊】你对" + talkDto.userName + "说：" + talkDto.text + "</color>" + "\n";
                }
                else
                {
                    talkText.text += "<color=#FF00FFFF>" + "【私聊】" + talkDto.userName + "对你说：" + talkDto.text + "</color>" + "\n";
                }
                break;
        }
        //string[] talkStrings = talkText.text.Trim('\n').Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //List<string> talkList=new List<string>();
        //if (talkStrings.Length > 6)
        //{
            
        //    talkList.AddRange(talkStrings);
        //    talkList.RemoveAt(0);
        //    talkText.text = String.Empty;
        //}
        //for (int i = 0; i < talkList.Count; i++)
        //{
        //        talkText.text += talkList[i] + "\n";
        //}
        talkTextScrollbar.value = 0;

        if (talkPool.Count > 0)
        {
            alltemp = talkPool.Pop();
            alltemp.SetActive(true);
        }
        else
        {
            alltemp = Instantiate(prefab);
        }
        alltemp.transform.SetParent(all.transform, false);
        alltemp.GetComponent<TalkText>().SetInfo(talkDto);
        allList.Add(alltemp);
        if (allList.Count > 50)
        {
            allList.RemoveAt(0);
            allList[0].gameObject.SetActive(false);
            allList[0].GetComponent<TalkText>().Clear();
            talkPool.Push(allList[0]);
        }
        allScrollbar.value = 0;
    }

    public void SetInput(int value,int userid,string name)
    {
        dropdown.value = value;
        receiverid = userid;
        this.text.ActivateInputField();
    }

    public void OnDropDownChanged(int index)
    {

       type = dropdown.options[index].text;
        receiverid = -1;
        switch (type)
        {
            case "世界":
               inputText.color=new Color(0,0.5f,1,1);;
                break;
            case "场景":
                 inputText.color = new Color(0,1,0.5f,1);;
                break;
            case "私聊":
                inputText.color = Color.magenta;
                break;
        }
       
    }

    private int receiverid = -1;
    private string type = "场景";
    public void OnSendClick()
    {
        if (this.text.text.Length <= 0)
        {
            WarrningManager.warringList.Add(new WarringModel("文本不能为空！",null,2));
            return;
        }

        TalkDTO talkDto = new TalkDTO();
        talkDto.text = text.text;     
        talkDto.receiverid = -1;
        switch (type)
        {
            case "世界":
                talkDto.talkType=TalkType.Word;
                NetIO.Instance.Write(Protocol.Map, -1, MapProtocol.Talk_CREQ, talkDto);
                break;
                 case "场景":
                talkDto.talkType = TalkType.Scene;
                NetIO.Instance.Write(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Talk_CREQ,
                    talkDto);
                break;
                 case "私聊":
                if (receiverid >= 0)
                {
                    talkDto.talkType = TalkType.One;
                    talkDto.receiverid = receiverid;
                    NetIO.Instance.Write(Protocol.Map,-1, MapProtocol.Talk_CREQ,
                        talkDto);
                }
                else
                {
                    WarrningManager.warringList.Add(new WarringModel("请选择聊天对象！", null, 2));

                }
                break;
        }
        text.text = "";
        text.ActivateInputField();
    }
    public void Close()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show()
    {
        text.ActivateInputField();
        transform.localScale = Vector3.one;
        transform.SetAsLastSibling();
        allScrollbar.value = 0;
        wordScrollbar.value = 0;
        sceneScrollbar.value = 0;
        oneScrollbar.value = 0;        
    }
}
