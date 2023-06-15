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

    private Quaternion LinearInterpolation(float currentTransitionT, List<InterpolationNode> nodes )
    {
        return Quaternion.identity;
        //return Vector3.LerpUnclamped(startingRot, destinationRot,currentTransitionT);
    }
    

}
