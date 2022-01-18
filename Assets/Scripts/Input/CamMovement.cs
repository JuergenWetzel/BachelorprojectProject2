using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private enum ActiveState
    {
        Nothing, 
        Zoom,
        ZoomScroll,
        Turn,
        Move
    }
    private Vector2 mouseDelta;
    private Vector2 move;
    private float scroll;
    private ActiveState activeState;
    private Vector3 deltaPos;
    private Vector3 deltaRot; 

    // Start is called before the first frame update
    void Start()
    {
        activeState = ActiveState.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        deltaRot = Vector3.zero;
        deltaPos = Vector3.zero;
        switch (activeState)
        {
            case ActiveState.Nothing:
                break;
            case ActiveState.Zoom:
                Zoom();
                break;
            case ActiveState.ZoomScroll:
                ZoomScroll();
                break;
            case ActiveState.Turn:
                Turn();
                break;
            case ActiveState.Move:
                Move();
                break;
            default:
                break;
        }
        if (activeState != ActiveState.Nothing)
        {
            settings.Camera(GuiSettings.Cam.Main).transform.position += deltaPos * Time.deltaTime * settings.CamSpeed;
            settings.Camera(GuiSettings.Cam.Main).transform.Rotate(deltaRot * Time.deltaTime * settings.CamSpeed);
            settings.Camera(GuiSettings.Cam.Main).transform.rotation = Quaternion.Euler(settings.Camera(GuiSettings.Cam.Main).transform.rotation.eulerAngles.x, settings.Camera(GuiSettings.Cam.Main).transform.rotation.eulerAngles.y, 0);
        }
    }

    public void OnCamMovement(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.name == "Move")
        {
            move = callbackContext.action.ReadValue<Vector2>();
            if (move.x == 0 && move.y == 0)
            {
                activeState = ActiveState.Nothing;
            }
            else
            {
                activeState = ActiveState.Move;
            }
        } else
        {
            if (callbackContext.canceled)
            {
                activeState = ActiveState.Nothing;
            }
            else if (callbackContext.started)
            {
                switch (callbackContext.action.name)
                {
                    case "Turn":
                        activeState = ActiveState.Turn;
                        break;
                    case "Zoom":
                        activeState = ActiveState.Zoom;
                        break;
                    case "ZoomScroll":
                        scroll = callbackContext.action.ReadValue<Vector2>().y;
                        activeState = ActiveState.ZoomScroll;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Move()
    {
        deltaPos += settings.Camera(GuiSettings.Cam.Main).transform.right * move.x + settings.Camera(GuiSettings.Cam.Main).transform.up * move.y;
    }
    private void Turn()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        deltaRot += new Vector3(mouseDelta.y, -mouseDelta.x, 0);
    }
    private void Zoom()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        deltaPos += settings.Camera(GuiSettings.Cam.Main).transform.forward * mouseDelta.y;
    }
    private void ZoomScroll()
    {
        deltaPos += settings.Camera(GuiSettings.Cam.Main).transform.forward * scroll / 10;
    }
}
