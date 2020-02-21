using UnityEngine;

// spawn player at our position
public class SpawnPlayer : MonoBehaviour
{
    void Awake()
    {// load whichever ship from spawn and create and instance in game
        if (Buttons.currentCraft == Buttons.playerCraft.sparrow)
        {
            Instantiate(Resources.Load("PlayerCraft1"), transform);
        }
        else if (Buttons.currentCraft == Buttons.playerCraft.bullet)
        {
            Instantiate(Resources.Load("PlayerCraft2"), transform);
        }
    }
}
