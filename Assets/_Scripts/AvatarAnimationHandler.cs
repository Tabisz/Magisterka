using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public class AvatarAnimationHandler : MonoBehaviour
    {
        public bool ImActive
        {
            get => imActive;
            set => imActive = value;
        }

        private bool imActive;

        [SerializeField]
        private Animator animator;

        public Animator Animator
        {
            get => animator;
        }

        [SerializeField]
        private Transform root;

        public Transform hips;
        
        public Dictionary<string,Transform> bones;

        private DataCollector dataCollector;

        UnityAction<AvatarAnimationHandler> OnCollectionEnd;

        public void Init( DataCollector dataCollector)
        {
            this.dataCollector = dataCollector;
            bones = new Dictionary<string, Transform>();
            imActive = true;

            animator.enabled = false;
            
            SetBonesRecursive(root);
            OnCollectionEnd += dataCollector.ListenForCollectionEnd;


            Debug.Log("collected bones: " + bones.Count);

        }
        

        public Dictionary<string, Transform>.KeyCollection GetBones()
        {
            return bones.Keys;
        }

        private void SetBonesRecursive(Transform parent)
        {
            foreach (Transform child in parent)
            {
                bones.Add(child.name,child);
                SetBonesRecursive(child);
            }
        }

        public List<BoneFrameData> GetFrameData()
        {
            List<BoneFrameData> bonesFrameData = new List<BoneFrameData>();
            
            foreach (var keyValuePair in bones)
            {
                Transform boneT = keyValuePair.Value;
                bonesFrameData.Add(
                    new BoneFrameData(boneT.position,boneT.rotation));
            }
        
            return bonesFrameData;
        }

        public void NotifyAnimationEnd()
        {
            OnCollectionEnd?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnCollectionEnd = null;
        }
    }
}





