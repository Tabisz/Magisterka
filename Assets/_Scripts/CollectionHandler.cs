using System.Collections;
using System.Collections.Generic;
using MarekTabiszewski.Core.AnimationDataCollection;
using UnityEngine;

public class CollectionHandler : MonoBehaviour
{

    public BaseInterpolationScript scriptA;
    public BaseInterpolationScript scriptB;

    public DataSaver dataSaver;

    // Update is called once per frame
    void Update()
    {
        if(!scriptA.Completed)
            scriptA.CustomUpdate();
        if(!scriptB.Completed)
            scriptB.CustomUpdate();

        if (scriptA.Completed && scriptB.Completed)
        {
            var preparedStringData  = dataSaver.CompareOneBoneBetweenAvatars(scriptA.rotationsForEachFrame, scriptB.rotationsForEachFrame, "RightUpLeg");
            dataSaver.SaveData(preparedStringData, "compare_RULeg");
            this.enabled = false;
        }
    }
}
