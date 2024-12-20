using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public int spawnRate;
    public int asteroidTypes;
    public int numLives;

    public TextMeshProUGUI TMPspawn;
    public TextMeshProUGUI TMPasts;
    public TextMeshProUGUI TMPlives;

    public GameObject menuButton;
    public GameObject gameButton;

    public UIManager um;

    public static List<int> settings;
    public static int spawnSet;
    public static int typesSet;
    public static int livesSet;

    // public GameManager gm;

    
    // Start is called before the first frame update
    void Start()
    {
        um = FindObjectOfType<UIManager>(); 

        UpdateSpawnRate();
        UpdateAsteroidTypes();
        UpdateNumLives();

        if ((menuButton.activeSelf && gameButton.activeSelf) || (!menuButton.activeSelf && !gameButton.activeSelf))
        {
            menuButton.SetActive(true);
            gameButton.SetActive(false);
        }

        // um.spawnSet = spawnRate;
        // um.typesSet = asteroidTypes;
        // um.livesSet = numLives;

        // if (gm.isActive) {
        //     gameButton.SetActive(true);
        // } else menuButton.SetActive(true);
    }

    public void UpdateSpawnRate()
    {
        string text;
        if (spawnRate == 3) { 
            spawnRate = 1;
        } else { spawnRate ++; }

        if (spawnRate == 2) { text = "Normal"; } else if (spawnRate == 3) { text = "Fast"; } else if (spawnRate == 1) { text = "Slow"; } else { text = "Error"; }

        TMPspawn.text = "Current: " + text;
        um.spawnSet = spawnRate;
        spawnSet = spawnRate;
    }

    public void UpdateAsteroidTypes()
    {
        if (asteroidTypes == 3) { 
            asteroidTypes = 1;
        } else { asteroidTypes ++; }

        TMPasts.text = "Current: " + asteroidTypes;
        um.typesSet = asteroidTypes;
        typesSet = asteroidTypes;
    }

    public void UpdateNumLives()
    {
        if (numLives == 3) { 
            numLives = 1;
        } else { numLives ++; }

        TMPlives.text = "Current: " + numLives;
        um.livesSet = numLives;
        livesSet = numLives;
    }

    // Update is called once per frame


}
