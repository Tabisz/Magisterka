using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InterpolationNode
{
    public InterpolationNode(Vector3 rotation, float frame)
    {
        this.rotation = rotation;
        this.frame = frame;
    }

    public Vector3 rotation;
    public float frame;
}
