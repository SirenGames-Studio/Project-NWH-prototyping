using SGS.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInterface : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject PauseMenu;
    public GameObject JournalSystem;

    [Header("Handling Inputs")]
    public PlayerInput PlayerInput;
    public InputsHandler PlayerInputHandler;
    private FrameInput _frameInput;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        _frameInput = PlayerInputHandler.FrameInput;

        if (_frameInput.InventoryMenu == true)
        {
            ToggleInventoryPanel();
        }
    }

    private void ToggleInventoryPanel()
    {
        InventoryMenu.SetActive(!InventoryMenu.activeSelf);
        Cursor.visible = InventoryMenu.activeSelf;
        Cursor.lockState = InventoryMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Debug.Log(Cursor.lockState.ToString());
    }
}