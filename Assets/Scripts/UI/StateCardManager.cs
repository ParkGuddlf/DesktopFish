using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

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
        totalgold.text = $"{gameDataManager.earnedGold}";
        runtime.text = $"{gameDataManager.runTimeSecond}";
        totalfish.text = $"{gameDataManager.fishCatchCount}";
        guidePersent.text = $"{(gameDataManager.saveGuideFish.Count * 100) / gameDataManager.totalFish}%";
        totalObject.text = $"{gameDataManager.CatchObjectCount}";
        if (gameDataManager.equipdata["Rod"].Count > 1)
        {
            damage.text = $"{gameDataManager.equipdata["Rod"].Find(x => gameDataManager.currentRod == x.id).attack}";
        }
        castingTime.text = $"{10.5f - gameDataManager.castingLevel * 0.5f}";
        GoldLv.text = $"{gameDataManager.goldLevel}น่";
        castingDelay.text = $"{4.1f - gameDataManager.atkDelayLevel * 0.1f}";
        spacialLv.text = $"{(float)gameDataManager.spacialLevel * 1000 / 10000}%";

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            gameDataManager.runTimeSecond += 1;
            timer = 0;
        }
    }
}
