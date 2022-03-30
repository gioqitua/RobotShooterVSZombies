using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public static float playerStartHealth = 100;

    [SerializeField] TMP_Text textHP;
    void Update()
    {
        if (PlayerHealth.playerStartHealth <= 0)
        {
            PlayerHealth.playerStartHealth = 100;
            SceneManager.LoadScene(0); // restart scene 
        }
        textHP.SetText("HP " + playerStartHealth.ToString());
    }

}
