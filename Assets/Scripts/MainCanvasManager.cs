using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public RectTransform mainPanel;

    public GameObject[] panels;

    public RectTransform startPosition;
    public RectTransform targetPosition;

    private void Start()
    {
        PanelOnOff(0);
    }

    public void PanelOnOff(int num)
    {
        foreach (GameObject go in panels) {
            go.SetActive(false);
        }
        panels[num].SetActive(true);
    }

    public void OpenUI()
    {
        StartCoroutine(MoveTargetPos(targetPosition));
    }
    public void CloseUI()
    {
        StartCoroutine(MoveTargetPos(startPosition));
    }
    IEnumerator MoveTargetPos(RectTransform moveTR)
    {
        while (Vector3.Distance(moveTR.anchoredPosition, mainPanel.anchoredPosition) > 0.1f)
        {
            yield return Time.deltaTime;
            mainPanel.anchoredPosition = Vector3.Lerp(mainPanel.anchoredPosition, moveTR.anchoredPosition, 0.5f);
        }
    }
}
