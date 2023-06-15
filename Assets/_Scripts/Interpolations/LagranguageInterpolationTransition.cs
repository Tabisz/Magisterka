using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagranguageInterpolationTransition : BaseInterpolationScript
{
    protected override void Start()
    {
        base.Start();
        rotationMethod = LagranguageInterpolation;
    }

    private Quaternion LagranguageInterpolation(float currentTransitionT, List<InterpolationNode> nodes)
    {
        Quaternion returning = Quaternion.identity;
        float axisResult = 0;
        float f1 = 1,f2 = 1;
        
        for (var i = 0; i < nodes.Count; i++)
        {
            f1 = f2 = 1;
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i != j)
                {
                    f1 *= currentTransitionT - nodes[j].frame;
                    f2 *= nodes[i].frame - nodes[j].frame;
                }
            }

            if (f2 != 0)
                axisResult += nodes[i].rotation.x * f1 / f2;
            else
                axisResult = nodes[0].rotation.x;
        }
        returning.x = axisResult;
        axisResult = 0;
        
        for (var i = 0; i < nodes.Count; i++)
        {
            f1 = f2 = 1;
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i != j)
                {
                    f1 *= currentTransitionT - nodes[j].frame;
                    f2 *= nodes[i].frame - nodes[j].frame;
                }
            }

            if (f2 != 0)
                axisResult += nodes[i].rotation.y * f1 / f2;
            else
                axisResult = nodes[0].rotation.y;
        }
        returning.y = axisResult;
        axisResult = 0;
        
        for (var i = 0; i < nodes.Count; i++)
        {
            f1 = f2 = 1;
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i != j)
                {
                    f1 *= currentTransitionT - nodes[j].frame;
                    f2 *= nodes[i].frame - nodes[j].frame;
                }
            }

            if (f2 != 0)
                axisResult += nodes[i].rotation.z * f1 / f2;
            else
                axisResult = nodes[0].rotation.z;
        }
        returning.z = axisResult;
        axisResult = 0;
        
        for (var i = 0; i < nodes.Count; i++)
        {
            f1 = f2 = 1;
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i != j)
                {
                    f1 *= currentTransitionT - nodes[j].frame;
                    f2 *= nodes[i].frame - nodes[j].frame;
                }
            }

            if (f2 != 0)
                axisResult += nodes[i].rotation.w * f1 / f2;
            else
                axisResult = nodes[0].rotation.w;
        }
        returning.w = axisResult;
        axisResult = 0;

        return returning;
    }
}
