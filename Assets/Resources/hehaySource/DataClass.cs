using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using komal;

namespace DataScript
{
    public class CellsDtMgr:ListDtMgr<CellsDt>
    {
        private Dictionary<int, CellsDt> idAndCellsDt = new Dictionary<int, CellsDt>();
        public Dictionary<int, CellsDt> getData()
        {
            return idAndCellsDt;
        }
        public CellsDt getDataById(int id)
        {
            return idAndCellsDt[id];
        }
    }
    public class NormalGameDtMgr
    {
        //public List<MapDt> listMapDt = new List<MapDt>();
        public NormalGameDt normalGameDt = new NormalGameDt();
        public void loadFile(string fileName)
        {

            if (Application.platform == RuntimePlatform.WindowsEditor||Application.platform==RuntimePlatform.OSXEditor)
            {
                string json = KomalUtil.Instance.__ReadFromStreamAssets(fileName);
                if (json.Length > 0)
                {
                    //mapDt = JsonConvert.DeserializeObject<MapDt>(json);
                    normalGameDt = JsonUtility.FromJson<NormalGameDt>(json);
                }
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                TextAsset textAsset;
                textAsset = Resources.Load<TextAsset>("NormalGameDt");
                Debug.Log("NormalGameDt.length" + textAsset);
                normalGameDt = JsonUtility.FromJson<NormalGameDt>(textAsset.text);
            }    

        }
    }
    public class ChallengeDtMgr
    {
       
