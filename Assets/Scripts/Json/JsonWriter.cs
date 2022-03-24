using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using RosSharp.RosBridgeClient;
using UnityEngine.UI;

[Serializable]
public class JsonWriter : MonoBehaviour
{
    private RoboterData[] Roboter;
    private string jsonString;

    private void Start()
    {
        int count = Datas.GroupToShowKs.GetComponentsInChildren<Toggle>().Length;
        GameObject[] roboter = GameObject.FindGameObjectsWithTag("Roboter");
        Roboter = new RoboterData[count];
        for (int i = 0; i < count; i++)
        {
            Roboter[i] = new RoboterData();
            
        }
        WriteJson();
    }

    private void WriteJson()
    {
        jsonString = JsonHelper<RoboterData>.ToJson(Roboter, true);
        File.WriteAllText(Pfad.Path, jsonString);
        Debug.Log("Json beschrieben: " + jsonString);
    }
}