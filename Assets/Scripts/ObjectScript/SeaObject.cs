using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeaObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = GameDataManager.Instance.resoureceManager.seaObject[Random.Range(0, 5)];

        Vector2 screenBoundsBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenBoundsTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width*0.5f, Screen.height));

        // ������ ��ġ ���
        float randomX = Random.Range(screenBoundsBottomLeft.x, screenBoundsTopRight.x);
        transform.position = new Vector3(randomX, 0,0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) // ���콺 ���� ��ư Ŭ��
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
                audioSource.volume = GameManager.instance.effectSound;
                audioSource.Play();
                Invoke("DestroyObject", 0.1f);
            }
        }
    }

    void DestroyObject()
    {        
        float gold = Random.Range(50, 150);
        GameDataManager.Instance.gold = gold;
        GameDataManager.Instance.CatchObjectCount++;
        var text = Managers.Resource.Instantiate("PopupTextCanvas");
        text.GetComponent<TextPopup>().textChange($"<color=yellow>+{gold} gold<color=yellow>");
        text.transform.position = transform.position;
        text.transform.localScale = Vector3.one * 0.01f;
        Managers.Resource.Destroy(gameObject);
    }
}
