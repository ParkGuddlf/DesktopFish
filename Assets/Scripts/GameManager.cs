﻿using SaveIsEasy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : BaseScene
{
    //싱글톤
    public static GameManager instance;

    private Scene selected;
    [Tooltip("항상 상단")]
    public bool isMostTop = true;
    [Range(0, 1)]
    public float bgmSound;
    [Range(0, 1)]
    public float effectSound;
    [Tooltip("배속")]
    public bool dubbleMode;
    public int screenWidth;
    [Tooltip("최소 0 최대 784")]
    public int screenHeight;
    //확대 단계
    [Range(-2, 2)]
    public int zoomLevel;

    public AudioSource bgmAudioSource;
    public AudioSource effectAudioSource;

    public bool isStart;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        LoadGamemanager();
        selected = SceneManager.GetSceneAt(0);
        bgmAudioSource.volume = bgmSound;
        effectAudioSource.volume = effectSound;
    }
    private void Update()
    {
        bgmAudioSource.volume = bgmSound;
        effectAudioSource.volume = effectSound;            
    }
    //불러오기 버튼
    public void Load()
    {
        SaveIsEasyAPI.LoadAll(selected);
        StartCoroutine(SpanwSetDataCo());
        StartCoroutine(MainCanvasManager.Instance.SetStoreInfo());
        //데이터매니저에서 전정된값들 불러와서 지정해주기
        GameDataManager.Instance.runTimeSecond = (int)SaveIsEasyAPI.ListOfValidSaves()[0].StatisticsTotalTimeInSecondsAsTimeSpan.TotalSeconds;
        
    }
    //캐릭터생성 및 데이터 초기화
    IEnumerator SpanwSetDataCo()
    {
        yield return new WaitForSeconds(0.1f);
        GameDataManager.Instance.SpawnCharecter();
        FishingSystem.instance.SetRodStatePercentage(GameDataManager.Instance.currentRod);
        CharecterManager.instance.castingSpeed = 10.5f - GameDataManager.Instance.castingLevel*0.5f;
        CharecterManager.instance.attackDelay = 4.1f - GameDataManager.Instance.atkDelayLevel * 0.1f;
        FishingSystem.instance.SetFishCatchPossible();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveQuit()
    {
        StartCoroutine(SaveGamemanager());
    }

    public void SaveButton()
    {
        Managers.Resource.Instantiate("PopupUI/Popup",MainCanvasManager.Instance.transform).GetComponent<PopupBase>().Textchange("데이터 저장이 완료 되었습니다.");
        SaveIsEasyAPI.SaveAll(selected);
    }

    private IEnumerator SaveGamemanager()
    {
        var popup = Managers.Resource.Instantiate("PopupUI/Popup", MainCanvasManager.Instance.transform);
        popup.GetComponent<PopupBase>().Textchange("데이터 저장이 완료 되었습니다.");

        //저장중 화면 띄우기
        PlayerPrefs.SetInt("isMostTop", Convert.ToInt16(isMostTop));
        PlayerPrefs.SetFloat("bgmSound", bgmSound);
        PlayerPrefs.SetFloat("effectSound", effectSound);
        PlayerPrefs.SetInt("dubbleMode", Convert.ToInt16(dubbleMode));
        PlayerPrefs.SetInt("screenWidth", screenWidth);
        PlayerPrefs.SetInt("screenHeight", screenHeight);
        SaveIsEasyAPI.SaveAll(selected);
        yield return SaveIsEasyAPI.IsSaved;
        yield return !popup.activeSelf;
        Application.Quit();
    }
    //게임매니저 데이터 불러오기
    private void LoadGamemanager()
    {
        isMostTop = PlayerPrefs.HasKey("isMostTop") ? Convert.ToBoolean(PlayerPrefs.GetInt("isMostTop")) : true;
        bgmSound = PlayerPrefs.HasKey("bgmSound") ? PlayerPrefs.GetFloat("bgmSound") : 0.8f;
        effectSound = PlayerPrefs.HasKey("effectSound") ? PlayerPrefs.GetFloat("effectSound") : 0.4f;
        dubbleMode = PlayerPrefs.HasKey("dubbleMode") ? Convert.ToBoolean(PlayerPrefs.GetInt("dubbleMode")) : false;
        screenWidth = PlayerPrefs.HasKey("screenWidth") ? PlayerPrefs.GetInt("screenWidth") : 0;
        screenHeight = PlayerPrefs.HasKey("screenHeight") ? PlayerPrefs.GetInt("screenHeight") : 0;
    }

    public override void Clear()
    {
        instance.Clear();
    }

    public void EffectSound(int clipNum)
    {

        effectAudioSource.clip = GameDataManager.Instance.resoureceManager.uiClips[clipNum];
        effectAudioSource.Play();
    }
}
