using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // public GameManager gm;
    // public Settings sm;

    public int spawnSet;
    public int typesSet;
    public int livesSet;
    public String currentSceneName;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnSet);
        SetCurrentScene();
        // sm = FindObjectOfType<Settings>();
    }

    public void SetCurrentScene()
    {
        string curr = SceneManager.GetActiveScene().name;
        if (curr != currentSceneName) currentSceneName = curr;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        TransferSettings();
        SetCurrentScene();
    }

    public void ReturnToMenu()
    {
        // if (gm.isActive)
        // {
        //     Time.timeScale = 0;
        // }
        SceneManager.LoadScene("Menu");
        buffer();
        TransferSettings();
        SetCurrentScene();
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("Controls");
        TransferSettings();
        SetCurrentScene();
    }

    public void OpenSettings()
    {
        string curr = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Settings");
        buffer();
        if (curr == "Game") {
            GameObject.Find("Back to Game Button").SetActive(true);
        } else{ GameObject.Find("Menu Button").SetActive(true); }
        TransferSettings();
        SetCurrentScene();
    }

    public void TransferSettings()
    {
        StartCoroutine(buffer());
        UIManager newUm = FindObjectOfType<UIManager>();

        
        Debug.Log("status: " + newUm != null);
        Debug.Log(this.gameObject == newUm.gameObject);
        Debug.Log(newUm.spawnSet);
        newUm.spawnSet = spawnSet;
        Debug.Log(newUm.spawnSet);
        newUm.typesSet = typesSet;
        newUm.livesSet = livesSet;
    }

    IEnumerator buffer()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
