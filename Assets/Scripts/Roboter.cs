using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Roboter
{
    /// <summary>
    /// Gibt alle Urdf Joints eines Roboters zurück, welche nicht Fixed sind
    /// </summary>
    /// <param name="urdfRobot"></param>
    /// <returns></returns>
    public static UrdfJoint[] NotFixedJoints(UrdfRobot urdfRobot)
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
}
