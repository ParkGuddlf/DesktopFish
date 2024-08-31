using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Define;

public class TEsttest : MonoBehaviour
{
    public float dayLength = 10.0f;
    public float nightLength = 10.0f;

    public Color dayColor = new Color(1.0f, 1.0f, 1.0f);
    public Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    public Color raindayColor = new Color(0.45f, 0.45f, 0.7f);
    public Color rainnightColor = new Color(0.05f, 0.05f, 0.05f);
    public Color snowdayColor = new Color(0.7f, 0.7f, 0.7f);
    public Color snownightColor = new Color(0.1f, 0.1f, 0.1f);

    private Light2D light2D;

    [SerializeField] GameObject nightLight;

    [SerializeField]
    GameObject[] weatherParticle;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        switch (GameDataManager.Instance.lastWeather)
        {
            case Weather.sun:
                light2D.color = GameDataManager.Instance.dayNight == DateTime.morning ? dayColor : nightColor;
                break;
            case Weather.rain:
                light2D.color = GameDataManager.Instance.dayNight == DateTime.morning ? raindayColor : rainnightColor;
                break;
            case Weather.snow:
                light2D.color = GameDataManager.Instance.dayNight == DateTime.morning ? snowdayColor : snownightColor;
                break;
            default:
                light2D.color = GameDataManager.Instance.dayNight == DateTime.morning ? dayColor : nightColor;
                break;
        }
    }

    void Update()
    {
        if (GameDataManager.Instance.dayTimer > 55)
        {
            light2D.color = Color.Lerp(light2D.color, TargetColor(), 0.05f);
        }
        switch (GameDataManager.Instance.lastWeather)
        {
            case Weather.sun:
                weatherParticle[0].SetActive(false);
                weatherParticle[1].SetActive(false);
                break;
            case Weather.rain:
                weatherParticle[0].SetActive(true);
                weatherParticle[1].SetActive(false);
                break;
            case Weather.snow:
                weatherParticle[0].SetActive(false);
                weatherParticle[1].SetActive(true);
                break;
            default:
                weatherParticle[0].SetActive(false);
                weatherParticle[1].SetActive(false);
                break;
        }
    }

    Color TargetColor()
    {
        nightLight.SetActive(GameDataManager.Instance.dayNight == DateTime.morning);
        switch (GameDataManager.Instance.lastWeather)
        {
            case Weather.sun:
                return GameDataManager.Instance.dayNight != DateTime.morning ? dayColor : nightColor;
            case Weather.rain:
                return GameDataManager.Instance.dayNight != DateTime.morning ? raindayColor : rainnightColor;
            case Weather.snow:
                return GameDataManager.Instance.dayNight != DateTime.morning ? snowdayColor : snownightColor;
            default:
                return GameDataManager.Instance.dayNight != DateTime.morning ? dayColor : nightColor;
        }
    }
}
