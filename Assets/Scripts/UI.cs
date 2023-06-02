using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI holds our elements in the scene
/// </summary>
[DefaultExecutionOrder(-9000)]
public class UI : MonoBehaviour
{
    static UI instance = null;
    void Awake()
    {
        instance = this;
        towerParent.SetActive(false);
        mouseParent.SetActive(false);
        menuParent.SetActive(false);

        breakGlassButton.onClick.RemoveAllListeners();
        breakGlassButton.onClick.AddListener(() =>
        {
            if (MouseController.TowerGO != null)
            {
                MouseController.TowerGO.BreakAllGlass();
            }
        });

        OnTowerChanged = (string sTower) =>
        {
            if (sTower == "")
            {
                towerText.text = "";
                towerParent.SetActive(false);
                menuParent.SetActive(false);
            }
            else
            {
                towerText.text = sTower;
                towerParent.SetActive(true);
                menuParent.SetActive(true);
            }
        };

        OnPieceChanged = (Stack stack) =>
        {
            if (stack == null)
            {
                mouseParent.SetActive(false);
            }
            else
            {
                mouseTexts[0].text = $"{stack.subject}";
                mouseTexts[1].text = $"{GetType(stack.mastery)}";
                mouseTexts[2].text = $"{stack.id}";
                mouseTexts[3].text = $"{stack.domain}";
                mouseTexts[4].text = $"{stack.standardid}";
                mouseParent.SetActive(true);
            }
        };
    }

    [SerializeField] TMPro.TextMeshProUGUI towerText = null;
    [SerializeField] GameObject towerParent = null;
    [SerializeField] GameObject menuParent = null;
    [SerializeField] Button breakGlassButton = null;
    [SerializeField] GameObject mouseParent = null;
    [SerializeField] TMPro.TextMeshProUGUI[] mouseTexts = null;

    public static System.Action<string> OnTowerChanged;
    public static System.Action<Stack> OnPieceChanged;

    public static JengaTypes GetType(int id)
    {
        switch (id)
        {
            case 0: return JengaTypes.Glass;
            case 1: return JengaTypes.Wood;
            case 2: return JengaTypes.Stone;
            default: return JengaTypes.Wood;
        }
    }
}