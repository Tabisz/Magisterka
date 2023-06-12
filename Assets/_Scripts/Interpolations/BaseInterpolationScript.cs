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
    
    protected Dictionary<string, List<InterpolationNode>>  rotationNodes;

    protected virtual void Start()
    {
        armatureRotation.CreateBoneRotators();
        rotationNodes = armatureRotation.GatherAllRotations();
    }
    
    private void Update()
    {
        currentFrame = Time.frameCount - startingFrame;
        if(currentFrame>=0 && currentFrame < transitionLenght)
            armatureRotation.SetupRotations( armatureRotation.CalculateCurrentRotationsForBones(rotationMethod,(float)CurrentFrame/transitionLenght , rotationNodes));

    }

    

}
