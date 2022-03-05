using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        int index = json.IndexOf(':');
        json = json.Remove(0, index);
        json = json.Insert(0, "{ \"Array\"");
        Debug.Log(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Array;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Array = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Array = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable] private class Wrapper<T>
    {
        public T[] Array;
    }
}*/

public class JsonHelper<T>
{
    public T[] Array;

    public static T[] FromJson(string json)
    {
        int index = json.IndexOf(':');
        json = json.Remove(0, index);
        json = json.Insert(0, "{ \"Array\"");
        Debug.Log(json);
        JsonHelper<T> wrapper = JsonUtility.FromJson<JsonHelper<T>>(json);
        return wrapper.Array;
    }

    public static string ToJson(T[] array)
    {
        JsonHelper<T> wrapper = new JsonHelper<T>();
        wrapper.Array = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson(T[] array, bool prettyPrint)
    {
        JsonHelper<T> wrapper = new JsonHelper<T>();
        wrapper.Array = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

}