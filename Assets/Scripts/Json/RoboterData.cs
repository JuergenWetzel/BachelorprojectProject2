using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RosSharp.Urdf;
using UnityEngine.UI;

/// <summary>
/// Alle aus JSON übergebenen Parameter für die Roboter werden hier für die Initialisierung zwischengespeichert.
/// </summary>
[Serializable]
public class RoboterData
{
    public string type;
    public string typ;
    public string ip;
    public string port;
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    private GameObject roboter;
    private RosConnector rosConnector;
    public string space;
    private enum ToggleTyp
    {
        Trajektorie, Koordinatensystem, Fokus
    }

    /// <summary>
    /// Erstellt den Roboter gemäß den Anforderungen oben
    /// 
    /// Die Position des Roboters und der Toggle in den Arrays in Datas ist immer index
    /// </summary>
    /// <param name="index"></param>
    public void Init(int index)
    {
        SetValues();
        ControlNamespace();
        roboter = Spawn();
        Debug.Log(position + ", " + rotation);
        Datas.Robots[index] = roboter;
        roboter.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        roboter.GetComponentInChildren<UrdfRobot>().gameObject.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        roboter.name = name;
        SetupRosConnector();
        CreateToggles(index, ToggleTyp.Trajektorie);
        CreateToggles(index, ToggleTyp.Koordinatensystem);
        CreateToggles(index, ToggleTyp.Fokus);
    }

    /// <summary>
    /// Wenn der ROS Namespace noch kein Schrägstrich vorangestellt hat wird dieser hier hinzugefügt. 
    /// 
    /// Für die Verwendung als Name in der Simulation hat dies keine Auswirkungen
    /// </summary>
    private void ControlNamespace()
    {
        if (space.IndexOf('/') != 0)
        {
            space = "/" + space;
        }
    }

    /// <summary>
    /// Weist dem Ros Connector des Roboters die entsprechenden Werte zu
    /// </summary>
    private void SetupRosConnector()
    {
        rosConnector = roboter.GetComponentInChildren<RosConnector>();
        rosConnector.RosBridgeServerUrl = @"ws://" + ip;
        rosConnector.GetComponent<JointStateSubscriber>().Topic = space + "/joint_states";
    }

    /// <summary>
    /// Erstellt Toggle für den Roboter
    /// 
    /// Benennt die Toggle nach dem Roboter und dem Verwendungszweck des Roboters, legt danach auch den Parent fest und setzt seinen Standardwert auf false
    /// Beim Toggle zum Fokussieren wird er zustätzlich in einer Gruppe zusammengefasst, damit nur einer gleichzeitig aktiv sein kann.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="toggleTyp"></param>
    private void CreateToggles(int index, ToggleTyp toggleTyp)
    {
        GameObject toggle = GameObject.Instantiate(Datas.TogglePrefab);
        toggle.GetComponentInChildren<Text>().text = roboter.name;
        toggle.GetComponent<Toggle>().isOn = false;

        string toggleName;
        GameObject parent;
        switch (toggleTyp)
        {
            case ToggleTyp.Trajektorie:
                toggleName = "toShowTraj_";
                parent = Datas.GroupToShowTraj;
                Datas.ToShowTraj[index] = toggle.GetComponent<Toggle>();
                break;
            case ToggleTyp.Koordinatensystem:
                toggleName = "toShowKs_";
                parent = Datas.GroupToShowKs;
                Datas.ToShowKs[index] = toggle.GetComponent<Toggle>();
                break;
            case ToggleTyp.Fokus:
                toggleName = "toFocus_";
                parent = Datas.GroupToZoomRobot;
                Datas.ToFocusRobot[index] = toggle.GetComponent<Toggle>();
                toggle.GetComponent<Toggle>().group = parent.GetComponent<ToggleGroup>();
                break;
            default:
                throw new MissingReferenceException("Es ist nicht festgelegt, wofür der Toggle benötigt wird");
        }
        toggle.name = toggleName + roboter.name;
        toggle.transform.SetParent(parent.transform, false);
    }

    /// <summary>
    /// Verarbeitet, ob die optionalen Parameter name und port gesetzt sind und wie der typ geschrieben ist
    /// </summary>
    public void SetValues()
    {
        if (name == null)
        {
            name = space;
        }
        if (port != null) 
        {
            ip += ":" + port;
        }
        if (type == null)
        {
            type = typ;
        }
    }

    /// <summary>
    /// Erstellt den Roboter entsprechend seinem Typ
    /// </summary>
    /// <returns></returns>
    public GameObject Spawn()
    {
        switch (type)
        {
            case "tea":
                return GameObject.Instantiate(Datas.RobotPrefabs[0]);
            case "ted":
                return GameObject.Instantiate(Datas.RobotPrefabs[1]);
            case "tim":
                return GameObject.Instantiate(Datas.RobotPrefabs[2]);
            case "tod":
                return GameObject.Instantiate(Datas.RobotPrefabs[3]);
            case "tom":
                return GameObject.Instantiate(Datas.RobotPrefabs[4]);
            default:
                throw new MissingReferenceException("ungültiger Type");
        }
    }
}
