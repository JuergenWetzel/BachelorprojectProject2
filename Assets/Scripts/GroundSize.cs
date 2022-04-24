using RosSharp.Urdf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSize : MonoBehaviour
{
    /// <summary>
    /// Passt die Größe des Bodens an die Position der Roboter an
    /// </summary>
    void Start()
    {
        float minx = 0;
        float maxx = 0;
        float minz = 0;
        float maxz = 0;
        foreach (GameObject roboter in Datas.Robots)
        {
            Vector3 position = roboter.GetComponentInChildren<UrdfRobot>().transform.position;
            if (position.x < minx)
            {
                minx = position.x;
            }
            if (position.x > maxx)
            {
                maxx = position.x;
            }
            if (position.z < minz)
            {
                minz = position.z;
            }
            if (position.z > maxz)
            {
                maxz = position.z;
            }
        }
        gameObject.transform.position = new Vector3((maxx - minx) / 2, 0, (maxz - minz) / 2);
        gameObject.transform.localScale = new Vector3((maxx - minx) / 2 + 30, 1, (maxz - minz) / 2 + 30);
    }
}
