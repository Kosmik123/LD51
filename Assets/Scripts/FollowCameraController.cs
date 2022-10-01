using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform followTarget;


    private void Update()
    {
        transform.position = followTarget.position;

        float horizontal = Input.GetAxis("Camera X");
        transform.Rotate(Vector3.up, horizontal * speed * Time.deltaTime);
    }
}
