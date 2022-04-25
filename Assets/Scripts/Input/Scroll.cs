using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Für die Scrollbar, wenn mehr als 15 Roboter im GUI aufgelistet werden müssten
/// </summary>
public class Scroll : MonoBehaviour
{
    private float[] splits;
    private Scrollbar scrollbar;
    private int steps;

    /// <summary>
    /// Starteinstellungen:
    /// 
    /// Wenn mehr als 15 Roboter vorhanden sind wird die Scrollbar erstellt.    
    /// Unterteilt die Bereiche der Scrollbar gleichmäßig entsprechend den zu vielen Roboters
    /// </summary>
    public void Init()
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
        OnChange(0.0f);
    }

    /// <summary>
    /// Wird aufgerufen, wenn die Scrollbar im GUI bewegt wird.
    /// 
    /// Durch Vergleich mit der Unterteilung aus Init wird der erste angezeigte Toggle bestimmt, die 15 nächsten Toggle werden automatisch auch angezeigt, der Rest nicht.
    /// Es muss davor überprüft werden, ob mehr als 15 Toggle existieren, ansonsten wird eine Exception geworfen
    /// </summary>
    /// <param name="value">Position der Scrollbar</param>
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
}
