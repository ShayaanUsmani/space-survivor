using UnityEngine;
using UnityEngine.UI;

// take cares of the boss health (updating and showing it to the player)
public class BossHealth : ObjectHit
{
    private Image bossHealthBarImage; // hp bar img
    private GameObject bossHealthBar; // the panel containing all the hp info for the boss
    public static float currentHealth { get; private set; } // current health of the boss
    private float fullHealth = 100f; // full health of the boss
    private float currentHealthBarFill; // the amount of health bar filled in
    private float healthLowerSmoothnessRate = 0.1f; // how fast the hp bar drops wehn updated (1 = right away)
    private bool enabledHealthBar; // bool for if the hp bar is enabled
    void Awake()
    {
        // find and assign resources or gameObjects in the game hierarchy
        bossHealthBarImage = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Game UI Panel").transform.Find("BossHealthBar").transform.Find("BossHealthBarImage").GetComponent<Image>();
        bossHealthBar = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Game UI Panel").transform.Find("BossHealthBar").gameObject;
        winScreen = GameObject.FindGameObjectWithTag("GameUI").transform.Find("Victory Panel").gameObject;
        deathVFX = Resources.Load("BigExplosionEffect") as GameObject;
        deathSound = Resources.Load("BossDeathSound") as AudioClip;
    }
    private void Start()
    {
        health = fullHealth; // set the health of the parent class as fullHealth
    }
    void Update()
    {
        currentHealth = health; // set the current health as the health of the parent class

        // enable all the hp stuff if we are in the boss fighting phase
        if (BossManager.currentPhase != BossManager.phase.entrance && !enabledHealthBar && health > 0)
        {
            enabledHealthBar = true;
            bossHealthBar.SetActive(true);
        }
        else if (BossManager.currentPhase == BossManager.phase.entrance)
        {
            bossHealthBar.SetActive(false);
        }

        // get the current health bar fill and the linearly interpolate the fill from current to updated hp in terms of hp bar fill
        currentHealthBarFill = bossHealthBarImage.fillAmount;

        bossHealthBarImage.fillAmount = Mathf.Lerp(currentHealthBarFill, health / fullHealth, healthLowerSmoothnessRate);


    }
    
}
