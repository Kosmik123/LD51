using NaughtyAttributes;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField, Required]
    private BattlerStats stats;

    [SerializeField]
    private float attackRange;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange);

    }


}

