using UnityEngine;
// ui during game
public class GameUI : GameMethods
{

        // main panels during the game
    private GameObject pausePanel;
    private GameObject gameUIPanel;

    // key to pause
    private KeyCode pauseKey = KeyCode.Escape;

    private void Start()
    {
        // assign game objects from hiearchy
        pausePanel = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Pause Panel").gameObject;
        gameUIPanel = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Game UI Panel").gameObject;

    }

    void Update()
    {
        // if the player presses pause key, pause
        if (Input.GetKeyDown(pauseKey))
        {
            Pause();
        }
        /*
        if(GameData.currentDimension == GameData.Dimension.Arcade && arcadeBar.fillAmount == 1)
        {
            ArcadeBarUse();
        }
        if(arcadeBar.fillAmount <= 0)
        {
            GameData.currentDimension = GameData.Dimension.Modern;
        }
        if(GameData.currentDimension == GameData.Dimension.Modern)
        {
            ArcadeBarRegen();
        }
    }

    void ArcadeBarRegen()
    {
        arcadeBar.fillAmount += (arcadeBarRegenRate * Time.deltaTime) / arcadeBarMax;
    }

    void ArcadeBarUse()
    {
        arcadeBar.fillAmount -= (arcadeBarUseRate * Time.deltaTime) / arcadeBarMax;
    }*/
    }
    // pause the game by setting time passing scale as 0
    void Pause()
    {
        if (gameUIPanel != null && pausePanel != null)
        {
            gameUIPanel.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
