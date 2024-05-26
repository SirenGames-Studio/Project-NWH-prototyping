using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour 
{

    public GameObject PausedTab;
    public GameObject InventoryTab;
    public GameObject JournalTab;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePausedState();
            UpdatePausedStateCanvas();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            HandleInventoryState();
            UpdatePausedStateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            HandleJournalState();
            UpdatePausedStateCanvas();
        }

        
    }

    private void HandleJournalState()
    {
        JournalTab.SetActive(!JournalTab.activeSelf);
    }

    private void HandleInventoryState()
    {
        InventoryTab.SetActive(!InventoryTab.activeSelf);
    }

    private void HandlePausedState()
    {
        PausedTab.SetActive(!PausedTab.activeSelf);
    }

    private void UpdatePausedStateCanvas()
    {
        bool isAnyActiveTab = JournalTab.activeSelf || InventoryTab.activeSelf || PausedTab.activeSelf;
        // UIManager.Instance.PauseMenu.gameObject.SetActive(isAnyActiveTab);
        Debug.Log(isAnyActiveTab);
        UIManager.Instance.PauseMenu.gameObject.SetActive(true);
       // UIManager.Instance.PausedState(isAnyActiveTab);
    }
}