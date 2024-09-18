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
    //�����ߴ��� ���ߴ��� �����ߴ��� ���ߴ���
    //���ž������� ���Ź�ư���� ���������� ������ư���ι�ư��� �ٸ��Ը����

    private void FixedUpdate()
    {
        equipObj.SetActive(GameDataManager.Instance.currentRod.Equals(equipInfo.id));
    }

    public override void DatainfoChange()
    {
        base.DatainfoChange();
        int maxProset = 10000;

        data_info =
        $"Ŀ  �� {((Mathf.Floor(((float.Parse(equipInfo.probabilitytable[0]) - GameDataManager.Instance.spacialLevel * 5) / maxProset) * 10000)) / 100)} %\n" +
        $"��  �� {(Mathf.Floor((float.Parse(equipInfo.probabilitytable[1]) / maxProset) * 1000)) / 10} %\n" +
        $"��  �� {(Mathf.Floor((float.Parse(equipInfo.probabilitytable[2]) / maxProset) * 1000)) / 10} %\n" +
        $"��  �� {(Mathf.Floor((float.Parse(equipInfo.probabilitytable[3]) / maxProset) * 1000)) / 10} %\n";

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
        data_sprite = sprite;
    }
    //��񺰷� 
    public void ButtonSystem()
    {
        if (isBuy == false)
        {
            if (price <= GameDataManager.Instance.gold)
            {
                GameDataManager.Instance.gold = -price;

                GameDataManager.Instance.rod[equipInfo.id] = true;

                isBuy = true;

                GameManager.instance.EffectSound(1);
            }
            else
                return;
        }
        else
        {
            GameManager.instance.EffectSound(0);
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
