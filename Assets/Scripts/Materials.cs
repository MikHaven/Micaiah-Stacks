using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This holds our Material for use when items request it.
/// </summary>
[DefaultExecutionOrder(-9000)]
public class Materials : MonoBehaviour
{
    static Materials instance = null;
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// We have material we set in the editor.  Then use static to get the data.
    /// </summary>
    [SerializeField] Material glassMat = null;
    public static Material GlassMat => (instance == null) ? null : instance.glassMat;

    [SerializeField] Material woodMat = null;
    public static Material WoodMat => (instance == null) ? null : instance.woodMat;

    [SerializeField] Material stoneMat = null;
    public static Material StoneMat => (instance == null) ? null : instance.stoneMat;
}