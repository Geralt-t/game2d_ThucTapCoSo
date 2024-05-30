using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
   [SerializeField] AudioSource audioSource;
    public AudioClip backGroundMenu;
    public AudioClip backGroundLevel1;
    public AudioClip backGroundLevel2;
   
    string sceneName;
    private bool isAudioPlaying = false;

    private void Start()
    {
        
        Scene currentScene=SceneManager.GetActiveScene();
         sceneName=currentScene.name;
        if (sceneName == "SampleScene")
        {
            audioSource.clip = backGroundLevel1;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = backGroundMenu;
            audioSource.Play();
        }
    }
   
}
