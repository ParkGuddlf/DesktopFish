using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static Define;

public class CharecterChangePanel : MonoBehaviour
{
    public void Change(int num)
    {
        GameDataManager.Instance.lastCharecter = (Charecter)num;
        CharecterManager.instance.CharecterSpreit();
        Managers.Resource.Destroy(gameObject);
    }    
}
