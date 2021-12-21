using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;
public class MyGesture_indexup : GestureWidget
{
    public override bool GestureCondition()
    {
        Vector3 up = new Vector3(0f,1f,0f);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, _handedness, out var indextip)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, _handedness, out var palmPose))
            return HandPoseUtils.IsThumbGrabbing(_handedness) &&
            HandPoseUtils.IsMiddleGrabbing(_handedness) &&
            !HandPoseUtils.IsIndexGrabbing(_handedness) &&
            IsPinkyGrabbing(_handedness) &&
            IsRingGrabbing(_handedness) && (Vector3.Angle(indextip.Position - palmPose.Position, up) < 30f);
        return false;
    }
}
