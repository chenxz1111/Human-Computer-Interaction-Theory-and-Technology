using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
public class MyGesture_down : GestureWidgetMove1D
{
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        Vector3 delta = now - old;
        Vector3 down = new Vector3(0f, -1f, 0f);
        return (delta.magnitude > 0.001 && Vector3.Angle(delta, down) < 20.0f);
    }

}