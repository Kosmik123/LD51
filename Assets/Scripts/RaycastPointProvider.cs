using NaughtyAttributes;
using UnityEngine;

public class RaycastPointProvider : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float detectionDistance;

    [SerializeField, ReadOnly]
    private Vector3 mousePosition;

    public Vector3? GetWorldPoint()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = 10;
        mousePosition = camera.ScreenToWorldPoint(mouseScreenPosition);

        if (Physics.Raycast(origin.position, mousePosition - origin.position, out var hitinfo, detectionDistance, layerMask))
            return hitinfo.point;
        
        return null;
    }
}
