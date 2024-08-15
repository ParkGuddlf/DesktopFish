using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class GuideBoxInfo : MonoBehaviour
{
    public Image guideimage;
    [SerializeField] TMP_Text nameText;
    public FishData fishData;

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
}
