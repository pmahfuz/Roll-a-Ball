using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // This method will be called when the Restart button is clicked
    public void Restart()
    {   
        // Load the first scene (assuming it's the main game scene)
        SceneManager.LoadScene("Minigame");
    }
}
