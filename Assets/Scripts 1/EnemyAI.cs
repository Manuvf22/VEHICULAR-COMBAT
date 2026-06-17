using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Targeting")]
    public float detectionRange = 30f;
    public float attackRange = 8f;
    [Range(0f, 1f)]
    public float friendlyFireChance = 0.15f; // 15% de chance de atacar a otro enemigo

    [Header("Attack")]
    public float damage = 10f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    [Header("Retarget")]
    public float retargetInterval = 5f; // cada cuántos segundos reconsidera el target
    private float nextRetargetTime = 0f;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentTarget = player;
    }

    void Update()
    {
        CleanTarget();

        if (Time.time >= nextRetargetTime)
        {
            PickTarget();
            nextRetargetTime = Time.time + retargetInterval;
        }

        if (currentTarget == null) return;

        float distToTarget = Vector3.Distance(transform.position, currentTarget.position);

        if (distToTarget <= attackRange)
        {
            agent.isStopped = true;
            TryAttack();
        }
        else if (distToTarget <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void PickTarget()
    {
        // Si el player está muerto, siempre ir a otro enemigo
        if (player == null)
        {
            PickEnemyTarget();
            return;
        }

        // 15% de chance de atacar a otro enemigo
        if (Random.value < friendlyFireChance)
            PickEnemyTarget();
        else
            currentTarget = player;
    }

    void PickEnemyTarget()
    {
        EnemyAI[] allEnemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        EnemyAI closest = null;
        float closestDist = Mathf.Infinity;

        foreach (EnemyAI enemy in allEnemies)
        {
            if (enemy == this) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy;
            }
        }

        currentTarget = closest != null ? closest.transform : player;
    }

    void CleanTarget()
    {
        // Si el target fue destruido, volver al player
        if (currentTarget == null)
            currentTarget = player;
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;

        // Daño al player
        PlayerHealth playerHealth = currentTarget?.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            return;
        }

        // Daño a otro enemigo
        EnemyHealth enemyHealth = currentTarget?.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
            enemyHealth.TakeDamage(damage);
    }
}