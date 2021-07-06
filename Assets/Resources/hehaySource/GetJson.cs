using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using DataScript;

public class GetJson : MonoBehaviour
{
    void Start()
    {
        //CellsDt
        /*List<CellsDt> datas = new List<CellsDt>();
        CellsDt cellsDt = new CellsDt();
        datas.Add(cellsDt);
        string json = JsonUtility.ToJson(new Serialization<CellsDt>(datas));

        Console.WriteLine("这是json数据"+json);
        string json = JsonUtility.ToJson(cellsDt);

        File.WriteAllText(Application.streamingAssetsPath + "/CellsDt.json", json, System.Text.Encoding.UTF8);*/

        //ChallengeMapDt
        /*List<ChallengeMapDt> datas = new List<ChallengeMapDt>();
        ChallengeMapDt cMap = new ChallengeMapDt();
        datas.Add(cMap);
        string json = JsonUtility.ToJson(new Serialization<ChallengeMapDt>(datas));
        File.WriteAllText(Application.streamingAssetsPath + "/ChallengeMapDt.json", json, System.Text.Encoding.UTF8);*/

        //GameDt
        /*GameDt data = new GameDt();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.streamingAssetsPath + "/GameDt.json", json, System.Text.Encoding.UTF8);

        //NormalGameDt
        NormalGameDt data1 = new NormalGameDt();
        string json1 = JsonUtility.ToJson(data1);
        File.WriteAllText(Application.streamingAssetsPath + "/NormalGameDt.json", json1, System.Text.Encoding.UTF8);

        //ProbabilityDt
        List<ProbabilityDt> datas = new List<ProbabilityDt>();
        ProbabilityDt data2 = new ProbabilityDt();
        datas.Add(data2);
        string json2 = JsonUtility.ToJson(new Serialization<ProbabilityDt>(datas));
        File.WriteAllText(Application.streamingAssetsPath + "/ProbabilityDt.json", json2, System.Text.Encoding.UTF8);*/

        /*List<TeachDt> datas = new List<TeachDt>();
        TeachDt data = new TeachDt();
        datas.Add(data);
        string json = JsonUtility.ToJson(new Serialization<TeachDt>(datas));
        File.WriteAllText(Application.streamingAssetsPath + "/TeachDt.json", json, System.Text.Encoding.UTF8);*/

        /*List<ClearDt> datas = new List<ClearDt>();
        ClearDt data = new ClearDt();
        datas.Add(data);
        string json = JsonUtility.ToJson(new Serialization<ClearDt>(datas));
        File.WriteAllText(Application.streamingAssetsPath + "/ClearDt.json", json, System.Text.Encoding.UTF8);*/
        FlowerDt data1 = new FlowerDt();
        string json1 = JsonUtility.ToJson(data1);
        File.WriteAllText(Application.streamingAssetsPath + "/FlowerDt.json", json1, System.Text.Encoding.UTF8);
    }

}

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}