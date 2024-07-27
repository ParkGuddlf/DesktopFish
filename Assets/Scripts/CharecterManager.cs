using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//ĳ���� ������ �����ϴ� Ŭ����
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
