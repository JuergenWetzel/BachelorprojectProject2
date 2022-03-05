using System.Collections.Generic;
using UnityEngine;
using RosSharp.Urdf;

[ExecuteInEditMode]
public class SpawnKS : MonoBehaviour
{
    private GameObject oldRobot;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject arrows;
    //private GameObject settings;

    private void Update()
    {
        if (robot != null) 
        {
            if (robot != oldRobot) 
            {
                SetKS();
                oldRobot = robot;
            }
        }
    }

    public void SetKS()
    {
        //settings = GameObject.Find("Input");
        TFMessageWriter[] tFMessageWriters = GetComponentsInChildren<TFMessageWriter>();
        UrdfJoint[] allJoints = robot.GetComponentsInChildren<UrdfJoint>();
        List<UrdfJoint> joints = new List<UrdfJoint>();
        foreach (UrdfJoint joint in allJoints)
        {
            if (joint.JointType != UrdfJoint.JointTypes.Fixed) 
            {
                joints.Add(joint);
            }
        }
        int diffKS = joints.Count - tFMessageWriters.Length;
        while (diffKS != 0)
        {
            if (diffKS > 0)
            {
                GameObject arrow = Instantiate(arrows, GetComponent<Transform>());
                //arrow.GetComponent<TFMessageWriter>().Settings = settings.GetComponent<Settings>();
                arrow.GetComponent<TFMessageWriter>().Ks = arrow;
                diffKS--;
            } else if (diffKS < 0)
            {
                GameObject.DestroyImmediate(tFMessageWriters[tFMessageWriters.Length - 1].gameObject, true);
                diffKS++;
            }
        }
        tFMessageWriters = GetComponentsInChildren<TFMessageWriter>();
        if (joints.Count != tFMessageWriters.Length) 
        {
            throw new MissingComponentException("Anzahl an Children und Joints stimmt nicht überein");
        } else
        {
            for (int i = 0; i < tFMessageWriters.Length; i++)
            {
                tFMessageWriters[i].Joint = joints[i].gameObject;
                tFMessageWriters[i].gameObject.name = "KS_" + tFMessageWriters[i].Joint.name;
                tFMessageWriters[i].DefRotation = new Vector3(0, 0, 0);
            }
        }
    }
}
