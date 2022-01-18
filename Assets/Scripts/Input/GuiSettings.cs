using UnityEngine;
using UnityEngine.InputSystem;

public class GuiSettings : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private bool activeGui;
    public enum Cam
    {
        Main,
        TeaLeft,
        TeaRight,
        Ted,
        TimLeft,
        TimRight,
        Tod,
        Tom
    }
    public enum Robot
    {
        Tea, 
        Ted, 
        Tim, 
        Tod, 
        Tom,
        Keiner
    }

    // Start is called before the first frame update
    void Start()
    {
        activeGui = false;
        settings.Gui.SetActive(activeGui);
    }

    public void OnOpenGui()
    {
        activeGui = !activeGui;
        if (activeGui)
        {
            settings.Gui.SetActive(true);
            settings.PlayerInput.SwitchCurrentActionMap("Gui");
        }
        else
        {
            settings.Gui.SetActive(false);
            settings.PlayerInput.SwitchCurrentActionMap("Steuerung");
        }
    }

    public void OnOpenGui(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            OnOpenGui();
        }
    }
}