using System;
using System.Collections;
using System.Collections.Generic;
using SGS.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Serialization;

namespace SGS.Controls
{
    public class CameraLookController : MonoBehaviour
    {
        [SerializeField]
        private float mouseSensitivity = 20f;

        [SerializeField]
        private Transform playerBody;
        public float RotationSpeed = 1.0f;
        [SerializeField]
        float xRotation = 0f;

        [Header("Handling Inputs")]
        public PlayerInput PlayerInput;
        public InputsHandler PlayerInputHandler;
        private FrameInput _frameInput;

        [Header("Cinemachine")] 
        public GameObject ChinemachineCameraTarget;

        public float TopClamp = 90.0f;
        public float BottomClamp = -90f;
        
        
        // cinemachine
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        [FormerlySerializedAs("deltaTimeMultiplier")] public float DeltaTimeMultiplier = 1f;
        private const float _threshold = 0.01f;
        private GameObject _mainCamera;

        
        private bool IsCurrentDeviceMouse
        {
            get
            {

#if ENABLE_INPUT_SYSTEM
                return PlayerInput.currentControlScheme == "Mouse&Keyboard";
#else
				return false;
#endif
               

            }
        }

        [HideInInspector] public bool canLook = true;
        
        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() 
        {
           // PlayerRotate();
        }

        private void LateUpdate()
        {
            if(canLook == true)
                CameraRotation();
        }

        private void PlayerRotate()
        {
            _frameInput = PlayerInputHandler.FrameInput;
            xRotation -= _frameInput.CameraLook.y;
            xRotation = Mathf.Clamp(xRotation,BottomClamp,TopClamp);
                
            transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            playerBody.Rotate(Vector3.up*_frameInput.CameraLook.x);

        }
        
        private void CameraRotation()
        {
            _frameInput = PlayerInputHandler.FrameInput;
            // if there is an input
            if (_frameInput.CameraLook.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime
              float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
              float verticalRotation = _frameInput.CameraLook.y * RotationSpeed * deltaTimeMultiplier;
              // Update Cinemachine camera target pitch
              _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch - verticalRotation, BottomClamp, TopClamp);
              ChinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

              // Calculate horizontal rotation (around the y-axis)
              float horizontalRotation = _frameInput.CameraLook.x * RotationSpeed * deltaTimeMultiplier;
              // Rotate the player left and right
              transform.Rotate(Vector3.up * horizontalRotation);
            }
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        public void ToggleCursor(bool toggle)
        {
            Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
            canLook = !toggle;
        }

    }

}