using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneRotator : MonoBehaviour
{

    public void RotateBone(Quaternion newRotation)
    {
        transform.localRotation = newRotation;
    }

    public Quaternion GetRotation => transform.localRotation;
}
