using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicPanels;
using UnityEngine.UI;
using System.IO;

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


    private int N = 0;

    public RenderTexture screenshotTex;
    static Vector2 initSize = new Vector2(700f, 700f);


    void Start()
    {
        //important
        PanelNotificationCenter.OnPanelClosed += closePanel;
        PanelNotificationCenter.OnTabClosed += closeTab;

        createPanel(imagePanel, new Vector2(400f, 300f), laptop_canvas1);
        createPanel(imagePanel, new Vector2(400f, 300f), laptop_canvas2);
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
        createPanel(videoPanel, new Vector2(2400f, 1000f), main_canvas);
    }
    public void playGame()
    {
        createPanel(gamePanel, new Vector2(700f, 700f), main_canvas);
    }



    public void createPanel(RectTransform c, Vector2 initSize, DynamicPanelsCanvas canvas)
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

    public void screenOn(int UI_id)
    {
        Transform canvas = this.transform.Find("UI" + UI_id.ToString()).Find("Canvas");
        canvas.gameObject.SetActive(true);
    }

    public void screenOff(int UI_id = 0)
    {
        Transform canvas = this.transform.Find("UI" + UI_id.ToString()).Find("Canvas");
        canvas.gameObject.SetActive(false);
    }

    public void OnShowTexture(RectTransform rect)
    {

        ///要现实的位置 RrctTransform, 转化到屏幕坐标。
        Vector3 vect = RectTransformUtility.WorldToScreenPoint(Camera.main, rect.gameObject.transform.position);
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();

        float radio = Screen.width / canvasScaler.referenceResolution.x;//适配
        float x = vect.x - rect.sizeDelta.x * rect.pivot.x * radio;
        float y = vect.y - rect.sizeDelta.y * rect.pivot.x * radio;


        Rect targetRect = new Rect(x, y, rect.sizeDelta.x * radio, rect.sizeDelta.y * radio);



        //Debug.Log(string.Format("x:{0}, y:{1}, width:{2}, height:{3}", targetRect.x, targetRect.y, targetRect.width, targetRect.height));

        //StartCoroutine(UploadPNG());

        //yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D((int)targetRect.width, (int)targetRect.height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect((int)targetRect.x, (int)targetRect.y, (int)targetRect.width, (int)targetRect.height), 0, 0);
        tex.Apply();

        //screenshotTex.

        string path = "Assets/Plugins/DynamicPanels/onMobileSavedScreen.png";
        Debug.Log(path);
        File.WriteAllBytes(path, tex.EncodeToPNG());


    }

}
