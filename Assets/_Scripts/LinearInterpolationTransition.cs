using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolationTransition : MonoBehaviour
{
    [SerializeField] private ArmatureRotation armatureRotation;
    void Start()
    {
        armatureRotation.CreateBoneRotators();
        var startingRotations = armatureRotation.GatherAllRotations();
        Dictionary<string, Vector3> destinationRotations = new Dictionary<string, Vector3>(startingRotations);
        
       armatureRotation.SetupRotations( armatureRotation.CalculateCurrentRotationsForBones(LinearInterpolation, 5, startingRotations,
            destinationRotations));
    }

    private Vector3 LinearInterpolation(float currentTime, float transitionTime, Vector3 startingRot,
        Vector3 destinationRot)
    {
        return Vector3.one;
    }

}
