using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPP
{
    public class ViewRotation : MonoBehaviour
    {
        [Header("To Link")]
        [SerializeField]
        private Transform player;

        [Header("Properties")]
        [SerializeField]
        public float mouseSensitivity = 100f;

        [SerializeField]
        private float minCameraAngle;
        [SerializeField]
        private float maxCameraAngle;

        [Header("States")]
        [SerializeField]
        private float xAngle = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xAngle = Mathf.Clamp(xAngle - mouseY, minCameraAngle, maxCameraAngle);

            transform.localRotation = Quaternion.AngleAxis(xAngle, Vector3.right);
            player.Rotate(Vector3.up * mouseX);
        }
    }
}