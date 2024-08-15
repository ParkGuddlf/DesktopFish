using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoureceManager : MonoBehaviour
{
    // 리소스저장해서 불러오는 용도로 사용
    [Header("생선잡힐때소리")]
    public AudioClip[] fishClips;
    [Header("배경음악")]
    public AudioClip[] bgmClips;
    [Header("유아이소리")]
    public AudioClip[] uiClips;

    [Header("생선이미지")]
    public Sprite[] fishSprites;
    [Header("아이콘이미지")]
    public Sprite[] iconSprites;
}
