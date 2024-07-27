using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//캐릭터 데이터 저장하는 클래스
public class CharecterManager : MonoBehaviour
{
    CharecterState charecterstate;
    CharecterAnimation charecterAnimation;

    public float castingSpeed;
    public float damage;

    private void Awake()
    {
        charecterAnimation = GetComponent<CharecterAnimation>();
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
