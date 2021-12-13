using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicPanels;

public class ScreenManger : MonoBehaviour
{
    public RectTransform new_content;
    public DynamicPanelsCanvas main_canvas;

    private int N = 0;


    void Start()
    {
        //important
        PanelNotificationCenter.OnPanelClosed += closePanel;
        PanelNotificationCenter.OnTabClosed += closeTab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPanel(RectTransform c)
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
        Panel panel = PanelUtils.CreatePanelFor(content, main_canvas);
        panel[0].Label = tabLabel;
        panel[0].MinSize = new Vector2(200f, 200f);
        panel.ResizeTo(new Vector2(300f, panel.MinSize.y));


        //PanelNotificationCenter.OnPanelClosed;
        int cycle_N = N++ % 8;
        // panel.MoveToPosition(new Vector2(panel.Size.x + 30 * cycle_N, main_canvas.Size.y - panel.Size.y - 100 - 30 * cycle_N));
        panel.MoveToPosition(new Vector2(30 * cycle_N + 100, main_canvas.Size.y - panel.Size.y - 30 * cycle_N - 100));
        //panel.MoveToPosition(new Vector2(30 * cycle_N, 30 * cycle_N));
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
}
