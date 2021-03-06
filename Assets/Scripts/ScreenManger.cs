using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicPanels;
using UnityEngine.UI;
using System.IO;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Video;

public class ScreenManger : MonoBehaviour
{
    public RectTransform new_content;
    public DynamicPanelsCanvas main_canvas;
    public DynamicPanelsCanvas back_canvas;
    public DynamicPanelsCanvas laptop_canvas1;
    public DynamicPanelsCanvas laptop_canvas2;
    public RectTransform videoPanel;
    public RectTransform imagePanel;
    public RectTransform textPanel;
    public RectTransform gamePanel;
    public GazeProvider gazeProvider;

    public GameObject clock;
    public GameObject weather;
    bool videoOn = false;
    bool gameOn = false;
    Panel gamePanelInstance = null;
    Transform canvas2048 = null;
    public Panel videoPanelInstance = null;
    public VideoPlayer videoPlayer = null;


    private int N = 0;

    public RenderTexture screenshotTex;
    public List<Panel> panel_list = new List<Panel>();


    void Start()
    {
        //important
        PanelNotificationCenter.OnPanelClosed += closePanel;
        PanelNotificationCenter.OnTabClosed += closeTab;

        //createPanel(imagePanel, new Vector2(800f, 600f), laptop_canvas1);
        createPanel(imagePanel, new Vector2(800f, 600f), laptop_canvas2);
        PanelDrag.gaze = gazeProvider;
        clock.SetActive(false);
        weather.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createText()
    {
        createPanel(textPanel, new Vector2(600f, 600f), main_canvas);
    }
    public void createImage()
    {
        createPanel(imagePanel, new Vector2(800f, 600f), main_canvas);
    }
    public void playVideo()
    {
        if (!videoOn)
        {
            videoPanelInstance = createPanel(videoPanel, new Vector2(1600f, 1200f), main_canvas);
            videoPlayer = videoPanelInstance.transform.Find("Content").Find("VideoPanel(Clone)").GetComponent<VideoPlayer>();
            transform.GetComponent<RoomMode>().toRelaxMode();
        }

        videoOn = true;
    }
    public void playGame()
    {
        if (!gameOn) {
            gamePanelInstance = createPanel(gamePanel, new Vector2(700f, 700f), main_canvas);
            canvas2048 = gamePanelInstance.transform.Find("Content").Find("GamePanel(Clone)").Find("2048").Find("Canvas");
        }
        gameOn = true;
    }

    public Panel createPanel(RectTransform c, Vector2 initSize, DynamicPanelsCanvas canvas)
    {
        RectTransform content;
        if (c)
            content = Instantiate(c) as RectTransform;
        else
            content = Instantiate(new_content) as RectTransform;
        //????????????
        //RectTransform content = Instantiate(new_content) as RectTransform;
        //e.transform.position = Vector3.zerobao

        //RectTransform content = new RectTransform();
        string tabLabel = "Panel " + N;
        Panel panel = PanelUtils.CreatePanelFor(content, canvas);
        panel[0].Label = tabLabel;
        panel[0].MinSize = new Vector2(200f, 200f);
        panel.ResizeTo(initSize);


        //PanelNotificationCenter.OnPanelClosed;
        int cycle_N = N++ % 8;
        // panel.MoveToPosition(new Vector2(panel.Size.x + 30 * cycle_N, main_canvas.Size.y - panel.Size.y - 100 - 30 * cycle_N));
        panel.MoveToPosition(new Vector2(30 * cycle_N + 100, canvas.Size.y - panel.Size.y - 30 * cycle_N - 100));
        //panel.MoveToPosition(new Vector2(30 * cycle_N, 30 * cycle_N));

        panel.gameObject.AddComponent<PanelDrag>();
        panel.gameObject.AddComponent<BoxCollider>();
        BoxCollider collider = panel.gameObject.GetComponent<BoxCollider>();
        collider.size = new Vector3(initSize.x, initSize.y, 0.001f);
        collider.center = initSize / 2;

        panel_list.Add(panel);
        return panel;
    }
    public void closePanelOnFocus()
    {
        for(int i = 0; i < panel_list.Count; i++)
        {
            if (panel_list[i].transform.GetComponent<PanelDrag>().focus)
            {
                Panel panelToClose = panel_list[i];
                if (gamePanelInstance == panelToClose)
                {
                    gameOn = false;
                    gamePanelInstance = null;
                }
                if (videoPanelInstance == panelToClose)
                {
                    videoOn = false;
                    videoPanelInstance = null;
                }
                panel_list.RemoveAt(i);
                closePanel(panelToClose);
                break;
            }
        }
    }

    private void closePanel(Panel panel)
    {
        //Destroy(panel);
        for (int i = panel.NumberOfTabs - 1; i >= 0; i--)
            panel[i].Destroy();
    }
    private void closeTab(PanelTab tab)
    {
        tab.Destroy();
    }

    public void mainScreenOn()
    {
        Transform canvas = this.transform.Find("UI_main").Find("Canvas");
        canvas.gameObject.SetActive(true);
    }

    public void mainScreenOff()
    {
        Transform canvas = this.transform.Find("UI_main").Find("Canvas");
        canvas.gameObject.SetActive(false);
    }

    public void showTime()
    {
        clock.SetActive(true);
    }
    public void hideTime()
    {
        clock.SetActive(false);
    }
    public void showWeather()
    {
        weather.SetActive(true);
    }
    public void hideWeather()
    {
        weather.SetActive(false);
    }

    public void gameUp() { if(gameOn) canvas2048.GetComponent<Smile2048>().onUp(); }
    public void gameDown() { if (gameOn) canvas2048.GetComponent<Smile2048>().onDown(); }
    public void gameLeft() { if (gameOn) canvas2048.GetComponent<Smile2048>().onLeft(); }
    public void gameRight() { if (gameOn) canvas2048.GetComponent<Smile2048>().onRight(); }

    public void videoPlay() { if (videoOn && (!videoPlayer.isPlaying)) videoPlayer.Play(); }
    public void videoPause() { if (videoOn && videoPlayer.isPlaying) videoPlayer.Pause(); }

    public void broadcast()
    {
        for (int i = 0; i < panel_list.Count; i++)
        {
            if (panel_list[i].transform.GetComponent<PanelDrag>().focus)
            {
                RectTransform rect = panel_list[i].transform.Find("Content").GetChild(0).GetComponent<RectTransform>();
                createPanel(rect, new Vector2(800f, 600f), laptop_canvas1);
                createPanel(rect, new Vector2(800f, 600f), laptop_canvas2);
                break;
            }
        }
    }
}
