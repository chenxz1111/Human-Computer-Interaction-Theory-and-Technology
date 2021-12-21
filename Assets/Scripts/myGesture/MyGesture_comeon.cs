using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_comeon : GestureWidget2Phase
{
    public Transform back;
    public GazeProvider gaze;
    private Vector3 oldindexfinger;

    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = now - old;
        Vector3 up = new Vector3(0f, 1f, 0f);
        return (delta.magnitude > 0.001 && Vector3.Angle(delta, up) < 10.0f);
    }

    public override bool GestureCondition1()
    {
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, _handedness, out var pose))
        {
            oldindexfinger = pose.Position;
        }
        return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
           !HandPoseUtils.IsMiddleGrabbing(_handedness) &&
           !HandPoseUtils.IsIndexGrabbing(_handedness) &&
           !IsPinkyGrabbing(_handedness) &&
           !IsRingGrabbing(_handedness);
    }

    public override bool GestureCondition2()
    {
        Vector3 delta = new Vector3();
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, _handedness, out var pose))
        {
            delta = oldindexfinger - pose.Position;
        }
        if(Vector3.Angle(gaze.GazeDirection, back.position - gaze.GazeOrigin) < 90f)
        {
            //facing back
            return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
            HandPoseUtils.IsMiddleGrabbing(_handedness) &&
            HandPoseUtils.IsIndexGrabbing(_handedness) &&
            IsPinkyGrabbing(_handedness) &&
            IsRingGrabbing(_handedness) && (Vector3.Angle(delta, back.position - gaze.GazeOrigin) < 90f);

        }
        //facing front
        return !HandPoseUtils.IsThumbGrabbing(_handedness) &&
            HandPoseUtils.IsMiddleGrabbing(_handedness) &&
            HandPoseUtils.IsIndexGrabbing(_handedness) &&
            IsPinkyGrabbing(_handedness) &&
            IsRingGrabbing(_handedness) && (Vector3.Angle(delta, back.position - gaze.GazeOrigin) > 90f);


    }
}