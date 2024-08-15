using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//케릭터의 움직임 및 애니메이션 불러와서 사용
public class CharecterController : MonoBehaviour
{
    CharecterAnimation charecterAnimation;
    CharecterManager charecterManager;

    public Transform FishingPoint;
    public Transform CharecterSpowPoint;

    Vector3 startPos = new Vector3(10.5f, -7.5f, 0);

    Vector3 targetPos = new Vector3(18.5f, -7.5f, 0);
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D bobber;
    public enum FishingState { Idle, Throwing, Catching }
    [SerializeField]
    public FishingState currentState;


    private void Awake()
    {
        charecterManager = GetComponent<CharecterManager>();
        charecterAnimation = GetComponent<CharecterAnimation>();
        CharecterSpowPoint = GameObject.Find("CharecterSpowPoint").transform;
        FishingPoint = GameObject.Find("FishingPoint").transform;
    }
    private void Start()
    {
        transform.position = CharecterSpowPoint.position;
        StartCoroutine(MoveTargetPos());
    }


    public IEnumerator MoveTargetPos()
    {
        transform.rotation = new Quaternion(0, 0, 0,0);
        charecterManager.charecterState = CharecterState.Walk;
        while (Vector3.Distance(FishingPoint.position, transform.position) > 0.1f)
        {
            yield return Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, FishingPoint.position, speed);
        }
        charecterManager.charecterState = CharecterState.Sit;
        yield return new WaitForSeconds(1f);
        isThrow = false;
        isCatch = false;
        isMove = false;
        currentState = FishingState.Throwing;
    }

    public IEnumerator PlaceChangeCo()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        bobber.gameObject.SetActive(false);
        charecterManager.charecterState = CharecterState.StandUp;
        yield return delay;
        transform.rotation = new Quaternion(0, 180, 0, 0);
        charecterManager.charecterState = CharecterState.Walk;
        while (Vector3.Distance(CharecterSpowPoint.position, transform.position) > 0.1f)
        {
            yield return Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, CharecterSpowPoint.position, speed);
        }
        yield return delay;
    }
    float timer = 0;
    public bool isThrow = false;
    public bool isCatch = false;
    public bool isMove = false;
    private void Update()
    {
        if (!isMove)
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
                        timer = 0.0f;
                        charecterManager.charecterState = CharecterState.Throw;                        
                        isThrow = true;
                    }
                    if (timer > 1f)
                    {
                        timer = 0.0f;
                        StartCoroutine(BobberPosition(true));
                        currentState = FishingState.Idle;
                        charecterManager.charecterState = CharecterState.Idle;
                    }
                    break;
                case FishingState.Catching:
                    if (!isCatch && timer > 0.5f)
                    {
                        FishingSystem.instance.FishData();
                        isCatch = true;
                    }
                    if (timer > 1.5f)
                    {
                        timer = 0.0f;

                        FishingSystem.instance.currentFishHp -= 1;
                        if (FishingSystem.instance.currentFishHp <= 0)
                        {
                            FishingSystem.instance.CatchFish();
                            isThrow = false;
                            isCatch = false;
                            StartCoroutine(BobberPosition(false));
                            currentState = FishingState.Throwing;
                            charecterManager.charecterState = CharecterState.Idle;
                        }
                        else
                        {
                            charecterManager.charecterState = CharecterState.Catch;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator BobberPosition(bool isOn)
    {
        if (isOn)
        {
            bobber.transform.localPosition = new Vector2(-4.5f, -1f);
            bobber.gameObject.SetActive(true);
            yield return null;

        }
        else
        {
            bobber.velocity = new Vector2(bobber.velocity.x, 0);
            bobber.AddForce(new Vector2(5, 15), ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(0.05f);
            bobber.gameObject.SetActive(false);
        }
    }
}
