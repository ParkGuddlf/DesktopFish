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
                data_info = $"ä���ӵ��� ����մϴ�.\n���� ���� : {GameDataManager.Instance.castingLevel}";
            if (num == 1)
                data_info = $"���ȹ�淮�� ����մϴ�.\n���� ���� : {GameDataManager.Instance.goldLevel}";
            if (num == 2)
                data_info = $"����ȵ���� ���� Ȯ���� ����մϴ�.\n���� ���� : {GameDataManager.Instance.spacialLevel}";
            if (num == 3)
                data_info = $"ĳ�����ֱⰡ �����մϴ�.\n���� ���� : {GameDataManager.Instance.atkDelayLevel}";
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
