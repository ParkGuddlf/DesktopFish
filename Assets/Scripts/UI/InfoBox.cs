using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] Image image;


    public void ChangeinfoBox(string name, string info, Sprite sprite)
    {
        nameText.text = name;
        infoText.text = info;
        image.sprite = sprite;
    }
}
