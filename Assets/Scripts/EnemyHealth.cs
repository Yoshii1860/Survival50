using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage, string partOfBody)
    {
        GetComponent<EnemyAI>().OnDamageTaken();
    
        if(partOfBody == "Head")
        {
            Debug.Log("Headshot");
            hitPoints -= damage*2.5f;
        }
        if(partOfBody == "Body")
        {
            Debug.Log("Bodyshot");
            hitPoints -= damage;
        }
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die() 
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
