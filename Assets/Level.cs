using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    bool playerDead = false;

    public static Level instance;
    public int playerHealthPublic;

    int destroyedShip = 0;
    Text dsText;

    int infestedPlanet = 0;
    Text ipText;

    float elapsedTime; // Store the total elapsed time
    public Text timerText;

    private int minutes;
    private int hours;

    Text healthText;
    Color deadColor;

    private void Awake()
    {
        instance = this;
        dsText = GameObject.Find("ShipsDestroyed").GetComponent<Text>();
        ipText = GameObject.Find("PlanetsInfested").GetComponent<Text>();
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        healthText = GameObject.Find("Health").GetComponent<Text>();

        ColorUtility.TryParseHtmlString("#666666", out deadColor);
    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f; // Initialize elapsedTime to 0
    }

    // Update is called once per frame

    void Update()
    {
        if (playerDead == false)
        {
            elapsedTime += Time.deltaTime;

            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            timerText.text = $"{hours} hours {minutes} minutes {seconds} seconds";
        }
    }

    public void AddShipDestroyed(int amountToAdd)
    {
        destroyedShip += amountToAdd;
        dsText.text = "Ships Destroyed : " + destroyedShip.ToString();
    }

    public void AddPlanetInfested(int amountToAdd)
    {
        infestedPlanet += amountToAdd;
        ipText.text = "Planets Infested : " + infestedPlanet.ToString();
    }

    public void updateHealth(int amount)
    {
        int playerHealth = amount;
        playerHealthPublic = amount;
        if (playerHealth <= 0)
        {
            playerDead = true;
            healthText.text = "Health : DEAD";
            healthText.color = deadColor;
        }
        else
        {
            healthText.text = "Health : " + playerHealth.ToString(); //lower health
        }
    }
}