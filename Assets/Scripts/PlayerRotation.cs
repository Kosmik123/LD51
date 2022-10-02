using NaughtyAttributes;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, Required]
    private RaycastPointProvider pointProvider;

    private void Update()
    {
        var point = pointProvider.GetWorldPoint();
        if (point != null)
        {
            Vector3 target = new Vector3(point.Value.x, transform.position.y, point.Value.z);
            transform.LookAt(target, Vector3.up);
        }
    
    }




}
