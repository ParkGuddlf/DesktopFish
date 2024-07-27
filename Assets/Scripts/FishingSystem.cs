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
    //무작위 데이터
    int DrawElement(List<string> elements)
    {
        // 랜덤 값을 생성하기 위한 Random 클래스 인스턴스 생성
        System.Random random = new System.Random();

        // 뽑기 시스템에서는 각 요소의 문자열 값을 정수로 변환하여 사용
        List<int> probabilities = elements.Select(int.Parse).ToList();

        // 전체 확률 합 구하기
        int totalSum = probabilities.Sum();

        // 0부터 전체 확률 합까지의 랜덤 값을 생성
        int randomNumber = random.Next(0, totalSum);

        // 랜덤 값이 어느 확률 구간에 속하는지 찾기
        int currentSum = 0;
        for (int i = 0; i < probabilities.Count; i++)
        {
            currentSum += probabilities[i];
            if (randomNumber < currentSum)
            {
                // 현재 확률 구간에 해당하는 요소 선택 i값으로 출력해서 i값에따라 노말 레어 에픽 전설로 분류
                return i;
            }
        }

        throw new InvalidOperationException("Invalid state reached in DrawElement method.");
    }

}
