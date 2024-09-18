using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateCardManager : MonoBehaviour
{
    private float timer = 0f;

    public GameObject card;

    [SerializeField] TMP_Text totalgold;
    [SerializeField] TMP_Text runtime;
    [SerializeField] TMP_Text totalfish;
    [SerializeField] TMP_Text guidePersent;
    [SerializeField] TMP_Text totalObject;
    [SerializeField] TMP_Text damage;
    [SerializeField] TMP_Text castingTime;
    [SerializeField] TMP_Text GoldLv;
    [SerializeField] TMP_Text castingDelay;
    [SerializeField] TMP_Text spacialLv;

    GameDataManager gameDataManager;

    private void Start()
    {
        gameDataManager = GameDataManager.Instance;
        card.SetActive(false);
    }

    public void SetPanelActive()
    {
        card.SetActive(!card.activeSelf);
    }

    private void Update()
    {
        totalgold.text = $"{gameDataManager.earnedGold}";
        runtime.text = TimeString();
        totalfish.text = $"{gameDataManager.fishCatchCount}";
        guidePersent.text = $"{(gameDataManager.saveGuideFish.Count * 100) / gameDataManager.totalFish}%";
        totalObject.text = $"{gameDataManager.CatchObjectCount}";
        if (gameDataManager.equipdata["Rod"].Count > 1)
        {
            damage.text = $"{gameDataManager.equipdata["Rod"].Find(x => gameDataManager.currentRod == x.id).attack}";
        }
        castingTime.text = $"{10.5f - gameDataManager.castingLevel * 0.5f}ÃÊ";
        GoldLv.text = $"{gameDataManager.goldLevel}¹è";
        castingDelay.text = $"{4.1f - gameDataManager.atkDelayLevel * 0.1f}ÃÊ";
        spacialLv.text = ((float)gameDataManager.spacialLevel*5 * 100 / (10000+ gameDataManager.spacialLevel*5)).ToString("F2")+"%";

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            gameDataManager.runTimeSecond += 1;
            if (gameDataManager.runTimeSecond > 360000)
                gameDataManager.runTimeSecond = 360000;
            timer = 0;
        }
        Challenge();
    }
    string TimeString()
    {
        TimeSpan time = TimeSpan.FromSeconds(gameDataManager.runTimeSecond);

        return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    [SerializeField] Challenge[] challengeIcon;

    void Challenge()
    {
        challengeIcon[0].VisionChallenge(gameDataManager.totalFish == gameDataManager.saveGuideFish.Count);
        challengeIcon[1].VisionChallenge(BuyChallenge());
        challengeIcon[2].VisionChallenge(gameDataManager.CatchObjectCount > 100);
        challengeIcon[3].VisionChallenge(IsAllChallenge());
    }
    bool IsAllChallenge()
    {
        for (int i = 0; i < challengeIcon.Length-1; i++) 
        {
            if (!challengeIcon[i].clear)
                return false;
        }
        return true;
    }
    bool BuyChallenge()
    {
        if (gameDataManager.rod.Count == gameDataManager.equipdata["Rod"].Count)
        {
            if (gameDataManager.castingLevel >= 15 && gameDataManager.goldLevel >= 10 && gameDataManager.spacialLevel >= 30 && gameDataManager.atkDelayLevel >= 30)
            {
                return true;
            }
        }
        return false;
    }
}
