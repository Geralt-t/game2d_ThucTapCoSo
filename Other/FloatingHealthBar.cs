using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void UpdateHealthBar(float currentHealth,float maxHealth)
    { 
        slider.value = currentHealth/maxHealth;
    }
    private void Update()
    {
        
    }
}
