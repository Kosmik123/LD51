using NaughtyAttributes;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Battler : MonoBehaviour
{
    public event System.Action OnDied;

    public event System.Action<int> OnAttackChanged;
    public event System.Action<int> OnDefenceChanged;

    public static event System.Action<Battler, int> OnBattlerDamaged;

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

    public HealthLossEffect healthLossEffectPrefab;

    [SerializeField, ReadOnly]
    private bool isDead;

    private new Collider collider;
    private readonly Collider[] detectedColliders = new Collider[32]; // 32= maximum number of attacked objects in single attack
    
    private bool isAttacking;

    private void OnEnable()
    {
        health.OnValueChanged += CheckDeath;
    }

    private void Start()
    {
        lastHealth = health.Value;
    }

    private int lastHealth;
    private void CheckDeath(int health)
    {
        int lostHealth = lastHealth - health;
        lastHealth = health;
        var healthLoss = Instantiate(healthLossEffectPrefab, transform);
        healthLoss.SetValue(lostHealth);
        
        //OnBattlerDamaged?.Invoke(this, lostHealth);
        if (health <= 0)
        {
            isDead = true;
            OnDied?.Invoke();
        }
    }

    public void DoAttack ()
    {
        StartCoroutine(AttackCo());
        
        int count = Physics.OverlapSphereNonAlloc(transform.position + transform.forward * attackRange, attackRange, detectedColliders);
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

