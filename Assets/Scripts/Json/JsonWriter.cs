using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using RosSharp.RosBridgeClient;


[Serializable]
public class JsonWriter : MonoBehaviour
{
    [SerializeField] string path;
    private RobotData[] Roboter;
    private string jsonString;

    private void Start()
    {
        RosConnector[] rosConnectors=GetComponentsInChildren<RosConnector>();
        Roboter = new RobotData[rosConnectors.Length];
        for (int i = 0; i < rosConnectors.Length; i++)
        {
            Roboter[i] = new RobotData(rosConnectors[i]);
        }
        WriteJson();
    }

    private void WriteJson()
    {
        path = Pfad.Path;
        jsonString = JsonHelper.ToJson<RobotData>(Roboter, true);
        Debug.Log(jsonString);
        File.WriteAllText(path, jsonString);
        Debug.Log("Json beschrieben");
    }
}