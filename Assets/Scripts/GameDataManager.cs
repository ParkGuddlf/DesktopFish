using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static Define;

public class GameDataManager : MonoBehaviour
{
    //�̱���
    public static GameDataManager Instance;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(FishDataLoadGoogleSheet());
        StartCoroutine(EquipDataLoadGoogleSheet());
    }

    public ResoureceManager resoureceManager;

    //���� ������ ����
    public Charecter lastCharecter;
    [SerializeField] Place LastPlace;
    public Place lastPlace { get { return LastPlace; } set { LastPlace = value; } }
    public Weather lastWeather;

    float elapsedTime;

    public int gold;
    public string currentRod = "a001";
    public string currentbobber = "b001";
    public string currentclothes;

    //���⿡ ���� �ɶ� ���� �˾�â ���� ���� ��ҽ��ϴ�
    public List<string> saveGuideFish = new List<string>();

    //���ô� �ر� ����
    public Dictionary<string, bool> rod = new Dictionary<string, bool>();
    //�� �ر�����
    public Dictionary<string, bool> bobber = new Dictionary<string, bool>();
    //�� �ر�����
    public Dictionary<string, bool> clothes = new Dictionary<string, bool>();

    //���۽�Ʈ
    //����� ������
    readonly string FISHADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:G";
    //��� ������
    readonly string EQUIPADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:G&gid=428092868";

    string sheetData;


    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime / 60f >= 1)
        {
            lastWeather = (Weather)Random.Range(0, 2);
            elapsedTime = 0;
        }
    }
    //���ν��۽� ĳ���� ���ù�ư �Լ�
    public void SelectCharecter(int charNum)
    {
        lastCharecter = (Charecter)charNum;
        SpawnCharecter();
        DataReset();
        StartCoroutine(MainCanvasManager.Instance.SetStoreInfo());
    }

    public void AddGuide(FishData fishData)
    {
        saveGuideFish.Add(fishData.id);
        MainCanvasManager.Instance.PopUPSpawn($"<color=red>{fishData.name}</color>��(��) ȹ���߽��ϴ�.");
    }

    //ĳ���� ����
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
    // ������ �ʱ�ȭ
    public void DataReset()
    {
        lastPlace = Place.A;
        lastWeather = Weather.sun;
        gold = 0;
        currentRod = "a001";
        currentbobber = "b001";
        string[] rodkeys = rod.Keys.ToArray();
        for (int i = 0; i < rodkeys.Length; i++)
        {
            rod[rodkeys[i]] = false;
        }
        string[] bobberkeys = bobber.Keys.ToArray();
        for (int i = 0; i < bobberkeys.Length; i++)
        {
            bobber[bobberkeys[i]] = false;
        }
        rod["a001"] = true;
        bobber["b001"] = true;
        FishingSystem.instance.SetRodStatePercentage(currentRod);
        CharecterManager.instance.castingSpeed = equipdata["Bobber"].Find(x => currentbobber == x.id).castingspeed;
    }

    #region ��Ʈ ������ �ҷ�����
    IEnumerator FishDataLoadGoogleSheet()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(FISHADDRESS))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                sheetData = www.downloadHandler.text;
        }
        FishData();
        MainCanvasManager.Instance.SetFishGuide();
    }

    IEnumerator EquipDataLoadGoogleSheet()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(EQUIPADDRESS))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                sheetData = www.downloadHandler.text;
        }
        EquipData();
    }

    public Dictionary<string, List<FishData>> fishdata = new Dictionary<string, List<FishData>>
    {
        { "Common",new List<FishData>() },
        { "Nomal",new List<FishData>() },
        { "Rare",new List<FishData>() },
        { "Epic",new List<FishData>() },
        { "Spacial",new List<FishData>() }
    };

    void FishData()
    {
        string[] rows = sheetData.Split('\n');


        foreach (string row in rows)
        {
            string[] cols = row.Split("\t");
            var data = new FishData() { rare = cols[0], id = cols[1], name = cols[2], hp = float.Parse(cols[3]), place = int.Parse(cols[4]), weather = cols[5], size = int.Parse(cols[6]) };
            fishdata[cols[0]].Add(data);
        }
        FishingSystem.instance.SetFishCatchPossible();
    }

    public Dictionary<string, List<EquipData>> equipdata = new Dictionary<string, List<EquipData>>
    {
        { "Rod",new List<EquipData>() },
        { "Bobber",new List<EquipData>() }
    };

    void EquipData()
    {
        string[] rows = sheetData.Split('\n');


        foreach (string row in rows)
        {
            string[] cols = row.Split("\t");
            if (cols[0] == "Rod")
            {
                var data = new EquipData() { id = cols[1], name = cols[2], price = int.Parse(cols[3]), probabilitytable = SplitString(cols[4]), attack = int.Parse(cols[5]) };
                equipdata[cols[0]].Add(data);
                rod.Add(cols[1], false);
            }
            else if (cols[0] == "Bobber")
            {
                var data = new EquipData() { id = cols[1], name = cols[2], price = int.Parse(cols[3]), castingspeed = int.Parse(cols[6]) };
                equipdata[cols[0]].Add(data);
                bobber.Add(cols[1], false);
            }
        }
        rod["a001"] = true;
        bobber["b001"] = true;
    }

    List<string> SplitString(string stringdata)
    {
        string[] strings = stringdata.Split(",");

        return strings.ToList();
    }
    #endregion
}