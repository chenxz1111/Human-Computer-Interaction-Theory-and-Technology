using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicPanels;
using UnityEngine.UI;
using System.IO;
using Microsoft.MixedReality.Toolkit.Input;

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


    private int N = 0;

    public RenderTexture screenshotTex;
    public List<Panel> panel_list = new List<Panel>();


    void Start()
    {
        //important
        PanelNotificationCenter.OnPanelClosed += closePanel;
        PanelNotificationCenter.OnTabClosed += closeTab;

        createPanel(imagePanel, new Vector2(400f, 300f), laptop_canvas1);
        createPanel(imagePanel, new Vector2(400f, 300f), laptop_canvas2);
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
        createPanel(videoPanel, new Vector2(1600f, 1200f), main_canvas);
    }
    public void playGame()
    {
        if (!gameOn)
            canvas2048 = createPanel(gamePanel, new Vector2(700f, 700f), main_canvas).transform.Find("Content").Find("GamePanel(Clone)").Find("2048").Find("Canvas");
        gameOn = true;
    }

    public Panel createPanel(RectTransform c, Vector2 initSize, DynamicPanelsCanvas canvas)
    {
        RectTransform content;
        if (c)
            content = Instantiate(c) as RectTransform;
        else
            content = Instantiate(new_content) as RectTransform;
        //进行对象复制
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

}
