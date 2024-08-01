using SaveIsEasy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Scene selected;

    public bool isMostTop = true;
    [Range(0, 1)]
    public float bgmSound;
    [Range(0, 1)]
    public float effectSound;
    public bool dubbleMode;
    public int screenWidth;
    public int screenHeight;

    [Range(-2, 2)]
    public int zoomLevel;

    public AudioSource bgmAudioSource;
    public AudioSource effectAudioSource;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        LoadGamemanager();
        selected = SceneManager.GetSceneAt(0);
        bgmAudioSource.volume = bgmSound;
        effectAudioSource.volume = effectSound;
    }

    public void Load()
    {
        SaveIsEasyAPI.LoadAll(selected);
        StartCoroutine(spanwco());
        GameDataManager.Instance.SetFishData();
    }

    IEnumerator spanwco()
    {
        yield return new WaitForSeconds(0.1f);
        GameDataManager.Instance.SpawnCharecter();
    }

    public void Quit()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        StartCoroutine(SaveGamemanager());
    }
    private IEnumerator SaveGamemanager()
    {
        PlayerPrefs.SetInt("isMostTop", Convert.ToInt16(isMostTop));
        PlayerPrefs.SetFloat("bgmSound", bgmSound);
        PlayerPrefs.SetFloat("effectSound", effectSound);
        PlayerPrefs.SetInt("dubbleMode", Convert.ToInt16(dubbleMode));
        PlayerPrefs.SetInt("screenWidth", screenWidth);
        PlayerPrefs.SetInt("screenHeight", screenHeight);
        yield return new WaitForSeconds(3f);
    }

    private void LoadGamemanager()
    {
        isMostTop = PlayerPrefs.HasKey("isMostTop") ? Convert.ToBoolean(PlayerPrefs.GetInt("isMostTop")) : true;
        bgmSound = PlayerPrefs.HasKey("bgmSound") ? PlayerPrefs.GetFloat("bgmSound") : 0.8f;
        effectSound = PlayerPrefs.HasKey("effectSound") ? PlayerPrefs.GetFloat("effectSound") : 0.4f;
        dubbleMode = PlayerPrefs.HasKey("dubbleMode") ? Convert.ToBoolean(PlayerPrefs.GetInt("dubbleMode")) : false;
        screenWidth = PlayerPrefs.HasKey("screenWidth") ? PlayerPrefs.GetInt("screenWidth") : 0;
        screenHeight = PlayerPrefs.HasKey("screenHeight") ? PlayerPrefs.GetInt("screenHeight") : 0;
    }
}
