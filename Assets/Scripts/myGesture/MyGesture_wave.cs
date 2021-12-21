using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
public class MyGesture_wave : GestureWidgetSpin
{
    public override bool GestureCondition(Vector3 old, Vector3 now)
    {
        return Vector3.Angle(old, now) > 45f;
    }

}