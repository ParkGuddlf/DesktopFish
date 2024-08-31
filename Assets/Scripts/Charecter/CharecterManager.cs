using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static Define;

//ĳ���� ������ �����ϴ� Ŭ����
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

    private void Update()
    {
        audioSource.volume = GameManager.instance.effectSound;
        bobberSource.volume = GameManager.instance.effectSound * 0.5f;
    }

}
