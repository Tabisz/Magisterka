using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureRotation : MonoBehaviour
{
    
    public Dictionary<string,BoneRotator> BoneRotators;

    public delegate Vector3 RotationCalculation(float currentTime, float transitionTime, Vector3 startingRot,
        Vector3 destinationRot);

    public Dictionary<string,Vector3> CalculateCurrentRotationsForBones(RotationCalculation calculation, float transitionTime, Dictionary<string,Vector3> startingRotations,  Dictionary<string,Vector3> destinationRotations )
    {
        Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();

        Vector3 currStartingRot, currDestinationRot;
        
        foreach (var boneRotator in BoneRotators)
        {
            if (startingRotations.TryGetValue(boneRotator.Key, out currStartingRot) &&
                destinationRotations.TryGetValue(boneRotator.Key, out currDestinationRot))
                dic.Add(boneRotator.Key,calculation.Invoke(Time.deltaTime, transitionTime, currStartingRot, currDestinationRot));

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

    public Dictionary<string, Vector3> GatherAllRotations()
    {
        Dictionary<string, Vector3> dic = new Dictionary<string, Vector3>();

        foreach (var rotator in BoneRotators)
        {
            dic.Add(rotator.Key, rotator.Value.transform.localRotation.eulerAngles);
        }

        return dic;
    }
    

    [ContextMenu("GenerateRotators")]
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