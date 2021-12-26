using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;
public class MyGesture_clap : GestureWidget
{
    public override bool GestureCondition()
    {
        Vector3 distance = new Vector3(0,1,0);
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out var leftpalmPose)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out var rightpalmPose))
            distance = leftpalmPose.Position - rightpalmPose.Position;
        return distance.sqrMagnitude < 0.01f;
    }
}
