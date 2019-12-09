using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contact : MonoBehaviour
{

    public void ReturnMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("BotonSalida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
