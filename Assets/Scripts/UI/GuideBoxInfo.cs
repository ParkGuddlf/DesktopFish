using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class GuideBoxInfo : MonoBehaviour, IPointerClickHandler
{
    public Image guideimage;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text rareText;
    public FishData fishData;
    public GameObject newFishMark;

    [SerializeField] Image dayImage;
    [SerializeField] Image weatherImage;
    [SerializeField] Image placeImage;

    [SerializeField] Sprite[] daySprite;
    [SerializeField] Sprite[] weatherSprite;
    [SerializeField] Sprite[] placeSprite;

    private void Start()
    {
        rareText.text = RareText();        
    }

    private void Update()
    {
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

    public void InfoImageChange(FishData fishData)
    {
        switch (fishData.place)
        {
            case 1:
                placeImage.sprite = placeSprite[1];
                break;
            case 2:
                placeImage.sprite = placeSprite[2];
                break;
            case 3:
                placeImage.sprite = placeSprite[3];
                break;
            default:
                placeImage.sprite = placeSprite[0];
                break;
        }
        switch (fishData.weather)
        {
            case "sun":
                weatherImage.sprite = weatherSprite[1];
                break;
            case "rain":
                weatherImage.sprite = weatherSprite[2];
                break;
            case "snow":
                weatherImage.sprite = weatherSprite[3];
                break;
            default:
                weatherImage.sprite = weatherSprite[0];
                break;
        }
        switch (fishData.time)
        {
            case "morning":
                dayImage.sprite = daySprite[1];
                break;
            case "night":
                dayImage.sprite = daySprite[2];
                break;
            default:
                dayImage.sprite = daySprite[0];
                break;
        }
        //dayImage.sprite;
        //weatherImage;
        //placeImage;
    }

    string RareText()
    {
        if (fishData != null)
        {
            switch (fishData.rare)
            {
                case "Common":
                    return "<color=#4B4B4B>Common</color>";
                case "Nomal":
                    return "<color=#00B709>Nomal</color>";
                case "Rare":
                    return "<color=blue>Rare</color>";
                case "Epic":
                    return "<color=purple>Epic</color>";
                case "Spacial":
                    return "<color=orange>Spacial</color>";
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
