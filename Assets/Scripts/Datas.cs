using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Datas : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject[] robotPrefabs;
    [SerializeField] private PlayerInput playerInput;
    private static GameObject[] robots;

    [SerializeField] private GameObject togglePrefab;
    [SerializeField] private Slider slCamSpeed;
    [SerializeField] private GameObject gui;
    [SerializeField] private GameObject groupToZoomRobot;
    [SerializeField] private GameObject groupToShowKs;
    private static Toggle[] toFocusRobot;
    private static Toggle[] toShowKs;
    
    private static bool[] showKs;
    private static string focusRobot;
    private float camSpeed;

    private static string jsonString;
    private static string path;

    private static List<GameObject> trajektorien;

    [SerializeField] private GameObject trajektoriePrefab;

    public static GameObject[] Robots { get => robots; set => robots = value; }
    public static Toggle[] ToFocusRobot { get => toFocusRobot; set => toFocusRobot = value; }
    public static Toggle[] ToShowKs { get => toShowKs; set => toShowKs = value; }
    public static bool[] ShowKs { get => showKs; set => showKs = value; }
    public float CamSpeed { get => camSpeed; set => camSpeed = 0.1f + 10 * value * SlCamSpeed.value * SlCamSpeed.value; }
    public static string JsonString { get => jsonString; set => jsonString = value; }
    public static string Path { get => path; set => path = value; }
    public static string FocusRobot { get => focusRobot; set => focusRobot = value; }
    public GameObject TogglePrefab { get => togglePrefab; set => togglePrefab = value; }
    public GameObject[] RobotPrefabs { get => robotPrefabs; set => robotPrefabs = value; }
    public PlayerInput PlayerInput { get => playerInput; set => playerInput = value; }
    public Slider SlCamSpeed { get => slCamSpeed; set => slCamSpeed = value; }
    public GameObject Gui { get => gui; set => gui = value; }
    public GameObject GroupToZoomRobot { get => groupToZoomRobot; set => groupToZoomRobot = value; }
    public GameObject GroupToShowKs { get => groupToShowKs; set => groupToShowKs = value; }
    public GameObject Cam { get => cam; set => cam = value; }
    public GameObject TrajektoriePrefab { get => trajektoriePrefab; }
    public static List<GameObject> Trajektorien { get => trajektorien; set => trajektorien = value; }
}
