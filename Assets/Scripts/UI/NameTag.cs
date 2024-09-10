using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NameTag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string infoname;
    GameObject nametag;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        nametag = Managers.Resource.Instantiate("PopupUI/NameTag");
        nametag.transform.position = eventData.position;
        nametag.GetComponentInChildren<TMP_Text>().text = infoname;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.Resource.Destroy(nametag);
    }
}
