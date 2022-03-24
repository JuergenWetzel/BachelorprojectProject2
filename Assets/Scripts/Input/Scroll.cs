using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    private float[] splits;
    private Scrollbar scrollbar;
    private int steps;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        float size = 15f / Datas.ToFocusRobot.Length;
        if (size >= 1)
        {
            gameObject.SetActive(false);
            return;
        }
        scrollbar.size = size;
        steps = Datas.ToFocusRobot.Length - 14;
        splits = new float[steps];
        for (int i = 0; i < splits.Length; i++)
        {
            splits[i] = 1f / splits.Length * (i + 1f);
        }
        splits[splits.Length - 1] = 1;
        string debug = "";
        for (int i = 0; i < splits.Length; i++)
        {
            debug += splits[i] + " ";
        }
        Debug.Log(debug);
        OnChange(0.0f);
    }

    public void Init()
    {
        Start();
    }

    public void OnChange(float value)
    {
        int pos = 0;
        while (splits[pos] < value) 
        {
            pos++;
            if (pos == splits.Length)
            {
                pos--;
                break;
            }
        }
        Debug.Log(Datas.ToShowKs.Length + "   " + value+ "   "+pos);
        for (int i = pos; i < pos + 15; i++)
        {
            Datas.ToFocusRobot[i].gameObject.SetActive(true);
            Datas.ToShowKs[i].gameObject.SetActive(true);
            Datas.ToShowTraj[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < pos; i++)
        {
            Datas.ToShowKs[i].gameObject.SetActive(false);
            Datas.ToFocusRobot[i].gameObject.SetActive(false);
            Datas.ToShowTraj[i].gameObject.SetActive(false);
        }
        for (int i = pos + 15; i < Datas.ToFocusRobot.Length; i++)
        {
            Datas.ToShowKs[i].gameObject.SetActive(false);
            Datas.ToFocusRobot[i].gameObject.SetActive(false);
            Datas.ToShowTraj[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
