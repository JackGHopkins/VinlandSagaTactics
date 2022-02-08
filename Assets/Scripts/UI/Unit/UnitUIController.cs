using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    [Header("Unit UI")]
    [SerializeField] private GameObject unitUIPanel;

    private UnitUI instance;
    private bool isUIPlaying;

    private void Awake()
    {
        if (instance)
            Debug.LogWarning("Found multiple UI Controllers in the scene.");

        instance = this;
    }

    void Start()
    {
        isUIPlaying = false;
        unitUIPanel.SetActive(false);
    }

    public void EnterUnitUIMode()
    {
        isUIPlaying = true;
        unitUIPanel.SetActive(true);
    }

    public void ExitUnitUIMode()
    {
        isUIPlaying = false;
        unitUIPanel.SetActive(false);
    }
}
