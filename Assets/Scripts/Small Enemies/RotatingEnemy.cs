using UnityEngine;

// spins the gameObject
public class RotatingEnemy : MonoBehaviour
{
    private GameObject player;
    private float aggroRange = 500f; // range before we start rotating
    private float rotateRate = 100f; // rate at which we rotate
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // assign playetr
    }
    // if in aggro range spin the object by the z axis (* Time.deltaTime to make it time independant
    // and give us room to work the pause menu and such
    void Update()
    {
        if(transform.position.z - player.transform.position.z <= aggroRange)
        {
            transform.Rotate(Vector3.forward * rotateRate * Time.deltaTime);
        }
    }
}
