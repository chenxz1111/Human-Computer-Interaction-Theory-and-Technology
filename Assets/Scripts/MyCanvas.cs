using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MyCanvas : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    public static MyCanvas canvasOnFocus = null;
    private Color color_IdleState = Color.cyan;
    private Color color_OnHover = Color.white;
    private Color color_OnSelect = Color.blue;
    private Color color_OnTransmit = Color.red;
    private Image image;
    public float dragTimeDelta = 0.6f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        if (gameObject.GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().size = transform.parent.GetComponent<RectTransform>().sizeDelta;
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        if (PanelDrag.timeAfterDrag < dragTimeDelta && this != canvasOnFocus) {
            //image.color = color_OnTransmit;
            Transform panel = PanelDrag.panelOnDrag;
            panel.SetParent(transform);
            panel.localEulerAngles = Vector3.zero;
            panel.localScale = Vector3.one;
            panel.position = eventData.Pointer.Position;
            panel.localPosition -= new Vector3(0, 0, panel.localPosition.z);
            RectTransform rect = panel.GetComponent<RectTransform>();

            // rect.anchoredPosition
            panel.GetComponent<PanelDrag>().Start();
        }
        //else
        //    image.color = color_OnHover;
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        //image.color = color_IdleState;
    }

    void IMixedRealityPointerHandler.OnPointerDown(
         MixedRealityPointerEventData eventData)
    { }

    void IMixedRealityPointerHandler.OnPointerDragged(
         MixedRealityPointerEventData eventData)
    { }

    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //image.color = color_OnSelect;
    }
    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //image.color = color_OnSelect;
    }
}
