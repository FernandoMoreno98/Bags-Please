using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("BotonSalida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void Replay()
    {
        FindObjectOfType<AudioManager>().Play("InicioPartida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
