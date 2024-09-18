using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField] Sprite[] sprites;

    public bool clear;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        //StartCoroutine(SpriteChange());
    }

    WaitForSeconds delay = new WaitForSeconds(0.1f);
    float time;
    int spriteNum;
    private void Update()
    {
        if (clear.Equals(false))
            return;

        time += Time.deltaTime;
        if (time > 0.1f)
        {
            image.sprite = sprites[spriteNum];
            spriteNum++;
            time = 0;
            if (spriteNum >= sprites.Length)
                spriteNum = 0;
        }
    }

    IEnumerator SpriteChange()
    {
        int i = 0;
        while (true)
        {
            image.sprite = sprites[i];
            yield return delay;
            i++;
            if (i >= sprites.Length)
                i = 0;
        }
    }

    public void VisionChallenge(bool isClear)
    {
        if (!isClear)
        {
            image.color = Color.black;
            image.sprite = sprites[0];
        }
        else
        {
            image.color = Color.white;
            clear = isClear;
        }
    }
}
