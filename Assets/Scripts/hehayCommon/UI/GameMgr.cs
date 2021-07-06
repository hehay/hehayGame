using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataScript;
using komal.puremvc;

using UnityEngine;

public class GameMgr : ComponentEx
{
    public static GameMgr Instance;

   

    public GameDt gameDt;
    public int id = 1;
    public bool isNewDay=false;
    public string name = null;
    
    
    private int highestScore = 0;
    public List<DayInfo> dayInfos = new List<DayInfo>();
    private int curRanking;
    public int CurRanking
    {
        get { return curRanking; }
        set { curRanking = value; }
    }
    public void RankingUP()
    {
        CurRanking = curRanking - 23;
    }
    public int HighestScore
    {
        get { return highestScore; }
        set
        {
            highestScore = value;
            facade.SendNotification("HighestScore", highestScore);
        }
    }
    
    private int rotCount = 0;
    public int RotCount
    {
        get { return rotCount; }
        set
        {
            rotCount = value;
            facade.SendNotification("RotCount", rotCount);
            
        }
    }
    public void AddRotCount(int count)
    {
        RotCount = rotCount + count;
    }
    public IEnumerator AddKeyCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            KeyCount = keyCount + 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
    

    public int keyCount = 0;
    public int KeyCount
    {
        get { return keyCount; }
        set
        {
            keyCount = value;
            facade.SendNotification("KeyCount", keyCount);
        }
    }
    public void ReduceKeyCount()
    {
        KeyCount = keyCount - 100;
    }
    private int exchangeBoxCount;
    public int ExChangeBoxCount
    {
        get { return exchangeBoxCount; }
        set
        {
            exchangeBoxCount = value;
            facade.SendNotification("ExchangeBoxCount", exchangeBoxCount);
        }
    }
    public void ReduceExchangeBoxCount()
    {
        exchangeBoxCount--;
        ExChangeBoxCount = exchangeBoxCount;
    }
    private int freeCount;
    public int FreeCount
    {
        get { return freeCount; }
        set
        {
            freeCount = value;
            facade.SendNotification("FreeCount", freeCount);
        }
    }
    public void ReduceFreeCount()
    {
        freeCount--;
        FreeCount = freeCount;
    }
    //上一次免费获得旋转币的时间
    public DateTime lastGetFreeTime;
    private bool canGetFree = false;
    public bool CanGetFree
    {
        get { return canGetFree; }
        set {
            canGetFree = value;
            facade.SendNotification("CanGetFree",canGetFree);
        }
    }
    private int[] uiModel = { 0, 0 };
    public int[] UIModel
    {
        set
        {
            uiModel = value;
            object[] param = { gameDt.UIModel[0], gameDt.UIModel[1] };
            facade.SendNotification("UIModel", param);
        }
    }
    public bool[] arrHasWin = { true, false, false, false };
    public bool[] ArrHasWin
    {
        get { return arrHasWin; }
        set {
            for (int i = 0; i < value.Length; i++)
            {
                arrHasWin[i] = value[i];
            }
            object[] param = { arrHasWin[0], arrHasWin[1], arrHasWin[2], arrHasWin[3] };
            facade.SendNotification("ArrHasWin", param);
            //int challengeActiveCount = GetActiveCount();
            //facade.SendNotification("ChallengeActiveCount", challengeActiveCount);
        }
    }
    
