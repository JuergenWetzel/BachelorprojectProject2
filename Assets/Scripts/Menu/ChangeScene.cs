using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private TMP_Text missingJson;

    /// <summary>
    /// Wechselt aus dem Hauptmenu in die Simulation
    /// </summary>
    public void OnSimulationStarten()
    {
        if (CheckFile(Pfad.Path)){
            Debug.Log("Simulation starten " + Pfad.Path);
            SceneManager.LoadScene("Simulation");
        } else
        {
            missingJson.text = "Es konnte unter " + Pfad.Path + " keine Datei gefunden werden";
        }
    }

    /// <summary>
    /// Prüft, ob der angegebene Pfad eine .json Datei ist. 
    /// Gibt auch true zurück, wenn im Name .json steckt
    /// </summary>
    /// <param name="path"></param>
    /// <returns>True falls es eine .json ist</returns>
    private bool CheckFile(string path)
    {
        string fileType = ".json";
        if (path.Contains(fileType) && File.Exists(path))
        {
            return true;
        } else 
        {
            return false;
        }
    }
}