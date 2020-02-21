using UnityEngine;
using UnityEngine.SceneManagement;
// WARNING: OBSTACLES DONT KILL EACHOTHER
public class ObjectHit : MonoBehaviour
{
    // we make health and dmg taken adjustible for child classes
    protected float health = 1f;
    protected float damageTaken = 1f;
    // we also allow them to have hit visuals and death visuals
    protected GameObject hitVFX; 
    protected GameObject deathVFX;
    // let them have a win screen in case we add multiple bosses with win announcements
    protected GameObject winScreen;

    // bools indicating if the boss dead or if we played deat soun yet
    private bool playedDeathSound;
    private bool bossDied;

    // attack and objetc tags
    private string playerTag = "Player";
    private string playerAttackTag = "Player Attack";

    private string enemyTag = "Enemy";
    private string enemyAttackTag = "Enemy Attack";

    private string bossTag = "Boss";
    private string bossAttackTag = "Boss Attack";

    private string obstacleTag = "Obstacle";

    // death sound
    protected AudioClip deathSound;

    void Awake()
    {// set winscreen as false
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider collider)
    {// if a gameobject's collider is triggerd, check the colliding object and the main object's tags to see if affected,
        if (gameObject.tag == playerTag) // PLAYER TRIGGERED
        {
            if (collider.tag == enemyAttackTag || collider.tag == enemyTag || collider.tag == bossAttackTag || collider.tag == obstacleTag)
            {
                if (hitVFX != null) // show the hit visuals if available
                {
                    Instantiate(hitVFX, transform);
                }

                if (!bossDied) // take dmg only if boss is alive since u win after killin him
                {
                    health -= damageTaken;
                }
            }
        }

        else
        {
            if (collider.tag == playerAttackTag || collider.tag == playerTag)
            {
                health -= damageTaken;

                if (hitVFX != null) // show hit visuals if available
                {
                    Instantiate(hitVFX, transform); 
                }

            }
            if (health <= 0f)
            {
                if (deathVFX != null) // show death visuals if can
                {
                    Instantiate(deathVFX, transform.position, transform.rotation);

                }

                if (gameObject.tag == bossTag) // if the boss just died, show victory screen and confirm bossDied
                {
                    winScreen.SetActive(true);
                    bossDied = true;

                }

                if (deathSound != null) // if there is a death sound then add an audiosource, play the sound
                                        // and destroy the object after the clip is done so we acctually hear it
                {
                    if (!playedDeathSound)
                    {
                        AudioSource deathAudioSource = gameObject.AddComponent<AudioSource>();
                        deathAudioSource.PlayOneShot(deathSound);
                        playedDeathSound = true;

                        Destroy(gameObject, deathSound.length); //waits till audio is finished playing before destroying.
                    }
                }
                else
                {
                    Destroy(gameObject);
                }

            }

        }

    }

}