using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} eliminado");
        GameManager.Instance?.EnemyKilled();
        Destroy(gameObject);
    }
}