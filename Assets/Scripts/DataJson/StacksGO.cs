using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;

/// <summary>
/// This is our data builder for our stack items
/// </summary>
[DefaultExecutionOrder(-5000)]
public class StacksGO : MonoBehaviour
{
    static StacksGO instance = null;
    void Awake()
    {
        instance = this;
        ParseData();
    }

    /// <summary>
    /// We pulled in the provided file.
    /// </summary>
    [SerializeField] TextAsset jsonFile = null;
    /// <summary>
    /// Formated the file for Unity to read.
    /// Used JsonHelper instaed
    /// </summary>
    [SerializeField] TextAsset jsonNonArrayFile = null;

    /// <summary>
    /// Switch between the two data types.
    /// UnityList, and JsonArray
    /// </summary>
    [SerializeField] bool useNonArray = true;

    /// <summary>
    /// Once we load the data, we hold the raw stacks here for review.
    /// </summary>
    [SerializeField] List<Stack> allStacks = null;

    /// <summary>
    /// Function to read the file and convert them to the Stack class.
    /// </summary>
    void ParseData()
    {
        allStacks = new List<Stack>();
        Stacks stacksInJson = null;
        if (useNonArray)
        {
            stacksInJson = JsonUtility.FromJson<Stacks>(jsonFile.text);
        }
        else
        {
            //List<Stack> stacksInJson = JsonUtility.FromJson(jsonFile.text, typeof(Stack));
            //Stacks stacksInJson = JsonConvert.DeserializeObject<Stacks>(FixJson(jsonFile.text));
            //stacksInJson = JsonUtility.FromJson<Stacks>(FixJson(jsonFile.text));
            Stack[] stacks = JsonHelper.FromJson<Stack>(jsonFile.text);
            stacksInJson = new Stacks(stacks);
        }
        foreach (Stack stack in stacksInJson.stacks)
        {
            allStacks.Add(stack);
            GradesGO.AddStack(stack);
            //Debug.Log("Found Stack: " + stack.id + " " + stack.standardid);
        }
        GradesGO.OnDataLoaded?.Invoke();
    }
}