using RosSharp.Urdf;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Wird einmal in der Szene benötigt und liest die Einstellungen im GUI aus, wenn es geschlossen wird.
/// </summary>
public class SaveSettings : MonoBehaviour
{
    /// <summary>
    /// Standardwerte:
    ///     Alle Koordinatensysteme aus
    ///     Kamerageschwindigkeit einstellen
    ///     
    /// </summary>
    private void Start()
    {
        bool[] show = new bool[Datas.Robots.Length];
        for (int i = 0; i < show.Length; i++)
        {
            show[i] = false;
        }
        Datas.ShowKs = show;
        Datas.ShowTraj = show;
        Roboter.SetCamSpeed();
        GameObject.Find("ScToggle").GetComponent<Scroll>().Init();
        //OnBuSaveSettings();
    }

    /// <summary>
    /// Vom Button aufgerufene Methode zum Speichern der Einstellungen
    /// </summary>
    public void OnBuSaveSettings()
    {
        SetCamFocus();
        Roboter.SetCamSpeed();
        SetVisualisierung<Trajektorie>(Datas.GroupToShowTraj);
        SetVisualisierung<KsEditMode>(Datas.GroupToShowKs);
    }

    /// <summary>
    /// Aktiviert oder deaktiviert Visualisierungen gemäß deren Toggle
    /// </summary>
    /// <typeparam name="T">Klasse, welche den Parent der Visualisierung eindeutig referenziert</typeparam>
    /// <param name="toggleGroup">Parent der Toggle für diese Visualisierung</param>
    private void SetVisualisierung<T>(GameObject toggleGroup) where T : MonoBehaviour
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                Datas.Robots[i].GetComponentInChildren<T>(true).gameObject.SetActive(true);
            } else
            {
                Datas.Robots[i].GetComponentInChildren<T>(true).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Fokussiert den ausgewählten Roboter
    /// 
    /// Liest alle Toggle für das Fokussieren eines Roboters aus, sobald einer aktiv ist wird die weitere Suche abgebrochen. Übergibt den Roboter MoveCam
    /// </summary>
    private void SetCamFocus()
    {
        Toggle[] focus = Datas.GroupToZoomRobot.GetComponentsInChildren<Toggle>();
        int index = 0;
        while (index < focus.Length) 
        {
            if (focus[index].isOn)
            {
                MoveCam(Datas.Robots[index].GetComponentInChildren<UrdfRobot>().gameObject);
                break;
            } else
            {
                index++;
            }
        }
    }

    /// <summary>
    /// Fokussiert das übergebene Objekt.
    /// 
    /// Das übergebene Objekt wird von vorne oben beobachtet
    /// </summary>
    /// <param name="focusObject">Zu fokussierendes Objekt</param>
    private void MoveCam(GameObject focusObject)
    {
        Vector3 pos = focusObject.transform.position;
        Vector3 rot = new Vector3(45, 180, 0);
        Datas.Cam.transform.rotation = Quaternion.Euler(rot);
        Datas.Cam.transform.position = pos - 10 * Datas.Cam.transform.forward;
    }
}
