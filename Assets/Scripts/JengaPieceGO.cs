using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We have 3 types of Mastery
/// </summary>
public enum JengaTypes
{
    Glass = 0, Wood = 1, Stone = 2, None = 3
}

/// <summary>
/// This is our Individual block that makes up a Jenga Piece.
/// </summary>
public class JengaPieceGO : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject WoodStoneGO = null;
    [SerializeField] GameObject GlassGOShatter = null;
    [SerializeField] GameObject GlassGOBlocks = null;
    [SerializeField] MeshRenderer mr = null;
    [SerializeField] Rigidbody rb = null;
    [SerializeField] BoxCollider bc = null;
    [SerializeField] BreakableWindow[] AllWindows = null;
    [SerializeField] JengaBreakBlocks AllBlocks = null;

    [Header("View in Inspector")]
    /// <summary>
    /// Property to show our Type in an enum.
    /// </summary>
    [SerializeField] JengaTypes jengaType = JengaTypes.Glass;
    public JengaTypes JengaType => jengaType;

    /// <summary>
    /// Field to hold our name, internal vs the go.
    /// </summary>
    [SerializeField] string _name = "";
    public string Name => _name;
    public void SetName(string sName) => _name = sName;

    /// <summary>
    /// Our mat is based on an index.
    /// -1 (Invalid)
    /// 0 (Glass)
    /// 1 (Wood)
    /// 2 (Stone)
    /// </summary>
    [SerializeField] int matType = -1;
    public int MatType => matType;
    public void SetMat(int iMat) => matType = iMat;

    /// <summary>
    /// Shatters the glass into Shards, imported from the Tower.
    /// </summary>
    [SerializeField] bool useShatterEffect = false;
    public bool UseShatterEffect => useShatterEffect;
    public void SetShatterEffect(bool isTrue = true)
    {
        useShatterEffect = isTrue;
        GlassGOShatter.SetActive(UseShatterEffect);
        GlassGOBlocks.SetActive(!UseShatterEffect);
    }
    /// <summary>
    /// Internal data so we can display once clicked.
    /// </summary>
    [SerializeField] Stack stack = null;
    public Stack Stack => stack;
    public void SetStack(Stack newStack)
    {
        stack = newStack;
        SetName(stack.subject);
        SetMat(stack.mastery);
        Apply();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MatType == -1)
        {
            int iMat = Random.Range(0, 3);
            SetMat(iMat);
            Apply();
        }
    }

    /// <summary>
    /// Once we make data changes we need to apply those to the GameObject.
    /// </summary>
    void Apply()
    {
        if (mr != null)
        {
            switch (MatType)
            {
                case 0:
                {
                    //mr.material = Materials.GlassMat;
                    WoodStoneGO.SetActive(false);
                    GlassGOShatter.SetActive(UseShatterEffect);
                    GlassGOBlocks.SetActive(!UseShatterEffect);
                    jengaType = JengaTypes.Glass;
                    break;
                }
                case 1:
                {
                    mr.material = Materials.WoodMat;
                    WoodStoneGO.SetActive(true);
                    GlassGOShatter.SetActive(UseShatterEffect);
                    GlassGOBlocks.SetActive(!UseShatterEffect);
                    jengaType = JengaTypes.Wood;
                    break;
                }
                case 2:
                {
                    mr.material = Materials.StoneMat;
                    WoodStoneGO.SetActive(true);
                    GlassGOShatter.SetActive(UseShatterEffect);
                    GlassGOBlocks.SetActive(!UseShatterEffect);
                    jengaType = JengaTypes.Stone;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Break the Block up, either shatter, or into the 4 parts.
    /// </summary>
    [ContextMenu("Shatter")]
    public void Break()
    {
        if (rb != null)
        {
            Destroy(rb);
        }
        if (bc != null)
        {
            Destroy(bc);
        }
        if (UseShatterEffect == false)
        {
            DoBreakPieces();
        }
        else
        {
            DoShatterWindow();
        }

        // Should clean up, but for now it works.
        //Destroy(gameObject, 1f);
    }

    /// <summary>
    /// Break into the 4 mini blocks.
    /// </summary>
    void DoBreakPieces()
    {
        // Split the block into 4 pieces, to make it cleaner.
        AllBlocks.BreakBlocks();
    }

    /// <summary>
    /// Shaters the glass into spliiters.
    /// Found that the shatter was non performant.
    /// </summary>
    void DoShatterWindow()
    {
        // We use the BreakableWindow to shatter into shards
        //foreach (var window in AllWindows)
        //{
        //    window.breakWindow();
        //}
    }
}