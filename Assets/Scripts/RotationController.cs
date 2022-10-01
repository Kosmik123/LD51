using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        float horizontal = Input.GetAxis("Camera X");
        transform.Rotate(Vector3.up, horizontal * speed * Time.deltaTime);
    }
}
