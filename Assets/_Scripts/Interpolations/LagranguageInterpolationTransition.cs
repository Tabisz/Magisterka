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

    private Vector3 LagranguageInterpolation(float currentTransitionT, List<InterpolationNode> nodes)
    {
        Vector3 returning = Vector3.zero;
        float axisResult = 0;
        float f1 = 1,f2 = 1;
        
        for (var i = 0; i < nodes.Count; i++)
        {
            f1 = f2 = 1;
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i != j)
                {
                    f1 *= currentTransitionT - nodes[j].rotation.x;
                    f2 *= nodes[i].rotation.x - nodes[j].rotation.x;
                }
                
                axisResult += nodes[i].frame * f1 / f2;
            }
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
                    f1 *= currentTransitionT - nodes[j].rotation.y;
                    f2 *= nodes[i].rotation.y - nodes[j].rotation.y;
                }
                
                axisResult += nodes[i].frame * f1 / f2;
            }
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
                    f1 *= currentTransitionT - nodes[j].rotation.z;
                    f2 *= nodes[i].rotation.z - nodes[j].rotation.z;
                }
                
                axisResult += nodes[i].frame * f1 / f2;
            }
        }
        returning.z = axisResult;

        return returning;
    }
}
