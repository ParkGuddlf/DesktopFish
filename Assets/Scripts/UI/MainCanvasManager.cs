using Lean.Pool;
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
    [Header("상점")]
    public GameObject itemBox;
    public Transform RodBox;
    public Transform BobberBox;

    public LeanGameObjectPool popupPool;

    bool isChangePlace;
    [Header("도감")]
    public GameObject guideBox;
    public Transform Guide;
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
        //데이터가지고 온거랑 저장한걸로 상정템 프리팹을 생성한다
        //equipdata정보로 버튼에 정보 넣어주고 흐짜흐짜하기
        yield return new WaitForSeconds(0.1f);
        int count = 0;
        foreach (EquipData go in GameDataManager.Instance.equipdata["Rod"])
        {
            Sprite sprite = GameDataManager.Instance.resoureceManager.iconSprites.FirstOrDefault(x => x.name.Equals(go.id));
            Instantiate(itemBox, RodBox).GetComponent<ItemBoxInfo>().SetInfo(go, GameDataManager.Instance.rod[go.id], "Rod", sprite);
            count++;
        }
        count = 0;
        foreach (EquipData go in GameDataManager.Instance.equipdata["Bobber"])
        {
            Sprite sprite = GameDataManager.Instance.resoureceManager.iconSprites.FirstOrDefault(x => x.name.Equals(go.id));
            Instantiate(itemBox, BobberBox).GetComponent<ItemBoxInfo>().SetInfo(go, GameDataManager.Instance.bobber[go.id], "Bobber", sprite);
            count++;
        }
    }

    public void PlaceChange(int placeNum)
    {
        if (isChangePlace)
            return;
        isChangePlace = true;
        GameDataManager.Instance.lastPlace = (Place)placeNum;
        FishingSystem.instance.SetFishCatchPossible();
        StartCoroutine(PlaceChageCo());
        //사운드도 변경해준다
    }
    [SerializeField] Image fadeImage;
    IEnumerator PlaceChageCo()
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
    //도감세팅
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

    //popup생성
    public void PopUPSpawn(string text)
    {
        PopupBase popup = popupPool.Spawn(popupPool.transform).GetComponent<PopupBase>();
        popup.Textchange(text);
    }
    
}
