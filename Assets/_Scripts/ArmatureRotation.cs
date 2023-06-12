using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmatureRotation : MonoBehaviour
{
    
    public Dictionary<string,BoneRotator> BoneRotators;

    public List<ArmatureRotation> poses;

    public float destinationFrame = 0;

    public delegate Vector3 RotationCalculation(float currentTransitionT,  List<InterpolationNode> nodes);

    // public Dictionary<string,Vector3> CalculateCurrentRotationsForBones(RotationCalculation calculation, float currentTransitionStage, Dictionary<string,Vector3> startingRotations,  Dictionary<string,Vector3> destinationRotations )
    // {
    //     Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();
    //
    //     Vector3 currStartingRot, currDestinationRot;
    //
    //     foreach (var boneRotator in BoneRotators)
    //     {
    //         if (startingRotations.TryGetValue(boneRotator.Key, out currStartingRot) &&
    //             destinationRotations.TryGetValue(boneRotator.Key, out currDestinationRot))
    //             dic.Add(boneRotator.Key,calculation.Invoke( currentTransitionStage, currStartingRot, currDestinationRot));
    //
    //     }
    //
    //     return dic;
    // }
    
    public Dictionary<string,Vector3> CalculateCurrentRotationsForBones(RotationCalculation calculation, float currentTransitionStage, Dictionary<string,List<InterpolationNode>> bonesInterpolationNodes )
    {
        Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();
        List<InterpolationNode> currentInterpolationNodeList;

        
        

        foreach (var boneRotator in BoneRotators)
        {
            if(bonesInterpolationNodes.TryGetValue(boneRotator.Key, out currentInterpolationNodeList))
                dic.Add(boneRotator.Key,calculation.Invoke( currentTransitionStage,currentInterpolationNodeList));

        }
        return dic;
    }
    
    public void SetupRotations(Dictionary<string, Vector3> rotationsDictionary)
    {
        BoneRotator currentRotator;
        foreach (var rotations in rotationsDictionary)
        {
            if (!BoneRotators.TryGetValue(rotations.Key, out currentRotator))
                continue;
            currentRotator.RotateBone(rotations.Value);
        }
    }

    public Dictionary<string, List<InterpolationNode>> GatherAllRotations()
    {
        Dictionary<string, List<InterpolationNode>> dic = new Dictionary<string, List<InterpolationNode>>();

        BoneRotator currRot;
        
        CreateBoneRotators();
        foreach (var pose in poses)
            pose.CreateBoneRotators();

        foreach (var rotator in BoneRotators)
        {
            List<InterpolationNode> nodesForBone = new List<InterpolationNode>();
            foreach (var pose in poses)
            {
                if (pose.BoneRotators.TryGetValue(rotator.Key, out currRot))
                        nodesForBone.Add(new InterpolationNode(currRot.GetRotation, pose.destinationFrame));
            }
            dic.Add(rotator.Key,nodesForBone);
        }

        return dic;
    }
    
    public void CreateBoneRotators()
    {
        var childs = GetChildren(gameObject, true);
        BoneRotators = new Dictionary<string, BoneRotator>();

        foreach (var c in childs)
        {
            var rotator = c.GetComponent<BoneRotator>();
            if(rotator)
                BoneRotators.Add(c.name, rotator);
            else if (c.childCount>0)
                BoneRotators.Add(c.name, c.gameObject.AddComponent<BoneRotator>());
        }
    }


    public List<Transform> GetChildren(GameObject gameObject, bool recursive)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in gameObject.transform)
        {
            children.Add(child);
            if(recursive)
                children.AddRange(GetChildren(child.gameObject,true));
        }

        return children;
    }

}
