using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static Define;
using static UnityEditor.Progress;

public class FishingSystem : MonoBehaviour
{
    public static FishingSystem instance;

    public List<string> percentage = new List<string> { "10000", "0", "0", "0", "0", "1" };
    string[] rare = { "Common", "Nomal", "Rare", "Epic", "Spacial" };

    public Lean.Pool.LeanGameObjectPool objectPool;

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
        objectPool = GetComponent<Lean.Pool.LeanGameObjectPool>();
    }

    public void SetFishCatchPossible()
    {
        switch (GameDataManager.Instance.lastPlace)
        {
            case Place.A:
                ListSetting(Place.A);
                break;
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

        foreach (var common in GameDataManager.Instance.fishdata["Common"])
        {
            if(common.place == (int)place)
                possibleCommon.Add(common);
        }
        foreach (var noaml in GameDataManager.Instance.fishdata["Nomal"])
        {
            if (noaml.place == (int)place)
                possibleNomal.Add(noaml);
        }
        foreach (var rare in GameDataManager.Instance.fishdata["Rare"])
        {
            if (rare.place == (int)place)
                possibleRare.Add(rare);
        }
        foreach (var epic in GameDataManager.Instance.fishdata["Epic"])
        {
            if (epic.place == (int)place)
                possibleEpic.Add(epic);
        }
        foreach (var spcial in GameDataManager.Instance.fishdata["Spacial"])
        {
            if (spcial.place == (int)place)
                possibleSpacial.Add(spcial);
        }
    }

    public void FishData()
    {
        string rareStr = rare[DrawElement(percentage)];
        
        switch (rareStr)
        {
            case "Common":
                currentFishData = possibleCommon[UnityEngine.Random.Range(0,possibleCommon.Count)];
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

    public void CatchFish()
    {
        objectPool.Spawn(transform);
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
