using System.Collections.Generic;
using UnityEngine;

// allows gameObjects to shoot a bullet
public class Shoot : GameMethods
{

    protected GameObject bulletPrefab; // the bullet prefab

    private GameObject bulletClone; // clone of the bullet created when we instantiate/spawn it

    protected List<GameObject> allGuns; // location and rotation of all the guns

    protected string weaponTag = "Gun"; // the default weapon tag

    protected string playerTag = "Player"; // default player tag

    private float despawnInTime = 0.75f; // time before bullet despawns

    private Vector3 bulletOffsetVect = new Vector3 (0f,0f,20f); // bullet offset from the weapon

    private int switchOffsetDirection = -1; // instead of having magic numbers multiply quantity, we call -1 switchDirectionsInZ
                                          // since it will help us switch the direction in which the bullet spawns relative to the
                                          // gameObject ( -300 + 20 vs -300 + (-20))

    private float bulletSpeed = 500f; // speed of the bullet

    private AudioSource shootSoundSource; // the audiosource the shooting sound will come from

    protected AudioClip shootSound; // the shoot sound

    private bool addedAudioSource; // a boolean telling us wether or not we have added and audiosoure to the gameObject

    void Start()
    {
        // if the gameObject does not have the player tag on them, then reverse their offset of bullet spawning relative to their
        // weapon
        if (gameObject.tag != playerTag)
        {
            bulletOffsetVect *= switchOffsetDirection;
        }
    }

    protected void FireBullet()
    {
        // take all the guns in the allGuns list and create an instance of a bullet using gun's position + the offset vector
        // after that accelerate the bullet and despawn it in due time
        // if an audio clip for the sound of shooting is available, play the sound
        foreach (GameObject gun in allGuns)
        {
            if (bulletPrefab != null)
            {
                bulletClone = Instantiate(bulletPrefab, gun.transform.position + bulletOffsetVect, gun.transform.rotation);
                /*if (GameData.currentDimension == GameData.Dimension.Modern)
                {
                    bulletClone = Instantiate(bulletPrefab, gun.transform.position + bulletOffsetVect, gun.transform.rotation);
                }
                else if (GameData.currentDimension == GameData.Dimension.Arcade)
                {
                    // if in arcade shoot all bullets at the whole spaceship body's y and z position with the guns' x position
                    bulletClone = Instantiate(bulletPrefab, new Vector3(gun.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z) + bulletOffsetVect, gun.transform.rotation);
                }*/
                AccelBullet(bulletClone);
                DespawnBulletInTime();
                if (shootSound != null)
                {
                    PlayShootSound();
                }
            }
        }
    }

    // find the rigidbody on the current bullet clone and add velocity forward to that bullet
    void AccelBullet(GameObject bullet)
    {
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    // Despawns the bullet when time reaches "despawnInTime"
    void DespawnBulletInTime()
    {
        Destroy(bulletClone, despawnInTime);
    }

    // if we haven't added an audiosource to the player then add one and set the clip on it as the shootSound
    // making sure to affect the condition "addedAudioSource" so we only add the source once
    void PlayShootSound()
    {
        if (!addedAudioSource)
        {
            shootSoundSource = gameObject.AddComponent<AudioSource>();
            shootSoundSource.clip = shootSound;
            addedAudioSource = true;
        }
        shootSoundSource.Play();
    }

}