using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolationTransition : BaseInterpolationScript
{
    protected override void Start()
    {
        base.Start();
        rotationMethod = LinearInterpolation;
    }

    private Vector3 LinearInterpolation(float currentTransitionT, Vector3 startingRot,
        Vector3 destinationRot)
    {
        return Vector3.LerpUnclamped(startingRot, destinationRot,currentTransitionT);
    }
    

}
