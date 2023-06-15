using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InterpolationNode
{
    public InterpolationNode(Quaternion rotation, float frame)
    {
        this.rotation = rotation;
        this.frame = frame;
    }

    public Quaternion rotation;
    public float frame;
}
