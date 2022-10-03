using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Battler : MonoBehaviour
{
    [SerializeField, Required]
    private BattlerStats stats;
    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;
    [SerializeField]
    private float attackDuration;

    [SerializeField, Required]
    private VariableAttribute health;

    [SerializeField]
    private int attack;
    [SerializeField]
    private int defence;

    private new Collider collider;

    private readonly Collider[] detectedColliders = new Collider[5]; // 5 = maximum number of attacked objects in single attack
    
    [SerializeField, ReadOnly]
    private bool isAttacking;

    public void Attack ()
    {
        StartCoroutine(DoAttack());
    }


    private static readonly WaitForEndOfFrame wait = new WaitForEndOfFrame();
    private IEnumerator DoAttack()
    {
        Vector3 attackCenter = new Vector3(0, 0, attackRange);
        isAttacking = true;
        float attackTime = 0;
        while (attackTime < attackDuration)
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position + attackCenter, attackRange, detectedColliders);
            for (int i = 0; i < count; i++)
            {
                var collider = detectedColliders[i];
                if (collider != null && collider.TryGetComponent<Battler>(out var otherBattler))
                {
                    if (otherBattler != this)
                        otherBattler.InflictDamage(attack);
                }
            }

            attackTime += Time.deltaTime;
            yield return wait;
        }
        isAttacking = false;
    }

    private void InflictDamage(int attack)
    {
        int damage = 4 * attack - 2 * defence;
        health.Change(-damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isAttacking ? Color.white : Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange);
    }
}

