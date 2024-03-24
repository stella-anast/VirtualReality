using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Animator playerAnimator;

    public KeyCode startCombatKey = KeyCode.F;
    public KeyCode attackKey = KeyCode.Mouse0; 

    private bool inCombatMode = false;
    private bool weaponDrawn = false;
    //press F to draw or sheath weapon & left mouse click to attack
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
       
        if (Input.GetKeyDown(startCombatKey))
        {
           
            ToggleCombatMode();
        }

        
        if (Input.GetKeyDown(attackKey) && weaponDrawn)
        {
           
            playerAnimator.SetTrigger("Attack");
        }
    }

    void ToggleCombatMode()
    {
        
        inCombatMode = !inCombatMode;

        
        if (inCombatMode)
        {
            
            playerAnimator.SetTrigger("DrawWeapon");
        }
        else
        {
            
            playerAnimator.SetTrigger("SheathWeapon");
        }
    }

    public void OnDrawWeapon()
    {     
        weaponDrawn = true;
    }
}
