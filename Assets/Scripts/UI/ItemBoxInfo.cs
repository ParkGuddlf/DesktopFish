using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemBoxInfo : MonoBehaviour
{
    EquipData equipInfo;
    string itemKind;
    public Image image;
    public TMP_Text priceText;
    [SerializeField] int price;
    [SerializeField] bool isBuy;
    [SerializeField] GameObject equipObj;
    //�����ߴ��� ���ߴ��� �����ߴ��� ���ߴ���
    //���ž������� ���Ź�ư���� ���������� ������ư���ι�ư��� �ٸ��Ը����

    private void FixedUpdate()
    {
        switch (itemKind)
        {
            case "Rod":
                equipObj.SetActive(GameDataManager.Instance.currentRod.Equals(equipInfo.id));
                //���� Ȯ������
                break;
            case "Bobber":
                equipObj.SetActive(GameDataManager.Instance.currentbobber.Equals(equipInfo.id));
                //ĳ����������
                break;
            default:
                equipObj.SetActive(GameDataManager.Instance.currentRod.Equals(equipInfo.id));
                break;
        }
    }

    public void SetInfo(EquipData itemIfo, bool isbuy, string item, Sprite sprite)
    {
        equipInfo = itemIfo;
        price = itemIfo.price;
        priceText.text = price.ToString();
        isBuy = isbuy;
        itemKind=item;
        image.sprite = sprite;
    }
    //��񺰷� 
    public void ButtonSystem()
    {
        if(isBuy == false)
        {
            if (price <= GameDataManager.Instance.gold)
            {
                GameDataManager.Instance.gold -= price;
                switch (itemKind)
                {
                    case "Rod":
                        GameDataManager.Instance.rod[equipInfo.id] = true;
                        break;
                    case "Bobber":
                        GameDataManager.Instance.bobber[equipInfo.id] = true;
                        break;
                    default:
                        break;
                }
                isBuy = true;
            }
            else
                return;
        }
            

        switch (itemKind)
        {
            case "Rod":
                FishingSystem.instance.percentage = equipInfo.probabilitytable;
                GameDataManager.Instance.currentRod = equipInfo.id;
                break;
            case "Bobber":
                GameDataManager.Instance.currentbobber = equipInfo.id;
                break;
            default:
                break;
        }
    }
}
