using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_left : GestureWidgetMove1D
{
    public GazeProvider gaze;
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = now - old;
        Vector3 up = new Vector3(0f, 1f, 0f);
        return delta.sqrMagnitude > 0.0001 && Vector3.Angle(up, Vector3.Cross(delta, gaze.GazeDirection)) < 20f;
    }

}