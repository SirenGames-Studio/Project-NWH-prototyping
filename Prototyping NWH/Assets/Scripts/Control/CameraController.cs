using Cinemachine;
using SGS.Controls;
using SGS.InputSystem;
using System;
using UnityEngine;
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

    [Header("Cinemachine")]
    public GameObject ChinemachineCameraTarget;

    public float TopClamp = 90.0f;
    public float BottomClamp = -90f;

    private Vector2 rotation = Vector2.zero;

    public CinemachineVirtualCamera virtualCamera;
    public float speed = 5.0f;
    public float sensitivity = 100.0f;

    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        _frameInput = PlayerInputHandler.FrameInput;
        rotation += _frameInput.CameraLook * mouseSensitivity * Time.deltaTime;
        virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView - rotation.y, 10f, 80f);
        virtualCamera.transform.localRotation = Quaternion.Euler(new Vector3(-rotation.y, rotation.x, 0));
        transform.Rotate(Vector3.up * _frameInput.CameraLook.x * mouseSensitivity * Time.deltaTime);

        playerBody.Rotate(Vector3.up * _frameInput.CameraLook.x * mouseSensitivity);
    }

}

