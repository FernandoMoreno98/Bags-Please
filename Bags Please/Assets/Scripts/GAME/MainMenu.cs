using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("InicioPartida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Settings()
    {
        FindObjectOfType<AudioManager>().Play("BotonEntrada");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Contact()
    {
        FindObjectOfType<AudioManager>().Play("BotonEntrada");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
