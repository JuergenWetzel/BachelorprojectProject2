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
    [SerializeField] private bool update;

    private void Start()
    {
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

    /// <summary>
    /// Überprüft, ob für ein Roboter neue Ks erstellt werden müssen oder nicht
    /// </summary>
    /// <returns>True, wenn der referenzierte Roboter sich ändert</returns>
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

    private void SpawnKS()
    {
        arrows = GetComponentsInChildren<Arrows>();
        joints = Roboter.NotFixedJoints(robot);
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
        }
    }
}
