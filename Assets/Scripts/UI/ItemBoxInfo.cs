using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemBoxInfo : UIInfoData
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
        equipObj.SetActive(GameDataManager.Instance.currentRod.Equals(equipInfo.id));
    }

    public void SetInfo(EquipData itemIfo, bool isbuy, string item, Sprite sprite)
    {
        equipInfo = itemIfo;
        price = itemIfo.price;
        priceText.text = price.ToString();
        isBuy = isbuy;
        itemKind = item;
        image.sprite = sprite;
        data_Name = equipInfo.name;

        int maxProset = GameDataManager.Instance.spacialLevel + 10000;

        data_info = $"커  먼 {(Mathf.Floor((float.Parse(itemIfo.probabilitytable[0]) / maxProset) * 1000)) / 10} %\n" +
        $"노  말 {(Mathf.Floor((float.Parse(itemIfo.probabilitytable[1]) / maxProset) * 1000)) / 10} %\n" +
        $"레  어 {(Mathf.Floor((float.Parse(itemIfo.probabilitytable[2]) / maxProset) * 1000)) / 10} %\n" +
        $"에  픽 {(Mathf.Floor((float.Parse(itemIfo.probabilitytable[3]) / maxProset) * 1000)) / 10} %\n";
        //$"스페셜 {GameDataManager.Instance.spacialLevel}\n";

        data_sprite = sprite;
    }
    //장비별로 
    public void ButtonSystem()
    {
        if (isBuy == false)
        {
            if (price <= GameDataManager.Instance.gold)
            {
                GameDataManager.Instance.gold = -price;

                GameDataManager.Instance.rod[equipInfo.id] = true;

                isBuy = true;

                GameManager.instance.effectAudioSource.clip = GameDataManager.Instance.resoureceManager.uiClips[1];
                GameManager.instance.effectAudioSource.Play();
            }
            else
                return;
        }
        else
        {
            GameManager.instance.effectAudioSource.clip = GameDataManager.Instance.resoureceManager.uiClips[0];
            GameManager.instance.effectAudioSource.Play();
        }


        switch (itemKind)
        {
            case "Rod":
                CharecterManager.instance.damage = equipInfo.attack;
                FishingSystem.instance.SetRodStatePercentage(equipInfo.id);
                GameDataManager.Instance.currentRod = equipInfo.id;
                break;
            case "Bobber":
                CharecterManager.instance.castingSpeed = equipInfo.castingspeed;
                break;
            default:
                break;
        }
    }
}
