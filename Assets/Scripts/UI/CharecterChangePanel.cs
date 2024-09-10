using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CharecterChangePanel : MonoBehaviour
{

    [SerializeField] Image charImage;
    [Header("캐릭터 변경 이미지")]
    [SerializeField] Sprite[] charUISprites;
    int charnum = 0;
    [SerializeField]
    TMP_Text nameText;
    [SerializeField]
    TMP_Text infoText;

    private void OnEnable()
    {
        charnum = (int)GameDataManager.Instance.lastCharecter;
        ChangeCharNum(0);
    }

    public void ChangeCharNum(int num)
    {
        charnum += num;
        if (charnum > 2)
            charnum = 0;
        else if (charnum < 0)
            charnum = 2;

        GameManager.instance.EffectSound(0);
        CharecterInfo();
    }

    //캐릭터 정보표시
    void CharecterInfo()
    {
        switch (charnum)
        {
            case 0:
                nameText.text = "<color=#00A0FF>타나세 쵸비</color>";
                infoText.text = "종족 : 남극 여우\r\n생일 : 10월29일\r\n나이 : 2짤\r\nMBTI : INTP\r\n";
                break;
            case 1:
                nameText.text = "<color=#AC4CAE>오새벽</color>";
                infoText.text = "종족 : 마족\r\n생일 : 12월24일\r\n나이 : 17세\r\nMBTI : ENFP\r\n";
                break;
            case 2:
                nameText.text = "<color=#EC566D>유채린</color>";
                infoText.text = "예명 : 체리\r\n생일 : 10월02일\r\n나이 : 2학년4반\r\nMBTI : ISFP\r\n";
                break;
        }
        charImage.sprite = charUISprites[charnum];
    }
    public void SelectChar()
    {
        GameDataManager.Instance.lastCharecter = (Charecter)charnum;
        CharecterManager.instance.CharecterSpreit();
        GameManager.instance.EffectSound(0);
    }
}
