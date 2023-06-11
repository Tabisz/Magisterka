using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInterpolationScript : MonoBehaviour
{
    [SerializeField] protected ArmatureRotation armatureRotation;
    [SerializeField] protected int transitionLenght = 10;
    protected int startingFrame = 0;
    
    protected int currentFrame;
    protected int CurrentFrame
    {
        get => currentFrame;
        private set => currentFrame = value;
    }

    protected ArmatureRotation.RotationCalculation rotationMethod;
    
    protected Dictionary<string, Vector3> destinationRotations;
    protected Dictionary<string, Vector3>  startingRotations;

    protected virtual void Start()
    {
        armatureRotation.CreateBoneRotators();
        startingRotations = armatureRotation.GatherAllRotations();
    }
    
    private void Update()
    {
        currentFrame = Time.frameCount - startingFrame;
        if(currentFrame>=0 && currentFrame < transitionLenght)
            armatureRotation.SetupRotations( armatureRotation.CalculateCurrentRotationsForBones(rotationMethod,(float)CurrentFrame/transitionLenght , startingRotations));

    }

    [ContextMenu("Setup Destination")]
    private void SetupDestinationRotations()
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
    private void PrintDestinationRotations()
    {
        if (destinationRotations == null) return;
        foreach (var destinationRotation in destinationRotations)
        {
            Debug.LogFormat("Destination for: {0} - {1} ",destinationRotation.Key,destinationRotation.Value.ToString());
        }
    }

}
