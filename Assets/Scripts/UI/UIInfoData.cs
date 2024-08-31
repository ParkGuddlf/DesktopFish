using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInfoData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    int num;
    public string data_Name;
    public string data_info;
    public Sprite data_sprite;

    public void DatainfoChange()
    {
        if (!GetComponent<ItemBoxInfo>())
        {
            if (num == 0)
                data_info = $"채집속도가 상승합니다.\n현재 레벨 : {GameDataManager.Instance.castingLevel}";
            if (num == 1)
                data_info = $"골드획득량이 상승합니다.\n현재 레벨 : {GameDataManager.Instance.goldLevel}";
            if (num == 2)
                data_info = $"스페셜등급이 잡힐 확률을 상승합니다.\n현재 레벨 : {GameDataManager.Instance.spacialLevel}";
            if (num == 3)
                data_info = $"캐스팅주기가 감소합니다.\n현재 레벨 : {GameDataManager.Instance.atkDelayLevel}";
        }
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
