using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;

    public RectTransform mainPanel;

    public GameObject[] panels;

    public RectTransform startPosition;
    public RectTransform targetPosition;

    [Header("���׷��̵� ��� �ؽ�Ʈ")]
    [SerializeField] TMP_Text goldCostText;
    [SerializeField] TMP_Text attackDelayCosttext;
    [SerializeField] TMP_Text casttingSpeedCosttext;
    [SerializeField] TMP_Text specialCosttext;
    [Header("����")]
    public GameObject itemBox;
    public Transform RodBox;

    bool isChangePlace;
    [Header("����")]
    public GameObject guideBox;
    public Transform Guide;
    public GameObject newFishMark;

    public InfoBox infoBox;

    [SerializeField] Image fadeImage;
    [SerializeField] Toggle closeToggle;
    [Header("�����г�")]
    [SerializeField] TMP_Text goldtext;
    [SerializeField] Image dayImage;
    [SerializeField] Image weatherImage;
    [SerializeField] Sprite[] daySprite;
    [SerializeField] Sprite[] weatherSprite;
    bool isMove = false;
    bool isOpen = false;

    [SerializeField] GameObject stateCard;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        goldtext.text = $"{GameDataManager.Instance.gold.ToString("F0")}";
        casttingSpeedCosttext.text = GameDataManager.Instance.castingLevel >= 15 ? $"ä���ӵ�\nMax" : $"ä���ӵ�\n{Cost(200, GameDataManager.Instance.castingLevel)}";
        attackDelayCosttext.text = GameDataManager.Instance.atkDelayLevel >= 30 ? $"ĳ�����ֱ�\nMax" : $"ĳ�����ֱ�\n{Cost(100, GameDataManager.Instance.atkDelayLevel)}";
        goldCostText.text = GameDataManager.Instance.goldLevel >= 10 ? $"���ȹ�淮\nMax" : $"���ȹ�淮\n{Cost(500, GameDataManager.Instance.goldLevel)}";
        specialCosttext.text = GameDataManager.Instance.spacialLevel >= 30 ? $"����ȵ��\nMax" : $"����ȵ��\n{Cost(300, GameDataManager.Instance.spacialLevel,1.2f)}";
        dayImage.sprite = daySprite[(int)GameDataManager.Instance.dayNight-1];
        weatherImage.sprite = weatherSprite[(int)GameDataManager.Instance.lastWeather - 1];
    }

    public void PanelOnOff(int num)
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(false);
        }
        GameManager.instance.EffectSound(0);
        panels[num].SetActive(true);
        if (num == 1)
            newFishMark.SetActive(false);
    }

    public void OpenUI(bool ison)
    {
        stateCard.SetActive(false);
        if (isMove || isOpen || ison)
            return;

        StartCoroutine(MoveTargetPos(targetPosition));
        isOpen = true;
    }
    public void CloseUI(bool ison)
    {
        stateCard.SetActive(false);
        if (isMove || !isOpen || ison)
            return;

        StartCoroutine(MoveTargetPos(startPosition));
        isOpen = false;
    }
    IEnumerator MoveTargetPos(RectTransform moveTR)
    {
        isMove = true;
        while (Vector3.Distance(moveTR.anchoredPosition, mainPanel.anchoredPosition) > 0.1f)
        {
            yield return Time.deltaTime;
            mainPanel.anchoredPosition = Vector3.Lerp(mainPanel.anchoredPosition, moveTR.anchoredPosition, 0.5f);
        }
        isMove = false;

    }

    public IEnumerator SetStoreInfo()
    {
        //�����Ͱ����� �°Ŷ� �����Ѱɷ� ������ �������� �����Ѵ�
        //equipdata������ ��ư�� ���� �־��ְ� ��¥��¥�ϱ�
        yield return new WaitForSeconds(1f);
        int count = 0;
        foreach (EquipData go in GameDataManager.Instance.equipdata["Rod"])
        {
            Sprite sprite = GameDataManager.Instance.resoureceManager.iconSprites.FirstOrDefault(x => x.name.Equals(go.id));
            if (!GameDataManager.Instance.rod.Keys.Contains(go.id))
            {
                GameDataManager.Instance.rod.Add(go.id, false);
            }
            Instantiate(itemBox, RodBox).GetComponent<ItemBoxInfo>().SetInfo(go, GameDataManager.Instance.rod[go.id], "Rod", sprite);
            count++;
        }
    }

    public void StateButton(int num)
    {
        //���̾������� �ִ뷹���϶��� �����������ּ���
        int cost = 0;
        switch (num)
        {
            case 0:
                if (GameDataManager.Instance.castingLevel < 15)
                {
                    cost = Cost(200, GameDataManager.Instance.castingLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameManager.instance.EffectSound(2);
                        GameDataManager.Instance.gold = -cost;
                        GameDataManager.Instance.castingLevel += 1;
                    }
                }
                break;
            case 1:
                if (GameDataManager.Instance.goldLevel < 10)
                {
                    cost = Cost(500, GameDataManager.Instance.goldLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameManager.instance.EffectSound(2);
                        GameDataManager.Instance.gold = -cost;
                        GameDataManager.Instance.goldLevel += 1;
                    }
                }
                break;
            case 2:
                if (GameDataManager.Instance.spacialLevel < 30)
                {
                    cost = Cost(300, GameDataManager.Instance.spacialLevel,1.2f);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameManager.instance.EffectSound(2);
                        GameDataManager.Instance.gold = -cost;
                        GameDataManager.Instance.spacialLevel += 1;
                        FishingSystem.instance.percentage[4] = $"{GameDataManager.Instance.spacialLevel * 5}";
                    }
                }
                break;
            case 3:
                if (GameDataManager.Instance.atkDelayLevel < 30)
                {
                    cost = Cost(100, GameDataManager.Instance.atkDelayLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameManager.instance.EffectSound(2);
                        GameDataManager.Instance.gold = -cost;
                        GameDataManager.Instance.atkDelayLevel += 1;
                    }
                }
                break;
        }
    }
    int Cost(int baseCost, int data, float Interest = 1.5f)
    {
        return (int)(baseCost * Mathf.Pow(data, Interest));
    }
    //��Һ���
    public void PlaceChange(int placeNum)
    {
        if (isChangePlace || placeNum.Equals((int)GameDataManager.Instance.lastPlace))
            return;
        fadeImage.gameObject.SetActive(true);
        isChangePlace = true;
        StartCoroutine(PlaceChageCo(placeNum));
    }
    IEnumerator PlaceChageCo(int placeNum)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        WaitForSeconds fadedelay = new WaitForSeconds(0.05f);

        var charControll = CharecterManager.instance.charecterController;
        charControll.isMove = true;
        closeToggle.isOn = true;
        //CloseUI(true);
        yield return delay;
        StartCoroutine(charControll.PlaceChangeCo());
        yield return delay;
        yield return delay;
        Color black = Color.black;
        black.a = 0f;
        for (int i = 0; i < 10; i++)
        {
            black.a += 0.1f * i;
            fadeImage.color = black;
            yield return fadedelay;
        }
        GameDataManager.Instance.lastPlace = (Place)placeNum;
        FishingSystem.instance.SetFishCatchPossible();
        for (int i = 0; i < 10; i++)
        {
            black.a -= 0.1f * i;
            fadeImage.color = black;
            yield return fadedelay;
        }
        StartCoroutine(charControll.MoveTargetPos());
        yield return delay;
        yield return delay;
        yield return delay;
        fadeImage.gameObject.SetActive(false);
        isChangePlace = false;
    }
    public List<GameObject> guidArray;
    //��������
    public void SetFishGuide()
    {
        string[] keys = GameDataManager.Instance.fishdata.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            for (int j = 0; j < GameDataManager.Instance.fishdata[keys[i]].Count; j++)
            {
                var guideBox = Instantiate(this.guideBox, Guide).GetComponent<GuideBoxInfo>();
                guidArray.Add(guideBox.gameObject);
                guideBox.name = GameDataManager.Instance.fishdata[keys[i]][j].id;
                guideBox.fishData = GameDataManager.Instance.fishdata[keys[i]][j];
                guideBox.InfoImageChange(GameDataManager.Instance.fishdata[keys[i]][j]);
                guideBox.guideimage.sprite = GameDataManager.Instance.resoureceManager.fishSprites.FirstOrDefault(x => x.name.Equals(GameDataManager.Instance.fishdata[keys[i]][j].id));
            }
        }
    }
    public void NewFish(FishData fishdata)
    {
        var newFish = Managers.Resource.Instantiate("PopupUI/NewFish", transform, 1);
        newFish.GetComponent<NewFishPopup>().InfoChange(fishdata);
    }
    //popup����
    public void PopUPSpawn(string text)
    {
        PopupBase popup = Managers.Resource.Instantiate("PopupUI/Popup", transform, 1).GetComponent<PopupBase>();
        popup.Textchange(text);
    }

}
