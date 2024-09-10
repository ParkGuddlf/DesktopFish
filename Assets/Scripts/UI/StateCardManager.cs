using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        card.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        card.SetActive(false);
    }

    private void Update()
    {
        totalgold.text = $"�������\n{gameDataManager.earnedGold}";
        runtime.text = TimeString();
        totalfish.text = $"���������\n{gameDataManager.fishCatchCount}";
        guidePersent.text = $"�����ϼ���\n{(gameDataManager.saveGuideFish.Count * 100) / gameDataManager.totalFish}%";
        totalObject.text = $"���ѹ���\n{gameDataManager.CatchObjectCount}";
        if (gameDataManager.equipdata["Rod"].Count > 1)
        {
            damage.text = $"���ݷ�\n{gameDataManager.equipdata["Rod"].Find(x => gameDataManager.currentRod == x.id).attack}";
        }
        castingTime.text = $"���ð�\n{10.5f - gameDataManager.castingLevel * 0.5f}��";
        GoldLv.text = $"������\n{gameDataManager.goldLevel}��";
        castingDelay.text = $"ĳ�����ֱ�\n{4.1f - gameDataManager.atkDelayLevel * 0.1f}��";
        spacialLv.text = $"����ȵ��Ȯ��\n{(float)gameDataManager.spacialLevel * 1000 / 10000}%";

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

        return string.Format("�÷���Ÿ��{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    [SerializeField] GameObject[] challengeIcon;

    void Challenge()
    {
        challengeIcon[0].SetActive(gameDataManager.totalFish == gameDataManager.saveGuideFish.Count);
        challengeIcon[1].SetActive(BuyChallenge());
        challengeIcon[2].SetActive(gameDataManager.CatchObjectCount > 100);
        challengeIcon[3].SetActive(challengeIcon[0].activeSelf && challengeIcon[1].activeSelf && challengeIcon[2].activeSelf);
    }
    bool BuyChallenge()
    {
        if(gameDataManager.rod.Count == gameDataManager.equipdata["Rod"].Count)
        {
            if (gameDataManager.castingLevel >= 15 && gameDataManager.goldLevel >= 15 && gameDataManager.spacialLevel >= 30 && gameDataManager.atkDelayLevel >= 30)
            {
                return true;
            }
        }
        return false;
    }
}
