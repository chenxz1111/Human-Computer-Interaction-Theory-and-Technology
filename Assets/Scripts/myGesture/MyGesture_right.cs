using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class MyGesture_right : GestureWidgetMove1D
{
    public GazeProvider gaze;
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = now - old;
        Vector3 down = new Vector3(0f, -1f, 0f);
        return Vector3.Angle(down, Vector3.Cross(delta, gaze.GazeDirection)) < 30f;
    }

}