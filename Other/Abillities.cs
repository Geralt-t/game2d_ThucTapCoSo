using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abillities : MonoBehaviour
{
    [SerializeField] Image image1;
    public PlayerController playerController;
    private bool isCooldown = false;
    public KeyCode abillity1;
    private void Start()
    {
        image1.fillAmount = 0;
    }
    void Update()
    {
        Ability1();
    }
    void Ability1()
    {
        if(Input.GetKey(abillity1)&& isCooldown==false&& playerController.isDashing)
        {
            isCooldown = true;
            image1.fillAmount = 1;
        }
        if(isCooldown)
        {
            image1.fillAmount-=1/playerController.dashCooldown*Time.deltaTime;
            if(image1.fillAmount<=0)
            {
                image1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
