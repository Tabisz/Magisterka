using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonInterpolationTransition : BaseInterpolationScript
{
    protected override void Start()
    {
        base.Start();
        rotationMethod = NewtonInterpolation;
    }

    private Quaternion NewtonInterpolation(float currentTransitionT, List<InterpolationNode> nodes)
    {
        Quaternion returning = nodes[0].rotation;

        float result = nodes[0].rotation.x;

        float[] a = new float[nodes.Count];

        for (var i = 0; i < nodes.Count; i++)
            a[i] = nodes[i].rotation.x;

        for (var i = 1; i < nodes.Count; i++)
        for (var j = 0; j < i; j++)
        {
                a[i] = (a[i] - a[j]) / (nodes[i].frame- nodes[j].frame);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            float f = 1;
            for (int j= 0; j<i; j++)
            {
                f *= currentTransitionT - nodes[j].rotation.x;
                result += a[i] * f; 
            }
        }

        returning.x = result;
        // /////////////////////////////////////////////////////

        result = nodes[0].rotation.y;


        for (var i = 0; i < nodes.Count; i++)
            a[i] = nodes[i].rotation.y;

        for (var i = 1; i < nodes.Count; i++)
        for (var j = 0; j < i; j++)
        {
            a[i] = (a[i] - a[j]) / (nodes[i].frame- nodes[j].frame);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            float f = 1;
            for (int j= 0; j<i; j++)
            {
                f *= currentTransitionT - nodes[j].rotation.y;
                result += a[i] * f; 
            }
        }

        returning.y = result;
        // /////////////////////////////////////////////

        result = nodes[0].rotation.z;

        for (var i = 0; i < nodes.Count; i++)
            a[i] = nodes[i].rotation.z;

        for (var i = 1; i < nodes.Count; i++)
        for (var j = 0; j < i; j++)
        {
            a[i] = (a[i] - a[j]) / (nodes[i].frame- nodes[j].frame);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            float f = 1;
            for (int j= 0; j<i; j++)
            {
                f *= currentTransitionT - nodes[j].rotation.z;
                result += a[i] * f; 
            }
        }

        returning.z = result;
        // ////////////////////////////////////

        result = nodes[0].rotation.w;

        for (var i = 0; i < nodes.Count; i++)
            a[i] = nodes[i].rotation.w;

        for (var i = 1; i < nodes.Count; i++)
        for (var j = 0; j < i; j++)
        {
            a[i] = (a[i] - a[j]) / (nodes[i].frame- nodes[j].frame);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            float f = 1;
            for (int j= 0; j<i; j++)
            {
                f *= currentTransitionT - nodes[j].rotation.w;
                result += a[i] * f; 
            }
        }

        returning.w = result;
        return returning;
    }
}
