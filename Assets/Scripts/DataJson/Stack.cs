using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stack is our incoming data, read from the raw file.
/// </summary>
[System.Serializable]
public class Stack
{
    public int id = 0;
    public string subject = "None";
    public string grade = "None";
    public int mastery = -1;
    public string domainid = "NA";
    public string domain = "None";
    public string cluster = "None";
    public string standardid = "None";
    public string standarddescription = "None";
}

/// <summary>
/// Wrapper our stacks, that holds all data
/// We split this later up into grades.
/// </summary>
[System.Serializable]
public class Stacks
{
    //stacks is case sensitive and must match the string "stacks" in the JSON.
    public Stack[] stacks;
    public Stacks(Stack[] newStacks)
    {
        stacks = newStacks;
    }
}