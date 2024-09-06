using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Camera))]
public class TransparentWindow : MonoBehaviour
{
    public static TransparentWindow Main = null;
    public static Camera Camera = null; //Used instead of Camera.main

    [Tooltip("What GameObject layers should trigger window focus when the mouse passes over objects?")] //
    [SerializeField] LayerMask clickLayerMask = ~0;

    [Tooltip("Allows Input to be detected even when focus is lost")] //
    [SerializeField] bool useSystemInput = false;

    [Tooltip("Should the window be fullscreen?")] //
    [SerializeField] bool fullscreen = true;

    [Tooltip("Force the window to match ScreenResolution")] //
    [SerializeField] bool customResolution = true;

    [Tooltip("Resolution the overlay should run at")] //
    [SerializeField] Vector2Int screenResolution = new Vector2Int(1280, 720);

    [Tooltip("The framerate the overlay should try to run at")] //
    [SerializeField] int targetFrameRate = 60;


    /////////////////////
    //Windows DLL stuff//
    /////////////////////

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

    [DllImport("user32.dll")]
    static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImportAttribute("user32.dll")]
    static extern bool ReleaseCapture();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    static extern int SetWindowPos(IntPtr hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Rectangle margins);


    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const int HWND_TOPMOST = -1;
    const int HWND_NOTOPMOST = -2;

    const int WM_SYSCOMMAND = 0x112;
    const int WM_MOUSE_MOVE = 0xF012;

    int fWidth;
    int fHeight;
    IntPtr hwnd = IntPtr.Zero;
    Rectangle margins;
    Rectangle windowRect;

    [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
    static extern int GetSystemMetrics(int nIndex);

    const int SM_CYCAPTION = 4; // Title bar height

    //BUG: Sometimes fails to SetResolution if not focused on startup - if using Start(), WindowBoundsCollider2D sometimes fails to set the correct size
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Main = this;

        Camera = GetComponent<Camera>();
        Camera.backgroundColor = new Color();
        Camera.clearFlags = CameraClearFlags.SolidColor;

        if (fullscreen && !customResolution)
        {
            screenResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        }
        screenResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        Screen.SetResolution(screenResolution.x, screenResolution.y, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

        Application.targetFrameRate = targetFrameRate;
        Application.runInBackground = true;

#if !UNITY_EDITOR
		fWidth = screenResolution.x;
		int titleBarHeight = GetSystemMetrics(SM_CYCAPTION);
		fHeight = screenResolution.y-40;
		margins = new Rectangle() {Left = -1};
		hwnd = GetActiveWindow();

		if (GetWindowRect(hwnd, out windowRect))
		{
			Debug.LogError("Couldn't get Window Rect");
		}

		SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
		SetWindowPos(hwnd, HWND_TOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);
		DwmExtendFrameIntoClientArea(hwnd, ref margins);
#endif
    }

    private void Start()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Update()
    {
        if (useSystemInput)
        {
            SystemInput.Process();
        }
        if (GameManager.instance != null)
        {
            SetClickThrough();
        }
    }

    int currentdisplayNum = 0;
    public void MoveWindowToNextDisplay(int value)
    {
        if (value < 0 || value >= Display.displays.Length)
        {
            Debug.LogError("Invalid display index.");
            return;
        }

        int currentX = Display.displays[currentdisplayNum].systemWidth;
        int newX = Display.displays[value].systemWidth;
        int minas = currentX - newX < 0 ? -1 : 1;

        newX = 0;
        for (int i = 0; i < value; i++)
        {
            newX += Display.displays[i].renderingWidth * minas;
        }

        screenResolution = new Vector2Int(Display.displays[value].systemWidth, Display.displays[value].systemHeight);
        Screen.SetResolution(screenResolution.x, screenResolution.y, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
        currentdisplayNum = value;
#if !UNITY_EDITOR
		fWidth = screenResolution.x;
		int titleBarHeight = GetSystemMetrics(SM_CYCAPTION);
		fHeight = screenResolution.y-40;
		margins = new Rectangle() {Left = -1};
		hwnd = GetActiveWindow();

		if (GetWindowRect(hwnd, out windowRect))
		{
			Debug.LogError("Couldn't get Window Rect");
		}

		SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
		SetWindowPos(hwnd, GameManager.instance.isMostTop ? HWND_TOPMOST : HWND_NOTOPMOST, newX, windowRect.Top, fWidth, fHeight, 32 | 64);
		DwmExtendFrameIntoClientArea(hwnd, ref margins);
#endif
    }


    //Returns true if the cursor is over a UI element or 2D physics object
    bool FocusForInput()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem && eventSystem.IsPointerOverGameObject())
        {
            return true;
        }

        Vector2 pos = Camera.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.OverlapPoint(pos, clickLayerMask);
    }

    void SetClickThrough()
    {
        var focusWindow = FocusForInput();

        //Get window position
        GetWindowRect(hwnd, out windowRect);

#if !UNITY_EDITOR
		if (focusWindow)
		{
			SetWindowLong (hwnd, -20, ~(((uint)524288) | ((uint)32)));
			SetWindowPos(hwnd, GameManager.instance.isMostTop ? HWND_TOPMOST : HWND_NOTOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);
		}
		else
		{
			SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
			SetWindowLong (hwnd, -20, (uint)524288 | (uint)32);
			SetLayeredWindowAttributes (hwnd, 0, 255, 2);
			SetWindowPos(hwnd, GameManager.instance.isMostTop ? HWND_TOPMOST : HWND_NOTOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);
		}
#endif
    }

    public static void DragWindow()
    {
#if !UNITY_EDITOR
		if (Screen.fullScreenMode != FullScreenMode.Windowed)
		{
			return;
		}
		ReleaseCapture ();
		SendMessage(Main.hwnd, WM_SYSCOMMAND, WM_MOUSE_MOVE, 0);
		Input.ResetInputAxes();
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}