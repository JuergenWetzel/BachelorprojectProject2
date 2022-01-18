using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private Settings settings;

    private void Start()
    {
        settings.ActiveCam = GuiSettings.Cam.Main;
        settings.FocusRobot = GuiSettings.Robot.Ted;
        settings.SlCamSpeed.value = 0.5f;
        bool[] showKse = new bool[5];
        for (int i = 0; i < showKse.Length; i++)
        {
            showKse[i] = false;
        }
        settings.ShowKse = showKse;
        OnBuSaveSettings();
    }

    public void OnBuSaveSettings()
    {
        SetCam();
        if (settings.ActiveCam == GuiSettings.Cam.Main) 
        {
            SetCamFocus();
            SetCamSpeed();
        }
        SetKS();
    }

    private void SetCamSpeed()
    {
        settings.CamSpeed = 25;
    }

    private void SetKS()
    {
        bool[] ks = new bool[settings.Koordinatensysteme.Length];
        for (int i = 0; i < ks.Length; i++)
        {
            ks[i] = settings.ToShowKSe[i].isOn;
        }
        settings.ShowKse = ks;
        settings.Koordinatensystem(GuiSettings.Robot.Tea).SetActive(ks[0]);
        settings.Koordinatensystem(GuiSettings.Robot.Ted).SetActive(ks[1]);
        settings.Koordinatensystem(GuiSettings.Robot.Tim).SetActive(ks[2]);
        settings.Koordinatensystem(GuiSettings.Robot.Tod).SetActive(ks[3]);
        settings.Koordinatensystem(GuiSettings.Robot.Tom).SetActive(ks[4]);
    }

    private void SetCam()
    {
        int index = 0;
        while (!settings.ToActiveCams[index].isOn)
        {
            index++;
        }
        settings.ActiveCam = (GuiSettings.Cam)index;
        EnableCam();
    }

    private void EnableCam()
    {
        for (int i = 0; i < settings.Cameras.Length; i++)
        {
            settings.Cameras[i].SetActive(false);
        }
        settings.Camera(settings.ActiveCam).SetActive(true);
    }

    private void SetCamFocus()
    {
        int index = 0;
        bool noCamFocus = false;
        bool findCamFocus = false;
        while (!noCamFocus && !findCamFocus)
        {
            if (settings.ToFocusRobots[index].isOn)
            {
                findCamFocus = true;
            } else
            {
                index++;
                if (index >= settings.ToFocusRobots.Length)
                {
                    noCamFocus = true;
                }
            }

        }
        if (!noCamFocus)
        {
            settings.FocusRobot = (GuiSettings.Robot)index;
            MoveCam();
        }
    }

    private void MoveCam()
    {
        Vector3 pos = settings.Robot(settings.FocusRobot).transform.position;
        Vector3 rot;
        switch (settings.FocusRobot)
        {
            case GuiSettings.Robot.Tea:
                rot = new Vector3(45, 180, 0);
                break;
            case GuiSettings.Robot.Ted:
                rot = new Vector3(45, 90, 0);
                break;
            case GuiSettings.Robot.Tim:
                rot = new Vector3(45, 180, 0);
                break;
            case GuiSettings.Robot.Tod:
                rot = new Vector3(45, -90, 0);
                break;
            case GuiSettings.Robot.Tom:
                rot = new Vector3(45, 180, 0);
                break;
            default:
                rot = Vector3.zero;
                break;
        }
        settings.Camera(settings.ActiveCam).transform.rotation = Quaternion.Euler(rot);
        settings.Camera(settings.ActiveCam).transform.position = pos - 10 * settings.Camera(settings.ActiveCam).transform.forward;
    }
}
