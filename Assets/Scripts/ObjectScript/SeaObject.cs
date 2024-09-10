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
        Vector2 screenBoundsTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));

        // 무작위 위치 계산
        float randomX = Random.Range(screenBoundsBottomLeft.x, screenBoundsTopRight.x);
        transform.position = new Vector3(randomX, 0, 0);
    }

    private void Update()
    {
        if (GetComponent<TargetJoint2D>())
            if (Input.GetMouseButtonUp(0))
            {
                audioSource.volume = GameManager.instance.effectSound;
                audioSource.Play();
                Invoke("DestroyObject", 0.1f);
            }
    }

    void DestroyObject()
    {
        float gold = Random.Range(100, 200);
        GameDataManager.Instance.gold = gold;
        GameDataManager.Instance.SeaObjectCount--;
        GameDataManager.Instance.CatchObjectCount++;
        var text = Managers.Resource.Instantiate("PopupTextCanvas");
        text.GetComponent<TextPopup>().textChange($"<color=yellow>+{gold} gold<color=yellow>");
        text.transform.position = transform.position;
        text.transform.localScale = Vector3.one * 0.01f;
        Managers.Resource.Destroy(gameObject);
    }
}
