using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Animator playerAnimator;

    public KeyCode startCombatKey = KeyCode.F; // Change this to the desired button/key

    private bool inCombatMode = false;
    private bool weaponDrawn = false;

    void Update()
    {
        // Check if the designated button is pressed
        if (Input.GetKeyDown(startCombatKey))
        {
            // Toggle combat mode
            ToggleCombatMode();
        }
        if (Input.GetMouseButtonDown(0) && weaponDrawn)
        {
            playerAnimator.SetTrigger("Attack");
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
        }
        else
        {
            // Trigger the "SheathWeapon" animation transition
            playerAnimator.SetTrigger("SheathWeapon");
        }
    }
    public void OnDrawWeapon()
    {
        // Set weaponDrawn to true when the weapon is drawn
        weaponDrawn = true;
    }
}
