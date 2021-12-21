using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_fistpalm : GestureWidget2Phase
{
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = now - old;
        Vector3 up = new Vector3(0f, 1f, 0f);
        return (delta.magnitude > 0.001 && Vector3.Angle(delta, up) < 10.0f);
        //return HandPoseUtils.IsThumbGrabbing(_handedness) &&
        //    HandPoseUtils.IsMiddleGrabbing(_handedness) &&
        //    HandPoseUtils.IsIndexGrabbing(_handedness) &&
        //    IsPinkyGrabbing(_handedness) &&
        //    IsRingGrabbing(_handedness);
    }

    public override bool GestureCondition1()
    {
        return HandPoseUtils.IsThumbGrabbing(_handedness) &&
           HandPoseUtils.IsMiddleGrabbing(_handedness) &&
           HandPoseUtils.IsIndexGrabbing(_handedness) &&
           IsPinkyGrabbing(_handedness) &&
           IsRingGrabbing(_handedness);
    }

    public override bool GestureCondition2()
    {
        return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
            !HandPoseUtils.IsMiddleGrabbing(_handedness) &&
            !HandPoseUtils.IsIndexGrabbing(_handedness) &&
            !IsPinkyGrabbing(_handedness) &&
            !IsRingGrabbing(_handedness);
    }
}