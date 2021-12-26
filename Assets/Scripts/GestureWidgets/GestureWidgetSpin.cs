using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;

/// <summary>
/// One hand gesture basic class
/// </summary>
public abstract class GestureWidgetSpin : Sensor
{
    public Handedness _handedness;

    private DateTime gestureStartTime;

    private Vector3 oldVec;

    private bool countDownStarted { get; set; }

    public GestureWidgetSpin(Handedness handedness = Handedness.Right)
    {
        _handedness = handedness;
    }


    /// <summary>
    /// Check the requirements, ex. which fingers should be grabbing
    /// </summary>
    /// <returns></returns>
    public abstract bool GestureCondition(Vector3 old, Vector3 now);

    /// <summary>
    /// The value to assign for gesture
    /// Override in your gesture class
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual bool TryGetGestureValue(out float value)
    {
        value = 0f;
        return false;
    }


    /// <summary>
    /// Trigger virtual sensors based on the Gesture condition
    /// </summary>
    public void GestureEventTrigger()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, _handedness, out var palmPose)
            && HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, _handedness, out var wristPose))
        {
            if (countDownStarted == false)
            {
                gestureStartTime = DateTime.Now;
                countDownStarted = true;
                oldVec = palmPose.Position - wristPose.Position;
            }
            if ((DateTime.Now.Subtract(gestureStartTime).TotalMilliseconds) > 100)
            {
                if (GestureCondition(oldVec, palmPose.Position - wristPose.Position))
                {
                    SensorTrigger();
                }
                else
                {
                    countDownStarted = false;
                    SensorUntrigger();
                }
                oldVec = palmPose.Position - wristPose.Position;
            }
        }
        else
        {
            SensorUntrigger();
            countDownStarted = false;
        }
    }

    public override void Start()
    {
        base.Start();

    }

    /// <summary>
    /// Update is called every frame
    /// Check the gesture 
    /// </summary>
    public override void Update()
    {
        base.Update();
        GestureEventTrigger();
    }



    /// <summary>
    /// Returns true if pinky finger tip is closer to wrist than pinky knuckle joint.
    /// </summary>
    /// <param name="hand">Hand to query joint pose against.</param>
    protected bool IsPinkyGrabbing(Handedness hand)
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, hand, out var wristPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, hand, out var indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, hand, out var indexKnucklePose))
        {
            // compare wrist-knuckle to wrist-tip
            Vector3 wristToIndexTip = indexTipPose.Position - wristPose.Position;
            Vector3 wristToIndexKnuckle = indexKnucklePose.Position - wristPose.Position;
            return wristToIndexKnuckle.sqrMagnitude >= wristToIndexTip.sqrMagnitude;
        }
        return false;
    }

    /// <summary>
    /// Returns true if pinky finger tip is closer to wrist than pinky knuckle joint.
    /// </summary>
    /// <param name="hand">Hand to query joint pose against.</param>
    protected bool IsRingGrabbing(Handedness hand)
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, hand, out var wristPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, hand, out var indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.RingKnuckle, hand, out var indexKnucklePose))
        {
            // compare wrist-knuckle to wrist-tip
            Vector3 wristToIndexTip = indexTipPose.Position - wristPose.Position;
            Vector3 wristToIndexKnuckle = indexKnucklePose.Position - wristPose.Position;
            return wristToIndexKnuckle.sqrMagnitude >= wristToIndexTip.sqrMagnitude;
        }
        return false;
    }

}
