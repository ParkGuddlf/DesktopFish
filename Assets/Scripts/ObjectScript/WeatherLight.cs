using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.Rendering.Universal;

//시간마다 나오는 이벤트
public class WeatherLight : MonoBehaviour
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

    GameDataManager gameDataManager;

    void Start()
    {
        gameDataManager = GameDataManager.Instance;
        light2D = GetComponent<Light2D>();
        switch (gameDataManager.lastWeather)
        {
            case Weather.sun:
                light2D.color = gameDataManager.dayNight == DateTime.morning ? dayColor : nightColor;
                break;
            case Weather.rain:
                light2D.color = gameDataManager.dayNight == DateTime.morning ? raindayColor : rainnightColor;
                break;
            case Weather.snow:
                light2D.color = gameDataManager.dayNight == DateTime.morning ? snowdayColor : snownightColor;
                break;
            default:
                light2D.color = gameDataManager.dayNight == DateTime.morning ? dayColor : nightColor;
                break;
        }
    }
    private void Update()
    {
        if (GameManager.instance.isStart)
        {
            gameDataManager.weatherTimer += Time.deltaTime;
            gameDataManager.dayTimer += Time.deltaTime;
            gameDataManager.spownTimer += Time.deltaTime;
            if (gameDataManager.weatherTimer / 120f >= 1)
            {
                gameDataManager.lastWeather = (Weather)Random.Range(1, 4);
                FishingSystem.instance.SetFishCatchPossible();
                gameDataManager.weatherTimer = 0;
            }
            if (gameDataManager.dayTimer / 60f >= 1)
            {
                gameDataManager.dayNight = gameDataManager.dayNight.Equals(DateTime.morning) ? DateTime.night : DateTime.morning;
                FishingSystem.instance.SetFishCatchPossible();
                gameDataManager.dayTimer = 0;
            }
            if (gameDataManager.spownTimer / 30f >= 1)
            {
                Managers.Resource.Instantiate("SeaObject");
                gameDataManager.spownTimer = 0;
            }

            light2D.color = Color.Lerp(light2D.color, TargetColor(), 0.05f);
        }

        switch (gameDataManager.lastWeather)
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
        nightLight.SetActive(gameDataManager.dayNight != DateTime.morning);
        switch (gameDataManager.lastWeather)
        {
            case Weather.sun:
                return gameDataManager.dayNight == DateTime.morning ? dayColor : nightColor;
            case Weather.rain:
                return gameDataManager.dayNight == DateTime.morning ? raindayColor : rainnightColor;
            case Weather.snow:
                return gameDataManager.dayNight == DateTime.morning ? snowdayColor : snownightColor;
            default:
                return gameDataManager.dayNight == DateTime.morning ? dayColor : nightColor;
        }
    }
}
