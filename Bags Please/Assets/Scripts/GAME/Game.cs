using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("FinalPartida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("BotonSalida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void modifyQuantity()
    {

    }

    public void buyProducts()
    {

    }
}
