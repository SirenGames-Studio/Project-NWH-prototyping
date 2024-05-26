using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PausedStateMenu : MonoBehaviour 
{

    public GameObject PausedTab;
    public GameObject InventoryTab;
    public GameObject JournalTab;

    private void Start()
    {
       
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME) { return; }

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
        if (JournalTab.activeSelf)
        {
            InventoryTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandleInventoryState()
    {
        InventoryTab.SetActive(!InventoryTab.activeSelf);
        if (InventoryTab.activeSelf)
        {
            JournalTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandlePausedState()
    {
        PausedTab.SetActive(!PausedTab.activeSelf);
        if (PausedTab.activeSelf)
        {
            InventoryTab.SetActive(false);
            JournalTab.SetActive(false);
        }
    }

    private void UpdatePausedStateCanvas()
    {
        bool isAnyActiveTab = JournalTab.activeSelf || InventoryTab.activeSelf || PausedTab.activeSelf;
        // UIManager.Instance.PauseMenu.gameObject.SetActive(isAnyActiveTab);
       //  UIManager.Instance.PausedGameState.SetActive(isAnyActiveTab);
       UIManager.Instance.PausedState(isAnyActiveTab);
    }
}