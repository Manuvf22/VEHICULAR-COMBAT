using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
    public string powerUpName;
    public float duration;
    public Sprite icon;

    public abstract void Apply(PlayerHealth playerHealth, PlayerShooting playerShooting);
    public abstract void Remove(PlayerHealth playerHealth, PlayerShooting playerShooting);
}