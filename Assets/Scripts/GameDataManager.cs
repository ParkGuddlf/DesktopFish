using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static Define;

public class GameDataManager : MonoBehaviour
{
    //데이터만 저장하고 불러오는 놈이다
    public static GameDataManager Instance;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(FishDataLoadGoogleSheet());
    }

    //게임 데이터 저장
    public Charecter lastCharecter;
    public Place lastPlace;
    public int gold;
    //낚시대는 캐스팅등급 공격력
    public List<bool> rod = new List<bool> { true, false, false, false, false, false, false };
    //찌는 캐스팅속도
    public List<bool> bobber = new List<bool> { true, false, false, false, false, false, false };
    //옷은 마릿수 골드획득량 공격주기 낚시후 다시던지는 딜레이 같은거 조정
    public List<bool> clothes = new List<bool> { true, false, false, false, false, false, false };

    //구글시트
    readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:F";
    string sheetData;

    public void SelectCharecter(int charNum)
    {
        lastCharecter = (Charecter)charNum;
        SpawnCharecter();
        DataReset();
    }

    public void SpawnCharecter()
    {
        switch (lastCharecter)
        {
            case Charecter.Bule:
                Debug.Log("Blue");
                break;
            case Charecter.Black:
                Debug.Log("Black");
                break;
            case Charecter.Pink:
                Debug.Log("Pink");
                break;
            default:
                break;
        }
        var charecter = Resources.Load<GameObject>("Prefabs/Charecter/Charecter");
        Transform parent = GameObject.Find("MapObjects").transform;
        Instantiate(charecter, parent);
    }

    public void SetFishData()
    {
        FishingSystem.instance.SetFishCatchPossible();
    }

    public void DataReset()
    {
        lastPlace = Place.A;
        gold = 0;
        rod = new List<bool> { true, false, false, false, false, false, false };
        bobber = new List<bool> { true, false, false, false, false, false, false };
        clothes = new List<bool> { true, false, false, false, false, false, false };
        SetFishData();
    }


    IEnumerator FishDataLoadGoogleSheet()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(ADDRESS))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                sheetData = www.downloadHandler.text;
        }
        DebugData();
    }

    public Dictionary<string, List<FishData>> fishdata = new Dictionary<string, List<FishData>>
    {
        { "Common",new List<FishData>() },
        { "Nomal",new List<FishData>() },
        { "Rare",new List<FishData>() },
        { "Epic",new List<FishData>() },
        { "Spacial",new List<FishData>() }
    };

    void DebugData()
    {
        string[] rows = sheetData.Split('\n');


        foreach (string row in rows)
        {
            string[] cols = row.Split("\t");
            var data = new FishData() { id = cols[1], name = cols[2], hp = float.Parse(cols[3]), place = int.Parse(cols[4]), weather = cols[5] };
            fishdata[cols[0]].Add(data);
        }
    }
}