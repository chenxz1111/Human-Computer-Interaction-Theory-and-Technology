using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;

public class MyGestureT : GestureWidget
{
    private MyGesture_indexup indexup;
    public override bool GestureCondition()
    {
        indexup = new MyGesture_indexup();
        Vector3 down = new Vector3(0f, -1f, 0f);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out var indextip)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out var palmPose))
            return !HandPoseUtils.IsThumbGrabbing(Handedness.Left) &&
            !HandPoseUtils.IsMiddleGrabbing(Handedness.Left) &&
            !HandPoseUtils.IsIndexGrabbing(Handedness.Left) &&
            !IsPinkyGrabbing(Handedness.Left) &&
            !IsRingGrabbing(Handedness.Left) && indexup.GestureCondition() && Vector3.Angle(indextip.Position - palmPose.Position, down) < 40f ;
        return false;
    }
}
