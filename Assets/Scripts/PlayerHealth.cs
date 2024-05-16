using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public float GetHealth()
    {
        return hitPoints;
    }

    public void ChangeHealth()
    {
        hitPoints = 100f;
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
  
  public void LoadHealth(float health)
  {
    hitPoints = health;
  }
}
