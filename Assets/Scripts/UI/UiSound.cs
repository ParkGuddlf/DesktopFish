using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiSound : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.effectAudioSource.clip = GameDataManager.Instance.resoureceManager.uiClips[0];
        GameManager.instance.effectAudioSource.Play();
    }
}
