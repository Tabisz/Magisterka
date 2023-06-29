using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public class DataCollector : MonoBehaviour
    {
        [SerializeField]
        private string animationDataName;
        private delegate void ComparingMethod(ref string[ , ] initialTable,List<List<Quaternion>> animationDataA, List<List<Quaternion>>animationDataB);
        

        public void SaveData(List<string> bonesNames,List<List<Quaternion>> fullAnimationData,List<List<Quaternion>> partialAnimationData)
        {
            string plainString="";

            string [,] comparedRotations = CompareBonesBetweenAvatars(bonesNames,fullAnimationData,partialAnimationData,CompareRotations);
            plainString = StringTableAsPlainString(comparedRotations);
            SaveStringToFile(plainString,animationDataName+"_rot");
        }
        public void SaveData(Dictionary<string,List<Quaternion>> AnimationData)
        {
            string plainString="";

            string [,] comparedRotations = PrintRotation(AnimationData);
            plainString = StringTableAsPlainString(comparedRotations);
            SaveStringToFile(plainString,animationDataName+"_rot");
        }

        private string[,] PrintRotation(Dictionary<string, List<Quaternion>> AnimationData)
        {
            int rowLength = 300;
            int colLength = 300;


            string[,] dataTable = new string[rowLength, colLength];

            for (int l = 1; l < rowLength; l++)//klatki
            {
                dataTable[l, 0] = "Klatka " + l;
            }

            //for (int i = 1; i < colLength; i++)//nazwy kosci
            //{
            //    dataTable[0, i] = "test";
            //}


            var i = 1;
            foreach (var keyValuePair in AnimationData)
            {

                dataTable[0, i] = keyValuePair.Key;//boneName
                var j = 1;
                foreach (var q in keyValuePair.Value)
                {
                    dataTable[j, i] = q.x.ToString();
                    j++;
                }
                i++;
            }

            return dataTable;
        }

        private string[,] CompareBonesBetweenAvatars(List<string> bonesNames,List<List<Quaternion>> animationDataA,List<List<Quaternion>> animationDataB, ComparingMethod comparingMethod)
        {
            int rowLength = animationDataA.Count + 1;
            Debug.Log("RowLenght "+rowLength);

            int colLength = bonesNames.Count + 1;

            string[,] dataTable = new string[rowLength,colLength];

            int i = 1;
            foreach(var item in bonesNames)//nazwy kosci
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

        private void CompareRotations(ref string[,] initialTable, List<List<Quaternion>> animationDataA,
            List<List<Quaternion>> animationDataB)
        {
            for (int j = 1; j < animationDataA.Count + 1; j++)
            {
                for (int k = 1; k < animationDataA[0].Count + 1; k++)
                {
                    float difference = Quaternion.Angle(animationDataA[j - 1][k - 1],animationDataB[j - 1][k - 1]);
                    initialTable[j, k] = difference.ToString();
                }
            }
        }
        private string StringTableAsPlainString(string[,] data)
        {
            string s = "";
            for (int i = 0; i < data.GetLength(1); i++)//rowLenght
            {
                for (int j = 0; j < data.GetLength(0); j++)//colLenght
                {
                    s += data[j, i]+";";
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
        
    }
}


