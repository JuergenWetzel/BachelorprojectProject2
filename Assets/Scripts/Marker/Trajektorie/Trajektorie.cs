using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Moveit;
using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajektorie : MonoBehaviour
{
    private RobotTrajectory robotTrajectory;
    private bool showTrajektorie;
    private RosSharp.RosBridgeClient.ExecuteTrajectoryActionGoalSubscriber goalSubscriber;
    private ExecuteTrajectoryActionGoal goal;
    private string[] joint_names;
    private JointStateWriter[] jointStateWriter;
    private GameObject trajektorienpunkte;

    public bool ShowTrajektorie { get => showTrajektorie; set => showTrajektorie = value; }


    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        goalSubscriber = GetComponent<ExecuteTrajectoryActionGoalSubscriber>();
        ShowTrajektorie = true;
        jointStateWriter = GetComponentsInChildren<JointStateWriter>();
        Transform[] kinder = GetComponentsInChildren<Transform>();
        foreach (Transform kind in kinder)
        {
            if (kind.gameObject.name == "Trajektorienpunkte") 
            {
                trajektorienpunkte = kind.gameObject;
            }
        }
        trajektorienpunkte.transform.SetPositionAndRotation(Datas.Robots[0].GetComponentInChildren<UrdfRobot>().transform.position, Datas.Robots[0].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (TrajektorieChanged())
        {
            goal = goalSubscriber.executeTrajectoryActionGoal;
            LoescheTrajektorie();
        }
        if (ShowTrajektorie && goal != null)
        {
            ErstelleTrajektorie();
        }
    }

    private void ErstelleTrajektorie()
    {
        int[] sortierung = SortiereJoints();
        for (int i = 0; i < goal.goal.trajectory.joint_trajectory.points.Length; i++)
        {
            for (int j = 0; j < goal.goal.trajectory.joint_trajectory.points[i].positions.Length; j++)
            {
                jointStateWriter[sortierung[j]].Write((float)goal.goal.trajectory.joint_trajectory.points[i].positions[j]);
            }
            foreach (JointStateWriter writer in jointStateWriter)
            {
                Vector3 position = Vector3.zero;
                Vector3 rotation = Vector3.zero;
                GameObject.Instantiate(GameObject.Find("Input").GetComponent<Datas>().TrajektoriePrefab, position, Quaternion.Euler(rotation), trajektorienpunkte.GetComponent<Transform>());
            }
        }
    }

    private void LoescheTrajektorie()
    {
        foreach (Transform transformer in trajektorienpunkte.GetComponentsInChildren<Transform>())
        {
            if (trajektorienpunkte.transform == transformer) 
            {
                continue;
            }
            Destroy(transformer.gameObject);
        }
    }

    private int[] SortiereJoints()
    {
        int[] sortierung = new int[jointStateWriter.Length];
        for (int i = 0; i < goal.goal.trajectory.joint_trajectory.joint_names.Length; i++)
        {
            for (int j = 0; j < jointStateWriter.Length; j++)
            {
                if (goal.goal.trajectory.joint_trajectory.joint_names[i]==jointStateWriter[j].GetComponent<UrdfJoint>().name)
                {
                    sortierung[i] = j;
                    break;
                }
            }
        }
        return sortierung;
    }

    private bool TrajektorieChanged()
    {
        if (goal == null && goalSubscriber.executeTrajectoryActionGoal != null)
        {
            return true;
        } else if (goal != null && goalSubscriber.executeTrajectoryActionGoal != null)
        {
            if (goal != goalSubscriber.executeTrajectoryActionGoal) 
            {
                return true;
            }
        }
        return false;
    }
}
