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
//public class MyCanvas : MonoBehaviour, IPointerEnterHandler
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
//    {
//        OnPointerEnteredCanvas(this, eventData);
//    }

//	public void OnPointerEnteredCanvas(MyCanvas canvas, PointerEventData pointer)
//	{
//		//if (draggedPanel != null && pointer.pointerDrag != null)
//		//{
//		//	PanelHeader header = pointer.pointerDrag.GetComponent<PanelHeader>();
//		//	if (header != null)
//		//	{
//		//		if (header.Panel == draggedPanel && header.Panel.RectTransform.parent != canvas.RectTransform)
//		//		{
//		//			previewPanelCanvas = canvas;

//		//			header.Panel.RectTransform.SetParent(canvas.RectTransform, false);
//		//			header.Panel.RectTransform.SetAsLastSibling();
//		//		}
//		//	}
//		//	else
//		//	{
//		//		PanelTab tab = pointer.pointerDrag.GetComponent<PanelTab>();
//		//		if (tab != null)
//		//		{
//		//			if (tab.Panel == draggedPanel && previewPanel.parent != canvas.RectTransform)
//		//			{
//		//				previewPanelCanvas = canvas;

//		//				if (hoveredAnchorZone && hoveredAnchorZone.Panel.Canvas != canvas)
//		//					hoveredAnchorZone.OnPointerExit(pointer);

//		//				previewPanel.SetParent(canvas.RectTransform, false);
//		//				previewPanel.SetAsLastSibling();
//		//			}
//		//		}
//		//	}
//		//}
//	}
//}
