using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 10f;
    [SerializeField] float maxDamageDistance = 5f; // Maximum distance for taking damage

    private void OnTriggerStay(Collider other)
    {
        // Check if the colliding object has a health system
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            // Calculate the distance between the fire source and the colliding object
            float distance = Vector3.Distance(transform.position, other.transform.position);

            // Check if the colliding object is within the maximum damage distance
            if (distance <= maxDamageDistance)
            {
                // Apply damage over time to the colliding object
                healthSystem.TakeDamage(damagePerSecond * Time.fixedDeltaTime);
            }
        }
    }
}
