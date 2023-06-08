using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public class DataCollector : MonoBehaviour
    {
        [SerializeField]
        private string animationDataName;

        [SerializeField] private TextMeshProUGUI textBox;

        [SerializeField]
        private AvatarAnimationHandler fullAvatarAnimationHandler;

        [SerializeField]
        private AvatarAnimationHandler partialAvatarAnimationHandler;

        private List<List<BoneFrameData>> fullAnimationData;
        private List<List<BoneFrameData>> partialAnimationData;
    
    
        private delegate void ComparingMethod(ref string[ , ] initialTable,List<List<BoneFrameData>> animationDataA, List<List<BoneFrameData>>animationDataB);

        private void Awake()
        {
            Application.targetFrameRate = 60;

        }


        private void Start()
        {
            fullAnimationData = new List<List<BoneFrameData>>();
            fullAvatarAnimationHandler.Init(this);
            partialAnimationData = new List<List<BoneFrameData>>();
            partialAvatarAnimationHandler.Init(this);

        }

        [ContextMenu("NameDataCollector")]
        private void NameDataCollector()
        {
            textBox.text = animationDataName;
        }
        private void Update()
        {
            if (fullAvatarAnimationHandler.ImActive &&
                partialAvatarAnimationHandler.ImActive)
            {
                SyncRootBones();
                fullAnimationData.Add(
                    fullAvatarAnimationHandler.GetFrameData());
                partialAnimationData.Add(
                    partialAvatarAnimationHandler.GetFrameData());
                fullAvatarAnimationHandler.Animator
                    .Update(Time.deltaTime);
                partialAvatarAnimationHandler.Animator
                    .Update(Time.deltaTime);
            }
            else
            {
                SaveData();
                enabled = false;
            }
        }

        private void SaveData()
        {
            string plainString="";
           
            string [,] comparedPositions = CompareBonesBetweenAvatars(fullAnimationData,partialAnimationData,ComparePositions);
            plainString = StringTableAsPlainString(comparedPositions);
            SaveStringToFile(plainString,animationDataName+"_pos");
           
            string [,] comparedRotations = CompareBonesBetweenAvatars(fullAnimationData,partialAnimationData,CompareRotations);
            plainString = StringTableAsPlainString(comparedRotations);
            SaveStringToFile(plainString,animationDataName+"_rot");
        }
        private void SyncRootBones()
        {
            if (partialAvatarAnimationHandler.hips && fullAvatarAnimationHandler.hips)
            {
                partialAvatarAnimationHandler.hips.position = fullAvatarAnimationHandler.hips.position;
                partialAvatarAnimationHandler.hips.rotation = fullAvatarAnimationHandler.hips.rotation;
            }
        }

        private string[,] CompareBonesBetweenAvatars(List<List<BoneFrameData>> animationDataA,List<List<BoneFrameData>> animationDataB, ComparingMethod comparingMethod)
        {
            int rowLength = animationDataA.Count + 1;
            Debug.Log("RowLenght "+rowLength);

            int colLength = fullAvatarAnimationHandler.GetBones().Count + 1;

            string[,] dataTable = new string[rowLength,colLength];

            int i = 1;
            foreach(var item in fullAvatarAnimationHandler.GetBones())//nazwy kosci
            {
                dataTable[0, i] = item;
                i++;
            }
            for (int l = 1; l < rowLength; l++)//klatki
            {
                dataTable[l,0] = "Klatka" + l;
            }

            comparingMethod.Invoke(ref dataTable,animationDataA, animationDataB);
        

            return dataTable;
        }

        private void ComparePositions(ref string[,] initialTable, List<List<BoneFrameData>> animationDataA,
            List<List<BoneFrameData>> animationDataB)
        {
            for (int j = 1; j < animationDataA.Count + 1; j++)
            {
                for (int k = 1; k < animationDataA[0].Count + 1; k++)
                {
                    float difference = Vector3.SqrMagnitude(animationDataA[j - 1][k - 1].pos - animationDataB[j - 1][k - 1].pos);
                    initialTable[j, k] = difference.ToString();
                }
            }
        }
    
        private void CompareRotations(ref string[,] initialTable, List<List<BoneFrameData>> animationDataA,
            List<List<BoneFrameData>> animationDataB)
        {
            for (int j = 1; j < animationDataA.Count + 1; j++)
            {
                for (int k = 1; k < animationDataA[0].Count + 1; k++)
                {
                    float difference = Quaternion.Angle(animationDataA[j - 1][k - 1].rot,animationDataB[j - 1][k - 1].rot);
                    initialTable[j, k] = difference.ToString();
                }
            }
        }
        private string StringTableAsPlainString(string[,] data)
        {
            string s = "";
            for (int i = 0; i < data.GetLength(0); i++)//rowLenght
            {
                for (int j = 0; j < data.GetLength(1); j++)//colLenght
                {
                    s += data[i, j]+";";
                }

                s += "\n";
            }

            return s;
        }
    
        private void SaveStringToFile(string text, string fileName)
        {
            string destination = Application.persistentDataPath + "/"+fileName+".csv";
            StreamWriter writer = new StreamWriter(destination, false);
            writer.Write(text);
            writer.Close();
        }

        public void ListenForCollectionEnd(AvatarAnimationHandler handler)
        {
            handler.ImActive = false;
            handler.gameObject.SetActive(false);
        }
    }
}


