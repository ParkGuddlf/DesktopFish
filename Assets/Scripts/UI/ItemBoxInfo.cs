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
    //장착했는지 안했는지 구매했는지 안했는지
    //구매안했으면 구매버튼으로 구매했으면 장착버튼으로버튼기능 다르게만들기

    private void FixedUpdate()
    {
        switch (itemKind)
        {
            case "Rod":
                equipObj.SetActive(GameDataManager.Instance.currentRod.Equals(equipInfo.id));
                //낚시 확률조정
                break;
            case "Bobber":
                equipObj.SetActive(GameDataManager.Instance.currentbobber.Equals(equipInfo.id));
                //캐릭스텟조정
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
    //장비별로 
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
