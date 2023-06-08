using UnityEngine;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public struct BoneFrameData
    {
        public Vector3 pos;
        public Quaternion rot;

        public BoneFrameData( Vector3 pos, Quaternion rot)
        {
            this.pos = pos;
            this.rot = rot;
        }
    }
}
