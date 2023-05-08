using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    [Range(0, 5f)][SerializeField] private float _AttackRange = 1.4f;
    [SerializeField] private LayerMask _EnemyLayers;
    [Range(0, 10)][SerializeField] private int AttackDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        void Attack()
        {
            // Play an attack animation
            animator.SetTrigger("Attack");

            // Detect enemies in range of attack
            Collider2D[] EnemiesInRange = Physics2D.OverlapCircleAll(transform.position, _AttackRange, _EnemyLayers);
     
            for (int i = 0; i < EnemiesInRange.Length; i++)
            {
                if (EnemiesInRange[i].gameObject != gameObject && EnemiesInRange[i].gameObject.tag == "Enemy") //Circlein Carptigi gameobject kendisi degilse ve Tagi Enemy ise vurma efekti 
                {
                    EnemyHealth enemyHeatlh = EnemiesInRange[i].gameObject.GetComponent<EnemyHealth>();
                    enemyHeatlh.GetHit(AttackDamage, this.gameObject);
                }
            }
            
            if (EnemiesInRange.Length <= 0) //Circle carpmazsa 
            {
                Debug.Log("No Enemies in Range");
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _AttackRange);
    }
}
