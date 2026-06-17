using UnityEngine;

[CreateAssetMenu(fileName = "HealPowerUp", menuName = "PowerUps/Heal")]
public class HealPowerUp : PowerUpData
{
    public float healAmount = 30f;

    public override void Apply(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        playerHealth.Heal(healAmount);
    }

    public override void Remove(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        // La curación es instantánea, no hay que revertir nada
    }
}