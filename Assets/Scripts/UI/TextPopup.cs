using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public void textChange(string text)
    {
        StartCoroutine(textCo(text));
    }

    IEnumerator textCo(string text)
    {
        _text.text = text;
        for (int i = 0; i < 5; i++)
        {
            transform.position += Vector3.up * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Managers.Resource.Destroy(gameObject);
    }
}
