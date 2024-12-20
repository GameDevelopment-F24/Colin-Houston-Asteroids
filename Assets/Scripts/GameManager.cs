using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;
    public Player player;
    public bool isActive;
    public UIManager um;

    public Asteroid astPrefab1;
    public Asteroid astPrefab2;
    public Asteroid astPrefab3;

    public Asteroid tinyAsteroid;
    public Transform playerTransform;
    public Transform spawnPos;
    public int resetDelay;

    public float moveDelay;

    public int asteroidDelay;
    private Vector2 left = new Vector2(-1, 0);
    private Vector2 right = new Vector2(1,0);

    private Vector2 up = new Vector2(0, 1);

    private Vector2 down = new Vector2(0, -1);
    private char dir;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI respawnText;
    public int lives;


    // Start is called before the first frame update
    void Start()
    {
        if (Settings.livesSet == 0) { lives = 3; } 
        else {
            lives = Settings.livesSet;
        }
        if (Settings.spawnSet == 1) { 
            asteroidDelay = 4;
        } else if (Settings.spawnSet == 2) {
            asteroidDelay = 3;
        } else {
            asteroidDelay = 2;
        }
        
        SpawnPlayer();

        if (playerTransform == null) {
            Debug.Log("$$$ERROR: playerTransform not set in GameManager.cs");
        }
        SpawnAsteroids(false, new Vector2(0,0));
        isActive = true;
        livesText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AsteroidTimer(){
        yield return new WaitForSeconds(asteroidDelay);
        SpawnAsteroids(false, new Vector2(0,0));
        
    }

    public void SpawnAsteroids(Boolean tiny, Vector2 pos){
        if (!tiny){
            Asteroid ast1 = Instantiate(astPrefab1, GetRandomPosition(), Quaternion.identity);
            ast1.direction = dir;
            ast1.SetVelocity();

            if (Settings.typesSet >= 2){
                Asteroid ast2 = Instantiate(astPrefab2, GetRandomPosition(), Quaternion.identity);
                ast2.direction = dir;
                ast2.SetVelocity();
            } else {
                Asteroid ast2 = Instantiate(astPrefab1, GetRandomPosition(), Quaternion.identity);
                ast2.direction = dir;
                ast2.SetVelocity();
            }

            if (Settings.typesSet == 3){
                Asteroid ast3 = Instantiate(astPrefab3, GetRandomPosition(), Quaternion.identity);
                ast3.direction = dir;
                ast3.SetVelocity(); 
            } else {
                Asteroid ast3 = Instantiate(astPrefab1, GetRandomPosition(), Quaternion.identity);
                ast3.direction = dir;
                ast3.SetVelocity(); 
            }
        } else {
            Asteroid ast1 = Instantiate(tinyAsteroid, pos, Quaternion.identity);
            ast1.direction = 'l';
            ast1.SetVelocity();

            Asteroid ast2 = Instantiate(tinyAsteroid, pos, Quaternion.identity);
            ast2.direction = 'r';
            ast2.SetVelocity();

            Asteroid ast3 = Instantiate(tinyAsteroid, pos, Quaternion.identity);
            ast3.direction = 'd';
            ast3.SetVelocity();

            Asteroid ast4 = Instantiate(tinyAsteroid, pos, Quaternion.identity);
            ast4.direction = 'u';
            ast4.SetVelocity();
        }
        StartCoroutine(AsteroidTimer());
    }

    public void GameOver(){
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame(){
        StartCoroutine(ResetTimer());
    }

    private IEnumerator ResetTimer()
    {
        deathText.gameObject.SetActive(true);
        respawnText.gameObject.SetActive(true);
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (!go.CompareTag("MainCamera") && !go.CompareTag("GameController") && !go.CompareTag("UI") && !go.CompareTag("Border")){
                    Debug.Log(go.name);
                    Destroy(go);
            }
        }

        SetLivesText();

        yield return new WaitForSeconds(resetDelay);
        deathText.gameObject.SetActive(false);
        respawnText.gameObject.SetActive(false);

        SpawnPlayer();

        Debug.Log("Coroutine ended!");
        
    }

    private Vector2 GetRandomPosition()
    {
        // Use the existing random position methods to decide the position
        int randomSide = UnityEngine.Random.Range(1, 5); // Choose a side to spawn from (1 to 4)
        switch (randomSide)
        {
            case 1: // Left
                dir = 'r';
                return astPrefab1.RandomPositionLeft();
            case 2: // Right
                dir = 'l';
                return astPrefab1.RandomPositionRight();
            case 3: // Top
                dir = 'd';
                return astPrefab1.RandomPositionTop();
            case 4: // Bottom
                dir = 'u';
                return astPrefab1.RandomPositionBottom();
            default:
                return Vector2.zero; // Fallback
        }
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + lives;
    }
    
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPos);
        player = FindObjectOfType<Player>();
    }
}
