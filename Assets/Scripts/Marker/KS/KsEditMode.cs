using RosSharp.Urdf;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class KsEditMode : MonoBehaviour
{
    [SerializeField] private UrdfRobot robot;
    private UrdfRobot oldRobot;
    private Arrows[] arrows;
    private UrdfJoint[] joints;
    [SerializeField] private Datas datas;
    [SerializeField] private bool update;

    private void Start()
    {
        datas = GameObject.Find("Input").GetComponent<Datas>();
        update = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (RobotChanged() || update) 
        {
            SpawnKS();
            update = false;
        }
    }

    private bool RobotChanged()
    {
        if (robot != null)
        {
            if (oldRobot == null) 
            {
                oldRobot = robot;
                return true;
            }
            if (!oldRobot.Equals(robot))
            {
                oldRobot = robot;
                return true;
            }
        }
        return false;
    }

    private UrdfJoint[] NotFixedJoints(UrdfRobot urdfRobot)
    {
        UrdfJoint[] allJoints = urdfRobot.gameObject.GetComponentsInChildren<UrdfJoint>();
        List<UrdfJoint> joints = new List<UrdfJoint>();
        foreach (UrdfJoint joint in allJoints)
        {
            if (joint.JointType != UrdfJoint.JointTypes.Fixed) 
            {
                joints.Add(joint);
            }
        }
        return joints.ToArray();
    }

    private void SpawnKS()
    {
        arrows = GetComponentsInChildren<Arrows>();
        joints = NotFixedJoints(robot);
        int diff = joints.Length - arrows.Length;
        while (diff != 0) 
        {
            if (diff > 0)
            {
                Instantiate(Resources.Load("Koordinatensystem", typeof(GameObject)), GetComponent<Transform>());
                diff--;
            } else
            {
                DestroyImmediate(arrows[-diff].gameObject);
                diff++;
            }
        }
        arrows = GetComponentsInChildren<Arrows>();
        diff = joints.Length - arrows.Length;
        if (diff != 0)
        {
            throw new System.Exception("Joints und Koordinatensysteme stimmen nicht"); 
        }
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].gameObject.GetComponent<KsBewegen>().Joint = joints[i];
            arrows[i].name = "Ks_" + joints[i].name;
            arrows[i].GetComponent<KsBewegen>().Data = datas;
        }
    }
}
