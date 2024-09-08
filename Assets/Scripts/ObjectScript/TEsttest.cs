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
    ParticleSystem particleSystem;
    AudioSource audioSource;

    public bool isJump;

    private void Start()
    {
        library = GetComponent<SpriteLibrary>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            // 카메라에서 마우스 위치를 화면 좌표에서 월드 좌표로 변환
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 해당 위치로 Raycast 발사
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider == null)
                return;
            // 클릭된 오브젝트가 있는지 확인
            if (hit.collider.gameObject.Equals(gameObject))
            {
                if (isJump)
                    return;
                else
                {
                    isJump = true;
                    Invoke("asdasd", 0.01f);
                }
            }
        }
    }
    void asdasd()
    {
        animator.SetTrigger("jump");
    }

    public void setsda()
    {
        library.spriteLibraryAsset = spriteLibraryAssets[Random.Range(0, 4)];
    }
    public void vooasd()
    {
        particleSystem.Play();
        isJump = false;
        GameDataManager.Instance.gold = 1;
        audioSource.volume = GameManager.instance.effectSound;
        audioSource.Play();
    }
}
