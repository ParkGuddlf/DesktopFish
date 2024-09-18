using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIInfoData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string data_Name;
    public string data_info;
    public Sprite data_sprite;

    public virtual void DatainfoChange()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MainCanvasManager.Instance.infoBox.gameObject.SetActive(true);
        DatainfoChange();
        MainCanvasManager.Instance.infoBox.ChangeinfoBox(data_Name, data_info, data_sprite);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MainCanvasManager.Instance.infoBox.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DatainfoChange();
        MainCanvasManager.Instance.infoBox.ChangeinfoBox(data_Name, data_info, data_sprite);
    }
}
