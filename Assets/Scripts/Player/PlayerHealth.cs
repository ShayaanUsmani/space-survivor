using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : ObjectHit
{
    private Image healthBar;
    private float fullHealth = 10f;
    private float currentHealthBarFill;
    private float healthLowerSmoothnessRate = 0.1f;
    void Awake()
    {
        // load resources n gameobjects from hierarchy
        healthBar = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Game UI Panel").transform.Find("HealthBar").GetComponent<Image>();
        hitVFX = Resources.Load("SmallExplosionEffect") as GameObject;
    }

    void Start()
    {// start the player with full health
        health = fullHealth;
    }

    void Update()
    {
        // lower the hp bar smoothly by using current and desired hp bar fill amounts
        currentHealthBarFill = healthBar.fillAmount;

        healthBar.fillAmount = Mathf.Lerp(currentHealthBarFill, health / fullHealth, healthLowerSmoothnessRate);

        // if we have 0 or below hp restart
        if (health <= 0f)
        {
            RestartLevel();
        }


    }
    // load the current scene again
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
