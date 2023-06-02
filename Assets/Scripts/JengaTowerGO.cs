using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Micaiah Stevens (mikhaven@hotmail.com)
/// 2 Hours Mark for building the tower.

/// <summary>
/// This sets up Tower types, based on testing.
/// Saved is the game view.
/// </summary>
public enum TowerData
{
    Prebuilt = 0, Preset = 1, Saved = 2
}

/// <summary>
/// Jenga Tower is a building base for all the Jenga Pieces
/// It sits on the center of the tower at the base.
/// TODO: UI and other elements to show the user what the tower represents.
/// FUTURE TODO: Make towers selectable and changable.
///     Seems better to do a level select screen in most puzzle based games.
///     Would be nice to have pictures of the towers as Identifiers
///     Unknown if that requires to generate the tower at least once, and take a digital picture.
/// </summary>
public class JengaTowerGO : MonoBehaviour
{
    //[SerializeField] string _name = "";
    public string Name => sGrade;

    /// <summary>
    /// Prefab object for the Jenga Piece
    /// </summary>
    [SerializeField] GameObject jengaPieceGO = null;
    
    /// <summary>
    /// Field to set our tower Grade text.
    /// </summary>
    [SerializeField] TMPro.TextMeshProUGUI gradeText = null;
    /// <summary>
    /// Editor command to set the Tower Height
    /// </summary>
    [SerializeField] int towerHeight = 7;
    /// <summary>
    /// Our test tower is prebuilt.
    /// </summary>
    [SerializeField] TowerData prebuilt = TowerData.Saved;
    /// <summary>
    /// Data for our tower.
    /// </summary>
    [SerializeField] string sGrade;
    public string SGrade => sGrade;

    /// <summary>
    /// Blocks sit on a 1.5f which is our constant.
    /// </summary>
    const float ZValue = 1.5f;

    /// <summary>
    /// Store the Gameobject for now
    /// To remove all the 'types' down the line.
    /// TODO: Convert to JengaPieceGO
    /// </summary>
    [SerializeField] List<JengaPieceGO> jengapieces = null;

    /// <summary>
    /// Store this to track the running routine
    /// This prevents it from running twice.
    /// </summary>
    Coroutine breakglassRoutine = null;

    [SerializeField] bool useShatterEffect = false;
    public bool UseShatterEffect => useShatterEffect;
    public void SetShatterEffect(bool isTrue = true) => useShatterEffect = isTrue;

    /// <summary>
    /// We set the tower data based on the Grade.
    /// This sets the name in front of the Tower.
    /// </summary>
    /// <param name="sNewGrade"></param>
    public void SetData(string sNewGrade)
    {
        sGrade = sNewGrade;
        prebuilt = TowerData.Saved;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gradeText != null)
        {
            gradeText.text = sGrade;
        }

