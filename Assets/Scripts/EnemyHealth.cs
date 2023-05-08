using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int HealthPoints = 2;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(int Damage)
    {
        HealthPoints -= Damage;
        Debug.Log("Enemy Health is " + HealthPoints);
        if(HealthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
