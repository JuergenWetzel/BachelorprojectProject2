using UnityEngine;
using UnityEngine.InputSystem;

public class GuiSettings : MonoBehaviour
{
    private bool activeGui;

    // Start is called before the first frame update
    void Start()
    {
        activeGui = false;
        Datas.Gui.SetActive(activeGui);
    }

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

    public void OnOpenGui(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            OnOpenGui();
        }
    }
}