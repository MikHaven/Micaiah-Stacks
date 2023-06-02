using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add in destruction plane, so pieces don't live forever falling.

/// <summary>
/// Almost a GameController
/// Holds the mouse data in memory.
/// So we can see what the Mouse is doing
/// </summary>
public class MouseController : MonoBehaviour
{
    static MouseController instance = null;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] JengaPieceGO pieceGO = null;
    public static JengaPieceGO PieceGO => instance.pieceGO;

    [SerializeField] JengaTowerGO towerGO = null;
    public static JengaTowerGO TowerGO => instance.towerGO;
    /// <summary>
    /// Holds our tower, so we can keep it here.
    /// Duplicates the data, but this keeps the mouse centralized.
    /// </summary>
    /// <param name="jTower"></param>
    public static void ChangeTower(JengaTowerGO jTower) => instance.towerGO = jTower;

    /// <summary>
    /// We use Physics and this limits it to the piece.
    /// </summary>
    [SerializeField] LayerMask pieceMask;

    // Update is called once per frame
    void Update()
    {
        //if (pieceGO == null)
        {
            CheckPieceInteraction();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pieceGO = null;
            UI.OnPieceChanged(null);
        }
    }

    /// <summary>
    /// We check if we want to interact with our piece.
    /// </summary>
    void CheckPieceInteraction()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // if right button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, pieceMask))
            {
                UnityEngine.Debug.LogWarning($"Clicked on {hit.transform.gameObject.name}");
                // the object identified by hit.transform was clicked
                JengaPieceGO jPiece = hit.transform.gameObject.GetComponent<JengaPieceGO>();
                if (jPiece != null)
                {
                    pieceGO = jPiece;
                    UI.OnPieceChanged(pieceGO.Stack);
                }
                else
                {
                    UI.OnPieceChanged(null);
                }
            }
        }
    }
}