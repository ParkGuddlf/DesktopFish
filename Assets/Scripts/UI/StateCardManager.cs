using SaveIsEasy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateCardManager : MonoBehaviour
{
    RectTransform rect;

    Vector2 startPos = new Vector2(35, 130);
    Vector2 endPos = new Vector2(35, 380);

    [SerializeField] Toggle cardOnoffToggle;
    bool isMove;

    private float timer = 0f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        cardOnoffToggle.onValueChanged.AddListener(MovePos);

    }
    [SerializeField, Multiline(5)]
    string value;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            GameDataManager.Instance.runTimeSecond += 1;
            timer = 0;
            value = $"{GameDataManager.Instance.fishCatchCount}\n{GameDataManager.Instance.earnedGold}\n{GameDataManager.Instance.runTimeSecond}\n";
        }
    }

    void MovePos(bool isOn)
    {
        if (isMove)
            return;

        if (isOn)
            StartCoroutine(MoveCo(endPos));
        else
            StartCoroutine(MoveCo(startPos));
    }

    IEnumerator MoveCo(Vector2 pos)
    {
        isMove = true;
        while (Vector2.Distance(rect.anchoredPosition, pos) > 0.1f)
        {
            yield return Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, pos, 0.5f);
        }
        isMove = false;
    }

}
