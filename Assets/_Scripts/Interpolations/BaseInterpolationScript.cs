using System;
using System.Collections;
using System.Collections.Generic;
using MarekTabiszewski.Core.AnimationDataCollection;
using UnityEngine;

public class BaseInterpolationScript : MonoBehaviour
{
    [SerializeField] protected ArmatureRotation armatureRotation;

    [SerializeField] protected DataCollector dataCollector;

    private Dictionary<string,List<Quaternion>> rotationsForEachFrame;

    private List<string> bones;
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
        rotationsForEachFrame = new Dictionary<string, List<Quaternion>>();
    }
    
    private void Update()
    {
        currentFrame = Time.frameCount - startingFrame;
        if (currentFrame >= 0 && currentFrame < transitionLenght)
        {
            List<Quaternion> currBoneFrameList;
            Quaternion currBoneQuat;
            
            var currentRotationsForBones =
                armatureRotation.CalculateCurrentRotationsForBones(rotationMethod, CurrentFrame, rotationNodes);
            
            foreach (var boneRot in currentRotationsForBones)
            {
                // if (rotationsForEachFrame.TryGetValue(boneRot.Key, out currBoneFrameList))
                // {
                //     if(currentRotationsForBones.TryGetValue(boneRot.Key, out currBoneQuat));
                //         currBoneFrameList.Add(currBoneQuat);
                // }
                // else
                // {
                //     var newList = new List<Quaternion>();
                //     newList.Add(currBoneQuat);
                //     rotationsForEachFrame.Add(boneRot.Key,newList);
                // }

                if (!rotationsForEachFrame.TryGetValue(boneRot.Key, out currBoneFrameList))
                {
                    currBoneFrameList = new List<Quaternion>();
                    rotationsForEachFrame.Add(boneRot.Key,currBoneFrameList);
                }
                if(currentRotationsForBones.TryGetValue(boneRot.Key, out currBoneQuat))
                    currBoneFrameList.Add(currBoneQuat);
                else
                    currBoneFrameList.Add(Quaternion.identity);
                    
            }
            
            armatureRotation.SetupRotations(currentRotationsForBones);
        }
        else
        {
            dataCollector.SaveData(rotationsForEachFrame);
            gameObject.SetActive(false);
            Application.Quit();
            Debug.Log("Write end");
        }

    }

    

}
