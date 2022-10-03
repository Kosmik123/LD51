using NaughtyAttributes;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Battler : MonoBehaviour
{
    public event System.Action OnDied;

    public event System.Action<int> OnAttackChanged;
    public event System.Action<int> OnDefenceChanged;

    [SerializeField, Required]
    private BattlerStats stats;
    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField]
    private float attackDuration;

    [SerializeField, Required]
    private VariableAttribute health;
    public VariableAttribute Health => health;

    [SerializeField]
    private int attack;
    public int Attack
    {
        get => attack; 
        set 
        { 
            attack = value;
            OnAttackChanged?.Invoke(attack);
        }
    }

    [SerializeField]
    private int defence;
    public int Defence { get => defence;
        set
        {
            defence = value;
            OnDefenceChanged?.Invoke(defence);
        }
    }

    [SerializeField, ReadOnly]
    private bool isDead;

    private new Collider collider;
    private readonly Collider[] detectedColliders = new Collider[32]; // 32= maximum number of attacked objects in single attack
    
    private bool isAttacking;

    private void OnEnable()
    {
        health.OnValueChanged += CheckDeath;
    }

    private void CheckDeath(int health)
    {
        if (health <= 0)
        {
            isDead = true;
            OnDied?.Invoke();
        }
    }

    public void DoAttack ()
    {
        StartCoroutine(AttackCo());
        
        Vector3 attackCenter = new Vector3(0, 0, attackRange);
        int count = Physics.OverlapSphereNonAlloc(transform.position + attackCenter, attackRange, detectedColliders);
        for (int i = 0; i < count; i++)
        {
            var collider = detectedColliders[i];
            if (collider != null && collider.TryGetComponent<Battler>(out var otherBattler))
            {
                if (otherBattler != this)
                    otherBattler.InflictDamage(Attack);
            }
        }
    }

    private static readonly WaitForEndOfFrame wait = new WaitForEndOfFrame();
    private IEnumerator AttackCo()
    {
        isAttacking = true;
        float attackTime = 0;
        while (attackTime < attackDuration)
        {
            attackTime += Time.deltaTime;
            yield return wait;
        }
        isAttacking = false;
    }

    private void InflictDamage(int attack)
    {
        int damage = 2 * attack - Defence;
        health.Change(-damage);
    }

    private void OnDisable()
    {
        health.OnValueChanged -= CheckDeath;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isAttacking ? Color.white : Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, attackRange);
    }
}

