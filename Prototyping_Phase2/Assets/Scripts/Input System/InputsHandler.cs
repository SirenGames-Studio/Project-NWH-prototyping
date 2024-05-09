using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGS.InputSystem
{
    public class InputsHandler : MonoBehaviour
    {
        public FrameInput FrameInput { get; private set; }


        private PlayerInputAction _playerInputActions;
        private InputAction _move;
        private InputAction _pickUp;
        private InputAction _open;
        private InputAction _cameraLook;

        //Input for UI
        private InputAction _inventoryMenu;
        private InputAction _pauseMenu;
        private InputAction _journalSystem;

        private void Awake()
        {
            _playerInputActions = new PlayerInputAction();
            _move = _playerInputActions.Player.Move;
            _pickUp = _playerInputActions.Player.Pickup;
            _open = _playerInputActions.Player.Open;
            _cameraLook = _playerInputActions.Player.Look;

            // for UI
            _inventoryMenu = _playerInputActions.UI.InventoryMenu;
            _pauseMenu = _playerInputActions.UI.PauseMenu;
            _journalSystem = _playerInputActions.UI.JournalSystem;
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void Update()
        {
            FrameInput = GatherInput();
        }

        private FrameInput GatherInput()
        {
            return new FrameInput
            {
                Move = _move.ReadValue<Vector2>(),
                Open = _open.WasPressedThisFrame(),
                PickUP = _pickUp.WasPressedThisFrame(),
                CameraLook = _cameraLook.ReadValue<Vector2>().normalized,
                // for UI
                InventoryMenu = _inventoryMenu.WasPerformedThisFrame(),
                PauseMenu = _pauseMenu.WasPerformedThisFrame(),
                JournalSystem = _journalSystem.WasPerformedThisFrame(),

            };
        }
    }
}


public struct FrameInput 
{
    public Vector2 Move;
    public bool PickUP;
    public bool Open;
    public Vector2 CameraLook;

    // For UI
    public bool InventoryMenu;
    public bool PauseMenu;
    public bool JournalSystem;
}

