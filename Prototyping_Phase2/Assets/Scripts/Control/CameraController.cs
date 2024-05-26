
using SGS.InputSystem;
using Unity.Cinemachine;
using UnityEngine;


public class CameraController : MonoBehaviour
{
   
    [SerializeField] private GameObject _cineMachineCam;
    private float _cinemachineTargetPitch;
    [SerializeField] private float mouseSensitivity = 2.0f; 

    [SerializeField] private float verticalSpeed = 2.0f;
    [SerializeField] private float horizontalSpeed = 2.0f;
    [SerializeField] private float clampAngle = 70.0f;



     [SerializeField]
    private InputsHandler _playerInput;
    private FrameInput _frameInput;

    private GameObject _mainCamera;
    private Vector2 smoothedInput;
    private bool canLook;
    private const float _threshold = 0.1f;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        }
    }

    private void LateUpdate()
    {
       
         RotateCamera();

    }
    private void RotateCamera()
    {
        _frameInput = _playerInput.FrameInput;

        if (_frameInput.CameraLook.sqrMagnitude >= _threshold)
        {

            smoothedInput = Vector2.Lerp(smoothedInput, _frameInput.CameraLook, _threshold);
            _cinemachineTargetPitch += smoothedInput.y * horizontalSpeed;
            float _rotationVelocity = smoothedInput.x * verticalSpeed;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -clampAngle, clampAngle);

            // Update Cinemachine camera target pitch
            _cineMachineCam.transform.localRotation = Quaternion.Euler(-_cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
        
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}
}

