using UnityEngine;

[CreateAssetMenu(fileName = "ShieldPowerUp", menuName = "PowerUps/Shield")]
public class ShieldPowerUp : PowerUpData
{
    public override void Apply(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        playerHealth.ActivateShield(duration);
    }

    public override void Remove(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        // El escudo se desactiva solo por duración en PlayerHealth
    }
}