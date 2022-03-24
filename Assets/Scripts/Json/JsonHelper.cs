using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonHelper<T>
{
    public T[] array;

    private static string ToLowerCase(string json, string oldWord, string newWord)
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

    private static int[] AllIndexOf(string json, string value)
    {
        List<int> index = new List<int>();
        index.Add(0);
        while (true)
        {
            int position = json.IndexOf(value, index[index.Count - 1] + 1);
            if (position == -1) 
            {
                break;
            }
            index.Add(position);
        }
        index.RemoveAt(0);
        if (index.Count==0)
        {
            index.Add(-1);
        }
        return index.ToArray();
    }

    public static T[] FromJson(string json)
    {
        int index = json.IndexOf('[');
        json = json.Remove(0, index);
        json = "{ \"array\":" + json;
        json = ToLowerCase(json, "namespace", "space");
        Debug.Log(json);
        JsonHelper<T> wrapper = JsonUtility.FromJson<JsonHelper<T>>(json);
        return wrapper.array;
    }

    public static string ToJson(T[] array)
    {
        JsonHelper<T> wrapper = new JsonHelper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson(T[] array, bool prettyPrint)
    {
        JsonHelper<T> wrapper = new JsonHelper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

}