using UnityEngine;
// makes cam follow player smoothly
public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    // camera offset from player and smoothness speed
    private Vector3 camOffsetPosModern = new Vector3(0f, 3f, -20f);
    //private Vector3 camOffsetPosArcade = new Vector3(0f, 150f, 20f);
    //private Vector3 camRotArcade = new Vector3(90f, 0f, 0f); //euler rotation
    private float camSmoothSpeed = 0.3f;


    void Start()
    {
        player = GameObject.FindWithTag("Player"); // we find the player using the assigned tag "Player"\
    }

    void FixedUpdate()
    {
        AdjustRotAndPos();
        
    }

    // TAKES CARE OF CAM ROTATIONAL ORIENTATION BETWEEN MODES //
    void AdjustRotAndPos()
    {
        
        //======================================= 3D / MODERN MODE =======================================/
        if (GameData.currentDimension == GameData.Dimension.Modern) // if current dimension enum is modern
        {
            Vector3 desiredPos = player.transform.position + camOffsetPosModern; //target position the cam wants to be in
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, camSmoothSpeed); //we go from start position
                                                                                                //(cam.transform.position) to
                                                                                                //the desired position in camSmoothness
                                                                                                //amount of time resulting in a natural
                                                                                                //feeling camera follow using Lerp
            transform.position = smoothedPos; // set cam position to the smoothed position
         /*   if (switchedToArcadeCam)
            {
                transform.Rotate(-camRotArcade);
                switchedToArcadeCam = false;
            }*/
        }
        //======================================= 2D / ARCADE MODE =======================================/
        /*else if ((GameData.currentDimension == GameData.Dimension.Arcade))// if current dimension enum is arcade
        {
            transform.position = player.transform.position + camOffsetPosArcade; // set cam position 2D
            // if we haven't switched to arcade mode camera
            // rotate the cam and set the bool to opposite so 
            // we don't do the same thing on next frame
            if (!switchedToArcadeCam)
            {
                transform.Rotate(camRotArcade);
                switchedToArcadeCam = true;
            }
        }*/

    }

}
