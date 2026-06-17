using UnityEngine;

[CreateAssetMenu(fileName = "DamagePowerUp", menuName = "PowerUps/Damage")]
public class DamagePowerUp : PowerUpData
{
    public float damageMultiplier = 2f;

    public override void Apply(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        playerShooting.damage *= damageMultiplier;
    }

    public override void Remove(PlayerHealth playerHealth, PlayerShooting playerShooting)
    {
        playerShooting.damage /= damageMultiplier;
    }
}