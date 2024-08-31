using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] sprites;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sprite = sprites[(int)GameDataManager.Instance.lastPlace-1];
    }
}
