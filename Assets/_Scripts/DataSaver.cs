using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MarekTabiszewski.Core.AnimationDataCollection
{
    public class DataSaver : MonoBehaviour
    {

        public void SaveData(string[,] stringTable, string animationDataName)
        {
            string plainString="";
            plainString = StringTableAsPlainString(stringTable);
            SaveStringToFile(plainString,animationDataName);
        }

        public string[,] PrintRotation(Dictionary<string, List<Quaternion>> AnimationData)
        {
            int rowLength = 300;
            int colLength = 300;


            string[,] dataTable = new string[rowLength, colLength];

            for (int l = 1; l < rowLength; l++)//klatki
            {
                dataTable[l, 0] = "Klatka " + l;
            }


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

        public string[,] PrintRotationChange(Dictionary<string, List<Quaternion>> AnimationData)
        {
            int rowLength = 300;
            int colLength = 300;


            string[,] dataTable = new string[rowLength, colLength];

            for (int l = 1; l < rowLength; l++)//klatki
            {
                dataTable[l, 0] = "Klatka " + l;
            }


            var i = 1;
            foreach (var keyValuePair in AnimationData)
            {

                dataTable[0, i] = keyValuePair.Key;//boneName

                for (var k = 1; k < keyValuePair.Value.Count; k++)
                {
                    dataTable[k, i] = Quaternion.Angle(keyValuePair.Value[k-1], keyValuePair.Value[k]).ToString();
                }
                i++;
            }

            return dataTable;
        }


        public string[,] CompareBonesBetweenAvatars(Dictionary<string, List<Quaternion>> animationDataA, Dictionary<string, List<Quaternion>> animationDataB)
        {
            int rowLength = 300;
            int colLength = 300;


            string[,] dataTable = new string[rowLength, colLength];

            for (int l = 1; l < rowLength; l++)//klatki
            {
                dataTable[l, 0] = "Klatka " + l;
            }


            var i = 1;
            foreach (var keyValuePair in animationDataA)
            {

                dataTable[0, i] = keyValuePair.Key;//boneName

                List<Quaternion> rotationB = new List<Quaternion>();
                if(animationDataB.TryGetValue(keyValuePair.Key, out rotationB))
                    if(rotationB.Count != keyValuePair.Value.Count)
                        continue;

                for (var k = 1; k < keyValuePair.Value.Count; k++)
                {
                    dataTable[k, i] = Quaternion.Angle(rotationB[k], keyValuePair.Value[k]).ToString();
                }
                i++;
            }

            return dataTable;
        }

        public string[,] CompareOneBoneBetweenAvatars(Dictionary<string, List<Quaternion>> animationDataA, Dictionary<string, List<Quaternion>> animationDataB, string compareBoneName)
        {
            int rowLength = 300;
            int colLength = 300;

            string[,] dataTable = new string[rowLength, colLength];

            for (int l = 1; l < rowLength; l++)//klatki
                dataTable[l, 0] = "Klatka " + l;
            
            dataTable[0, 1] = compareBoneName;

            List<Quaternion> rotationA = new List<Quaternion>();
            List<Quaternion> rotationB = new List<Quaternion>();

            if (animationDataA.TryGetValue(compareBoneName, out rotationA) && animationDataB.TryGetValue(compareBoneName, out rotationB))
            {
                for (var k = 1; k < rotationB.Count; k++)
                    dataTable[k, 1] = Quaternion.Angle(rotationB[k], rotationA[k]).ToString();
            }
            Debug.Log("Compare done");
            return dataTable;
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
            Debug.Log("Parsing Done");
            return s;
        }
    
        private void SaveStringToFile(string text, string fileName)
        {
            string destination = Application.persistentDataPath + "/"+fileName+".csv";
            StreamWriter writer = new StreamWriter(destination, false);
            writer.Write(text);
            writer.Close();
            Debug.Log("Writing done");

        }

    }
}


