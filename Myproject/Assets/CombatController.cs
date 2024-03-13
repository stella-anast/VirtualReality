using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject weapon; // Reference to the weapon GameObject
    public Transform handTransform; // Reference to the hand transform where the weapon will be held
    public KeyCode startCombatKey = KeyCode.F; // Change this to the desired button/key

    private bool inCombatMode = false;

    void Update()
    {
        // Check if the designated button is pressed
        if (Input.GetKeyDown(startCombatKey))
        {
            // Toggle combat mode
            ToggleCombatMode();
        }
    }

    void ToggleCombatMode()
    {
        // Toggle the combat mode flag
        inCombatMode = !inCombatMode;

        // Trigger combat animations or actions based on the combat mode
        if (inCombatMode)
        {
            // Trigger the "DrawWeapon" animation transition
            playerAnimator.SetTrigger("DrawWeapon");

            // Set the "SheathWeapon" boolean to false (if it's currently true)
            playerAnimator.SetBool("SheathWeapon", false);
        }
        else
        {
            // Set the "SheathWeapon" boolean to true
            playerAnimator.SetBool("SheathWeapon", true);
        }
    }
}
