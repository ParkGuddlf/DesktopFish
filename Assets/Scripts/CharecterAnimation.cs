using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//캐릭터의 애니매이션 저장
public class CharecterAnimation : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimaControll(CharecterState charecterstate)
    {
        switch (charecterstate)
        {
            case CharecterState.Idle:
                Idle();
                break;
            case CharecterState.Walk:
                Walk();
                break;
            case CharecterState.Sit:
                Sit();
                break;
            case CharecterState.Throw:
                Throw();
                break;
            case CharecterState.Catch:
                Catch();
                break;
            default:
                Idle();
                break;
        }
    }


    public void Idle()
    {
        animator.SetTrigger("Idle");
    }
    public void Walk()
    {
        animator.SetTrigger("Walk");
    }

    public void Throw()
    {
        animator.SetTrigger("Throw");
    }

    public void Catch()
    {
        animator.SetTrigger("Catch");
    }
    public void Sit()
    {
        animator.SetTrigger("Sit");
    }
}