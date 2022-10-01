using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    [SerializeField]
    private Transform copiedTransform;

    private void Update()
    {
        transform.rotation = Quaternion.AngleAxis(copiedTransform.rotation.eulerAngles.y, Vector3.up);
    }
}
