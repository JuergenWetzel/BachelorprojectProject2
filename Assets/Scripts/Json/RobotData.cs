using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RobotData
{
    private RosConnector rosConnector;
    private GameObject robot;
    public string Name;
    public Vector3 Position;
    public Vector3 Rotation;
    public string ip;
    public string Topic;
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public string Ip;
    public string IP;
    public string topic;
    public string port;
    public string Port;

    public RobotData(RosConnector connector)
    {
        rosConnector = connector; 
        robot = rosConnector.gameObject.GetComponent<JointStatePatcher>().UrdfRobot.gameObject;
        Name = robot.name;
        Position = robot.transform.position;
        Rotation = robot.transform.rotation.eulerAngles;
        Topic = rosConnector.gameObject.GetComponent<JointStateSubscriber>().Topic;
        char[] url = rosConnector.RosBridgeServerUrl.ToCharArray();
        int doppelpunkt = 0;
        Ip = "";
        Port = "";
        while (url[doppelpunkt] != ':')
        {
            doppelpunkt++;
        }
        for (int i = 5; i < doppelpunkt; i++)
        {
            Ip = port + url[i];
        }
        for (int i = doppelpunkt+1; i < url.Length; i++)
        {
            Port = port + url[i];
        }
    }
}
