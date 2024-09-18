using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NameTag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] string infoname;
    GameObject nametag;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        nametag = Managers.Resource.Instantiate("PopupUI/NameTag",transform.parent);
        nametag.transform.position = eventData.position-Vector2.one*2;
        nametag.GetComponentInChildren<TMP_Text>().text = infoname;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.Resource.Destroy(nametag);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        nametag.transform.position = eventData.position-Vector2.one*2;
    }
}
