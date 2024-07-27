using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public RectTransform mainPanel;
    public void OpenUI()
    {
        StartCoroutine(MoveTargetPos(new Vector3(630,-400,0)));
    }
    public void CloseUI()
    {
        StartCoroutine(MoveTargetPos(new Vector3(1000,-400,0)));
    }
    IEnumerator MoveTargetPos(Vector3 _targetPos)
    {
        while (Vector3.Distance(_targetPos, mainPanel.anchoredPosition) > 0.1f)
        {
            yield return Time.deltaTime;
            mainPanel.anchoredPosition = Vector3.Lerp(mainPanel.anchoredPosition, _targetPos,0.5f);
        }
    }
}
