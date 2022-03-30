using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajektorie : MonoBehaviour
{
    private double time;
    private int index;
    private GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        robot = GetComponentInParent<UrdfRobot>().gameObject;
        time = 0;
        index = 0;
        while (true)
        {
            if (Datas.Robots[index].GetComponentInChildren<UrdfRobot>().Equals(robot))
            {
                break;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.4) 
        {
            UrdfJoint[] joints = Roboter.NotFixedJoints(Datas.Robots[index].GetComponentInChildren<UrdfRobot>());
            for (int i = 0; i < joints.Length; i++)
            {
                GameObject traj = GameObject.Instantiate(Datas.TrajPrefab, gameObject.transform);
                traj.transform.SetPositionAndRotation(joints[i].transform.position, joints[i].transform.rotation);
                GameObject.Destroy(traj, 5f);
            }
        }
    }
}
