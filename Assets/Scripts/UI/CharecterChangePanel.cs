using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class CharecterChangePanel : MonoBehaviour
{

    [SerializeField] Image charImage;
    [Header("ĳ���� ���� �̹���")]
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

    //ĳ���� ����ǥ��
    void CharecterInfo()
    {
        switch (charnum)
        {
            case 0:
                nameText.text = "<color=#001EFF>Ÿ���� �ݺ�</color>";
                infoText.text = "���� : ���� ����\r\n���� : 10��29��\r\n���� : 2��\r\nMBTI : INTP\r\n�ҳ��� : �Ӷ���\r\n";
                break;
            case 1:
                nameText.text = "<color=#41035A>������</color>";
                infoText.text = "���� : ����\r\n���� : 12��24��\r\n���� : 17��\r\nMBTI : ENFP\r\n�ҳ��� : ũ����\r\n";
                break;
            case 2:
                nameText.text = "<color=#CF354A>��ä��</color>";
                infoText.text = "���� : ü��\r\n���� : 10��02��\r\n���� : 24��\r\nMBTI : ISFP\r\n�ҳ��� : ä����\r\n";
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
