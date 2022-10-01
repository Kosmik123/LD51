using UnityEngine;

namespace FPP
{
    [RequireComponent(typeof(PersonController))]
    public class PlayerMovement : MonoBehaviour
    {
        private PersonController controller;

        [System.Serializable]
        public class RunProperties
        {
            [SerializeField]
            private KeyCode key = KeyCode.LeftShift;
            public KeyCode Key => key;
            [SerializeField]
            private float speedModifier = 1.5f;
            public float SpeedModifier => speedModifier;
        }

        [Header("Settings")]
        [SerializeField]
        private RunProperties runProperties;

        [Range(0f, 1f)]
        [SerializeField]
        private float sideWalkModifier = 1;

        [SerializeField]
        private bool smoothInput;

        [Header("States")]
        [SerializeField]
        private bool isRunning;
        [SerializeField]
        private bool isJumping;
        public float horizontal, vertical;

        private void Awake()
        {
            controller = GetComponent<PersonController>();
        }

        void Start()
        {

        }

        void Update()
        {
            GetInputs();

            DoMove();
            DoJump();

            //animator.SetFloat("moveSpeed", Mathf.Abs(vertical) + Mathf.Abs(horizontal));
            //animator.SetBool("isRunning", isRunning);
        }

        private void GetInputs()
        {
            horizontal = Input.GetAxisRaw("Horizontal") * (smoothInput ? Mathf.Abs(Input.GetAxis("Horizontal")) : 1);
            vertical = Input.GetAxisRaw("Vertical") * (smoothInput ? Mathf.Abs(Input.GetAxis("Vertical")) : 1);
            isRunning = Input.GetKey(runProperties.Key);
            isJumping = Input.GetButton("Jump");
        }

        private void DoMove()
        {
            Vector3 moveDirection =
                vertical * transform.forward +
                horizontal * sideWalkModifier * transform.right;

            float speed = isRunning ? runProperties.SpeedModifier : 1;
            controller.Move(moveDirection.normalized * speed);
        }

        void DoJump()
        {
            if (isJumping)
                controller.Jump();
        }

    }
}