        jengapieces = new List<JengaPieceGO>();
        switch (prebuilt)
        {
            case TowerData.Prebuilt:
            {
                BuildPrebuilt();
                break;
            }
            case TowerData.Preset:
            {
                BuildPreset();
                break;
            }
            case TowerData.Saved:
            {
                BuildData();
                break;
            }
        }
    }

    /// <summary>
    /// We have used prebuilt towers to test
    /// This just takes those gameojects and sets them up.
    /// </summary>
    void BuildPrebuilt()
    {
        int h = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            JengaPieceGO jGO = go.GetComponent<JengaPieceGO>();
            jGO.SetShatterEffect(UseShatterEffect);
            if (jGO != null)
            {
                int iHeight = Mathf.RoundToInt(jGO.transform.position.y);
                jGO.name = $"Jenga {h} on row {(iHeight + 1)}";
                jengapieces.Add(jGO);
                jGO.SetName(go.name);
                h++;
            }
        }
    }

    /// <summary>
    /// We have a normal Jenga tower that we build, based on the Height and Row
    /// This is for testing as well, towers without data.
    /// </summary>
    void BuildPreset()
    {
        int h = 1;
        Vector3Int vPos = Vector3Int.RoundToInt(transform.position);
        for (int iHeight = 0; iHeight <= towerHeight; iHeight++)
        {
            for (int jRow = 0; jRow <= 2; jRow++)
            {
                GameObject go = CreatePiece(new Vector3(0, iHeight, jRow * ZValue) + vPos);
                go.name = $"Jenga {h} on row {(iHeight + 1)}";
                if (iHeight % 2 != 0)
                {
                    int k = jRow;
                    k -= 1;
                    go.transform.position = new Vector3(k * ZValue, iHeight, ZValue) + vPos;
                    go.transform.localEulerAngles = new Vector3(0, 90f, 0);
                }
                JengaPieceGO jGO = go.GetComponent<JengaPieceGO>();
                jGO.SetShatterEffect(UseShatterEffect);
                if (jGO != null)
                {
                    jGO.SetName(go.name);
                    jengapieces.Add(jGO);
                    h++;
                }
            }
        }
    }

    /// <summary>
    /// This builds the tower.
    /// (Code could be saved, by build the towers in both dynamic and data driven the same.)
    /// Then uses the data to disable the gameobjects the tower does not need
    /// </summary>
    void BuildData()
    {
        int h = 1;
        Vector3Int vPos = Vector3Int.RoundToInt(transform.position);
        for (int iHeight = 0; iHeight <= towerHeight; iHeight++)
        {
            for (int jRow = 0; jRow <= 2; jRow++)
            {
                GameObject go = CreatePiece(new Vector3(0, iHeight, jRow * ZValue) + vPos);
                go.name = $"Jenga {h} on row {(iHeight + 1)}";
                if (iHeight % 2 != 0)
                {
                    int k = jRow;
                    k -= 1;
                    go.transform.position = new Vector3(k * ZValue, iHeight, ZValue) + vPos;
                    go.transform.localEulerAngles = new Vector3(0, 90f, 0);
                }
                JengaPieceGO jGO = go.GetComponent<JengaPieceGO>();
                jGO.SetShatterEffect(UseShatterEffect);
                if (jGO != null)
                {
                    jGO.SetName(go.name);
                    if (GradesGO.HasData(sGrade))
                    {
                        jengapieces.Add(jGO);
                        h++;
                    }
                    else
                    {
                        jGO.gameObject.SetActive(false);
                    }
                }
            }
        }
        Stack[] currentTower = GradesGO.GetCurrentTower(Name);
        if (GradesGO.CurrentGrade.Length > jengapieces.Count)
        {
            UnityEngine.Debug.LogWarning($"{Name} Data exceeds Jenga pieces");
        }
        for (int j = 0; j < jengapieces.Count; j++)
        {
            jengapieces[j].SetShatterEffect(UseShatterEffect);
            if (currentTower.Length <= j)
            {
                jengapieces[j].gameObject.SetActive(false);
            }
            else
            {
                jengapieces[j].gameObject.SetActive(true);
                jengapieces[j].SetStack(currentTower[j]);
            }
        }
    }

    /// <summary>
    /// Generic function to create our go from our prefab.
    /// Default parameters, with the position feed in.
    /// </summary>
    /// <param name="pos">Location data</param>
    /// <returns></returns>
    GameObject CreatePiece(Vector3 pos)
    {
        return GameObject.Instantiate(jengaPieceGO, pos, Quaternion.identity, this.transform);
    }

    /// <summary>
    /// Breaks all glass.
    /// Calls the routine.
    /// </summary>
    [ContextMenu("Break All Glass")]
    public void BreakAllGlass()
    {
        jengapieces.RemoveAll(item => item == null);
        if (breakglassRoutine == null)
        {
            breakglassRoutine = StartCoroutine(BreakingGlassRoutine());
        }
    }


    /// <summary>
    /// Steps through the pieces from the bottom up.
    /// Yield for dramatic pause.
    /// Future proof with the piece type, so can break any block.
    /// (Note no other blocks are breakable at this time.)
    /// </summary>
    /// <param name="jType"></param>
    /// <returns></returns>
    IEnumerator BreakingGlassRoutine(JengaTypes jType = JengaTypes.Glass)
    {
        foreach (var piece in jengapieces)
        {
            if (piece.JengaType == jType)
            {
                piece.SetShatterEffect(UseShatterEffect);
                piece.Break();
                yield return new WaitForSeconds(0.15f);
            }
        }
        breakglassRoutine = null;
    }
}