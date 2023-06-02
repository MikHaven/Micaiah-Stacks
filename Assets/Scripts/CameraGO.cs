using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Limited camera control to select towers and rotate.
/// </summary>
[DefaultExecutionOrder(1000)]
public class CameraGO : MonoBehaviour
{
    static CameraGO instance = null;

    /// <summary>
    /// We hold a transform taget to focus our camera.
    /// </summary>
    [SerializeField] Transform camTarget = null;
    /// <summary>
    /// How fast the camera moves to respond to input.
    /// </summary>
    [SerializeField] float speed = 5f;

    /// <summary>
    /// We hold masks for Physics to selected
    /// </summary>
    [SerializeField] LayerMask towerMask;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectTower();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        CheckSelectTower();
    }

    /// <summary>
    /// We can select the tower with the middle mouse button.
    /// This sets the camera, and lets us rotate around the selected tower.
    /// </summary>
    void CheckSelectTower()
    {
        if (Input.GetMouseButtonDown(2))
        {
            SelectTower();
        }
    }

    /// <summary>
    /// Selects the Tower based on the Physics.
    /// </summary>
    void SelectTower()
    {
        // if left button pressed...
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, towerMask))
        {
            UnityEngine.Debug.LogWarning($"Clicked on {hit.transform.gameObject.name}");
            // the object identified by hit.transform was clicked
            // do whatever you want
            JengaTowerGO jTower = hit.transform.gameObject.GetComponent<JengaTowerGO>();
            if (jTower != null)
            {
                CameraGO.ChangeTower(jTower);
                MouseController.ChangeTower(jTower);
                UI.OnTowerChanged(jTower.Name);
                GradesGO.ChangeTower(jTower.SGrade);
            }
        }
    }

    /// <summary>
    /// Rotate camera based on the mouse input.
    /// </summary>
    void RotateCamera()
    {
        if (camTarget == null) return;
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.RotateAround(camTarget.position,
                                            gameObject.transform.up,
                                            -Input.GetAxis("Mouse X") * speed);

            //gameObject.transform.RotateAround(camTarget.position,
            //                                gameObject.transform.right,
            //                                -Input.GetAxis("Mouse Y") * speed);
        }
    }

    /// <summary>
    /// Once we have the tower clicked on, we pass it here to focus the camera.
    /// TODO: Add in smoothing.
    /// </summary>
    /// <param name="towerGO"></param>
    public static void ChangeTower(JengaTowerGO towerGO)
    {
        //if (instance.camTarget != towerGO.transform)
        {
            instance.camTarget = towerGO.transform;
            instance.transform.eulerAngles = Vector3.zero;
            instance.transform.position = towerGO.transform.position;
        }
    }
}
