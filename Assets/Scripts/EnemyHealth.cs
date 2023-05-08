using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Range(1, 100)][SerializeField] int HealthPoints = 2;
    [Range(1, 100)][SerializeField] float KnockbackForce = 20;
    Rigidbody2D rb = new Rigidbody2D();

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetHit(int Damage, GameObject Attacker)
    {
        HealthPoints -= Damage;
        Debug.Log("Enemy Health is " + HealthPoints);

        if(HealthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            KnockBack(Attacker.transform.position);
        }
    }

    public void KnockBack(Vector3 attackerPosition)
    {
        Vector3 knockbackDirection = transform.position - attackerPosition;
        knockbackDirection.Normalize();
        Vector3 knockbackForce = knockbackDirection * KnockbackForce;
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
