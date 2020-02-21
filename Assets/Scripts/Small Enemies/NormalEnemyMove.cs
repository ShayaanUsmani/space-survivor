using System.Collections;
using UnityEngine;

// moves the enemy when i range of the player
public class NormalEnemyMove : MonoBehaviour
{
    private GameObject player;
    private float enemyMoveSpeed = 2f;

    // have we reached the x or y of the player
    private bool onceReachedX;
    private bool onceReachedY;

    // distance before we do the special move and the start time of the move
    private float distBeforeSpecialMove = 2f;
    private float specialMoveStartTime = 30f;

    // range befroe agrod from player
    private float agroRange = 170f;

    // amplitdue of our special movement
    private float specialAmplitude;
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // we find the player using the assigned tag "Player"
    }

    // Update is called once per frame
    void Update()
    {
        // constantly try to move and attack
        Move();
        Attack();
    }

    private void Move()
    {
        // if the enemy is in decided range in the z axis of the player
        if (gameObject.transform.position.z - player.transform.position.z <= agroRange)
        {
            // adjust positions in x and y
            PositionInX();

            PositionInY();

        }

    }

    void Attack()
    {
        // If in range to be active
        if (gameObject.transform.position.z - player.transform.position.z <= agroRange)
        {
            // if the gameObject this script is attached to has a script called "ShootBullet"
            if (gameObject.GetComponent<Shoot>() != null)
            {// enable that script
                gameObject.GetComponent<Shoot>().enabled = true;
            }
        }
        // otherwise
        else
        {  // if the gameObject this script is attached to has a script called "ShootBullet"
            if (gameObject.GetComponent<Shoot>() != null)
            {   // disable that script
                gameObject.GetComponent<Shoot>().enabled = false;
            }
        }

    }


    void PositionInX()
    {
        if (!onceReachedX)
        {
            // try to get infront of player by checking relative x and y positions and acting on those values
            if (player.transform.position.x > transform.position.x)
            {
                gameObject.transform.Translate(Vector3.left * enemyMoveSpeed);
            }
            if (player.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.Translate(Vector3.right * enemyMoveSpeed);
            }

            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= distBeforeSpecialMove) // when we are "distBeforeSpecialMove" away from the player in the x dimension
            {
                onceReachedX = true; // we have reached the goal X position/range once
                specialMoveStartTime = Time.time;  // we are now about to start the special move so we set the specialMoveStartTime to the current time
            }
        }
        else
        { // to get the enemy moving in a wave in the x axis we use the sin function
            float timeSinceStart = Time.time - specialMoveStartTime;
            gameObject.transform.position = new Vector3(specialAmplitude * Mathf.Sin(timeSinceStart), gameObject.transform.position.y, gameObject.transform.position.z);

        }
    }

    void PositionInY()
    {
        // if we haven't reached the player in y axis once yet then try to reach them by checking relative y pos and adjusting
        if (!onceReachedY)
        {
            if (gameObject.transform.position.y < player.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.up * enemyMoveSpeed);
            }
            if (gameObject.transform.position.y > player.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.down * enemyMoveSpeed);
            }
            if (gameObject.transform.position.y == player.transform.position.y) // when we are "distBeforeSpecialMove" away from the player in the x dimension
            {
                onceReachedY = true;
            }
        }

    }

}
