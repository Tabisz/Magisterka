using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneRotator : MonoBehaviour
{

    public void RotateBone(Vector3 newEulerAngles)
    {
        transform.localRotation = Quaternion.Euler(newEulerAngles);
    }

    public Vector3 GetRotation => transform.localRotation.eulerAngles;
}
