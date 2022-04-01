using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Liest den übergebenen JSON String aus und erstellt entsprechend die Objekte der Klasse Roboterdata
/// </summary>
public class JsonReader : MonoBehaviour
{
    /// <summary>
    /// Starteinstellungen
    /// 
    /// Ruft den JSON Parser auf
    /// </summary>
    private void Awake()
    {
        Reader();
    }

    /// <summary>
    /// Erstellt die Arrays in Datas entsprechend der Anzahl an erstellten Robotern
    /// </summary>
    /// <param name="count"></param>
    private void InitSettings(int count)
    {
        Datas.Robots =  new GameObject[count];
        Datas.ToFocusRobot = new Toggle[count];
        Datas.ToShowKs = new Toggle[count];
        Datas.ToShowTraj = new Toggle[count];
        Datas.ShowKs = new bool[count];
        Datas.ShowTraj = new bool[count];
    }

    /// <summary>
    /// Konvertiert den JSON String in Kleinbuchstaben und ruft den Parser auf. Speichert und initialisiert alle Roboter
    /// </summary>
    private void Reader()
    {
        string json = JsonHelper<RoboterData>.ToLowerCase(Datas.JsonString, "namespace", "space");
        RoboterData[] roboterDatas = JsonHelper<RoboterData>.FromJson(json);
        Debug.Log("Anzahl Roboter:" + roboterDatas.Length);
        InitSettings(roboterDatas.Length);
        for (int i = 0; i < roboterDatas.Length; i++)
        {
            roboterDatas[i].Init(i);
        }
    }
}