using UnityEngine;

public class FireDamageHandler : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to apply per second
    public float damageInterval = 0.5f; // Time interval between each damage application (e.g., 0.5 seconds)
    private float lastDamageTime; // Time of the last damage application

    void Start()
    {
        lastDamageTime = Time.time;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if enough time has passed since the last damage application
            if (Time.time - lastDamageTime >= damageInterval)
            {
                // Apply damage to the player
                HealthSystem playerHealth = other.GetComponent<HealthSystem>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastDamageTime = Time.time; // Update last damage time
                }
            }
        }
    }
}
