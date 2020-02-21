using UnityEngine;

// Makes the normal enemy shoot
public class NormalEnemyAttack : Shoot
{
    private float timeLeftTillShoot; //the time left until we shoot
    private float shootDelay = 0.5f; //the time between every shot

    private void Awake()
    {
        shootSound = Resources.Load("NormalEnemyShootSound") as AudioClip; // assign the shoot sound to a file in resources
                                                                           // folder
        bulletPrefab = Resources.Load("Normal Enemy Bullet") as GameObject; // assign the bulletPrefab to the 
                                                                            // normal enemy bullet in the resources folder
    }
    void Start()
    {
        allGuns = FindChildrenPartTagged(gameObject, weaponTag); // find all children of the gameObject with the defaulted
                                                                 // tag for weapons and assign them to the allGuns list
        timeLeftTillShoot = shootDelay; //set the time before we first shoot to the baseline time

    }
    void Update()
    {
        // if time before we shoot is 0 then reset it and fire a bullet, otherwise subtract time passing from it

        if (timeLeftTillShoot <= 0f)
        {
            timeLeftTillShoot = shootDelay;
            FireBullet();
        }
        else
        {
            timeLeftTillShoot -= Time.deltaTime;
        }
    }
}
