using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;

/// <summary>
/// One hand gesture basic class
/// </summary>
public abstract class GestureWidget2Phase : Sensor
{
    public Handedness _handedness;

    private DateTime gestureStartTime;

    private Vector3 oldPalmPos;

    private int state;

    private bool countDownStarted { get; set; }

    public GestureWidget2Phase(Handedness handedness = Handedness.Right)
    {
        _handedness = handedness;
        state = 0;
    }


    /// <summary>
    /// Check the requirements, ex. which fingers should be grabbing
    /// </summary>
    /// <returns></returns>
    public abstract bool GestureCondition(Vector3 old, Vector3 now);
    public abstract bool GestureCondition1();
    public abstract bool GestureCondition2();

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
        if (state == 0)
        {
            if (GestureCondition1())
            {
                if (countDownStarted == false)
                {
                    gestureStartTime = DateTime.Now;
                    countDownStarted = true;
                }
                state = 1;
            }
            else
            {
                SensorUntrigger();
                countDownStarted = false;
            }
        }
        else if(state == 1)
        {
            if ((DateTime.Now.Subtract(gestureStartTime).TotalMilliseconds) > 100)
            {
                if (GestureCondition2())
                {
                    SensorTrigger();
                    state = 0;
                }
                else
                {
                    countDownStarted = false;
                    SensorUntrigger();
                    state = 0;
                }
            }
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
