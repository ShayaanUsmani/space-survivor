using UnityEngine;

// manages the boss's state and hence movement
public class BossManager : MonoBehaviour
{
    public enum phase // phases of the bossS
    {
        entrance,
        fighting
    }
    // the current phase of the boss
    public static phase currentPhase { get; private set; }

    // the player
    GameObject player;
    // how far away (in z axis) should the boss be at baseline
    Vector3 followOffsetVect = Vector3.forward * 70f;
    // how far away (in z axis) should the boss be at baseline during the entrance
    Vector3 entanceFollowOffsetVect = Vector3.forward * 300f;

    // a boolean signifying if the boss is following the player to not
    public static bool followPLayer { get; private set; }

    // rate at which the boss rotates to player
    private float rotationToPlayerRate = 0.005f;

    // beat drops of our specific battle music timed
    private float firstBeatDrop = 4.6f;
    private float secondBeatDrop = 8.11f;
    private float thirdBeatDrop = 11.8f;
    private float fourthBeatDrop = 15.45f;
    
    // how far away can the player be before the boss gets aggrod
    private float aggroRange = 70f;

    // the rate at which the boss follows the players movements used for smoother movement
    private float followPlayerRate = 0.05f;
    // the rate at which the boss goes from the entrance phase to the fighting phase
    private float entranceToFightingRate = 0.1f;
    // time since the entrance began
    private float timeSinceEntrance;
    // bools showing if we have started the timer or not and if the boss increased in size
    private bool enableTimer, gotBigger;
    // how big relative to the boss's scale should he get when we make him bigger
    private float getBiggerMultiplier = 2.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // assign player
        currentPhase = phase.entrance; // make sure starting phase is entrance
        followPLayer = false;   // make sure the followPlayer bool is false at the start since it is static and remaking our
                                // scene while this bool has once been made as true will keep it as true
    
    }

    // --------------------------------------------------------------------------------//
    void Update()
    {
        // if the boss is in range of player affect bools to indicate as such
        if (InRangeOfObject(player, aggroRange) && !followPLayer)
        {
            enableTimer = true;
            followPLayer = true;
            currentPhase = phase.entrance;  // just again for future adjustments, make sure the current phase is entrance

        }

        // if the timer is enabled, count time on timeSinceEntrance
        if (enableTimer)
        {
            timeSinceEntrance += Time.deltaTime;
        }

        // if we are in the entrance phase, call the EntrancePhase method and if on fighting then FightingPhase
        switch (currentPhase)
        {
            case phase.entrance:
                EntrancePhase();
                break;

            case phase.fighting:
                FightingPhase();
                break;
        }

    }
    // -----------------------------------------------------------------------------------//

    private void EntrancePhase()
    {
        // before first beat drop facee directly (180 deg) away from the player
        if (timeSinceEntrance <= firstBeatDrop)
        {
            Vector3 direction = player.transform.position - gameObject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
            transform.Rotate(0f, 180f, 0f);
        }
        // after second beat drop slowly turn to the player ( rotationToPlayerRate towards them every time we run here)
        else if (timeSinceEntrance > firstBeatDrop && timeSinceEntrance <= secondBeatDrop)
        {
            Vector3 direction = player.transform.position - gameObject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationToPlayerRate);
        }

        // if we surpass the second beat drop then turn the boss to the player instantly
        else
        {
            Vector3 direction = player.transform.position - gameObject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }

        // on third beat make the boss bigger
        if (timeSinceEntrance >= thirdBeatDrop && !gotBigger)
        {
            transform.localScale *= getBiggerMultiplier;
            gotBigger = true;
        }
        // on fourth the current phase becomes the fighting phase
        if (timeSinceEntrance >= fourthBeatDrop)
        {
            currentPhase = phase.fighting;
        }
    }

    // transition from entrance offset to follow offset and keep pointing at the player
    private void FightingPhase()
    {
        if (followOffsetVect != entanceFollowOffsetVect)
        {
            followOffsetVect = Vector3.Lerp(followOffsetVect, entanceFollowOffsetVect, entranceToFightingRate);
        }
        Vector3 direction = player.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }


    // every physics system update, if we are suppose to follow the player, follow their position using linear interpolation for play feel
    private void FixedUpdate()
    {
        if (followPLayer)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + followOffsetVect, followPlayerRate);
        }
    }

    // returns true if we are in the range of the target (in z due to our game lay out) else returns false
    private bool InRangeOfObject(GameObject target, float range)
    {
        if (Mathf.Abs(player.transform.position.z - transform.position.z) <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
