using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class NewFishPopup : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] Image fishImage;

    Vector2 startPos = new Vector2(-250,35);
    Vector2 endPos= new Vector2(0,35);
    AudioSource audio;

    [SerializeField] TMP_Text rareText;
    [SerializeField] TMP_Text nameText;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        rect.anchoredPosition = startPos;
        audio.volume = GameManager.instance.effectSound;
    }

    private void Update()
    {
        if (Vector2.Distance(rect.anchoredPosition, endPos) > 0.1f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, endPos, 0.1f);
        }
        else
            Invoke("DestroyPopup", 2);
    }

    private void DestroyPopup()
    {
        Managers.Resource.Destroy(gameObject);
    }

    public void InfoChange(FishData fishData)
    {
        int index = fishData.id.IndexOf('_');
        fishImage.sprite = GameDataManager.Instance.resoureceManager.fishSprites[int.Parse(fishData.id.Substring(index + 1))];
        rareText.text = RareText(fishData);
        nameText.text = fishData.name;
    }

    string RareText(FishData fishData)
    {
        if (fishData != null)
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

}
