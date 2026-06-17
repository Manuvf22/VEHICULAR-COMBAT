using UnityEngine;
using System.Collections;

public class PowerUpPickup : MonoBehaviour
{
    public PowerUpData powerUpData;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();

        if (playerHealth == null || playerShooting == null) return;

        StartCoroutine(ApplyPowerUp(playerHealth, playerShooting));
        // Desactivar el visual pero esperar a que termine la corrutina
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>()?.gameObject.SetActive(false);
    }

    IEnumerator ApplyPowerUp(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        powerUpData.Apply(playerHealth, playerShooting);
        yield return new WaitForSeconds(powerUpData.duration);
        powerUpData.Remove(playerHealth, playerShooting);
        Destroy(gameObject);
    }
}