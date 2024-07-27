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
	public float sound;
	public bool isMute;
	public bool dubbleMode;
	public int screenWidth;
	public int screenHeight;
	//확대 축소
	//각자 유튜브 이동하는 버튼이라던지
    private void Awake()
    {
        if(instance == null)
			instance = this;
    }

    private void Start()
    {
        selected = SceneManager.GetSceneAt(0);
    }
    public void Load()
    {
        SaveIsEasyAPI.LoadAll(selected);
        StartCoroutine(spanwco());
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
}
