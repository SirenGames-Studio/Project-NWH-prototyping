using System.Collections;
using System.Collections.Generic;
using SGS.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using SGS.Saving;

namespace SGS.Controls
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class FirstPersonController : MonoBehaviour, ISaveable
    {
        [Header("Player")]
        [SerializeField]
        private float _moveSpeed = 15f;
        [SerializeField]
        private float _gravity = -9.81f;

        [Header("Ground Check")]
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private LayerMask _groundMask;

        [SerializeField]
        private bool _is_Grounded;

        #region Other_Settings
        private CharacterController _playerController;
        [SerializeField]
        private InputsHandler _playerInput;
        private FrameInput _frameInput;

        #endregion
        [HideInInspector]
        public Vector2 movementInput;
        private Vector3 velocity;

        private void Awake()
        {
            _playerInput = GetComponent<InputsHandler>();
            _playerController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MoveCharacter();
        }

        private void FixedUpdate()
        {
            _is_Grounded = Physics.CheckSphere(groundCheck.position, groundDistance, _groundMask);

            if (_is_Grounded && velocity.y < 0) 
            {
                velocity.y = -2f;
            }

            velocity.y += _gravity * Time.deltaTime;
        
        }

        private void MoveCharacter()
        {
            _frameInput = _playerInput.FrameInput;
            Vector3 moveDirection = new Vector3(_frameInput.Move.x, 0f, _frameInput.Move.y);
            moveDirection = transform.TransformDirection(moveDirection);
            _playerController.Move(moveDirection * _moveSpeed * Time.deltaTime);
        }

        public object CaptureState()
        {
            return new SerializableVector3(this.transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            this.transform.position = position.GetVector3();
        }
    }

}