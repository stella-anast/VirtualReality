using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Animator playerAnimator;

    public KeyCode startCombatKey = KeyCode.F; // Change this to the desired button/key
    public KeyCode attackKey = KeyCode.Mouse0; // Change this to the desired attack button/key

    private bool inCombatMode = false;
    private bool weaponDrawn = false;
    void Start()
    {
        // Ensure cursor is unlocked and visible when the game starts
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        // Check if the designated button is pressed
        if (Input.GetKeyDown(startCombatKey))
        {
            // Toggle combat mode
            ToggleCombatMode();
        }

        // Check if the attack button is pressed and weapon is drawn
        if (Input.GetKeyDown(attackKey) && weaponDrawn)
        {
            // Trigger the attack animation directly
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