        public ChallengeDt challengeDt = new ChallengeDt();
        public void Load()
        {
            
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                string json = KomalUtil.Instance.__ReadFromStreamAssets("ChallengeDt.json");
                if (json.Length > 0)
                {
                    challengeDt = JsonUtility.FromJson<ChallengeDt>(json);
                }
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                TextAsset textAsset;
                textAsset = Resources.Load<TextAsset>("ChallengeDt");
                challengeDt = JsonUtility.FromJson<ChallengeDt>(textAsset.text);
            }

        }
    }
    public class ProbabilityDtMgr:ListDtMgr<ProbabilityDt>
    {
       
    }
    public class TeachDtMgr:ListDtMgr<TeachDt>
    {
       
    }
    public class ClearDtMgr:ListDtMgr<ClearDt>
    {
        public void LoadAllFile()
        {
            TextAsset[] textAssets;
            textAssets = Resources.LoadAll<TextAsset>("CommonLevel");
            for (int i = 0; i < textAssets.Length; i++)
            {
                ClearDt dt = JsonUtility.FromJson<ClearDt>(textAssets[i].text);
                data.Add(dt);
            }
        }
        public ClearDt GetDataById(int id)
        {
            foreach (var clearDt in data)
            {
                if (id == clearDt.id)
                    return clearDt;
            }
            return null;
        }

    }
    public class FlowerDtMgr : ListDtMgr<FlowerDt> 
    {
        public void LoadAllFile() 
        {
            TextAsset[] textAssets;
            textAssets = Resources.LoadAll<TextAsset>("FlowerLevel");
            for (int i = 0; i < textAssets.Length; i++)
            {
                FlowerDt dt = JsonUtility.FromJson<FlowerDt>(textAssets[i].text);
                data.Add(dt);
            }
        }
        public FlowerDt GetDataById(int id)
        {
            foreach (var flowerDt in data)
            {
                if (id == flowerDt.id) return flowerDt;
            }
            return null;
        }
    }
    public class GameDtMgr
    {
        public GameDt gameDt = new GameDt();
        public void loadFile(string fileName)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                string json = KomalUtil.Instance.__ReadFromStreamAssets(fileName);

                if (json.Length > 0)
                {

                    //gameDt = JsonConvert.DeserializeObject<GameDt>(json);
                    gameDt = JsonUtility.FromJson<GameDt>(json);
                }
            }
            else if (Application.platform==RuntimePlatform.Android)
            {
                TextAsset textAsset;
                textAsset = Resources.Load<TextAsset>("GameDt");
                Debug.Log("GameDt.length" + textAsset);
                gameDt = JsonUtility.FromJson<GameDt>(textAsset.text);
            }
        }
    }
    public class TrophyDtMgr : ListDtMgr<TrophyDt> { }
    public class LevelDtMgr : ListDtMgr<LevelDt>
    {
        public void LoadAllFile(string fileName)
        {
            TextAsset[] textAssets;
            textAssets = Resources.LoadAll<TextAsset>(fileName);
            for (int i = 0; i < textAssets.Length; i++)
            {
                LevelDt dt = JsonUtility.FromJson<LevelDt>(textAssets[i].text);
                data.Add(dt);
            }
        }
    }
    
    [Serializable]
    public class CellsDt
    {
        public int id = 1;
        public int[] cellsDt = {0,0,0,0,0,
                            0,0,0,0,0,
                            0,0,1,0,0,
                            0,0,0,0,0,
                            0,0,0,0,0};

        public int score = 1;
        public IV2 offset;
        public bool isRotated = false;
        public bool isRotatingBody = false;
    }
    [Serializable]
    public class IV2
    {
        public float x;
        public float y;
    }
    [Serializable]
    public class ProbabilityDt
    {
        public int id = 1;
        public int lowScore = 0;
        public int highScore = 20;
        public int[] probability = { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10,
                       1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10
                     };
    }
    [Serializable]
    public class TrophyDt
    {
        public int id = 1;
        public int lowScore = 0;
        public int highScore = 1000;
    }
    [Serializable]
    public class NormalGameDt
    {
        public int id = 1001;
        public int[] data =
           {0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0
       };
        public bool reborn = false;
        public int score;
        public int totalScore;
        public string exitTime;
        
        public string elapsedTime;
        public int curEnterTimes;
        public int curGameoverCount;
        public int totalFailure;
        public int lastFailureScore;
        //public bool isRotate;
        public int curRefresh;
        public int totalRefresh;
        public int curPlace;
        public int totalPlace;
        public int eliminateCount;
        public int totalEliminate;
        public int breakRecordTimes;
        public int[] arrId = { 0, 0, 0, 0 };
        public bool[] arrIsShow = { true, true, true, true };
        public int[] arrCurState = { 5, 5, 5, 5 };
        public int[] arrStateBk = { 5, 5, 5, 5 };
    }
    [Serializable]
    public class GameDt
    {
        public int id = 0;
        public bool hasTeach = false;
        public string name;
        public int highestScore;
        public int rotCount;
        public int keyCount;
        public bool canChallenge;
        public int quitCount;
        public int enterTimes;
        public string firstDay;
        public string lastDay;
        public int firstDayTimes;
        public int[] UIModel = { 0, 0 };
        public bool[] arrHasWin;
        public int[] arrCurMapId = { 1, 2, 3, 4 };
        public List<int> winMapIds;
        public int freeCount;
        public int exchangeBoxCount;
        public string lastGetFreeTime;
        public int curTotalScore;
        public int curRanking;
    }
    
    [Serializable]
    public class TeachDt
    {
        public int id = 1;
        public int[] mapDt = new int[100];
        public int[] arrId = { 1, 1, 1, 1 };
        public bool[] arrIsShow = { false, false, false, false };
        public int[] arrCurState = { 0, 0, 0, 0 };
        public Vector3 startPos = new Vector3(0, 0, 0);
        public Vector3 endPos = new Vector3(0, 0, 0);
    }
    [Serializable]
    public class ClearDt
    {
        public int id;
        public int[] mapDt;
        public int[] arrId;
        public int[] arrState;
        public Vector3 startPos;
        public Vector3 endPos;
    }
    [Serializable]
    public class FlowerDt 
    {
        public int id=2001;
        public int[] mapDt= {
                              0,1,2,0,0,0,0,0,0,0,
                              1,1,1,1,0,0,0,0,0,0,
                              1,2,2,2,1,0,0,0,1,1,
                              0,1,0,0,0,1,0,0,2,0,
                              1,2,2,2,0,0,0,2,1,1,
                              0,1,2,1,0,1,0,0,1,0,
                              0,1,1,1,0,0,0,1,0,0,
                              1,0,1,1,0,0,0,0,0,0,
                              0,0,1,0,0,0,0,0,0,1,
                              0,1,1,0,0,0,0,1,0,0,
                             };
        public int[] arrId= { 12,11,9,2};
        public int[] arrState= { 0,1,1,0};
        public Vector2[] arrFlowerPos= {new Vector2(1,2),new Vector2(1,4),new Vector2(2,6),new Vector2(8,4)};
    }
    [Serializable]
    public class ChallengeDt
    {
        public int[] arrMapId;
        public string lastDay;
        public List<int> noPassClearMap;
        public List<int> noPassFlowerMap;
       
    }
    [Serializable]
    public class LevelDt
    {
        public int levelId;
        public int divide;
        public int target;
        public string[] underblocks;
        public int[] gridblocks;
        public bool[] targetflower;
    }
    
    public class ListDtMgr<T>
    {
        public List<T> data = new List<T>();
        public void loadFile(string fileName)
        {
            data = KomalUtil.Instance.ReadFromStreamAssets<Serialization<T>>(fileName).ToList();
            /*if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                /*TextAsset textAsset ; 
                string name = fileName.Replace(".json", "");
                Debug.Log("进来CellsDt");
                textAsset = Resources.Load<TextAsset>(name);
                Debug.Log("进来CellsDt:" + textAsset);
                Debug.Log(name + ".length" + textAsset);
                data = JsonUtility.FromJson<Serialization<T>>(textAsset.text).ToList();*/

                /*string json = KomalUtil.Instance.__ReadFromStreamAssets(fileName);
                if (json.Length > 0)
                {
                    data = JsonUtility.FromJson<Serialization<T>>(json).ToList();
                }
                
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                TextAsset textAsset;
                string name = fileName.Replace(".json","");
                Debug.Log("进来CellsDt");
                textAsset = Resources.Load<TextAsset>(name);
                Debug.Log("进来CellsDt:"+textAsset);
                Debug.Log(name+".length" + textAsset);
                data = JsonUtility.FromJson<Serialization<T>>(textAsset.text).ToList();
            }*/
        }
    }
    [Serializable]
    public class DayInfo
    {
        public string day;
        public int trophyId;
    }
    
}

