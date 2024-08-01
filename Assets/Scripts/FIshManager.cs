using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Define;


public class FIshManager : MonoBehaviour
{
    public string fishRare;
    public FishData fishData;
    public float hp;

    WaitForSeconds delay = new WaitForSeconds(3f);

    [SerializeField] Rigidbody2D rigidbody2d;
    Vector2 direction = new Vector2(5,15);
    private void OnEnable()
    {
        fishData = FishingSystem.instance.currentFishData;
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
        rigidbody2d.AddForce(direction, ForceMode2D.Impulse);
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground")
        {
            FishingSystem.instance.objectPool.DespawnOldest();
            GameDataManager.Instance.gold += Gold();
        }
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
    //[Range(0f, 1f)]
    //public float value = 0f;
    //public Vector3 p1;
    //public Vector3 p2;
    //public Vector3 p3;
    //public Vector3 p4;
    //IEnumerator AppearanceItem()
    //{
    //    FishingSystem.instance.FishData(this);
    //    while (value < 1f)
    //    {
    //        Vector3 currentPosition = transform.localPosition;
    //        Vector3 nextPosition = BazierLerp(p1, p2, p3, p4, value);

    //        Vector3 direction = nextPosition - currentPosition;
    //        if (direction != Vector3.zero)
    //        {
    //            // Calculate rotation
    //            Quaternion rotation = Quaternion.LookRotation(direction);
    //            transform.rotation = new Quaternion(0,0, rotation.x, wight);
    //        }

    //        transform.position = nextPosition;
    //        yield return Time.deltaTime;
    //        value += 0.01f;
    //    }

    //    yield return delay;
    //    FishingSystem.instance.objectPool.DespawnOldest();
    //    GameDataManager.Instance.gold += Gold();
    //    value = 0;
    //}

    //public Vector3 BazierLerp(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    //{
    //    Vector3 a = Vector3.Lerp(p1, p2, value);
    //    Vector3 b = Vector3.Lerp(p2, p3, value);
    //    Vector3 c = Vector3.Lerp(p3, p4, value);

    //    Vector3 d = Vector3.Lerp(a, b, value);
    //    Vector3 e = Vector3.Lerp(b, c, value);

    //    Vector3 f = Vector3.Lerp(d, e, value);
    //    return f;
    //}
}
