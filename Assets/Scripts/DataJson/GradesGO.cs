using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Once we have our data in the Stacks
/// We convert them to Grades.
/// These are the pillars of our data.
/// </summary>
[DefaultExecutionOrder(-6000)]
public class GradesGO : MonoBehaviour
{
    static GradesGO instance = null;
    void Awake()
    {
        instance = this;
        //grades = new List<Grade>();
        sortedStacks = new Dictionary<string, List<Stack>>();
        OnDataLoaded += DoDataLoaded;
    }

    /// <summary>
    /// We test based on loading data.
    /// </summary>
    [SerializeField] bool loadData = true;

    //[SerializeField] List<Grade> grades = null;

    /// <summary>
    /// This shows us the current grade.
    /// </summary>
    [SerializeField] string selectedGradeString = "None";
    /// <summary>
    /// This is the current grade we have selected.
    /// </summary>
    [SerializeField] List<Stack> selectedGrade = null;
    /// <summary>
    /// We use arrays to seperate our internal data from calls.
    /// </summary>
    public static Stack[] CurrentGrade => instance.selectedGrade.ToArray();
    /// <summary>
    /// We need to request data based on the grade.
    /// </summary>
    /// <param name="sGrade"></param>
    /// <returns></returns>
    public static Stack[] GetCurrentTower(string sGrade) => sortedStacks[sGrade].ToArray();

    /// <summary>
    /// Preset data from the Editor
    /// </summary>
    [SerializeField] GameObject towerPrefab = null;
    /// <summary>
    /// Our testing data needs to be turned off.
    /// </summary>
    [SerializeField] GameObject[] OldTowers;

    /// <summary>
    /// Preset data we use to fill
    /// TODO: Convert to Drop Box.
    /// Kindergarten 1st to 5th
    /// </summary>
    string editorGrade = "6th Grade";
    //string currentGrade = "7th Grade";
    //string currentGrade = "8th Grade";
    //string currentGrade = "Algebra I"; // Highschool (9th through 12th)

    /// <summary>
    /// Once we load our data, we sort them into Grades.
    /// </summary>
    static Dictionary<string, List<Stack>> sortedStacks = null;

    /// <summary>
    /// Once we have loaded the data, we call actions to respond.
    /// </summary>
    public static System.Action OnDataLoaded = null;

    /// <summary>
    /// Once our StacksGO has loaded the data we add to the GradesGO
    /// </summary>
    /// <param name="stack"></param>
    public static void AddStack(Stack stack)
    {
        if (sortedStacks.ContainsKey(stack.grade))
        {
            sortedStacks[stack.grade].Add(stack);
        }
        else
        {
            sortedStacks.Add(stack.grade, new List<Stack>());
            sortedStacks[stack.grade].Add(stack);
        }
    }

    /// <summary>
    /// We ask if we have data, so we can Prebuild the tower.
    /// </summary>
    /// <param name="sGrade"></param>
    /// <returns></returns>
    public static bool HasData(string sGrade) => sortedStacks.ContainsKey(sGrade);

    /// <summary>
    /// This response to our users input.
    /// Changes the tower data.
    /// </summary>
    /// <param name="sGrade"></param>
    public static void ChangeTower(string sGrade)
    {
        instance.selectedGradeString = sGrade;
        if (sortedStacks.ContainsKey(sGrade))
        {
            instance.selectedGrade = new List<Stack>();
            instance.selectedGrade = new List<Stack>(sortedStacks[instance.selectedGradeString]);
        }
        else
        {
            UnityEngine.Debug.LogWarning($"Grade not Found {sGrade}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // We loaded in our preset editor data.
        // The UI will response to changes.
        if (sortedStacks.ContainsKey(instance.editorGrade))
        {
            selectedGrade = new List<Stack>(sortedStacks[instance.editorGrade]);
        }
    }

    /// <summary>
    /// Once we have loaded the data.
    /// We need to clean up our testing scene.
    /// </summary>
    void DoDataLoaded()
    {
        // Set our towers in the scene to false to hide.
        foreach(var tower in OldTowers)
        {
            tower.SetActive(false);
        }
        // If we are loading data, we read in our grades, and create a tower for each.
        if (loadData)
        {
            int iTower = -7;
            int t = 0;
            foreach(var stack in sortedStacks.Keys)
            {
                GameObject go = GameObject.Instantiate(towerPrefab, this.transform);
                JengaTowerGO towerGO = go.GetComponent<JengaTowerGO>();
                if (t == 0)
                {
                    towerGO.SetShatterEffect(false);
                }
                go.transform.position = new Vector3(iTower, 0f, 0f);
                towerGO.SetData(stack);
                iTower += 7;
                t++;
            }
        }
    }
}