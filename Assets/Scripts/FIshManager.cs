using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class FIshManager : MonoBehaviour
{
    public string fishRare;
    public FishData fishData;

    WaitForSeconds delay = new WaitForSeconds(3f);

    [Range(0f, 1f)]
    public float value = 0f;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    private void OnEnable()
    {
        StartCoroutine(AppearanceItem());
    }
    IEnumerator AppearanceItem()
    {
        FishingSystem.instance.FishData(this);
        while (value < 1f)
        {
            transform.localPosition = BazierLerp(p1, p2, p3, p4, value);
            yield return Time.deltaTime;
            value += 0.01f;
        }

        yield return delay;
        FishingSystem.instance.objectPool.DespawnOldest();
        GameDataManager.Instance.gold += Gold();
        value = 0;
    }

    public Vector3 BazierLerp(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        Vector3 a = Vector3.Lerp(p1, p2, value);
        Vector3 b = Vector3.Lerp(p2, p3, value);
        Vector3 c = Vector3.Lerp(p3, p4, value);

        Vector3 d = Vector3.Lerp(a, b, value);
        Vector3 e = Vector3.Lerp(b, c, value);

        Vector3 f = Vector3.Lerp(d, e, value);
        return f;
    }
    int Gold()
    {
        int resultGold = 0;
        switch (fishRare)
        {
            case "Common":
                resultGold = Random.Range(1, 10);
                break;
            case "Nomal":
                resultGold = Random.Range(5, 15);
                break;
            case "Rare":
                resultGold = Random.Range(10, 20);
                break;
            case "Epic":
                resultGold = Random.Range(20, 30);
                break;
            case "Spacial":
                resultGold = Random.Range(0, 0);
                break;
        }
        return resultGold;
    }
}
