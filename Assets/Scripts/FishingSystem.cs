using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingSystem : MonoBehaviour
{
    public static FishingSystem instance;

    public List<string> percentage = new List<string> { "10000", "0", "0", "0", "0", "1" };
    string[] rare = { "Common", "Nomal", "Rare", "Epic", "Spacial" };

    public Lean.Pool.LeanGameObjectPool objectPool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectPool = GetComponent<Lean.Pool.LeanGameObjectPool>();
    }

    public void FishData(FIshManager fish)
    {
        string rareStr = rare[DrawElement(percentage)];
        var data = GameDataManager.Instance.fishdata[rareStr];
        fish.fishRare = rareStr;
        fish.fishData = data[UnityEngine.Random.Range(0, data.Count)];
    }

    public void CatchFish()
    {
        objectPool.Spawn();
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
