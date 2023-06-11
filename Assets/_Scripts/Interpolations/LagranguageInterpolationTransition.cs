using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagranguageInterpolationTransition : BaseInterpolationScript
{
    protected override void Start()
    {
        base.Start();
        rotationMethod = LagranguageInterpolation;
    }

    private Vector3 LagranguageInterpolation(float currentTransitionT, Vector3 startingRot,
        Vector3 destinationRot)
    {
        return Vector3.LerpUnclamped(startingRot, destinationRot,currentTransitionT);
    }
}
