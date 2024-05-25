using System;
using SGS.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInterface : MonoBehaviour
{
    public GameObject SystemUICanva;
    public GameObject InventoryMenu;
    public GameObject PauseMenu;
    public GameObject JournalSystem;

    [Header("Handling Inputs")]
    public PlayerInput PlayerInput;
    public InputsHandler PlayerInputHandler;
    private FrameInput _frameInput;

    [SerializeField]
    private bool _isSystemUIVisible = false;

    private void Update()
    {
        _frameInput = PlayerInputHandler.FrameInput;

        if (_frameInput.InventoryMenu == true)
        {
          //  _isSystemUIVisible = true;
            ToggleInventoryPanel();
          
        } else if (_frameInput.PauseMenu == true)
        {   
           // _isSystemUIVisible = true;
            TogglePausePanel();

        } else if (_frameInput.JournalSystem == true) 
        {
           // _isSystemUIVisible = true;
            ToggleJournalPanel();
        }

        ToggleSystemUI();
    }

    private void ToggleInventoryPanel()
    {
            
        InventoryMenu.SetActive(!InventoryMenu.activeSelf);
        PauseMenu.SetActive(false);
        JournalSystem.SetActive(false);
    }

    private void TogglePausePanel()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        InventoryMenu.SetActive(false);
        JournalSystem.SetActive(false);
    }

    private void ToggleJournalPanel()
    {
        JournalSystem.SetActive(!JournalSystem.activeSelf);
        PauseMenu.SetActive(false);
        InventoryMenu.SetActive(false);
    }

    private void ToggleSystemUI()
    {
         if (InventoryMenu.activeSelf || PauseMenu.activeSelf || JournalSystem.activeSelf)
    {
        // UI_Manager.Instance.CursorVisibility(true);
         SystemUICanva.SetActive(true); // At least one panel is active, so keep the main canvas active
                                       //_isSystemUIVisible = true;
    }
    else
    {
       // UI_Manager.Instance.CursorVisibility(false);
        SystemUICanva.SetActive(false); // All panels are inactive, so turn off the main canvas
      //  _isSystemUIVisible = false;
    }
       
        // Cursor.visible = _isSystemUIVisible;
        // Cursor.lockState = _isSystemUIVisible ? CursorLockMode.None : CursorLockMode.Locked;

    }
}
