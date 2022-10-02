using NaughtyAttributes;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxValue;

    [SerializeField, ReadOnly]
    private int value;
    public int Value => value;
    



}


public class Battler : MonoBehaviour
{
    [SerializeField, Required]
    private BattlerStats stats;
    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;
    [SerializeField]
    private float attackDuration;

    [SerializeField]
    private Health health;
    
    
    [SerializeField, ReadOnly]
    private bool isAttacking;


    public void Attack (Battler attackTarget)
    {
        
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = isAttacking ? Color.white : Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange);
    }


}

