using RosSharp.Urdf;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class KsEditMode : MonoBehaviour
{
    [SerializeField] private UrdfRobot robot;
    private UrdfRobot oldRobot;
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
    /// �berpr�ft, ob f�r ein Roboter neue Ks erstellt werden m�ssen oder nicht
    /// </summary>
    /// <returns>True, wenn der referenzierte Roboter sich �ndert</returns>
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


    /// <summary>
    /// Passt die Anzahl der Koordinatensysteme an den Roboter an und ordnet sie den beweglichen Gelenken des Roboters zu
    /// </summary>
    private void SpawnKS()
    {
        Arrows[] arrows = GetComponentsInChildren<Arrows>();
        UrdfJoint[] joints = Roboter.NotFixedJoints(robot);
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
