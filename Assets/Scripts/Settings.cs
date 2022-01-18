using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RosSharp.Urdf;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    // Wichtige Objekte alphabetisch sortiert
    [SerializeField] private GameObject[] robots;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private GameObject[] rosConnectors;
    [SerializeField] private GameObject[] koordinatensysteme;
    [SerializeField] private PlayerInput playerInput;

    // Felder in Gui
    [SerializeField] private GameObject gui;
    [SerializeField] private GameObject groupToActiveCam;
    [SerializeField] private GameObject groupToZoomRobot;
    [SerializeField] private GameObject groupToShowKS;
    private Toggle[] toActiveCams;
    private Toggle[] toFocusRobots;
    private Toggle[] toShowKse;
    [SerializeField] private Slider slCamSpeed;

    //aktuelle Werte
    private GuiSettings.Cam activeCam;
    private GuiSettings.Robot focusRobot;
    private float camSpeed;
    private bool[] showKSe;

    private void Awake()
    {
        toActiveCams = groupToActiveCam.GetComponentsInChildren<Toggle>();
        toFocusRobots = groupToZoomRobot.GetComponentsInChildren<Toggle>();
        toShowKse = groupToShowKS.GetComponentsInChildren<Toggle>();
    }

    public GameObject Robot(GuiSettings.Robot robot)
    {
        switch (robot)
        {
            case GuiSettings.Robot.Tea:
                return robots[0];
            case GuiSettings.Robot.Ted:
                return robots[1];
            case GuiSettings.Robot.Tim:
                return robots[2];
            case GuiSettings.Robot.Tod:
                return robots[3];
            case GuiSettings.Robot.Tom:
                return robots[4];
            default:
                throw new MissingReferenceException("Roboter nicht gefunden");
        }
    }
    public GameObject Camera(GuiSettings.Cam camera)
    {
        switch (camera)
        {
            case GuiSettings.Cam.Main:
                return cameras[0];
            case GuiSettings.Cam.TeaLeft:
                return cameras[1];
            case GuiSettings.Cam.TeaRight:
                return cameras[2];
            case GuiSettings.Cam.Ted:
                return cameras[3];
            case GuiSettings.Cam.TimLeft:
                return cameras[4];
            case GuiSettings.Cam.TimRight:
                return cameras[5];
            case GuiSettings.Cam.Tod:
                return cameras[6];
            case GuiSettings.Cam.Tom:
                return cameras[7];
            default: 
                throw new MissingReferenceException("Kamera nicht gefunden");
        }
    }
    public GameObject RosConnector(GuiSettings.Robot robot)
    {
        switch (robot)
        {
            case GuiSettings.Robot.Tea:
                return rosConnectors[0];
            case GuiSettings.Robot.Ted:
                return rosConnectors[1];
            case GuiSettings.Robot.Tim:
                return rosConnectors[2];
            case GuiSettings.Robot.Tod:
                return rosConnectors[3];
            case GuiSettings.Robot.Tom:
                return rosConnectors[4];
            default:
            throw new MissingReferenceException("RosConnector nicht gefunden");
        }
    }
    public GameObject Koordinatensystem(GuiSettings.Robot robot)
    {
        switch (robot)
        {
            case GuiSettings.Robot.Tea:
                return koordinatensysteme[0];
            case GuiSettings.Robot.Ted:
                return koordinatensysteme[1];
            case GuiSettings.Robot.Tim:
                return koordinatensysteme[2];
            case GuiSettings.Robot.Tod:
                return koordinatensysteme[3];
            case GuiSettings.Robot.Tom:
                return koordinatensysteme[4];
            default:
            throw new MissingReferenceException("Koordinatensystem nicht gefunden");
        }
    }
    public PlayerInput PlayerInput
    {
        get { return playerInput; }
    }
    public GameObject[] Robots
    {
        get { return robots; }
    }
    public GameObject[] Cameras
    {
        get { return cameras; }
    }
    public GameObject[] RosConnectors
    {
        get { return rosConnectors; }
    }
    public GameObject[] Koordinatensysteme
    {
        get { return koordinatensysteme; }
    }
    public GameObject Gui
    {
        get { return gui; }
    }
    public Toggle[] ToActiveCams
    {
        get { return toActiveCams; }
    }
    public Toggle[] ToFocusRobots
    {
        get { return toFocusRobots; }
    }
    public Toggle[] ToShowKSe
    {
        get { return toShowKse; }
    }
    public Slider SlCamSpeed
    {
        get { return slCamSpeed; }
    }
    public float CamSpeed
    {
        get { return camSpeed; }
        set { camSpeed = 0.1f + value * slCamSpeed.value * slCamSpeed.value; }
    }
    public bool[] ShowKse
    {
        get { return showKSe; }
        set { showKSe = value; }
    }
    public GuiSettings.Cam ActiveCam
    {
        get { return activeCam; }
        set { activeCam = value; }
    }
    public GuiSettings.Robot FocusRobot
    {
        get { return focusRobot; }
        set { focusRobot = value; }
    }
    public GameObject[] RobotJoints(int index)
    {
        List<GameObject> joints = new List<GameObject>();
        foreach (UrdfJoint joint in robots[index].GetComponentsInChildren<UrdfJoint>())
        {
            if (joint.JointType!=UrdfJoint.JointTypes.Fixed)
            {
                joints.Add(joint.gameObject);
            }
        }
        return joints.ToArray();
    }
}
