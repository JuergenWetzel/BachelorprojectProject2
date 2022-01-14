using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSettings : MonoBehaviour
{
    private GuiSettings.ActiveCam activeCam;
    private GuiSettings.RobotZoom robotZoom;
    [SerializeField] private Camera[] cam;
    [SerializeField] private GameObject toActiveCam;
    [SerializeField] private GameObject toGoToRobot;
    [SerializeField] private GameObject[] robot;
    private Toggle[] tosActiveCam;
    private Toggle[] tosGoToRobot;
    [SerializeField] private Slider slSpeed;

    public GuiSettings.ActiveCam ActiveCam
    {
        get { return activeCam; }
    }

    public GameObject Cam
    {
        get
        {
            switch (activeCam)
            {
                case GuiSettings.ActiveCam.Main:
                    return cam[0].gameObject;
                case GuiSettings.ActiveCam.TeaLeft:
                    return cam[1].gameObject;
                case GuiSettings.ActiveCam.TeaRight:
                    return cam[2].gameObject;
                case GuiSettings.ActiveCam.Ted:
                    return cam[3].gameObject;
                case GuiSettings.ActiveCam.TimLeft:
                    return cam[4].gameObject;
                case GuiSettings.ActiveCam.TimRight:
                    return cam[5].gameObject;
                case GuiSettings.ActiveCam.Tod:
                    return cam[6].gameObject;
                case GuiSettings.ActiveCam.Tom:
                    return cam[7].gameObject;
                default:
                    throw new MissingReferenceException("Keine aktive Kamera ausgewählt");
            }
        }
    }

    private void Start()
    {
        tosActiveCam = toActiveCam.GetComponentsInChildren<Toggle>();
        tosGoToRobot = toGoToRobot.GetComponentsInChildren<Toggle>();
        tosActiveCam[0].isOn = true;
        tosGoToRobot[0].isOn = true;
        slSpeed.value = 0.5f;
        OnBuSaveSettings();
    }

    public void OnBuSaveSettings()
    {
        SetCam();
        SetCamZoom();
        if (activeCam == GuiSettings.ActiveCam.Main) 
        {
            GetComponent<CamMovement>().Speed = 25 * slSpeed.value * slSpeed.value;
        }
    }

    private void SetCam()
    {
        int index = 0;
        while (!tosActiveCam[index].isOn)
        {
            index++;
        }
        switch (index)
        {
            case 0:
                activeCam = GuiSettings.ActiveCam.Main;
                break;
            case 1:
                activeCam = GuiSettings.ActiveCam.TeaLeft;
                break;
            case 2:
                activeCam = GuiSettings.ActiveCam.TeaRight;
                break;
            case 3:
                activeCam = GuiSettings.ActiveCam.Ted;
                break;
            case 4:
                activeCam = GuiSettings.ActiveCam.TimLeft;
                break;
            case 5:
                activeCam = GuiSettings.ActiveCam.TimRight;
                break;
            case 6:
                activeCam = GuiSettings.ActiveCam.Tod;
                break;
            case 7:
                activeCam = GuiSettings.ActiveCam.Tom;
                break;
            default:
                throw new KeyNotFoundException("Keine Kamera ausgewählt");
        }
        EnableCam();

    }

    private void EnableCam()
    {
        for (int i = 0; i < cam.Length; i++)
        {
            cam[i].gameObject.SetActive(false);
        }
        switch (activeCam)
        {
            case GuiSettings.ActiveCam.Main:
                cam[0].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.TeaLeft:
                cam[1].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.TeaRight:
                cam[2].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.Ted:
                cam[3].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.TimLeft:
                cam[4].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.TimRight:
                cam[5].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.Tod:
                cam[6].gameObject.SetActive(true);
                break;
            case GuiSettings.ActiveCam.Tom:
                cam[7].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void SetCamZoom()
    {
        if (activeCam == GuiSettings.ActiveCam.Main)
        {
            int index = 0;
            while (!tosGoToRobot[index].isOn)
            {
                index++;
                if (index>=tosGoToRobot.Length)
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    robotZoom = GuiSettings.RobotZoom.Tea;
                    MoveCam(index);
                    break;
                case 1:
                    robotZoom = GuiSettings.RobotZoom.Ted;
                    MoveCam(index);
                    break;
                case 2:
                    robotZoom = GuiSettings.RobotZoom.Tim;
                    MoveCam(index);
                    break;
                case 3:
                    robotZoom = GuiSettings.RobotZoom.Tod;
                    MoveCam(index);
                    break;
                case 4:
                    robotZoom = GuiSettings.RobotZoom.Tom;
                    MoveCam(index);
                    break;
                default:
                    robotZoom = GuiSettings.RobotZoom.Keiner;
                    break;
            }
            
        }
    }

    private void MoveCam(int index)
    {
        Vector3 pos = robot[index].transform.position;
        Vector3 rot;
        switch (index)
        {
            case 1:
                rot = new Vector3(45, 90, 0);
                break;
            case 3:
                rot = new Vector3(45, -90, 0);
                break;
            default:
                rot = new Vector3(45, 180, 0);
                break;
        }
        cam[0].gameObject.transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
        cam[0].gameObject.transform.position -= 10 * cam[0].gameObject.transform.forward;
    }
}
