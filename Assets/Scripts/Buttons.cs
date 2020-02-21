using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // pause panel and the game UI panel
    private GameObject pausePanel; 
    private GameObject gameUIPanel;

    public enum playerCraft
    {
        sparrow,
        bullet
    }
    public static playerCraft currentCraft { get; private set; }
    void Start()
    {
        // reference gameobjects fromt he heirarchy
        pausePanel = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Pause Panel").gameObject;
        gameUIPanel = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Game UI Panel").gameObject;
    }
    /* void Awake()
     {
         GameData.currentScreen = GameData.Screen.Menu;
     }

     // Update is called once per frame
     void Update()
     {
         /////////////////// GAME SCREEN CHECK //////////////////////
         if (GameData.currentScreen == GameData.Screen.Menu)
         {
             gameObject.SetActive(true);
         }

         else
         {
             gameObject.SetActive(false);
         }
         ///////////////////////////////////////////////////////////
     }
     */
    public void PlayGame() // Method for playing actual game when "PLAY" button clicked on canvas
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);

    }
    public void LoadMainMenu() // Method for going to main menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    /*
    public void ArcadeMode()
    {
        GameData.currentDimension = GameData.Dimension.Arcade;
    }

    public void ModernMode()
    {
        GameData.currentDimension = GameData.Dimension.Modern;
    }*/

        // select the craft by taking input fromt he eventsystem 
    public void SelectCharacter()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Craft1")
        {
            currentCraft = playerCraft.sparrow;
         //   GameData.player = (GameObject)Resources.Load("PlayerCraft1");
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Craft2")
        {
            currentCraft = playerCraft.bullet;
            //  GameData.player = (GameObject)Resources.Load("PlayerCraft2");
        }
    }

    // set timescale back to 100% (1)
    public void Resume()
    {
        if (gameUIPanel != null && pausePanel != null)
        {
            gameUIPanel.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    // quit the game

    public void ExitGame()
    {
        Application.Quit();
    }
}

