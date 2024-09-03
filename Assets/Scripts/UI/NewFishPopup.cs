using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewFishPopup : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] Image fishImage;

    Vector2 startPos = new Vector2(-250,35);
    Vector2 endPos= new Vector2(0,35);

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rect.anchoredPosition = startPos;
    }

    private void Update()
    {
        if (Vector2.Distance(rect.anchoredPosition, endPos) > 0.1f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, endPos, 0.1f);
        }
        else
            Invoke("DestroyPopup", 2);
    }

    private void DestroyPopup()
    {
        Managers.Resource.Destroy(gameObject);
    }

    public void ImageChange(string id)
    {
        int index = id.IndexOf('_');
        fishImage.sprite = GameDataManager.Instance.resoureceManager.fishSprites[int.Parse(id.Substring(index + 1))];
    }

}
