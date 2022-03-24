using UnityEngine;
using UnityEngine.UI;

public class JsonReader : MonoBehaviour
{
    [SerializeField] private Datas datas;
    private RobotData[] robotDatas;

    private void Awake()
    {
        Reader();
    }

    private void InitSettings(int count)
    {
        Datas.Robots =  new GameObject[count];
        Datas.ToFocusRobot = new Toggle[count];
        Datas.ToShowKs = new Toggle[count];
        Datas.ToShowTraj = new Toggle[count];
        Datas.ShowKs = new bool[count];
        Datas.ShowTraj = new bool[count];
    }

    private void Reader()
    {
        RoboterData[] roboterDatas = JsonHelper<RoboterData>.FromJson(Datas.JsonString);
        Debug.Log("Anzahl Roboter:" + roboterDatas.Length);
        InitSettings(roboterDatas.Length);
        for (int i = 0; i < roboterDatas.Length; i++)
        {
            roboterDatas[i].Init(i);
        }
    }
}