using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    /// <summary>
    /// Beendet das Programm ohne Bestätigung
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }
}
