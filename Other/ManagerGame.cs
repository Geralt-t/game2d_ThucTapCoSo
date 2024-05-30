using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
   
    public GameObject Boss;
    public GameObject PauseMenu;
    public GameObject GameOverScreen;
    private void Start()
    {
        Boss.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
    }
   
}