    private long curTime;
    public long CurTime
    {
        get {

            return curTime;

        }
    }
    private bool playMusic=true;
    public bool PlayMusic
    {
        get { return playMusic; }
        set
        {
            playMusic = value;
            facade.SendNotification("PlayMusic", playMusic.ToString());
        }
    }
    private bool vibrate=true;
    public bool Vibrate
    {
        get { return vibrate; }
        set
        {
            vibrate = value;
            facade.SendNotification("Vibrate", vibrate.ToString());
        }
    }
    private int curTotalScore;
    public int CurTotalScore
    {
        get { return curTotalScore; }
        set
        {
            curTotalScore = value;
            facade.SendNotification("CurTotalScore",curTotalScore);
        }
    }
    private string challengeTime;
    public string ChallengeTime
    {
        get { return challengeTime; }
        set
        {
            challengeTime = value;
            facade.SendNotification("ChallengeTime", challengeTime);
        }
    }
    //距离再次获得免费旋转币的时间
    //private string nextFreeTime;
    public bool hasTeach;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        DontDestroyOnLoad(transform);

    }
    private int leftOpenBoxTimes;
    public int LeftOpenBoxTimes
    {
        get { return leftOpenBoxTimes; }
        set
        {
            leftOpenBoxTimes = value;
            facade.SendNotification("LeftOpenBoxTime", leftOpenBoxTimes);
        }
    }
    public int enterTimes;
    public int quitCount;
    private bool canChallenge;
    public bool CanChallenge
    {
        get { return canChallenge; }
        set
        {
            canChallenge = value;
            facade.SendNotification("CanChallenge",canChallenge);
        }
    }
    public bool breakRecord = false;
    public bool getNewTrophy = false;
    public void ReduceLeftOpenBoxTime(int n)
    {
        LeftOpenBoxTimes = leftOpenBoxTimes - n;
    }
    public DateTime firstDay;
    public DateTime today;
    public DateTime lastDay;
    public int firstDayTimes;
    public int[] arrCurMapId;
    public List<int> winMapIds;
    public float heighRatio = 0;
    public float widthRatio = 0;

    void Start()
    {
        Input.multiTouchEnabled = false;
        InitData();
        heighRatio = Screen.height / 1136;
        widthRatio = Screen.width / 640;
        //next = gameObject.GetUTCTime(DateTime.UtcNow.ToString()) + 86400000;
        //ChessBoard.Instance.Init();
        //CellsMgr.Instance.Init();
    }
    public void InitData()
    {
        string data = PlayerPrefs.GetString("GameDt");
        if (data=="")
        {
            today = DateTime.Today;
            firstDay = DateTime.Today;
            lastDay = DateTime.Today;
            isNewDay = true;
            hasTeach = false;
            name = "";
            highestScore = 0;
            rotCount = 0;
            keyCount = 0;
            canChallenge = false;
            quitCount = 0;
            enterTimes = 0;
            firstDayTimes = 0;
            curRanking = 998;
        }
        else
        {
            gameDt=JsonUtility.FromJson<GameDt>(data);
            firstDay = DateTime.Parse(gameDt.firstDay); 
            lastDay = DateTime.Parse(gameDt.lastDay);
            today = DateTime.Today;
            if (today != lastDay)
            {
                isNewDay = true;
            }
            else isNewDay = false;
            hasTeach = gameDt.hasTeach;
            name = gameDt.name;
            highestScore = gameDt.highestScore;
            rotCount = gameDt.rotCount;
            keyCount = gameDt.keyCount;
            canChallenge = gameDt.canChallenge;
            quitCount = gameDt.quitCount;
            enterTimes = gameDt.enterTimes;
            firstDayTimes = gameDt.firstDayTimes;
            curRanking = gameDt.curRanking;
            string days = PlayerPrefs.GetString("DayInfos");
            if (days != "")
            {
                List<DayInfo> infos = JsonUtility.FromJson<Serialization<DayInfo>>(days).ToList();
                for (int i = 0; i < infos.Count; i++)
                {
                    DayInfo info = new DayInfo();
                    info.day = infos[i].day;
                    info.trophyId = infos[i].trophyId;
                    dayInfos.Add(info);
                }
            }
            

        }
        if (isNewDay)
        {
            FreeCount = 10;
            ExChangeBoxCount = 10;
            CanGetFree = true;
            curTotalScore = 0;
            for (int i = 0; i < 4; i++)
            {
                arrHasWin[i] = false;
            }
        }
        else
        {
            FreeCount = gameDt.freeCount;
            ExChangeBoxCount = gameDt.exchangeBoxCount;
            CurTotalScore = gameDt.curTotalScore;
            for (int i = 0; i < 4; i++)
            {
                arrHasWin[i] = gameDt.arrHasWin[i];
            }
            arrCurMapId = gameDt.arrCurMapId;
            winMapIds = gameDt.winMapIds;
            lastGetFreeTime = DateTime.Parse(gameDt.lastGetFreeTime);
            TestNextFreeTime();
        }
    }
    public void RefreshDayInfo(string day,int trophyId)
    {
        int count = dayInfos.Count;
        if (count > 0&&day == dayInfos[count - 1].day)
        {
            dayInfos[count - 1].trophyId = trophyId;
        }
        else
        {
            DayInfo newDay = new DayInfo();
            newDay.day = day;
            newDay.trophyId = trophyId;
            dayInfos.Add(newDay);
        }
    }
    public void GetCurTime()
    {
        string s = new TimeSpan(0, 0, (int)(DateTime.Today.AddHours(29) - DateTime.Now).TotalSeconds).ToString();
        ChallengeTime = s;
        int n = (int)((DateTime.Today.AddHours(24) - DateTime.Now).TotalSeconds);
        if (n == 0)
        {
            FreeCount = 10;
            ExChangeBoxCount = 10;
            CanGetFree = true;
            curTotalScore = 0;
            for (int i = 0; i < 4; i++)
            {
                arrHasWin[i] = false;
            }
        }
    }
    public void TestNextFreeTime()
    {
        if (canGetFree) return;
        DateTime now = DateTime.Now;
        int totalSeconds = (int)(lastGetFreeTime.AddMinutes(30) - DateTime.Now).TotalSeconds;
        string s = new TimeSpan(0, 0,totalSeconds).ToString();
        facade.SendNotification("NextFreeTime", s);
        if (totalSeconds<=0)
        {
            CanGetFree = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetCurTime();
        TestNextFreeTime();
    }
    private void OnApplicationQuit()
    {
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            
        }
        
    }
    public void SaveData() 
    {
        GameDt data = new GameDt
        {
            id = 0,
            name = name,
            hasTeach = hasTeach,
            highestScore = highestScore,
            rotCount = rotCount,
            keyCount = keyCount,
            canChallenge = canChallenge,
            quitCount = quitCount,
            enterTimes = enterTimes,
            firstDay = firstDay.ToString(),
            lastDay = DateTime.Today.ToString(),
            firstDayTimes = firstDayTimes,
            UIModel = uiModel,
            arrHasWin = arrHasWin,
            arrCurMapId = arrCurMapId,
            winMapIds = winMapIds,
            freeCount = freeCount,
            exchangeBoxCount=exchangeBoxCount,
            lastGetFreeTime = lastGetFreeTime.ToString(),
            curTotalScore = curTotalScore,
            curRanking=curRanking
            
        };
        
        /*GameDt data = new GameDt
        {
            id = 0,
            name = name,
            hasTeach = true,
            highestScore = 2200,
            rotCount = 5,
            keyCount = 300,
            canChallenge = true,
            quitCount = quitCount,
            enterTimes = enterTimes,
            firstDay = firstDay.ToString(),
            lastDay = DateTime.Today.ToString(),
            firstDayTimes = firstDayTimes,
            UIModel = uiModel,
            arrHasWin = arrHasWin,
            arrCurMapId = arrCurMapId,
            winMapIds = winMapIds,
            freeCount = freeCount,
            exchangeBoxCount = 8,
            lastGetFreeTime = lastGetFreeTime.ToString(),
            curTotalScore = 8000,
            curRanking = 998
        };*/
        
        string json = JsonUtility.ToJson(data);
        //File.WriteAllText(Application.streamingAssetsPath + "/GameDt.json", json, System.Text.Encoding.UTF8);
        PlayerPrefs.SetString("GameDt", json);
        if (dayInfos.Count > 0)
        {
            string daysJson = JsonUtility.ToJson(new Serialization<DayInfo>(dayInfos));
            PlayerPrefs.SetString("DayInfos", daysJson);
        }
        
    }
    public void Today()
    {
        today = DateTime.Today;
        if (today !=DateTime.Parse(gameDt.lastDay))
        {
            quitCount = 0;
        }
        else
        {
            quitCount = gameDt.quitCount;
        }

    }
    public void AddCurTotalScore(int count)
    {
        curTotalScore += count;
        CurTotalScore = curTotalScore;
    }
    public void ReduceRotCount()
    {
        if (rotCount>0)
        {
            rotCount--;
        }
        RotCount = rotCount;
    }
  /*  public void SendToMainInterface()
    {
        KeyCount = keyCount;
        CanChallenge = canChallenge;
        CanGetFree = canGetFree;
        facade.SendNotification("ChallengeActiveCount", GetActiveCount());
        FreeCount = freeCount;
        ExChangeBoxCount = exchangeBoxCount;
    }*/
    public void SendToMainSetting()
    {
        PlayMusic = playMusic;
        Vibrate = vibrate;
        
    }
    public int GetActiveCount()
    {
        int winCount = 0;
        for (int i = 0; i < arrHasWin.Length; i++)
        {
            if (arrHasWin[i]) winCount++;
        }
        if (curTotalScore >= 2500&&curTotalScore<4000) return 2-winCount;
        if (curTotalScore >= 4000 && curTotalScore < 8000) return 3 - winCount;
        if (curTotalScore >= 8000) return 4 - winCount;
        return 1 - winCount;    
    }
    public void SendToSetting()
    {
        PlayMusic = playMusic;
        Vibrate = vibrate;
    }
    public void SendToShop()
    {
        RotCount = rotCount;
        CanGetFree = canGetFree;
        FreeCount = freeCount;
        TestNextFreeTime();
    }
    /*public void SendToPuzzleChallenge()
    {
        ArrHasWin = arrHasWin;
        CurTotalScore =curTotalScore;
        GetCurTime();
    }*/
    public void SendToNormalGame()
    {
        HighestScore = highestScore;
        RotCount = rotCount;
        KeyCount = keyCount;
        CanChallenge = canChallenge;
    }
   
        
    
    public void SendToRotateBox()
    {
        LeftOpenBoxTimes = leftOpenBoxTimes;
    }
}
