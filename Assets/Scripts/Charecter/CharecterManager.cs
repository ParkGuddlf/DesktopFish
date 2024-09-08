using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static Define;

//캐릭터 데이터 저장하는 클래스
public class CharecterManager : MonoBehaviour
{
    public static CharecterManager instance;

    CharecterState charecterstate;
    public CharecterState charecterState
    {
        get { return charecterstate; }
        set
        {
            charecterstate = value;
            charecterAnimation.AnimaControll(value);
        }
    }
    public CharecterAnimation charecterAnimation;
    public CharecterController charecterController;

    [SerializeField]
    SpriteLibrary spritelibrary;
    [SerializeField]
    SpriteLibraryAsset[] spriteLibraryAsset;

    public float castingSpeed;
    public float attackDelay;
    public float throwDelay = 1f;
    public float catchDelay = 0.5f;
    public int damage;

    public AudioSource audioSource;
    public AudioSource bobberSource;
    [SerializeField] int FeverGage;
    public int feverGage
    {
        get { return FeverGage; }
        set
        {
            FeverGage = value;
            if (FeverGage >= 110)
            {
                isFever= true;
                Fever();
                Invoke("StateReset",8f);
            }
        }
    }
    public bool isFever;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        charecterAnimation = GetComponent<CharecterAnimation>();
        charecterController = GetComponent<CharecterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CharecterSpreit();
    }

    private void Update()
    {
        if (!isFever)
        {
            audioSource.volume = GameManager.instance.effectSound;
            bobberSource.volume = GameManager.instance.effectSound * 0.5f;
        }
        else
        {
            audioSource.volume =0;
            bobberSource.volume = 0;
        }
    }
    public void CharecterSpreit()
    {
        switch (GameDataManager.Instance.lastCharecter)
        {
            case Charecter.Bule:
                spritelibrary.spriteLibraryAsset = spriteLibraryAsset[0];
                break;
            case Charecter.Black:
                spritelibrary.spriteLibraryAsset = spriteLibraryAsset[1];
                break;
            case Charecter.Pink:
                spritelibrary.spriteLibraryAsset = spriteLibraryAsset[2];
                break;
            default:
                spritelibrary.spriteLibraryAsset = spriteLibraryAsset[0];
                break;
        }
    }
    void Fever()
    {
        Managers.Resource.Instantiate("Fever");
        GameManager.instance.bgmAudioSource.clip = GameDataManager.Instance.resoureceManager.bgmClips[1];
        GameManager.instance.bgmAudioSource.Play();
        castingSpeed = 0;
        attackDelay = 0;
        throwDelay = 0.1f;
        catchDelay = 0;
    }
    void StateReset()
    {
        castingSpeed = 10.5f - GameDataManager.Instance.castingLevel * 0.5f;
        attackDelay = 4.1f - GameDataManager.Instance.atkDelayLevel * 0.1f;
        throwDelay = 1f;
        catchDelay = 0.5f;
        feverGage = 0;
        isFever = false;
        Managers.Resource.Destroy(GameObject.Find("Fever"));
        GameManager.instance.bgmAudioSource.clip = GameDataManager.Instance.resoureceManager.bgmClips[0];
        GameManager.instance.bgmAudioSource.Play();
    }
}
