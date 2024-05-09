
using SGS.Controls;
using SGS.InputSystem;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;



public class CameraController : MonoBehaviour
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

       public float TopClamp = 90.0f;
    public float BottomClamp = -90f;

    private float rotationX = 0f;

    private CinemachineCamera virtualCamera;
    private CinemachinePanTilt _pov;
    public float speed = 5.0f;
    public float sensitivity = 100.0f;

    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        _frameInput = PlayerInputHandler.FrameInput;
        rotationX -= _frameInput.CameraLook.y;
        rotationX =  Mathf.Clamp(rotationX, -90f, 90f);
        virtualCamera.transform.localRotation = Quaternion.Euler(new Vector3( rotationX, 0,0));
        transform.localRotation = Quaternion.Euler (Vector3.up * _frameInput.CameraLook.x * mouseSensitivity * Time.deltaTime);

        ///..................New Way ..................................///

        
        /// ......................NEw wayyy ...............................///

        
    }

}

