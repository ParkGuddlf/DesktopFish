using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class FishingSystem : MonoBehaviour
{
    public static FishingSystem instance;

    public List<string> percentage = new List<string> { "10000", "0", "0", "0", "0" };
    string[] rare = { "Common", "Nomal", "Rare", "Epic", "Spacial" };

    AudioSource audioSource;

    public List<FishData> possibleCommon = new List<FishData>();
    public List<FishData> possibleNomal = new List<FishData>();
    public List<FishData> possibleRare = new List<FishData>();
    public List<FishData> possibleEpic = new List<FishData>();
    public List<FishData> possibleSpacial = new List<FishData>();

    public FishData currentFishData = new FishData();
    public float currentFishHp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetRodStatePercentage(string rodid)
    {
        percentage = GameDataManager.Instance.equipdata["Rod"].Find(x => rodid == x.id).probabilitytable;
        percentage[4] = $"{GameDataManager.Instance.spacialLevel}";
        CharecterManager.instance.damage = GameDataManager.Instance.equipdata["Rod"].Find(x => rodid == x.id).attack;
    }

    public void SetFishCatchPossible()
    {
        switch (GameDataManager.Instance.lastPlace)
        {
            case Place.B:
                ListSetting(Place.B);
                break;
            case Place.C:
                ListSetting(Place.C);
                break;
            case Place.D:
                ListSetting(Place.D);
                break;
            default:
                ListSetting(Place.A);
                break;
        }
    }
    //���� ���������� �־��ش�
    void ListSetting(Place place)
    {
        if (possibleCommon.Count > 0)
            possibleCommon.Clear();
        if (possibleNomal.Count > 0)
            possibleNomal.Clear();
        if (possibleRare.Count > 0)
            possibleRare.Clear();
        if (possibleEpic.Count > 0)
            possibleEpic.Clear();
        if (possibleSpacial.Count > 0)
            possibleSpacial.Clear();
        CurrentPossibleCatchFish(GameDataManager.Instance.fishdata["Common"], possibleCommon);
        CurrentPossibleCatchFish(GameDataManager.Instance.fishdata["Nomal"], possibleNomal);
        CurrentPossibleCatchFish(GameDataManager.Instance.fishdata["Rare"], possibleRare);
        CurrentPossibleCatchFish(GameDataManager.Instance.fishdata["Epic"], possibleEpic);
        CurrentPossibleCatchFish(GameDataManager.Instance.fishdata["Spacial"], possibleSpacial);
    }

    void CurrentPossibleCatchFish(List<FishData> fishdata, List<FishData> addlist)
    {
        var place = GameDataManager.Instance.lastPlace;
        string weather = GameDataManager.Instance.lastWeather.ToString();
        string dayNight = GameDataManager.Instance.dayNight.ToString();
        foreach (var common in fishdata)
        {
            if (common.place == (int)place || common.place == 0)
                if (common.weather == weather || common.weather == "nomal")
                    if (common.time == dayNight || common.time == "all")
                        addlist.Add(common);
        }
    }

    public void FishData()
    {
        string rareStr = rare[DrawElement(percentage)];

        switch (rareStr)
        {
            case "Common":
                currentFishData = possibleCommon[UnityEngine.Random.Range(0, possibleCommon.Count)];
                break;
            case "Nomal":
                currentFishData = possibleNomal[UnityEngine.Random.Range(0, possibleNomal.Count)];
                break;
            case "Rare":
                currentFishData = possibleRare[UnityEngine.Random.Range(0, possibleRare.Count)];
                break;
            case "Epic":
                currentFishData = possibleEpic[UnityEngine.Random.Range(0, possibleEpic.Count)];
                break;
            case "Spacial":
                currentFishData = possibleSpacial[UnityEngine.Random.Range(0, possibleSpacial.Count)];
                break;
            default:
                currentFishData = possibleCommon[UnityEngine.Random.Range(0, possibleCommon.Count)];
                break;
        }
        currentFishHp = currentFishData.hp;
    }

    public void CatchFish(bool fever)
    {
        if (!fever)
        {
            audioSource.clip = GameDataManager.Instance.resoureceManager.fishClips[currentFishData.size];
            audioSource.Play();
        }
        Managers.Resource.Instantiate("Fish/Fish",transform,1);
    }
    //������ ������
    int DrawElement(List<string> elements)
    {
        // ���� ���� �����ϱ� ���� Random Ŭ���� �ν��Ͻ� ����
        System.Random random = new System.Random();

        // �̱� �ý��ۿ����� �� ����� ���ڿ� ���� ������ ��ȯ�Ͽ� ���
        List<int> probabilities = elements.Select(int.Parse).ToList();

        // ��ü Ȯ�� �� ���ϱ�
        int totalSum = probabilities.Sum();

        // 0���� ��ü Ȯ�� �ձ����� ���� ���� ����
        int randomNumber = random.Next(0, totalSum);

        // ���� ���� ��� Ȯ�� ������ ���ϴ��� ã��
        int currentSum = 0;
        for (int i = 0; i < probabilities.Count; i++)
        {
            currentSum += probabilities[i];
            if (randomNumber < currentSum)
            {
                // ���� Ȯ�� ������ �ش��ϴ� ��� ���� i������ ����ؼ� i�������� �븻 ���� ���� ������ �з�
                return i;
            }
        }

        throw new InvalidOperationException("Invalid state reached in DrawElement method.");
    }

}
