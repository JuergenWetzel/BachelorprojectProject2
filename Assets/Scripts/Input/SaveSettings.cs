using RosSharp.Urdf;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private Datas data;

    private void Start()
    {
        bool[] showKse = new bool[Datas.Robots.Length];
        for (int i = 0; i < showKse.Length; i++)
        {
            showKse[i] = false;
        }
        Datas.ShowKs = showKse;
        SetCamSpeed();
        //OnBuSaveSettings();
    }

    public void OnBuSaveSettings()
    {
        SetCamFocus();
        SetCamSpeed();
        SetKS();
    }

    private void SetCamSpeed()
    {
        data.CamSpeed = 10;
    }

    private void SetKS()
    {
        Toggle[] ks = data.GroupToShowKs.GetComponentsInChildren<Toggle>();
        Debug.Log("ksLength: " + ks.Length + " RobotsLength: " + Datas.Robots.Length + " CamLength: " + data.GroupToZoomRobot.GetComponentsInChildren<Toggle>().Length);
        List<int> indexKs = new List<int>();
        List<int> indexRoboter = new List<int>();
        for (int i = 0; i < ks.Length; i++)
        {
            if (ks[i].isOn)
            {
                indexKs.Add(i);
            }
            Datas.Robots[i].GetComponentInChildren<KsEditMode>(true).gameObject.SetActive(false);
            //Debug.Log(Datas.Robots[i].GetComponentInChildren<KsEditMode>().gameObject.activeSelf);
        }
        foreach (int index in indexKs)
        {
            int i = 0;
            while (i < ks.Length) 
            {
                if (ks[index].gameObject.name.Contains(Datas.Robots[i].name)) 
                {
                    indexRoboter.Add(i);
                }
                i++;
            }
        }
        foreach (int index in indexRoboter)
        {
            Datas.Robots[index].GetComponentInChildren<KsEditMode>(true).gameObject.SetActive(true);
        }
    }

    private void SetCamFocus()
    {
        Toggle[] focus = data.GroupToZoomRobot.GetComponentsInChildren<Toggle>();
        int indexFocus = 0;
        int indexRoboter = 0;
        while (indexFocus < focus.Length) 
        {
            if (focus[indexFocus].isOn)
            {
                break;
            } else
            {
                indexFocus++;
            }
        }
        if (indexFocus<focus.Length)
        {
            while (indexRoboter<focus.Length)
            {
                //Debug.Log(focus[indexFocus].gameObject.name + "    " + Datas.Robots[indexRoboter].name);
                if (focus[indexFocus].gameObject.name.Contains(Datas.Robots[indexRoboter].name)) 
                {
                    break;
                } else
                {
                    indexRoboter++;
                }
            }
            MoveCam(indexRoboter);
        }
    }

    private void MoveCam(int index)
    {
        Vector3 pos = Datas.Robots[index].GetComponentInChildren<UrdfRobot>().transform.position;
        Vector3 rot = new Vector3(45, 180, 0);
        data.Cam.transform.rotation = Quaternion.Euler(rot);
        data.Cam.transform.position = pos - 10 * data.Cam.transform.forward;
    }
}
