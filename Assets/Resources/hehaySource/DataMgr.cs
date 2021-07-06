using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataScript;
using komal;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    public static DataMgr Instance;
    public CellsDtMgr cellsDtMgr;
    public NormalGameDtMgr normalGameDtMgr;
    public ProbabilityDtMgr probabilityDtMgr;
    public GameDtMgr gameDtMgr;
    public TeachDtMgr teachDtMgr;
    public ClearDtMgr clearDtMgr;
    public TrophyDtMgr trophyDtMgr;
    public LevelDtMgr levelDtMgr;
    public ChallengeDtMgr challengeDtMgr;
    public FlowerDtMgr flowerDtMgr;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(transform);
        Init();
    }
    private void Start()
    {


    }
    void Init()
    {
        Debug.Log("DataMgr0001");
        cellsDtMgr = new CellsDtMgr();

        cellsDtMgr.loadFile("CellsDt.json");
        Debug.Log("DataMgr0002");
        normalGameDtMgr = new NormalGameDtMgr();
        normalGameDtMgr.loadFile("NormalGameDt.json");
        Debug.Log("DataMgr0003");
        probabilityDtMgr = new ProbabilityDtMgr();
        probabilityDtMgr.loadFile("ProbabilityDt.json");
        Debug.Log("DataMgr0004");
        gameDtMgr = new GameDtMgr();
        gameDtMgr.loadFile("GameDt.json");
        Debug.Log("DataMgr0005");
        teachDtMgr = new TeachDtMgr();
        teachDtMgr.loadFile("TeachDt.json");
        Debug.Log("DataMgr0006");
        clearDtMgr = new ClearDtMgr();
        clearDtMgr.LoadAllFile();
        Debug.Log("DataMgr0007");
        trophyDtMgr = new TrophyDtMgr();
        trophyDtMgr.loadFile("TrophyDt.json");
        Debug.Log("DataMgr0008");
        challengeDtMgr = new ChallengeDtMgr();
        flowerDtMgr = new FlowerDtMgr();
        flowerDtMgr.LoadAllFile();
        //用于与邵伟数据转换的
        /*levelDtMgr = new LevelDtMgr();
        //转换清盘关卡数据
        //levelDtMgr.LoadAllFile("CommonLevel");

        //DataChange dc = new DataChange();
        //dc.changeCommonLevel(levelDtMgr.data);
        //转换收集花的关卡数据
        levelDtMgr.LoadAllFile("FlowerLevel");
        DataChange dc = new DataChange();
        dc.changeFlowerLevel(levelDtMgr.data);*/
        //ReadAndWrite.SaveData<GameDt>("data", new GameDt());
    }
    public CellsDt GetCellsDtById(int id)
    {
        foreach (var cell in cellsDtMgr.data)
        {
            if (id == cell.id)
            {
                return cell;
            }
        }
        return null;
    }
}
