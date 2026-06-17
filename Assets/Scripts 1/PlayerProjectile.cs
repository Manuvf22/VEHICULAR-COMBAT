using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector]
    public float damage = 20f;

   
        

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }

       
}