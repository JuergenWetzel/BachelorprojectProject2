using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        activeState = ActiveState.Nothing;
    }

    // Update is called once per frame
    /// <summary>
    /// Wählt die Bewegungsart der Kamera in diesem Frame aus. Anschließend wird die Kamera entsprechend bewegt.
    /// </summary>
    void Update()
    {
        Vector3 deltaPos = Vector3.zero;
        switch (activeState)
        {
            case ActiveState.Nothing:
                break;
            case ActiveState.Zoom:
                deltaPos = Zoom();
                break;
            case ActiveState.ZoomScroll:
                deltaPos = ZoomScroll();            
                break;
            case ActiveState.Turn:
                Vector3 deltaRot = Turn();                
                Datas.Cam.transform.Rotate(deltaRot * Time.deltaTime * Datas.CamSpeed);
                Datas.Cam.transform.rotation = Quaternion.Euler(Datas.Cam.transform.rotation.eulerAngles.x, Datas.Cam.transform.rotation.eulerAngles.y, 0);
                break;
            case ActiveState.Move:
                deltaPos = Move();
                break;
            default:
                throw new MissingReferenceException("Kein Status ausgewählt");
        }
        if (activeState != ActiveState.Nothing && activeState != ActiveState.Turn)
        {
            Datas.Cam.transform.position += deltaPos * Time.deltaTime * Datas.CamSpeed;
        }
    }

    /// <summary>
    /// Auswahl der aktiven Kamera
    /// </summary>
    /// <param name="callbackContext"></param>
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

    /// <summary>
    /// Bewegt die Kamera nach links, rechts, oben oder unten
    /// </summary>
    /// <returns>Vector3 mit Bewegungsrichtung</returns>
    private Vector3 Move()
    {
        return Datas.Cam.transform.right * move.x + Datas.Cam.transform.up * move.y;
    }

    /// <summary>
    /// Dreht die Kamera gemäß der Mausbewegung
    /// </summary>
    /// <returns>Vector3 mit Drehrichtung</returns>
    private Vector3 Turn()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        return new Vector3(mouseDelta.y, -mouseDelta.x, 0);
    }

    /// <summary>
    /// Bewegt die Kamera vorwärts oder rückwärts gemäß der Mausbewegung
    /// </summary>
    /// <returns>Vector3 mit Bewegungsrichtung</returns>
    private Vector3 Zoom()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        return Datas.Cam.transform.forward * mouseDelta.y;
    }

    /// <summary>
    /// Bewegt die Kamera vorwärts oder rückwärts gemäß dem Mausrad
    /// </summary>
    /// <returns>Vector3 mit Bewegungsrichtung</returns>
    private Vector3 ZoomScroll()
    {
        return Datas.Cam.transform.forward * scroll / 10;
    }
}
