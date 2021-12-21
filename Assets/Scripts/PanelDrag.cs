using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;
using DynamicPanels;

public class PanelDrag : MonoBehaviour, IMixedRealityPointerHandler
{
    public static float timeAfterDrag = 999;
    public static Transform panelOnDrag = null;
    public bool isPointing;
    public Vector3 downPos;
    public Vector3 dragPos;
    public Vector3 previousPos;
    //public Vector3 res;
    public float speed = 4f;
    // public float decayRate = 0.6f;
    float distanceRatio = 1.0f;
    Vector2 canvasSize;
    static public GazeProvider gaze;
    public bool focus;


    private float pointerRefDistance = 1f;

    private Vector3 pointerLocalGrabPoint;
    private Vector3 objectLocalGrabPoint;
    private Vector3 grabToObject;

    /// <summary>
    /// Setup function
    /// </summary>
    public void Setup(MixedRealityPose pointerCentroidPose, Vector3 grabCentroid, MixedRealityPose objectPose, Vector3 objectScale)
    {

        Quaternion worldToPointerRotation = Quaternion.Inverse(pointerCentroidPose.Rotation);
        pointerLocalGrabPoint = worldToPointerRotation * (grabCentroid - pointerCentroidPose.Position);

        objectLocalGrabPoint = Quaternion.Inverse(objectPose.Rotation) * (grabCentroid - objectPose.Position);
        objectLocalGrabPoint = objectLocalGrabPoint.Div(objectScale);

        //grabToObject = objectPose.Position - grabCentroid;
    }

    public void Start()
    {
        focus = false;
        isPointing = false;
        previousPos = transform.position;
        canvasSize = transform.parent.parent.GetComponent<RectTransform>().sizeDelta;
        Debug.Log("PanelDrag start");

    }

    void Update()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        //float prev_x = rect.anchoredPosition.x, prev_y = rect.anchoredPosition.y;
        if (isPointing)
        {
            transform.position = previousPos + (dragPos - downPos) * speed / distanceRatio;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            //Debug.Log(finalLocalPos.ToString());
            //Debug.Log(distanceRatio);
        }
        Vector2 final_anchor = rect.anchoredPosition;
        //if ((final_anchor.x < 0) || (final_anchor.x > canvasSize.x - rect.sizeDelta.x)) final_anchor.x = prev_x;
        //if ((final_anchor.y < 0) || (final_anchor.y > canvasSize.y - rect.sizeDelta.y)) final_anchor.y = prev_y;
        final_anchor.x = Mathf.Max(0, Mathf.Min(canvasSize.x - rect.sizeDelta.x, final_anchor.x));
        final_anchor.y = Mathf.Max(0, Mathf.Min(canvasSize.y - rect.sizeDelta.y, final_anchor.y));
        rect.anchoredPosition = final_anchor;

        timeAfterDrag += Time.deltaTime;
        if(gaze.GazeTarget == this.gameObject)
        {
            focus = true;
        }
        else
        {
            focus = false;
        }
    }

    private bool withinCanvas(Vector3 pos)
    {
        Vector2 panelSize = transform.GetComponent<RectTransform>().sizeDelta;
        return (pos.x >= 0) && (pos.x <= canvasSize.x - panelSize.x) && (pos.y >= 0) && (pos.y <= canvasSize.y - panelSize.y);
    }

    /// <inheritdoc />
    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        isPointing = false;
        previousPos = transform.position;
        timeAfterDrag = 0;
    }

    /// <inheritdoc />
    void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
    {
        isPointing = true;
        downPos = eventData.Pointer.Position;
        MixedRealityPose pointerPose = new MixedRealityPose(eventData.Pointer.Position, eventData.Pointer.Rotation);
        MixedRealityPose hostPose = new MixedRealityPose(transform.position, transform.rotation);
        Setup(pointerPose, eventData.Pointer.Position, hostPose, transform.localScale);

        MyCanvas.canvasOnFocus = transform.parent.GetComponent<MyCanvas>();
        panelOnDrag = transform;

        transform.GetComponent<Panel>().BringForward();
    }

    private float GetDistanceToBody(MixedRealityPose pointerCentroidPose)
    {
        // The body is treated as a ray, parallel to the y-axis, where the start is head position.
        // This means that moving your hand down such that is the same distance from the body will
        // not cause the manipulated object to move further away from your hand. However, when you
        // move your hand upward, away from your head, the manipulated object will be pushed away.
        if (pointerCentroidPose.Position.y > CameraCache.Main.transform.position.y)
        {
            return Vector3.Distance(pointerCentroidPose.Position, CameraCache.Main.transform.position);
        }
        else
        {
            Vector2 headPosXZ = new Vector2(CameraCache.Main.transform.position.x, CameraCache.Main.transform.position.z);
            Vector2 pointerPosXZ = new Vector2(pointerCentroidPose.Position.x, pointerCentroidPose.Position.z);

            return Vector2.Distance(pointerPosXZ, headPosXZ);
        }
    }
    /// <inheritdoc />
    void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData) {
        dragPos = eventData.Pointer.Position;


        MixedRealityPose pointerCentroidPose = new MixedRealityPose(eventData.Pointer.Position, eventData.Pointer.Rotation);
        Quaternion objectRotation = transform.rotation;
        Vector3 objectScale = transform.localScale;



        // Compute how far away the object should be based on the ratio of the current to original hand distance
        float currentHandDistance = GetDistanceToBody(pointerCentroidPose);
        distanceRatio = currentHandDistance / pointerRefDistance;

        //res = pointerCentroidPose.Position + grabToObject + (pointerCentroidPose.Rotation * pointerLocalGrabPoint) * distanceRatio;

    }

    /// <inheritdoc />
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    {

    }

}

