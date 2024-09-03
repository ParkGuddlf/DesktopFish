using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class GuideBoxInfo : MonoBehaviour , IPointerClickHandler
{
    public Image guideimage;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text rareText;
    public FishData fishData;
    public GameObject newFishMark;


    private void Update()
    {
        rareText.text = RareText();
        if (GameDataManager.Instance.saveGuideFish.Contains(fishData.id))
            CaughtFish();
        else
            NoCaughtFish();
    }

    void CaughtFish()
    {
        guideimage.color = Color.white;
        nameText.text = fishData.name;
    }
    void NoCaughtFish()
    {
        guideimage.color = Color.black;
        nameText.text = "??????";
    }

    string RareText()
    {
        if(fishData != null)
        {
            switch (fishData.rare)
            {
                case "Common":
                    return "Common";
                case "Nomal":
                    return "<color=green>Nomal<color=green>";
                case "Rare":
                    return "<color=blue>Rare<color=blue>";
                case "Epic":
                    return "<color=purple>Epic<color=purple>";
                case "Spacial":
                    return "<color=orange>Spacial<color=orange>";
                default:
                    return "";
            }
        }
        else
            return "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        newFishMark.SetActive(false);
    }
}
