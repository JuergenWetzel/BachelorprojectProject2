using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajektorie : MonoBehaviour
{
    private double time;
    [SerializeField] private GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    /// <summary>
    /// Alle 0,4 Sekunden wird ein Punkt an allen Joints erstellt.
    /// Wird nach 5 Sekunden automatisch wieder gelöscht
    /// </summary>
    void Update()       
    {
        time += Time.deltaTime;
        if (time > 0.4) 
        {
            UrdfJoint[] joints = Roboter.NotFixedJoints(robot.GetComponentInChildren<UrdfRobot>());
            for (int i = 0; i < joints.Length; i++)
            {
                GameObject traj = GameObject.Instantiate(Datas.TrajPrefab, gameObject.transform);
                traj.transform.SetPositionAndRotation(joints[i].transform.position, joints[i].transform.rotation);
                GameObject.Destroy(traj, 5f);
            }
        }
    }
}