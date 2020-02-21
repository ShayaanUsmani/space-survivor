using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ATTACHED ON: OBJECTS BETWEEN PLAYER AND END
public class ObjectsInPath : MonoBehaviour
{
    private GameObject player;
    private float passingDistBeforeDespawn = 65f; // distance after which game object gets destroyed (in z due to game layout)
    
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // we find the player using the assigned tag "Player"
       
    }

    void Update()
    {
        DestroyAfterPassingPlayer();
    }
        /*  private void ActiveInY()
          {        
              if (GameData.currentDimension == GameData.Dimension.Arcade)
              { 
                  if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y)>= arcadeAcceptableY)
                  {
                      if (gameObject.GetComponent<MeshRenderer>() != null) // if this gameobject contains a meshrenderer
                      {
                        //  gameObject.GetComponent<MeshRenderer>().enabled = false; // disable to make this gameobject invisible
                          gameObject.SetActive(false);

                      }
                      if (gameObject.GetComponent<Enemy1Move>() != null)
                      {   
                          gameObject.GetComponent<Enemy1Move>().enabled = false;
                      }
                  }


              }
              else if (GameData.currentDimension == GameData.Dimension.Modern)
              {
                  if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= arcadeAcceptableY)
                  {
                      if (gameObject.GetComponent<MeshRenderer>() != null)
                      {
                          gameObject.GetComponent<MeshRenderer>().enabled = true;


                      }
                      if (gameObject.GetComponent<Enemy1Move>() != null)
                      {
                          gameObject.GetComponent<Enemy1Move>().enabled = true;
                      }
                  }

              }
          }*/

        // if the object's pos in z + the allowed passing dist added are less than or equal to the player's then destroy it
        // NOTE: we gain position in z for the player as they go forward
    private void DestroyAfterPassingPlayer()
    {
        if (transform.position.z  + passingDistBeforeDespawn <= player.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
