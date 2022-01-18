using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using RosSharp.RosBridgeClient;

public class JsonReader : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private RobotData[] robotDatas;

    private void Awake()
    {
        if (Pfad.Path==null)
        {
            Pfad.Path = @"C:\Users\juerg\Documents\UnityProjects\Bachelorarbeit\BachelorarbeitProject\Assets\Beschreibung.json";
        }
    }

    private void Start()
    {
        string jsonString = File.ReadAllText(Pfad.Path);
        Debug.Log(jsonString);
        robotDatas = JsonHelper.FromJson<RobotData>(jsonString);
        if (robotDatas != null)
        {
            foreach (RobotData robotData in robotDatas)
            {
                string url = @"ws://";
                int index = 0;
                string nam;
                if (robotData.Name != null)
                {
                    nam = robotData.Name;
                } else
                {
                    nam = robotData.name;
                }
                while (settings.Robots[index].name != nam)
                {
                    index++;
                    if (index >= settings.Robots.Length)
                    {
                        throw new InvalidDataException("Kein Roboter mit diesem Name: " + robotData.Name);
                    }
                }
                if (robotData.IP != null)
                {
                    url += robotData.IP;
                } else if (robotData.ip != null)
                {
                    url += robotData.ip;
                } else if (robotData.Ip != null)
                {
                    url += robotData.Ip;
                } else
                {
                    throw new InvalidDataException("Keine IP bei " + robotData.Name + " angegeben");
                }
                url += ":";
                if (robotData.port != null)
                {
                    url += robotData.port;
                } else if (robotData.Port != null)
                {
                    url += robotData.Port;
                } else
                {
                    throw new InvalidDataException("Kein Port bei " + robotData.Name + " angegeben");
                }
                settings.RosConnectors[index].GetComponent<RosConnector>().RosBridgeServerUrl = url;
                if (robotData.Topic != null)
                {
                    settings.RosConnectors[index].GetComponent<JointStateSubscriber>().Topic = robotData.Topic;
                } else if (robotData.topic != null)
                {
                    settings.RosConnectors[index].GetComponent<JointStateSubscriber>().Topic = robotData.topic;
                } else
                {
                    throw new InvalidDataException("Kein Topic bei " + robotData.Name + " angegeben");
                }
                Vector3 pos;
                Vector3 rot;
                if (robotData.Position != null)
                {
                    pos = robotData.Position;
                } else if (robotData.position != null)
                {
                    pos = robotData.position;
                } else
                {
                    throw new InvalidDataException("Keine Position bei " + robotData.Name + " angegeben");
                }
                if (robotData.Rotation != null)
                {
                    rot = robotData.Rotation;
                }
                else if (robotData.rotation != null)
                {
                    rot = robotData.rotation;
                }
                else
                {
                    throw new InvalidDataException("Keine Rotation bei " + robotData.Name + " angegeben");
                }
                settings.Robots[index].transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
            }
        } else
        {
            throw new InvalidDataException("Aus " + Pfad.Path + " konnte keine Roboterbeschreibung ausgelesen werden");
        }
    }

}