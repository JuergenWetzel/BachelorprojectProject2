using UnityEngine;
using TMPro;

public class PathChange : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    // Start is called before the first frame update
    private void Start()
    {
        Pfad.Path = Application.dataPath + "/Beschreibung.json";
        inputField.text = Pfad.Path;
    }

    /// <summary>
    /// Setzt den Pfad zur Beschreibung gleich dem Text im inputField
    /// </summary>
    public void PathChanged()
    {
        Pfad.Path = inputField.text;
    }
}
