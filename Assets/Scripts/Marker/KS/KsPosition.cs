using RosSharp.Urdf;
using UnityEngine;
using static System.Math;

public class KsPosition : MonoBehaviour
{
    [SerializeField] private UrdfJoint joint;
    [SerializeField] private Vector3 rotateRos;
    [SerializeField] private Datas datas;
    private Vector3 rotation;
    private bool basis;
    private GameObject parentJoint;

    public UrdfJoint Joint { get => joint; set => joint = value; }
    public Vector3 RotateRos { get => rotateRos; set => rotateRos = value; }
    public Vector3 Rotation { get => rotation; private set => rotation = value; }
    public GameObject ParentJoint { get => parentJoint; set => parentJoint = value; }
    public bool Basis { get => basis; set => basis = value; }

    // Update is called once per frame
    void Update()
    {
        rotation = joint.gameObject.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Vektordrehung2() + rotateRos);
        transform.position = joint.transform.position - (joint.transform.position - datas.Cam.transform.position).normalized;
    }

    private Vector3 Vektordrehung()
    {
        double konvInRad = PI / 180;
        double[] rotDouble = new double[3] { (double)rotateRos.x * konvInRad, (double)rotateRos.y * konvInRad, (double)rotateRos.z * konvInRad };
        double[] rotGedreht = new double[3];
        rotGedreht[0] = (Cos(rotDouble[0]) * Cos(rotDouble[2]) - Sin(rotDouble[0]) * Cos(rotDouble[1]) * Sin(rotDouble[2])) * rotation.x
            + (-Cos(rotDouble[0]) * Sin(rotDouble[2]) - Sin(rotDouble[0]) * Cos(rotDouble[1]) * Cos(rotDouble[2])) * rotation.y
            + (Sin(rotDouble[0]) * Sin(rotDouble[1])) * rotation.z;
        rotGedreht[1] = (Sin(rotDouble[0]) * Cos(rotDouble[2]) + Cos(rotDouble[0]) * Cos(rotDouble[1]) * Sin(rotDouble[2])) * rotation.x
            + (-Sin(rotDouble[0]) * Sin(rotDouble[2]) + Cos(rotDouble[0]) * Cos(rotDouble[1]) * Cos(rotDouble[2])) * rotation.y
            + (-Cos(rotDouble[0]) * Sin(rotDouble[1])) * rotation.z;
        rotGedreht[2] = (Sin(rotDouble[1]) * Sin(rotDouble[2])) * rotation.x
            + (Sin(rotDouble[1]) * Cos(rotDouble[2])) * rotation.y
            + Cos(rotDouble[1]) * rotation.z;
        Debug.Log(rotation.x + ", " + rotation.y + ",  " + rotation.z);
        Debug.Log(rotGedreht);
        return new Vector3((float)rotGedreht[0], (float)rotGedreht[1], (float)rotGedreht[2]);
    }

    private Vector3 Vektordrehung2()
    {
        double konvInRad = PI / 180;
        double sinAlpha;
        double sinBeta;
        double sinGamma;
        double cosAlpha;
        double cosBeta;
        double cosGamma;
        double[] rotDouble = new double[3] { (double)rotateRos.x * konvInRad, (double)rotateRos.y * konvInRad, (double)rotateRos.z * konvInRad };
        switch (rotateRos.x)
        {
            case 0:
                cosAlpha = 1;
                sinAlpha = 0;
                break;
            case 90:
                cosAlpha = 0;
                sinAlpha = 1;
                break;
            case -90:
                cosAlpha = 0;
                sinAlpha = -1;
                break;
            case 180:
                cosAlpha = -1;
                sinAlpha = 0;
                break;
            default:
                cosAlpha = Cos((double)rotateRos.x * konvInRad);
                sinAlpha = Sin((double)rotateRos.x * konvInRad);
                break;
        }
        switch (rotateRos.y)
        {
            case 0:
                cosBeta = 1;
                sinBeta = 0;
                break;
            case 90:
                cosBeta = 0;
                sinBeta = 1;
                break;
            case -90:
                cosBeta = 0;
                sinBeta = -1;
                break;
            case 180:
                cosBeta = -1;
                sinBeta = 0;
                break;
            default:
                cosBeta = Cos((double)rotateRos.y * konvInRad);
                sinBeta = Sin((double)rotateRos.y * konvInRad);
                break;
        }
        switch (rotateRos.z)
        {
            case 0:
                cosGamma = 1;
                sinGamma = 0;
                break;
            case 90:
                cosGamma = 0;
                sinGamma = 1;
                break;
            case -90:
                cosGamma = 0;
                sinGamma = -1;
                break;
            case 180:
                cosGamma = -1;
                sinGamma = 0;
                break;
            default:
                cosGamma = Cos((double)rotateRos.z * konvInRad);
                sinGamma = Sin((double)rotateRos.z * konvInRad);
                break;
        }

        double[] rotGedreht = new double[3];
        rotGedreht[0] = (cosAlpha * cosGamma - sinAlpha * cosBeta * sinGamma) * rotation.x
            + (-cosAlpha * sinGamma - sinAlpha * cosBeta * cosGamma) * rotation.y
            + (sinAlpha * sinBeta) * rotation.z;
        rotGedreht[1] = (sinAlpha * cosGamma + cosAlpha * cosBeta * sinGamma) * rotation.x
            + (-sinAlpha * sinGamma + cosAlpha * cosBeta * cosGamma) * rotation.y
            + (-cosAlpha * sinBeta) * rotation.z;
        rotGedreht[2] = (sinBeta * sinGamma) * rotation.x
            + (sinBeta * cosGamma) * rotation.y
            + cosBeta * rotation.z;
        Debug.Log(rotation.x + ", " + rotation.y + ",  " + rotation.z);
        Debug.Log(rotGedreht[0] + ", " + rotGedreht[1] + ", " + rotGedreht[2]);
        return new Vector3(GradAngepasst((float)rotGedreht[0]), GradAngepasst((float)rotGedreht[1]), GradAngepasst((float)rotGedreht[2]));
    }

    private float GradAngepasst(float value)
    {
        while (value > 180) 
        {
            value -= 360;
        }
        while (value < -180) 
        {
            value += 360;
        }
        return value;
    }
}
