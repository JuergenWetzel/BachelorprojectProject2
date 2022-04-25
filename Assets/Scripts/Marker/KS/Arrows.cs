using UnityEngine;

/// <summary>
/// Komponente bei jedem Koordinatensystem
/// Im Editor kann für den Pfeil ausgewählt werden, welche Pfeile in welcher Farbe angezeigt werden
/// </summary>
public class Arrows : MonoBehaviour
{
    private GameObject[] arrows;
    [SerializeField] private Material[] rgb;
    [SerializeField] private Orientierung blauerPfeil;
    [SerializeField] private Orientierung roterPfeil;
    [SerializeField] private Orientierung gruenerPfeil;
    private Farbe vorne;
    private Farbe hinten;
    private Farbe links;
    private Farbe rechts;
    private Farbe oben;
    private Farbe unten;
    private Farbe[] position;

    private enum Orientierung
    {
        vorne, hinten, links, rechts, oben, unten
    }

    private enum Farbe
    {
        rot, gruen, blau, nichts
    }

    private void Awake()
    {
        Pfeil[] pfeile = GetComponentsInChildren<Pfeil>();
        arrows = SortArrows(pfeile);
    }

    /// <summary>
    /// Übergibt alle (möglichen) Pfeilspitzen eines Koordinatensystems
    /// </summary>
    /// <param name="pfeile"></param>
    /// <returns></returns>
    private GameObject[] SortArrows(Pfeil[] pfeile)
    {
        GameObject[] sortiert = new GameObject[pfeile.Length];
        for (int i = 0; i < pfeile.Length; i++)
        {
            switch (pfeile[i].gameObject.name)
            {
                case "Vorne":
                    sortiert[0] = pfeile[i].gameObject;
                    break;
                case "Hinten":
                    sortiert[1] = pfeile[i].gameObject;
                    break;
                case "Links":
                    sortiert[2] = pfeile[i].gameObject;
                    break;
                case "Rechts":
                    sortiert[3] = pfeile[i].gameObject;
                    break;
                case "Oben":
                    sortiert[4] = pfeile[i].gameObject;
                    break;
                case "Unten":
                    sortiert[5] = pfeile[i].gameObject;
                    break;
                default:
                    break;
            }
        }
        return sortiert;
    }


    private void Start()
    {
        if (Kontrollzahl(roterPfeil) + Kontrollzahl(gruenerPfeil) + Kontrollzahl(blauerPfeil) != 111) 
        {
            throw new MissingReferenceException("Pfeile sind ungültig angeornet");
        }
        FarbeZuweisen(gruenerPfeil, Farbe.gruen);
        FarbeZuweisen(blauerPfeil, Farbe.blau);
        FarbeZuweisen(roterPfeil, Farbe.rot);
        position = new Farbe[6] { vorne, hinten, links, rechts, oben, unten};
        for (int i = 0; i < 6; i++)
        {
            PfeileMarkieren(i);
        }
    }

    /// <summary>
    /// Färbt die Pfeile in der gewünschten Farbe
    /// </summary>
    /// <param name="index">Index des Pfeils im </param>
    private void PfeileMarkieren(int index)
    {
        Renderer[] renderers = arrows[index].GetComponentsInChildren<Renderer>();
        switch (position[index])
        {
            case Farbe.rot:
                renderers[0].material = rgb[0];
                renderers[1].material = rgb[0];
                renderers[0].gameObject.SetActive(true);
                break;
            case Farbe.gruen:
                renderers[0].material = rgb[1];
                renderers[1].material = rgb[1];
                renderers[0].gameObject.SetActive(true);
                break;
            case Farbe.blau:
                renderers[0].material = rgb[2];
                renderers[1].material = rgb[2];
                renderers[0].gameObject.SetActive(true);
                break;
            case Farbe.nichts:
                renderers[0].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Kontrolliert, dass nur 3 Pfeilspitzen sichtbar sind
    /// 
    /// Wenn 2 in die gleiche / entgegengesetzte Richtung zeigen kommt ein Fehler
    /// </summary>
    /// <param name="pfeil"></param>
    /// <returns></returns>
    private int Kontrollzahl(Orientierung pfeil)
    {
        switch (pfeil)
        {
            case Orientierung.vorne:
                return 1;
            case Orientierung.hinten:
                return 1;
            case Orientierung.links:
                return 10;
            case Orientierung.rechts:
                return 10;
            case Orientierung.oben:
                return 100;
            case Orientierung.unten:
                return 100;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Ordnet einer Position eine Farbe zu
    /// Für die Farbe ist die Position bekannt
    /// </summary>
    /// <param name="pfeil">Richtung der Pfeilspitze</param>
    /// <param name="farbe">Farbe die zugeordnet wird</param>
    private void FarbeZuweisen(Orientierung pfeil, Farbe farbe)
    {
        switch (pfeil)
        {
            case Orientierung.vorne:
                vorne = farbe;
                hinten = Farbe.nichts;
                break;
            case Orientierung.hinten:
                hinten = farbe;
                vorne = Farbe.nichts;
                break;
            case Orientierung.links:
                links = farbe;
                rechts = Farbe.nichts;
                break;
            case Orientierung.rechts:
                rechts = farbe;
                links = Farbe.nichts;
                break;
            case Orientierung.oben:
                oben = farbe;
                unten = Farbe.nichts;
                break;
            case Orientierung.unten:
                unten = farbe;
                oben = Farbe.nichts;
                break;
            default:
                break;
        }
    }
}