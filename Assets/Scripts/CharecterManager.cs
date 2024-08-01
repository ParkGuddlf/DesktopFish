using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static Define;

//캐릭터 데이터 저장하는 클래스
public class CharecterManager : MonoBehaviour
{
    CharecterState charecterstate;
    CharecterAnimation charecterAnimation;

    [SerializeField]
    SpriteLibrary spritelibrary;
    [SerializeField]
    SpriteLibraryAsset[] spriteLibraryAsset;

    public float castingSpeed;
    public float damage;

    private void Awake()
    {
        charecterAnimation = GetComponent<CharecterAnimation>();
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
    int asd = 0;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            asdasd();
    }
    void asdasd()
    {
        asd++;
        if (asd > 2)
            asd = 0;
        spritelibrary.spriteLibraryAsset = spriteLibraryAsset[asd];
    }

    public CharecterState charecterState
    {
        get { return charecterstate; }
        set 
        {
            charecterstate = value;
            charecterAnimation.AnimaControll(value);
        }
    }
}
