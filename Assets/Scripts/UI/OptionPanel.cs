using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Slider bgmSoundSlider;
    public Slider effectSoundSlider;
    public Toggle MostTopToggle;
    public Toggle DobbleToggle;

    private void Start()
    {
        UIRest();
    }

    public void UIRest()
    {
        bgmSoundSlider.value = GameManager.instance.bgmSound;
        effectSoundSlider.value = GameManager.instance.effectSound;
        MostTopToggle.isOn = GameManager.instance.isMostTop;
        DobbleToggle.isOn = GameManager.instance.dubbleMode;
    }

    public void SoundSliderMetherd(string soundname)
    {
        if (soundname == "bgm")
            GameManager.instance.bgmSound = bgmSoundSlider.value;
        else
            GameManager.instance.effectSound = effectSoundSlider.value;
    }

    public void MostTop()
    {
        GameManager.instance.isMostTop = MostTopToggle.isOn;
    }
    public void DobbleMode()
    {
        GameManager.instance.dubbleMode = DobbleToggle.isOn;
    }
    public void ChangedDisPlay()
    {
        Camera.main.GetComponent<TransparentWindow>().MoveWindowToNextDisplay();
    }
}
