using System;
using UnityEngine;

/// <summary>
/// Hilfsklasse, um anstatt eines Gameobject ein Array aus dem json String auszulesen
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonHelper<T>
{
    public T[] array;

    /// <summary>
    /// Konvertiert den ganzen Text mit Ausnahme von oldWord in Kleinbuchstaben, ersetzt dabei oldWord durch newWord
    /// </summary>
    /// <param name="json">String aus JSON Dokument</param>
    /// <param name="oldWord">zu ersetzender Variablenname in JSON, der entsprechende Wert wird nicht Kleingeschrieben</param>
    /// <param name="newWord">ersetzt oldWord im JSON String</param>
    /// <returns>JSON String, allerdings bis auf oldWord in Kleinbuchstaben konvertiert</returns>
    public static string ToLowerCase(string json, string oldWord, string newWord)
    {   
        string jsonNeu = json.ToLower();
        int index =jsonNeu.IndexOf(oldWord);
        int trys = 0;
        int end;
        while (index != -1)
        {
            end = jsonNeu.IndexOf("\"", jsonNeu.IndexOf("\"", jsonNeu.IndexOf("\"", index + 1) + 1) + 1);
            jsonNeu = jsonNeu.Remove(index, end - index);
            string edit = newWord + json.Substring(index + oldWord.Length + trys * (newWord.Length - 1), end - index - oldWord.Length);
            jsonNeu = jsonNeu.Insert(index, edit);
            index = jsonNeu.IndexOf(oldWord);
            if (trys > int.MaxValue - 1) 
            {
                throw new OverflowException("Endlosschleife");
            }
            trys++;
        }
        return jsonNeu;
    }

    /// <summary>
    /// Erstellt einen Array von seiner JSON Repräsentation
    /// 
    /// Nutzt dazu einen Wrapper und JsonUtility.FromJson(string json). Konvertiert den Anfang des JSON Dokuments, sodass der Wrapper von außen unsichtbar ist.
    /// </summary>
    /// <param name="json">Array an Objekten in einem JSON String</param>
    /// <returns>Array an Objekten aus dem übergebenen String</returns>
    public static T[] FromJson(string json)
    {
        json = json.Remove(0, json.IndexOf('['));
        json = "{ \"array\":" + json;
        JsonHelper<T> wrapper = JsonUtility.FromJson<JsonHelper<T>>(json);
        return wrapper.array;
    }
}