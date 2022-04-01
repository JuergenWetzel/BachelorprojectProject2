using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Verweis auf wichtige GameObjects etc.
/// 
/// SerializeField erlaubt immer vorhandene Objekte im Editor auszuwählen, allerdings sind diese dann nicht static
/// Um auf alle Objekte zuzugreifen werden die Werte in der Awake Funktion in static Variablen kopiert
/// </summary>
public class Datas : MonoBehaviour
{
    [SerializeField] private GameObject camField;
    [SerializeField] private GameObject[] roboterPrefabsField;
    [SerializeField] private PlayerInput playerInputField;
    private static GameObject cam;
    private static GameObject[] roboterPrefabs;
    private static PlayerInput playerInput;
    private static GameObject[] robots;

    [SerializeField] private GameObject togglePrefabField;
    [SerializeField] private Slider slCamSpeedField;
    [SerializeField] private GameObject guiField;
    [SerializeField] private GameObject groupToZoomRobotField;
    [SerializeField] private GameObject groupToShowKsField;
    [SerializeField] private GameObject groupToShowTrajField;
    [SerializeField] private GameObject trajPrefabField;
    private static GameObject trajPrefab;
    private static GameObject togglePrefab;
    private static Slider slCamSpeed;
    private static GameObject gui;
    private static GameObject groupToZoomRobot;
    private static GameObject groupToShowKs;
    private static GameObject groupToShowTraj;
    private static Toggle[] toFocusRobot;
    private static Toggle[] toShowKs;
    private static Toggle[] toShowTraj;
    
    private static bool[] showKs;
    private static bool[] showTraj;
    private static string focusRobot;
    private static float camSpeed;

    private static string jsonString;
    private static string path;

    private void Awake()
    {
        cam = camField;
        roboterPrefabs = roboterPrefabsField;
        playerInput = playerInputField;
        togglePrefab = togglePrefabField;
        slCamSpeed = slCamSpeedField;
        gui = guiField;
        groupToShowKs = groupToShowKsField;
        groupToShowTraj = groupToShowTrajField;
        groupToZoomRobot = groupToZoomRobotField;
        trajPrefab = trajPrefabField;
    }

    /// <summary>
    /// Liste aller Roboter. Es wird das Parent referenziert, für Zugriff auf den eigentlichen digitalen Zwilling GetComponent<UrdfRobot>().gameObject
    /// </summary>
    public static GameObject[] Robots { get => robots; set => robots = value; }
    /// <summary>
    /// Liste aller Toggle im Gui um einen Roboter zu fokussieren
    /// </summary>
    public static Toggle[] ToFocusRobot { get => toFocusRobot; set => toFocusRobot = value; }
    /// <summary>
    /// Liste aller Toggle im Gui um ein KS anzuzeigen
    /// </summary>
    public static Toggle[] ToShowKs { get => toShowKs; set => toShowKs = value; }
    /// <summary>
    /// Liste der Werte der Toggle, ob ein KS angezeigt wird
    /// </summary>
    public static bool[] ShowKs { get => showKs; set => showKs = value; }
    /// <summary>
    /// Faktor für die Geschwindigkeit der Kamera
    /// </summary>
    public static float CamSpeed { get => camSpeed; set => camSpeed = 0.1f + 10 * value * SlCamSpeed.value * SlCamSpeed.value; }
    /// <summary>
    /// Ausgelesener String aus der Anlagenbeschreibung
    /// </summary>
    public static string JsonString { get => jsonString; set => jsonString = value; }
    /// <summary>
    /// Pfad zur Anlagenbeschreibung
    /// </summary>
    public static string Path { get => path; set => path = value; }
    /// <summary>
    /// Name des fokussierten Roboters
    /// </summary>
    public static string FocusRobot { get => focusRobot; set => focusRobot = value; }
    /// <summary>
    /// Prefab zum Erstellen der Toggle
    /// </summary>
    public static GameObject TogglePrefab { get => togglePrefab; }
    /// <summary>
    /// Prefab aller Roboter
    /// </summary>
    public static GameObject[] RobotPrefabs { get => roboterPrefabs; }
    /// <summary>
    /// Player Input zur Eingabesteuerung
    /// </summary>
    public static PlayerInput PlayerInput { get => playerInput; }
    /// <summary>
    /// Slider zur Einstellung der Kamerageschwindigkeit
    /// </summary>
    public static Slider SlCamSpeed { get => slCamSpeed; }
    /// <summary>
    /// Gui Parent. Wird benötigt um das Gui einzuschalten oder auszuschalten
    /// </summary>
    public static GameObject Gui { get => gui; }
    /// <summary>
    /// Parent für alle ToFocusRobot
    /// </summary>
    public static GameObject GroupToZoomRobot { get => groupToZoomRobot; }
    /// <summary>
    /// Parent für alle ToShowKs
    /// </summary>
    public static GameObject GroupToShowKs { get => groupToShowKs; }
    /// <summary>
    /// Kamera
    /// </summary>
    public static GameObject Cam { get => cam; }
    /// <summary>
    /// Parent für alle ToShowTraj
    /// </summary>
    public static GameObject GroupToShowTraj { get => groupToShowTraj; }
    /// <summary>
    /// Liste aller Toggle im Gui um die Trajektorie anzuzeigen
    /// </summary>
    public static Toggle[] ToShowTraj { get => toShowTraj; set => toShowTraj = value; }
    /// <summary>
    /// Liste der Werte der Toggle, ob die Trajektorie angezeigt wird
    /// </summary>
    public static bool[] ShowTraj { get => showTraj; set => showTraj = value; }
    /// <summary>
    /// Prefab für die Trajektorie
    /// </summary>
    public static GameObject TrajPrefab { get => trajPrefab; }
}
