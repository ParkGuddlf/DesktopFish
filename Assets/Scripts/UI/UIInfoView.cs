using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoView : UIInfoData
{
    [SerializeField] int num;
    public override void DatainfoChange()
    {
        if (num == 0)
            data_info = $"ä���ӵ��� ����մϴ�.\n���� ���� : {GameDataManager.Instance.castingLevel}";
        if (num == 1)
            data_info = $"���ȹ�淮�� ����մϴ�.\n���� ���� : {GameDataManager.Instance.goldLevel}";
        if (num == 2)
            data_info = $"����ȵ���� ���� Ȯ���� ����մϴ�.\n���� ���� : {GameDataManager.Instance.spacialLevel}";
        if (num == 3)
            data_info = $"ĳ�����ֱⰡ �����մϴ�.\n���� ���� : {GameDataManager.Instance.atkDelayLevel}";
    }
}
