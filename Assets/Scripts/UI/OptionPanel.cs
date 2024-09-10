using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Slider bgmSoundSlider;
    public Slider effectSoundSlider;
    public Toggle MostTopToggle;

    public TMP_Dropdown dropdown;

    private void Start()
    {
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < Display.displays.Length; i++)
        {
            optionList.Add(new TMP_Dropdown.OptionData($"화면 {i}"));
        }
        dropdown.AddOptions(optionList);
        Camera.main.GetComponent<TransparentWindow>().MoveWindowToNextDisplay(0);
        UIRest();

    }

    public void UIRest()
    {
        bgmSoundSlider.value = GameManager.instance.bgmSound;
        effectSoundSlider.value = GameManager.instance.effectSound;
        MostTopToggle.isOn = GameManager.instance.isMostTop;
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
    public void ChangedDisPlay()
    {
        var _camera = Camera.main;
        _camera.GetComponent<TransparentWindow>().MoveWindowToNextDisplay(dropdown.value);
        if (!_camera.GetComponent<TransparentWindow>().isVertical)
        {
            _camera.orthographicSize = 10 - (2.5f * GameManager.instance.zoomLevel);
            _camera.transform.position = new Vector3(4.5f * GameManager.instance.zoomLevel, -2.5f * GameManager.instance.zoomLevel);
        }
        else
        {
            //카메라사이즈12 - 21 - 30
            //제일작은게 10,2 - 5,11 - 0,20
            _camera.orthographicSize = 21 - (4.5f * GameManager.instance.zoomLevel);
            _camera.transform.position = new Vector3(5 + (2.5f * GameManager.instance.zoomLevel), 11 - (4.5f * GameManager.instance.zoomLevel));
        }
    }

    public void ChangeCharecter()
    {
        Managers.Resource.Instantiate("PopupUI/CharectersChange", MainCanvasManager.Instance.transform, 1);
    }

}
