using UnityEngine;
using UnityEngine.InputSystem;

public class GuiSettings : MonoBehaviour
{
    [SerializeField] private Datas datas;
    private bool activeGui;

    // Start is called before the first frame update
    void Start()
    {
        activeGui = false;
        datas.Gui.SetActive(activeGui);
    }

    public void OnOpenGui()
    {
        activeGui = !activeGui;
        if (activeGui)
        {
            datas.Gui.SetActive(true);
            datas.PlayerInput.SwitchCurrentActionMap("Gui");
        }
        else
        {
            datas.Gui.SetActive(false);
            datas.PlayerInput.SwitchCurrentActionMap("Steuerung");
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