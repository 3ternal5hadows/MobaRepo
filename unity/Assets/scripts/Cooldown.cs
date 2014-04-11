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
    /// Gets the progress of the cooldown, returns 0 - 1, 0 is just used, 1 is available
    /// </summary>
    /// <returns>returns 0 - 1</returns>
    public float GetCooldownPercent()
    {
        if (IsOffCooldown)
        {
            return 1;
        }
        return cooldownTimer.GetPercentComplete();
    }
}
