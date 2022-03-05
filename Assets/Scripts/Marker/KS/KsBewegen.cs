using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KsBewegen : MonoBehaviour
{
    [SerializeField] private UrdfJoint joint;
    [SerializeField] private Datas data;

    public UrdfJoint Joint { get => joint; set => joint = value; }
    public Datas Data { get => data; set => data = value; }

    private Vector3 Position()
    {
        Vector3 pos = joint.transform.position;
        Vector3 camDirection = joint.transform.position - Data.Cam.transform.position;

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