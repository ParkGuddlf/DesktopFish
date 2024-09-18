using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DuckClick : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            // ī�޶󿡼� ���콺 ��ġ�� ȭ�� ��ǥ���� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // �ش� ��ġ�� Raycast �߻�
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider == null)
                return;
            // Ŭ���� ������Ʈ�� �ִ��� Ȯ��
            if (hit.collider.gameObject.Equals(gameObject))
            {
                if (isJump)
                    return;
                else
                {
                    isJump = true;
                    Invoke("DuckJump", 0.01f);
                }
            }
        }
    }
    void DuckJump()
    {
        animator.SetTrigger("jump");
    }

    public void setsda()
    {
        library.spriteLibraryAsset = spriteLibraryAssets[Random.Range(0, 4)];
    }
    public void vooasd()
    {
        var text = Managers.Resource.Instantiate("PopupTextCanvas");
        text.GetComponent<TextPopup>().textChange($"<color=yellow>+10 gold<color=yellow>");
        text.transform.position = transform.position;
        text.transform.localScale = Vector3.one * 0.01f;
        isJump = false;
        GameDataManager.Instance.gold = 10;
        audioSource.volume = GameManager.instance.effectSound;
        audioSource.Play();
    }
}
