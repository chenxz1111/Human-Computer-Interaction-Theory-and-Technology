using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;
public class MyGesture_thumbdown : GestureWidget
{
    public override bool GestureCondition()
    {
        Vector3 down = new Vector3(0f, -1f, 0f);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, _handedness, out var thumbtip)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, _handedness, out var palmPose))
            return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
            HandPoseUtils.IsMiddleGrabbing(_handedness) &&
            HandPoseUtils.IsIndexGrabbing(_handedness) &&
            IsPinkyGrabbing(_handedness) &&
            IsRingGrabbing(_handedness) && (Vector3.Angle(thumbtip.Position - palmPose.Position, down) < 30f);
        return false;
    }
}
