using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float Health = 100f;

    public void DecreaseHealth(float Damage)
    {
        Health -= Damage;
        if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    // -- Back At It Again
}
