using System.Collections;
using System.Collections.Generic;
using SGS.InputSystem;
using UnityEngine;

namespace SGS.Controls
{
    public class CameraLookController : MonoBehaviour
    {
        [SerializeField]
        private float mouseSensitivity = 20f;

        [SerializeField]
        private Transform playerBody;

        [SerializeField]
        float xRotation = 0f;

        public PlayerInputs PlayerInput;
        private FrameInput _frameInput;

        private void Start() 
        {
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() 
        {
            PlayerRotate();
        }

        private void PlayerRotate()
        {
            _frameInput = PlayerInput.FrameInput;
            xRotation -= _frameInput.CameraLook.y;
            xRotation = Mathf.Clamp(xRotation,-70f,70f);
                
            transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            playerBody.Rotate(Vector3.up*_frameInput.CameraLook.x);

        }

    }

}