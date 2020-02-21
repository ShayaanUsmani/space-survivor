using UnityEngine;

// shoots towards player with another gameobject
public class BossAttack : Shoot
{
    private float shootDelay = 0.05f; // rate of fire
    private float timeBeforeShoot; // time before fire
    private GameObject player; // player gameobject
    private GameObject mouthGun; // mouthGun gameobject

    void Awake()
    {
        shootSound = Resources.Load("BossEnemyShootSound") as AudioClip; // assign an audio file from resources folder to a protected variable
        bulletPrefab = Resources.Load("Boss Enemy Bullet") as GameObject; // assign its bullet prefab from resources folder to a protected variable
    }

    void Start()
    {
        timeBeforeShoot = shootDelay; // set time before firing to the base line time
        player = GameObject.FindGameObjectWithTag(playerTag); //find a gameobject in heirarchy with the player's tag
        mouthGun = FindChildTagged(gameObject, weaponTag); //find a child of the gameobject with the defaulted weapon tag
        allGuns = FindChildrenPartTagged(gameObject, weaponTag); // find all children of the gameObject with the defaulted
                                                                 // tag for weapons and assign them to the allGuns list
    }

    void Update()
    {   
        // if the gameObject is currently fighting then point the weapon at the player and shoot every "shootDelay" seconds

        if (BossManager.currentPhase == BossManager.phase.fighting)  
        {
            pointAtPlayer();

            if (timeBeforeShoot <= 0)
            {
                timeBeforeShoot = shootDelay;
                FireBullet();
            }
            else
            {
                timeBeforeShoot -= Time.deltaTime;
            }


        }
    }

    // points the mouth gun towards the player
    void pointAtPlayer()
    {
        Vector3 direction = player.transform.position - mouthGun.transform.position; //get the direction of the player relative to the
                                                                                     //mouth by subtracting both of their position vectors
        
        mouthGun.transform.rotation = Quaternion.LookRotation(direction); // set the mouthGun's rotation towards the player by passing
                                                                          // our direction vector into the Quaternion LookRotation method
                                                                          // to make the mouth point/look at the player
    }
}
