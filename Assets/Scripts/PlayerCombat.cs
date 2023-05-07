using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;


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

        }
    }
}
