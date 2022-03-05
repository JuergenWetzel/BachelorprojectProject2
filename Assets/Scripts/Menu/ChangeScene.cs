using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private TMP_Text missingJson;

    public void SetPath(string value)
    {
        Datas.Path = value;
    }

    /// <summary>
    /// Wechselt aus dem Hauptmenu in die Simulation
    /// </summary>
    public void OnSimulationStarten()
    {
        if (Datas.Path == null) 
        {
            Datas.Path = GameObject.Find("InJson").GetComponent<TMP_InputField>().text;
        }
        if (File.Exists(Datas.Path))
        {
            Datas.JsonString = File.ReadAllText(Datas.Path);
            SceneManager.LoadScene("Simulation");
        } else
        {
            missingJson.text = "Es konnte unter " + Pfad.Path + " keine Datei gefunden werden";
        }
    }
}