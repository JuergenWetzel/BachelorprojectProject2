using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text missingJson;
    [SerializeField] private TMP_InputField inputField;
    private string path;

    /// <summary>
    /// Wechselt aus dem Hauptmenu in die Simulation
    /// 
    /// Speichert zuerst den Dateipfad, damit die Simulation geladen wird. Wenn er existiert wird die Szene gewechselt, andernfalls wird eine kleine Meldung eingeblendet.
    /// </summary>
    public void OnSimulationLaden()
    {
        path = inputField.text;
        if (File.Exists(path))
        {
            Datas.JsonString = File.ReadAllText(path);
            SceneManager.LoadScene("Simulation");
        } else
        {
            missingJson.text = "Es konnte unter " + path + " keine Datei gefunden werden";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField.text = Application.dataPath + "/Beschreibung.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
