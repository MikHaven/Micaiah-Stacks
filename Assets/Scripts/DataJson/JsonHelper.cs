using UnityEngine;

/// <summary>
/// These helper functions work with JsonUtility
/// Json needs help
/// Also need to format the data for Unity.
/// </summary>
public static class JsonHelper
{
    public static T[] FromJson<T>(string json, bool bfix = false)
    {
        string sJson = json;
        if (bfix)
        {
            sJson = FixJson(json);
        }
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    /// <summary>
    /// Unity needs to have List data
    /// Even though Json is formatted correcty
    /// Unity needs to read in the data.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    static string FixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    /// <summary>
    /// This helper function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    /// <summary>
    /// Converts data to Json.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="prettyPrint"></param>
    /// <returns></returns>
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    /// <summary>
    /// Wrapper to hold Items in a List (Array)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}