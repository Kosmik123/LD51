using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPP
{
    public class CharacterJump : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce;
        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }

    }

    [RequireComponent(typeof(CharacterController))]
    public class PersonController : MonoBehaviour
    {
        public event System.Action<int> OnJumpChanged;
        public event System.Action<int> OnSpeedChanged;

        private CharacterController controller;
        private Animator animator;

        [Header("To Link")]
        [SerializeField]
        private Transform model;
        [SerializeField]
        private Transform groundPoint;

        [Header("Settings")]
        [SerializeField]
        private float gravityScale = 1;
        [SerializeField]
        private float groundCheckRadius = 0.2f;
        [SerializeField]
        private LayerMask groundLayers;

        [SerializeField]
        private float moveSpeed;
        public float MoveSpeed
        {
            get => moveSpeed;
            set 
            { 
                moveSpeed = value;
                OnSpeedChanged?.Invoke((int)moveSpeed);
            }
        }

        [SerializeField]
        private float jumpForce;
        public float JumpForce
        {
            get => jumpForce;
            set
            {
                jumpForce = value;
                OnJumpChanged?.Invoke((int)jumpForce);
            }
        }

        [Header("States")]
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private bool isGrounded;
        public bool IsOnGround => isGrounded;
        [SerializeField]
        private bool isJumping;
        private bool firstFixedUpdate;

        #region PUBLIC
        public void Move(Vector3 direction)
        {
            velocity.x = direction.x * moveSpeed;
            velocity.z = direction.z * moveSpeed;
        }

        public void Jump()
        {
            if (isGrounded)
                velocity.y = jumpForce;
        }
        #endregion

        #region PRIVATE
#if UNITY_EDITOR
        private void Reset()
        {
            if (groundPoint == null)
            {
                var groundPointObj = new GameObject("Ground Point");
                groundPoint = groundPointObj.transform;
                groundPoint.parent = transform;
                groundPoint.localPosition = Vector3.down;
            }
        }
#endif

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            //StartCoroutine(nameof(ResetModelTransform));
        }

        void Update()
        {
            isGrounded = IsGrounded();
            ApplyGravity();
            controller.Move(velocity * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (firstFixedUpdate)
                FirstFixedUpdate();
            firstFixedUpdate = false;

            // NEVER USE IN FIXED UPDATE
            //controller.Move(velocity * Time.fixedDeltaTime);
        }

        private void FirstFixedUpdate()
        {
            // velocity = Vector3.zero;

        }

        private void LateUpdate()
        {
            firstFixedUpdate = true;
        }

        protected IEnumerator ResetModelTransform()
        {
            var wait = new WaitForEndOfFrame();
            while (true)
            {
                yield return wait;
                model.localPosition = Vector3.zero;
                model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.identity, 0.7f);
            }
        }

        private void ApplyGravity()
        {
            if (isGrounded && velocity.y <= 0)
                velocity.y = 0.5f * gravityScale * Physics.gravity.y;
            else
                velocity += gravityScale * Time.deltaTime * Physics.gravity;
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(groundPoint.position, groundCheckRadius, groundLayers);
        }



#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (groundPoint != null)
            {
                Gizmos.color = new Color(0.5f, 0.3f, 0);
                Gizmos.DrawSphere(groundPoint.position, groundCheckRadius);
            }
        }
#endif
        #endregion
    }
}