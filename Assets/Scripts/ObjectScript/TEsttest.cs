using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.Animation;

public class TEsttest : MonoBehaviour
{
    SpriteLibrary library;
    SpriteResolver spriteResolver;

    [SerializeField]
    SpriteLibraryAsset[] spriteLibraryAssets;

    Animator animator;

    AudioSource audioSource;

    public bool isJump;

    private void Start()
    {
        library = GetComponent<SpriteLibrary>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        if (isJump)
            return;
        else
        {
            isJump = true;
            Invoke("asdasd", 0.1f);
        }
    }
    void asdasd()
    {
        animator.SetTrigger("jump");
        audioSource.volume = GameManager.instance.effectSound;
        audioSource.Play();
    }

    public void setsda()
    {
        library.spriteLibraryAsset = spriteLibraryAssets[Random.Range(0, 4)];
    }
    public void vooasd()
    {
        isJump = false;
    }
}
