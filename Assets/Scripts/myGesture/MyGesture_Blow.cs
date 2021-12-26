using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_Blow : GestureWidgetMove1D
{
    public GazeProvider gaze;
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = old - now;
        return (delta.magnitude > 0.01 && Vector3.Angle(delta, gaze.GazeDirection) < 20.0f);
    }

}