using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] List<GameObject> butons;
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

    public void buyProducts(Alimento.enAlimentos a)
    {

        WareHouseComponent[] wareHouses = FindObjectsOfType(typeof(WareHouseComponent)) as WareHouseComponent[];
        WareHouseComponent closest = wareHouses[0]; //A priori solo hay un warehouse en el supermercado
        closest.GetComponent<BackpackComponent>().Add(a, 1);
    }

    void Start()
    {
        for(int i = 0; i<Alimento.enAlimentos.GetNames(typeof(Alimento.enAlimentos)).Length;i++)
        {
            if (i < butons.Count)
            {
                Debug.LogError(i);
                butons[i].GetComponent<Button>().onClick.AddListener(() => buyProducts((Alimento.enAlimentos)i));
            }
            
        }
    }
}
