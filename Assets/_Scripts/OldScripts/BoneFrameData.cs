using UnityEngine;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public struct BoneFrameData
    {
        public Quaternion rot;

        public BoneFrameData( Quaternion rot)
        {
            this.rot = rot;
        }
    }
}
