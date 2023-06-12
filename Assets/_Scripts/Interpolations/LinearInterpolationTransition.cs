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

    private Vector3 LinearInterpolation(float currentTransitionT, List<InterpolationNode> nodes )
    {
        return Vector3.zero;
        //return Vector3.LerpUnclamped(startingRot, destinationRot,currentTransitionT);
    }
    

}
