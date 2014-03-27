using UnityEngine;
using System.Collections;

public class Cooldown
{
    private Timer cooldownTimer;
    private bool isOnCooldown;
    public bool IsOnCooldown
    {
        get { return isOnCooldown; }
    }
    public bool IsOffCooldown
    {
        get { return !IsOnCooldown; }
    }

    public Cooldown(float cooldownTime)
    {
        cooldownTimer = new Timer(cooldownTime);
        isOnCooldown = false;
    }

    public void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer.Update();
            if (cooldownTimer.HasCompleted())
            {
                isOnCooldown = false;
            }
        }
    }

    public void GoOnCooldown()
    {
        isOnCooldown = true;
    }

    /// <summary>
    /// Gets the progress of the cooldown, returns 0 - 1
    /// </summary>
    /// <returns>returns 0 - 1</returns>
    public float GetCooldownPercent()
    {
        return cooldownTimer.GetPercentComplete();
    }
}
