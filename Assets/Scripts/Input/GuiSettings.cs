using UnityEngine;
using UnityEngine.InputSystem;

public class GuiSettings : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private bool activeGui;
    [SerializeField] private GameObject gui;
    public enum ActiveCam
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
    public enum RobotZoom
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
        gui.SetActive(activeGui);
    }

    public void OnOpenGui()
    {
        activeGui = !activeGui;
        if (activeGui)
        {
            gui.SetActive(true);
            playerInput.SwitchCurrentActionMap("Gui");
        }
        else
        {
            gui.SetActive(false);
            playerInput.SwitchCurrentActionMap("Steuerung");
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