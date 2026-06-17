using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Shield")]
    private bool isShielded = false;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isShielded) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    private System.Collections.IEnumerator ShieldCoroutine(float duration)
    {
        isShielded = true;
        yield return new WaitForSeconds(duration);
        isShielded = false;
    }

    void Die()
    {
        Debug.Log("Player murió - Game Over");
        GameManager.Instance?.PlayerDied();
        gameObject.SetActive(false);
    }
}