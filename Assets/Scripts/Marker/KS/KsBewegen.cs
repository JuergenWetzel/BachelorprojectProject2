using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KsBewegen : MonoBehaviour
{
    private UrdfJoint joint;

    public UrdfJoint Joint { get => joint; set => joint = value; }

    /// <summary>
    /// Positioniert das Koordinatensystem 1m vor dem Gelenk in Richtung der Kamera
    /// </summary>
    /// <returns></returns>
    private Vector3 Position()
    {
        Vector3 pos = joint.transform.position;
        Vector3 camDirection = joint.transform.position - Datas.Cam.transform.position;
        return pos - camDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Position();
        Vector3 rot = Joint.transform.rotation.eulerAngles;
        transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
    }
}