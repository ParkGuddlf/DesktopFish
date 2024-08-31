using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

    [SerializeField] TMP_Text goldtext;
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

    public InfoBox infoBox;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PanelOnOff(0);
    }

    private void Update()
    {
        goldtext.text = $"{GameDataManager.Instance.gold}";
        casttingSpeedCosttext.text = GameDataManager.Instance.castingLevel >=15 ? $"ä���ӵ�\nMax" : $"ä���ӵ�\n{Cost(200, GameDataManager.Instance.castingLevel)}";
        attackDelayCosttext.text = GameDataManager.Instance.atkDelayLevel >= 30 ? $"ĳ�����ֱ�\nMax" : $"ĳ�����ֱ�\n{Cost(100, GameDataManager.Instance.atkDelayLevel)}";
        goldCostText.text = GameDataManager.Instance.goldLevel >= 15 ? $"���ȹ�淮\nMax" : $"���ȹ�淮\n{Cost(1000, GameDataManager.Instance.goldLevel)}";
        specialCosttext.text = GameDataManager.Instance.spacialLevel >= 30 ? $"����ȵ��\nMax" : $"����ȵ��\n{Cost(500, GameDataManager.Instance.spacialLevel)}";
    }

    public void PanelOnOff(int num)
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(false);
        }
        panels[num].SetActive(true);
    }

    public void OpenUI()
    {
        StartCoroutine(MoveTargetPos(targetPosition));
    }
    public void CloseUI()
    {
        StartCoroutine(MoveTargetPos(startPosition));
    }
    IEnumerator MoveTargetPos(RectTransform moveTR)
    {
        while (Vector3.Distance(moveTR.anchoredPosition, mainPanel.anchoredPosition) > 0.1f)
        {
            yield return Time.deltaTime;
            mainPanel.anchoredPosition = Vector3.Lerp(mainPanel.anchoredPosition, moveTR.anchoredPosition, 0.5f);
        }
    }

    public IEnumerator SetStoreInfo()
    {
        //�����Ͱ����� �°Ŷ� �����Ѱɷ� ������ �������� �����Ѵ�
        //equipdata������ ��ư�� ���� �־��ְ� ��¥��¥�ϱ�
        yield return new WaitForSeconds(0.1f);
        int count = 0;
        foreach (EquipData go in GameDataManager.Instance.equipdata["Rod"])
        {
            Sprite sprite = GameDataManager.Instance.resoureceManager.iconSprites.FirstOrDefault(x => x.name.Equals(go.id));
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
                        GameDataManager.Instance.gold -= cost;
                        GameDataManager.Instance.castingLevel += 1;
                    }
                }
                break;
            case 1:
                if (GameDataManager.Instance.goldLevel < 15)
                {
                    cost = Cost(1000, GameDataManager.Instance.goldLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameDataManager.Instance.gold -= cost;
                        GameDataManager.Instance.goldLevel += 1;
                    }
                }
                break;
            case 2:
                if (GameDataManager.Instance.spacialLevel < 30)
                {
                    cost = Cost(500, GameDataManager.Instance.spacialLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameDataManager.Instance.gold -= cost;
                        GameDataManager.Instance.spacialLevel += 1;
                    }
                }
                break;
            case 3:
                if (GameDataManager.Instance.atkDelayLevel < 30)
                {
                    cost = Cost(100, GameDataManager.Instance.atkDelayLevel);
                    if (cost <= GameDataManager.Instance.gold)
                    {
                        GameDataManager.Instance.gold -= cost;
                        GameDataManager.Instance.atkDelayLevel += 1;
                    }
                }
                break;
        }
    }
    int Cost(int baseCost, int data)
    {
        return (int)(baseCost * Mathf.Pow(data, 1.5f));
    }
    //��Һ���
    public void PlaceChange(int placeNum)
    {
        if (isChangePlace)
            return;
        isChangePlace = true;
        StartCoroutine(PlaceChageCo(placeNum));
        //���嵵 �������ش�
    }

    [SerializeField] Image fadeImage;
    IEnumerator PlaceChageCo(int placeNum)
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        WaitForSeconds fadedelay = new WaitForSeconds(0.05f);
        fadeImage.gameObject.SetActive(true);


        var charControll = CharecterManager.instance.charecterController;
        charControll.isMove = true;
        CloseUI();
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
        fadeImage.gameObject.SetActive(false);
        isChangePlace = false;
    }
    //��������
    public void SetFishGuide()
    {
        string[] keys = GameDataManager.Instance.fishdata.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            for (int j = 0; j < GameDataManager.Instance.fishdata[keys[i]].Count; j++)
            {
                var guideBox = Instantiate(this.guideBox, Guide).GetComponent<GuideBoxInfo>();
                guideBox.fishData = GameDataManager.Instance.fishdata[keys[i]][j];
                guideBox.guideimage.sprite = GameDataManager.Instance.resoureceManager.fishSprites.FirstOrDefault(x => x.name.Equals(GameDataManager.Instance.fishdata[keys[i]][j].id));
            }
        }
    }

    //popup����
    public void PopUPSpawn(string text)
    {
        PopupBase popup = Managers.Resource.Instantiate("PopupUI/Popup", transform, 1).GetComponent<PopupBase>();
        popup.Textchange(text);
    }

}