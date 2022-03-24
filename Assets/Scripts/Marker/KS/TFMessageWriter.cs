using UnityEngine;

public class TFMessageWriter : MonoBehaviour
{
    private bool showMarker = true; 
    [SerializeField] private GameObject joint;
    [SerializeField] private string frame_id;
    [SerializeField] private GameObject ks;
    [SerializeField] private Vector3 defRotation;
    private Vector3 translation; //nicht ben�tigt?
    private Quaternion rotation;
    
    public bool ShowMarker
    {
        set { showMarker = value; }
        get { return showMarker; }
    }
    public string Frame_id
    {
        get { return frame_id; }
    }
    public Vector3 Translation
    {
        set { translation = value; }
        get { return translation; }
    }
    public Quaternion Rotation
    {
        set { rotation = value; }
        get { return rotation; }
    }
    public Vector3 DefRotation
    {
        get { return defRotation; }
        set { defRotation = value; }
    }
    public GameObject Joint
    {
        set { joint = value; }
        get { return joint; }
    }
    public GameObject Ks
    {
        set { ks = value; }
        get { return ks; }
    }



    // Start is called before the first frame update
    void Start()
    {
        if (ks == null)
        {
            ks = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showMarker) { 
            WriteUpdate(); 
        }
    }

    private void WriteUpdate()
    {
        Debug.Log(rotation.eulerAngles);
        ks.transform.rotation = Quaternion.Euler(rotation.eulerAngles + defRotation);
        Vector3 camDirection = Datas.Cam.transform.position - joint.transform.position;
        //camDirection.x *= 3;
        camDirection = camDirection.normalized;
        ks.transform.position = joint.transform.position + camDirection;
    }
}
