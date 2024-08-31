using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    RectTransform rectTransform;
    WaitForSeconds moveDelay = new WaitForSeconds(0.05f);
    WaitForSeconds waitDelay = new WaitForSeconds(3f);

    Vector2 startpos = new Vector2(0,-25);
    Vector2 targetpos = new Vector2(0,10);

    [SerializeField] TMP_Text popuptext;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = startpos;
        StartCoroutine(PopUpOnEable());

    }
    IEnumerator PopUpOnEable()
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, targetpos) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetpos, 0.5f);
            yield return moveDelay;
        }
        yield return waitDelay;
        while (Vector2.Distance(rectTransform.anchoredPosition, startpos) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, startpos, 0.5f);
            yield return moveDelay;
        }
        Managers.Resource.Destroy(gameObject);
    }

    public void Textchange(string text)
    {
        popuptext.text = text;
    }
}
