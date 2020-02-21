using System.Collections.Generic;
using UnityEngine;

// lets us control the movement fo the player


public class PlayerController : GameMethods
{
    private float forceFwd = 2000f;
    private float moveForce = 1000f;
    private float rotSpeed = 2.5f;

    // max and mins of where the player can go in x and y
    private float playerMaxX = 80f;
    private float playerMinX = -80f;
    private float playerMaxY = 80f;
    private float playerMinY = -80f;

    private Rigidbody rb; // This is the rigidbody of the player spacecraft

    // keys
    private KeyCode boostKey = KeyCode.RightShift;
    private KeyCode rotRKey = KeyCode.L;
    private KeyCode rotLKey = KeyCode.K;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;

    // bools indicating if we preesed certain keys
    private bool pressedW;
    private bool pressedA;
    private bool pressedS;
    private bool pressedD;

    private bool pressedBoost;

    private bool pressedRotRight;
    private bool pressedRotLeft;

    // list of thrusters, thruster tag, the path of the thrust trail in our resources folder, the actual thrustTrail and its clone we will make,
    // the list of all clones, and a boolean indicating if we made the thrust trails
    private List<GameObject> thrusters;
    private string thrusterTag = "Thruster";
    private string thrusterVisualsPath = "Thrust Trail";
    private GameObject thrustTrail;
    private GameObject thrustTrailClone;
    private List<GameObject> thrustTrailClonesList = new List<GameObject>();
    private bool madeThrustTrailClones;
    /* private string warpDrivePath = "Warp Drive";
     private GameObject warpDrive;
     private GameObject warpDriveClone;*/


    
    void Start()
    {
        transform.position = Vector3.zero; // start player at 0 start position

        rb = gameObject.GetComponent<Rigidbody>();  // we get the component of rigidbody from inheritor of this script

        thrusters = FindChildrenPartTagged(gameObject, thrusterTag);

        thrustTrail = Resources.Load(thrusterVisualsPath) as GameObject;

       // warpDrive = Resources.Load(warpDrivePath) as GameObject;

    }

    void Update()
    {
        // COLLECT INPUT AND AFFECT BOOLS //
        // we check take input in Update and execute via bools in the Fixed Update since Fixed updates occur more than normal Updates
        // and Input is only collected every update meaning collecting in Fixed Update can lead to input being interperted incorrectly
        MoveInput();      

    }
    void FixedUpdate()
    {
        BaseMovement();
        StabalizeRotation();
        if (pressedW)
        {
            GoUp();
        }

        if (pressedA)
        {
            GoLeft();
        }

        if (pressedS)
        {
            GoDown();
        }

        if (pressedD)
        {
            GoRight();
        }

        if (pressedBoost)
        {
            BoostFwd();
            
            if(!madeThrustTrailClones)
            {
                foreach(GameObject thruster in thrusters)
                {
                    thrustTrailClone = Instantiate(thrustTrail, thruster.transform);
                    thrustTrailClonesList.Add(thrustTrailClone);
                }
                madeThrustTrailClones = true;
            }
        }
        else
        {
            if(madeThrustTrailClones)
            {
                foreach (GameObject thrustTrailClone in thrustTrailClonesList)
                {
                    Destroy(thrustTrailClone);
                }
                madeThrustTrailClones = false;
            }
        }
        if (pressedRotRight)
        {
            RotRight();
        }
        
        if (pressedRotLeft)
        {
            RotLeft();
        }
    }
    

    // the base movement is forward, this method just adds forward velocity to the player
    void BaseMovement()
    {
        rb.AddForce(Vector3.forward * forceFwd);
    }


    //========================== Methods for Moving ===================================//
    void BoostFwd()
    {
        rb.AddForce(Vector3.forward * moveForce);
    }


    void RotRight()
    {

        transform.Rotate(new Vector3(0f, 0f, -rotSpeed));
    }
    void RotLeft()
    {
        transform.Rotate(new Vector3(0f, 0f, rotSpeed));
    }
    ///////////////////////////////////////////////////-----------TODO: STORE ANGLES AND SLERP RATES IN GAMEDATA----------------------=============================
    void StabalizeRotation()
    {
        if(!(pressedD || pressedA || pressedRotLeft || pressedRotRight))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero) , 0.1f);
        }
    }

    void GoRight()
    {
        if (transform.position.x < playerMaxX)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f,-45f), 0.1f);
            rb.AddForce(Vector3.right * moveForce);
        }
    }
    void GoLeft()
    {
        if (transform.position.x > playerMinX)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 45f), 0.1f);
            rb.AddForce(Vector3.left * moveForce);
        }
    }
        

    void GoUp()
    {
        if (transform.position.y < playerMaxY)
        {
            rb.AddForce(Vector3.up * moveForce);
        }
    }
    void GoDown()
    {
        if (transform.position.y > playerMinY)
        {
            rb.AddForce(Vector3.down * moveForce);
        }
    }

    //============================== Using Input for Methods for Moving =============================//

    
    void MoveInput()
    {
        //        Gather WASD Input         //
        /////////////
        if (Input.GetKey(upKey))
        {
            if (GameData.currentDimension == GameData.Dimension.Modern)
            {
                pressedW = true;
            }

        }
        else
        {
            pressedW = false;
        }
        /////////////
        if (Input.GetKey(leftKey))
        {
            pressedA = true;
        }
        else
        {
            pressedA = false;
        }
        /////////////
        if (Input.GetKey(downKey))
        {
            if (GameData.currentDimension == GameData.Dimension.Modern)
            {
                pressedS = true;
            }
        }
        else
        {
            pressedS = false;
        }
        /////////////
        if (Input.GetKey(rightKey))
        {
            pressedD = true;
        }
        else
        {
            pressedD = false;
        }
        /////////////
        //        Gather Boost Forward Input         //

        if (Input.GetKey(boostKey))
        {
            pressedBoost = true;
        }
        else
        {
            pressedBoost = false;
        }

        //         Gather Rotation Input           //

        if (Input.GetKey(rotRKey))
        {
            pressedRotRight = true;
        }
        else
        {
            pressedRotRight = false;
        }
        if (Input.GetKey(rotLKey))
        {
            pressedRotLeft = true;
        }
        else
        {
            pressedRotLeft = false;
        }
    }

    
    


}