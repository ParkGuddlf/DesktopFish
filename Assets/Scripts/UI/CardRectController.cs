using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardRectController : MonoBehaviour, IPointerMoveHandler
{
    RectTransform m_RectTransform;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector3 m_targetEulerAngles = Vector3.zero;
        float x = -Mathf.Clamp((eventData.position.x - 960) / 960, -1,1)*40;
        float y = -Mathf.Clamp((eventData.position.y - 540) / 540, -1,1)*20;
        m_targetEulerAngles = new Vector3(y, x, 0);
        m_RectTransform.eulerAngles = m_targetEulerAngles;
    }

}
