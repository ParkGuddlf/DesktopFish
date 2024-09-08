using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
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
    public DateTime dayNight;
    public float weatherTimer;
    public float dayTimer;
    public float spownTimer;
    [SerializeField]
    private float Gold;
    public float gold
    {
        get { return Gold; }
        set
        {
            Gold += value;
            earnedGold += value;
        }
    }
    public string currentRod = "a001";
    [SerializeField]
    private int CastingLevel = 1;
    [SerializeField]
    private int GoldLevel = 1;
    [SerializeField]
    private int SpacialLevel = 1;
    [SerializeField]
    private int AtkDelayLevel = 1;
    public int castingLevel
    {
        get { return CastingLevel; }
        set
        {
            CastingLevel = value < 16 ? value : 15;
            CharecterManager.instance.castingSpeed = 10.5f - castingLevel * 0.5f;
        }
    }
    public int goldLevel
    {
        get { return GoldLevel; }
        set { GoldLevel = value < 16 ? value : 15; }
    }
    public int spacialLevel
    {
        get { return SpacialLevel; }
        set { SpacialLevel = value < 31 ? value : 30; }
    }
    public int atkDelayLevel
    {
        get { return AtkDelayLevel; }
        set
        {
            AtkDelayLevel = value < 31 ? value : 30;
            CharecterManager.instance.attackDelay = 4.1f - atkDelayLevel * 0.1f;
        }
    }

    public float fishCatchCount;
    public float earnedGold;
    public float CatchObjectCount;
    public float runTimeSecond;


    //���⿡ ���� �ɶ� ���� �˾�â ���� ���� ��ҽ��ϴ�
    public List<string> saveGuideFish = new List<string>();

    //���ô� �ر� ����
    public Dictionary<string, bool> rod = new Dictionary<string, bool>();

    //���۽�Ʈ
    //�����Ϳ� ����� ������
    readonly string FISHADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:J";
    //�����Ϳ� ��� ������
    readonly string EQUIPADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:G&gid=428092868";

    readonly string BuildFISHADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:J&gid=823048447";
    readonly string BuildEQUIPADDRESS = "https://docs.google.com/spreadsheets/d/1I3j43QsjWIu1Hro2TAboQeWrvgd2TKXtsPQw8LNWdKg/export?format=tsv&range=A2:G&gid=579945735";

    string sheetData;

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
        var charecter = Resources.Load<GameObject>("Prefabs/Charecter/Charecter");
        Transform parent = GameObject.Find("MapObjects").transform;
        Instantiate(charecter, parent);
        GameManager.instance.isStart = true;
    }
    // ������ �ʱ�ȭ
    public void DataReset()
    {
        lastPlace = Place.B;
        lastWeather = Weather.sun;
        gold = 0;
        weatherTimer = 0;
        dayTimer = 0;
        currentRod = "a001";
        castingLevel = 1;
        goldLevel = 1;
        spacialLevel = 1;
        atkDelayLevel = 1;
        string[] rodkeys = rod.Keys.ToArray();
        for (int i = 0; i < rodkeys.Length; i++)
        {
            rod[rodkeys[i]] = false;
        }

        rod["a001"] = true;
        FishingSystem.instance.SetRodStatePercentage(currentRod);
        FishingSystem.instance.SetFishCatchPossible();
        CharecterManager.instance.castingSpeed = 10.5f - castingLevel * 0.5f;
        //�ѽð� ���� ������ �̷��͵� �������ֱ�
        fishCatchCount = 0;
        earnedGold = 0;
        CatchObjectCount = 0;
    }

    #region ��Ʈ ������ �ҷ�����
    IEnumerator FishDataLoadGoogleSheet()
    {
        string _FISHADDRESS;
#if UNITY_EDITOR
        _FISHADDRESS = FISHADDRESS;
#elif !UNITY_EDITOR
        _FISHADDRESS = BuildFISHADDRESS;
#endif
        using (UnityWebRequest www = UnityWebRequest.Get(_FISHADDRESS))
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
        string _EQUIPADDRESS;
#if UNITY_EDITOR
        _EQUIPADDRESS = EQUIPADDRESS;
#elif !UNITY_EDITOR
        _EQUIPADDRESS = BuildEQUIPADDRESS;
#endif
        using (UnityWebRequest www = UnityWebRequest.Get(_EQUIPADDRESS))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                sheetData = www.downloadHandler.text;
        }
        EquipData();
    }
    [HideInInspector]
    public int totalFish =1;
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

        totalFish= rows.Length;

        foreach (string row in rows)
        {
            string[] cols = row.Split("\t");
            var data = new FishData() { rare = cols[0], id = cols[1], name = cols[2], hp = float.Parse(cols[3]), place = int.Parse(cols[4]), weather = cols[5], size = int.Parse(cols[6]), time = cols[7].Replace("\r", "") };
            fishdata[cols[0]].Add(data);
        }
        //FishingSystem.instance.SetFishCatchPossible();
    }

    public Dictionary<string, List<EquipData>> equipdata = new Dictionary<string, List<EquipData>>
    {
        { "Rod",new List<EquipData>() }
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
        }
        rod["a001"] = true;
    }

    List<string> SplitString(string stringdata)
    {
        string[] strings = stringdata.Split(",");

        return strings.ToList();
    }
    #endregion
}