using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Firing Points")]
    public Transform firePointLeft;
    public Transform firePointRight;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 30f;
    public float damage = 20f;

    [Header("Aiming Cone")]
    [Range(10f, 90f)]
    public float coneAngle = 45f; // ángulo máximo desde el frente del auto

    [Header("Fire Rate")]
    public float fireRate = 0.3f;
    private float nextFireTime = 0f;

    private Vector3 aimDirection; // dirección final de disparo (ya clampeada)

    void Update()
    {
        UpdateAimDirection();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void UpdateAimDirection()
    {
        // Raycast del mouse al plano del suelo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);
            Vector3 toMouse = (mouseWorldPos - transform.position).normalized;

            // Ángulo entre el frente del auto y la dirección al mouse
            float angle = Vector3.SignedAngle(transform.forward, toMouse, Vector3.up);

            // Clampear dentro del cono
            float clampedAngle = Mathf.Clamp(angle, -coneAngle, coneAngle);
            aimDirection = Quaternion.AngleAxis(clampedAngle, Vector3.up) * transform.forward;
        }
        else
        {
            // Si no hay hit, disparar recto al frente
            aimDirection = transform.forward;
        }
    }

    void Shoot()
    {
        SpawnProjectile(firePointLeft);
        SpawnProjectile(firePointRight);
    }

    void SpawnProjectile(Transform firePoint)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(aimDirection));
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = aimDirection * projectileSpeed;

        PlayerProjectile projScript = proj.GetComponent<PlayerProjectile>();
        if (projScript != null)
            projScript.damage = damage;

        Destroy(proj, 4f);
    }

    // Visualizar el cono en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 leftBound = Quaternion.AngleAxis(-coneAngle, Vector3.up) * transform.forward;
        Vector3 rightBound = Quaternion.AngleAxis(coneAngle, Vector3.up) * transform.forward;
        Gizmos.DrawRay(transform.position, leftBound * 5f);
        Gizmos.DrawRay(transform.position, rightBound * 5f);
    }
}