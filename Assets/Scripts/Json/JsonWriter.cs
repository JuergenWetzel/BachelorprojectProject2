using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using RosSharp.RosBridgeClient;


[Serializable]
public class JsonWriter : MonoBehaviour
{
    [SerializeField] Settings settings;
    private RobotData[] Roboter;
    private string jsonString;

    private void Start()
    {
        Roboter = new RobotData[settings.RosConnectors.Length];
        for (int i = 0; i < settings.RosConnectors.Length; i++)
        {
            Roboter[i] = new RobotData(settings.RosConnectors[i].GetComponent<RosConnector>());
        }
        WriteJson();
    }

    private void WriteJson()
    {
        jsonString = JsonHelper.ToJson<RobotData>(Roboter, true);
        File.WriteAllText(Pfad.Path, jsonString);
        Debug.Log("Json beschrieben: " + jsonString);
    }
}