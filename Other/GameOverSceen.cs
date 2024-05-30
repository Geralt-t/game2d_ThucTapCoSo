using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceen : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject Boss;
    public GameObject player;
    public PlayerController pc;
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        pc.PlayerIsDead = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        gameOverScreen.SetActive(false);
    }
    private void Update()
    {
        if (!player.activeSelf|| !Boss.activeSelf)
        {
            
            gameOverScreen.SetActive(true);
        }

        
    }
}
