using System;

public class EventManager : Singleton<EventManager>
{
    // Triggered when click on Start or Retry btn
    public static Action OnStartGame;
    
    /// <summary>
    /// Triggered when the player's health changes.
    /// Parameters:
    /// - int currentHealth: The player's current health.
    /// - int maxHealth: The player's maximum health.
    /// </summary>
    public static Action<int, int> OnHealthChanged;

    /// <summary>
    /// Triggered when the player dies (e.g., health reaches 0).
    /// </summary>
    public static Action OnPlayerDied;
    
        /// <summary>
    /// Triggered when the player's ammo changes.
    /// Parameters:
    /// - int currentAmmo: The player's current ammo count.
    /// - int maxAmmo: The maximum ammo capacity.
    /// </summary>
    public static Action<int, int> OnAmmoChanged;

    /// <summary>
    /// Triggered when the player reloads their weapon.
    /// </summary>
    public static Action OnReloaded;
}
