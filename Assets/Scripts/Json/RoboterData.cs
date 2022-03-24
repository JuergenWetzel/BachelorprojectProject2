using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RosSharp.Urdf;
using UnityEngine.UI;

[Serializable]
public class RoboterData
{
    public string type;
    public string Type;
    public string ip;
    public string IP;
    public string Ip;
    public string Port;
    public string port;
    public string name;
    public string Name;
    public Vector3 position;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 rotation;
    public string Namespace;
    private GameObject roboter;
    private RosConnector rosConnector;
    public string space;

    public void Init(int index)
    {
        if (space.IndexOf('/') != 0)
        {
            space = "/" + space;
        }
        Debug.Log("init Robot Nr. " + index);
        Namespace = space;
        //SetValues();
        roboter = Spawn();
        Debug.Log(position + ", " + rotation);
        Datas.Robots[index] = roboter;
        roboter.tag = "Roboter";
        roboter.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        roboter.GetComponentInChildren<UrdfRobot>().gameObject.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        roboter.name = name;
        SetupRosConnector();
        CreateToggles(index);
    }

    private void SetupRosConnector()
    {
        rosConnector = roboter.GetComponentInChildren<RosConnector>();
        rosConnector.RosBridgeServerUrl = @"ws://" + ip;
        rosConnector.GetComponent<JointStateSubscriber>().Topic = Namespace + "/joint_states";
    }

    private void CreateToggles(int index)
    {
        GameObject toFocus = GameObject.Instantiate(Datas.TogglePrefab, Datas.GroupToZoomRobot.transform);
        toFocus.GetComponent<Toggle>().group = Datas.GroupToZoomRobot.GetComponent<ToggleGroup>();
        toFocus.GetComponent<Toggle>().isOn = false;
        toFocus.name = "toFocus_" + roboter.name;
        toFocus.GetComponentInChildren<Text>().text = roboter.name;
        GameObject toKs = GameObject.Instantiate(Datas.TogglePrefab, Datas.GroupToShowKs.transform);
        toKs.GetComponent<Toggle>().group = Datas.GroupToShowKs.GetComponent<ToggleGroup>();
        toKs.GetComponent<Toggle>().isOn = false;
        toKs.name = "toShowKs_" + roboter.name;
        toKs.GetComponentInChildren<Text>().text = roboter.name;
        Datas.ToFocusRobot[index] = toFocus.GetComponent<Toggle>();
        Datas.ToShowKs[index] = toKs.GetComponent<Toggle>();
        Datas.ShowKs[index] = false;
        GameObject toTraj = GameObject.Instantiate(Datas.TogglePrefab, Datas.GroupToShowTraj.transform);
        toTraj.GetComponent<Toggle>().isOn = false;
        toTraj.name = "toShowTraj_" + roboter.name;
        toTraj.GetComponentInChildren<Text>().text = roboter.name;
        Datas.ToShowTraj[index] = toTraj.GetComponent<Toggle>();
        Datas.ShowTraj[index] = false;
    }

    public void SetValues()
    {
        if (type == null)
        {
            type = Type;
        }
        type = type.ToLower();
        if (ip == null)
        {
            if (Ip == null) 
            {
                ip = IP;
            } else
            {
                ip = Ip;
            }
        }
        if (port != null)
        {
            ip += ":" + port;
        } else if (Port != null)
        {
            ip += ":" + Port;
        }
        if (name == null)
        {
            if (Name!=null)
            {
                name = Name;
            }
            else
            {
                name = Namespace;
            }
        }
        if (Position != Vector3.zero)
        {
            position = Position;
        }
        if (rotation == Vector3.zero) 
        {
            rotation = Rotation;
        }

    }

    public GameObject Spawn()
    {
        switch (type)
        {
            case "tea":
                return SpawnTea();
            case "ted":
                return SpawnTed();
            case "tim":
                return SpawnTim();
            case "tod":
                return SpawnTod();
            case "tom":
                return SpawnTom();
            default:
                throw new MissingReferenceException("ungültiger Type");
        }
    }

    private GameObject SpawnTea()
    {
        return GameObject.Instantiate(Datas.RobotPrefabs[0]);
    }
    private GameObject SpawnTed()
    {
        return GameObject.Instantiate(Datas.RobotPrefabs[1]);
    }
    private GameObject SpawnTim()
    {
        return GameObject.Instantiate(Datas.RobotPrefabs[2]);
    }
    private GameObject SpawnTod()
    {
        return GameObject.Instantiate(Datas.RobotPrefabs[3]);
    }
    private GameObject SpawnTom()
    {
        return GameObject.Instantiate(Datas.RobotPrefabs[4]);
    }
}
