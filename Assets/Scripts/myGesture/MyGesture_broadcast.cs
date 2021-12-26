using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_broadcast : GestureWidget2Phase
{
    public GazeProvider gaze;
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
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, _handedness, out var thumbtip)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, _handedness, out var indextip)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, _handedness, out var palmPose))
            return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
                !HandPoseUtils.IsMiddleGrabbing(_handedness) &&
                !HandPoseUtils.IsIndexGrabbing(_handedness) &&
                !IsPinkyGrabbing(_handedness) &&
                !IsRingGrabbing(_handedness) && (Vector3.Angle(Vector3.Cross(thumbtip.Position - palmPose.Position, indextip.Position - palmPose.Position), gaze.GazeDirection) > 120f);
        return false;
    }
}