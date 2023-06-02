using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Breaks up the main block into 4 subblocks
/// Love minecraft, and this feels good.
/// </summary>
public class JengaBreakBlocks : MonoBehaviour
{
    /// <summary>
    /// We have a main display object.
    /// </summary>
    [SerializeField] GameObject ParentGlass = null;
    /// <summary>
    /// These are the sub blocks that are preset.
    /// </summary>
    [SerializeField] GameObject[] GOs = null;

    void Awake()
    {
        for (int i = 0; i < GOs.Length; i++)
        {
            GOs[i].SetActive(false);
        }
        ParentGlass.SetActive(true);
    }

    /// <summary>
    /// We shatter the Main block into 4 blocks.
    /// </summary>
    [ContextMenu("Shatter")]
    public void BreakBlocks()
    {
        for (int i = 0; i < GOs.Length; i++)
        {
            GOs[i].SetActive(true);
        }
        ParentGlass.gameObject.SetActive(false);
        for (int i = 0; i < GOs.Length; i++)
        {
            Rigidbody rb = GOs[i].GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = GOs[i].AddComponent<Rigidbody>();
            }
            if (rb != null)
            {
                rb.mass = 10f;
                rb.angularDrag = 10f;
                rb.drag = 10f;
                float k = 5f;
                Vector3 vForce = new Vector3(0f, k, 0f);
                Vector3 vGravity = new Vector3(0f, -10f, 0f);
                Vector3 vTorque = new Vector3(k, 0f, k);
                rb.AddRelativeForce(vForce, ForceMode.VelocityChange);
                rb.AddRelativeTorque(vTorque);
                rb.AddForce(vForce, ForceMode.Impulse);
                rb.AddTorque(vTorque);
            }
        }
    }
}