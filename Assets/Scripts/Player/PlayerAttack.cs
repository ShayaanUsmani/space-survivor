using UnityEngine;

// lets the player shoot when allowed to
public class PlayerAttack : Shoot
{
    private static KeyCode attackKey = KeyCode.Space; // space bar key

    private bool playerAttackEnabled; // bool indicating wether we can attack or not

    private void Awake()
    {
        // load our bullet prefab
       bulletPrefab = Resources.Load("Player Bullet") as GameObject;

    }
    void Start()
    {   
        allGuns = FindChildrenPartTagged(gameObject, weaponTag); // find all children of the gameObject with the defaulted
                                                                 // tag for weapons and assign them to the allGuns list
    }
    void Update()
    {
        // if we can attack then attack on spacebar
        if (playerAttackEnabled)
        {
            if (Input.GetKeyDown(attackKey))
            {
                FireBullet();
            }
        }
        // else check if we are in the boss fight yet and if we are then we can start to attack
        else
        {
            if (BossManager.currentPhase == BossManager.phase.fighting)
            {
                playerAttackEnabled = true;
            }
        }
    }
}
