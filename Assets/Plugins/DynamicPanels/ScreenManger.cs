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

    private int N = 0;

    public RenderTexture screenshotTex;


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
        panel.ResizeTo(new Vector2(700f, 700f));


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
