using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomIn : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] CanvasScaler scaler;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Zoom_IN();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Zoom_OUT();

    }

    public void Zoom_IN()
    {
        if (GameManager.instance.zoomLevel < 2)
        {
            GameManager.instance.zoomLevel++;
            CanvasScale();
        }
    }
    public void Zoom_OUT()
    {
        if (GameManager.instance.zoomLevel > -2)
        {
            GameManager.instance.zoomLevel--;
            CanvasScale();
        }
    }

    void CanvasScale()
    {

        if (!_camera.GetComponent<TransparentWindow>().isVertical)
        {
            _camera.orthographicSize = 10 - (2.5f * GameManager.instance.zoomLevel);
            _camera.transform.position = new Vector3(4.5f * GameManager.instance.zoomLevel, -2.5f * GameManager.instance.zoomLevel);
        }
        else
        {
            _camera.orthographicSize = 21 - (4.5f * GameManager.instance.zoomLevel);
            _camera.transform.position = new Vector3(5 + (2.5f * GameManager.instance.zoomLevel), 11 - (4.5f * GameManager.instance.zoomLevel));
        }
        switch (GameManager.instance.zoomLevel)
        {
            case -2:
                scaler.referenceResolution = new Vector2(2560, 1440);
                break;
            case -1:
                scaler.referenceResolution = new Vector2(2560, 1440);
                break;
            case 0:
                scaler.referenceResolution = new Vector2(1920, 1080);
                break;
            case 1:
                scaler.referenceResolution = new Vector2(1280, 720);

                break;
            case 2:
                scaler.referenceResolution = new Vector2(960, 540);
                break;
        }
    }
}
