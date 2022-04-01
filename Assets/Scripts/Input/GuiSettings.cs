using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Verantwortlich für die Sichtbarkeit des GUIs und welche Actions gerade benutzt werden
/// </summary>
public class GuiSettings : MonoBehaviour
{
    private bool activeGui;

    /// <summary>
    /// Standardeinstellungen: 
    ///     Gui deaktiviert
    /// </summary>
    void Start()
    {
        activeGui = false;
        Datas.Gui.SetActive(activeGui);
    }

    /// <summary>
    /// Aktiviert/Deaktiviert das Gui und Deaktiviert/Aktiviert damit die Kamerasteuerung
    /// </summary>
    public void OnOpenGui()
    {
        activeGui = !activeGui;
        if (activeGui)
        {
            Datas.Gui.SetActive(true);
            Datas.PlayerInput.SwitchCurrentActionMap("Gui");
        }
        else
        {
            Datas.Gui.SetActive(false);
            Datas.PlayerInput.SwitchCurrentActionMap("Steuerung");
        }
    }

    /// <summary>
    /// Wenn das Gui mittels Tasten geöffnet wird wird diese Methode aufgerufen. 
    /// Stellt sicher, dass die Funktion nur einmal und nicht dreimal aufgerufen wird.
    /// </summary>
    /// <param name="callbackContext"></param>
    public void OnOpenGui(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            OnOpenGui();
        }
    }
}