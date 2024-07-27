using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//케릭터의 움직임 및 애니메이션 불러와서 사용
public class CharecterController : MonoBehaviour
{
    CharecterAnimation charecterAnimation;
    CharecterManager charecterManager;

    Vector3 startPos = new Vector3(10.5f, -8.5f, 0);

    Vector3 targetPos = new Vector3(18.5f, -8.5f, 0);
    [SerializeField]
    float speed;

    enum FishingState { Idle, Throwing, Catching }
    private FishingState currentState;

    private void Awake()
    {
        charecterManager = GetComponent<CharecterManager>();
        charecterAnimation = GetComponent<CharecterAnimation>();
    }
    void Start()
    {
        StartCoroutine(MoveTargetPos(startPos));
    }


    IEnumerator MoveTargetPos(Vector3 _targetPos)
    {
        charecterManager.charecterState = CharecterState.Walk;
        while (Vector3.Distance(_targetPos, transform.position) > 0.1f)
        {
            yield return Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed);
        }
        charecterManager.charecterState = CharecterState.Sit;
        yield return new WaitForSeconds(1f);
        currentState = FishingState.Throwing;
    }
    float timer = 0;
    bool isThrow = false;
    bool isCatch = false;
    private void Update()
    {
        timer += Time.deltaTime;
        switch (currentState)
        {
            case FishingState.Idle:
                if (timer > charecterManager.castingSpeed)
                {
                    timer = 0.0f;
                    currentState = FishingState.Catching;
                    charecterManager.charecterState = CharecterState.Catch;
                }
                break;
            case FishingState.Throwing:
                if (!isThrow)
                {
                    charecterManager.charecterState = CharecterState.Throw;
                    isThrow = true;
                }
                if (timer > 1f)
                {
                    timer = 0.0f;
                    currentState = FishingState.Idle;
                    charecterManager.charecterState = CharecterState.Idle;
                }
                break;
            case FishingState.Catching:
                if (!isCatch && timer > 0.5f)
                {
                    FishingSystem.instance.CatchFish();
                    isCatch = true;
                }
                if (timer > 1.5f)
                {
                    timer = 0.0f;
                    isThrow = false;
                    isCatch = false;
                    currentState = FishingState.Throwing;
                    charecterManager.charecterState = CharecterState.Idle;
                }
                break;
            default:
                break;
        }
    }
}
