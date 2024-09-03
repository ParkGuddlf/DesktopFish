using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeaObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = GameDataManager.Instance.resoureceManager.seaObject[Random.Range(0, 5)];

        Vector2 screenBoundsBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenBoundsTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width*0.5f, Screen.height));

        // 무작위 위치 계산
        float randomX = Random.Range(screenBoundsBottomLeft.x, screenBoundsTopRight.x);
        transform.position = new Vector3(randomX, 0,0);
    }
    private void Update()
    {
        if (GetComponent<TargetJoint2D>())
            if (Input.GetMouseButtonUp(0))
                DestroyObject();
    }

    void DestroyObject()
    {
        float gold = Random.Range(50, 150);
        GameDataManager.Instance.gold += gold;
        var text = Managers.Resource.Instantiate("PopupTextCanvas");
        text.GetComponent<TextPopup>().textChange($"<color=yellow>+{gold} gold<color=yellow>");
        text.transform.position = transform.position;
        Managers.Resource.Destroy(gameObject);
    }
}
