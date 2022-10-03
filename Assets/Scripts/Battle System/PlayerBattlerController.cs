using NaughtyAttributes;
using UnityEngine;



public class PlayerBattlerController : MonoBehaviour
{
    [SerializeField, Required]
    private Battler battler;

    [SerializeField, InputAxis]
    private string attackButton;

    [SerializeField]
    private  Animator animator;


    private void Update()
    {
        if (Input.GetButtonDown(attackButton))
        {
            battler.DoAttack();
            animator.SetTrigger("Attack");
        }
    }

}

