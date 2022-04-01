using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] public static float playerHealth = 100;

    [SerializeField] TMP_Text textHP;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (PlayerHealth.playerHealth <= 0)
        {
            PlayerHealth.playerHealth = 100;
            SceneManager.LoadScene(0); // restart scene 
        }
        textHP.SetText("HP " + playerHealth.ToString());
    }
    public void GetDamage(float damage)
    {
        playerHealth -= damage;         
    }
   
}
