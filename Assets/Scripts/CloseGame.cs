using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    /// <summary>
    /// Beendet das Programm ohne Best�tigung
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }
}
