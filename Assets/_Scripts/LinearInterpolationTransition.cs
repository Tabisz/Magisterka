using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolationTransition : MonoBehaviour
{
    [SerializeField] private ArmatureRotation armatureRotation;

    [SerializeField] private int transitionLenght = 10;

    private int startingFrame = 0;


    private Dictionary<string, Vector3> destinationRotations;
    private Dictionary<string, Vector3>  startingRotations;
    void Start()
    {
        armatureRotation.CreateBoneRotators();
        startingRotations = armatureRotation.GatherAllRotations();
    }

    private void Update()
    {
        var currentFrame = Time.frameCount - startingFrame;
        if(currentFrame>=0 && currentFrame < transitionLenght)
            armatureRotation.SetupRotations( armatureRotation.CalculateCurrentRotationsForBones(LinearInterpolation,(float)currentFrame/transitionLenght , startingRotations));
    }

    private Vector3 LinearInterpolation(float currentTransitionT, Vector3 startingRot,
        Vector3 destinationRot)
    {
        return Vector3.LerpUnclamped(startingRot, destinationRot,currentTransitionT);
    }


    [ContextMenu("Setup Destination")]
    public void SetupDestinationRotations()
    {
        armatureRotation.CreateBoneRotators();
        if (destinationRotations != null)
            destinationRotations.Clear();
        else
            destinationRotations = new Dictionary<string, Vector3>();
            
        
        foreach (var bone in armatureRotation.BoneRotators)
        {
            destinationRotations.Add(bone.Key,bone.Value.GetRotation);
        }
    }
    
    
    [ContextMenu("Print Destination")]
    public void PrintDestinationRotations()
    {
        if (destinationRotations == null) return;
        foreach (var destinationRotation in destinationRotations)
        {
            Debug.LogFormat("Destination for: {0} - {1} ",destinationRotation.Key,destinationRotation.Value.ToString());
        }
    }

}
