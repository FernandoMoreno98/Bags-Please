using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Client : Labourer
{
    public List<Alimento.enAlimentos> listaDelaCompra;
    public Dictionary<Alimento.enAlimentos, int> listaDelaCompraDictionary;
    public float maxFoodToBuy = 8;
    public float minFoodToBuy = 4;
    public float minFoodToBuyOfCertainProduct = 1;
    public float maxFoodToBuyOfCertainProduct = 2;
    public int currentFoodOnList = 0;

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        if (listaDelaCompra.Count == 0)
        {
            goal.Add(new KeyValuePair<string, object>("Irse",true));
        }
        else
        {
            goal.Add(new KeyValuePair<string, object>("completingList", true));
        }    
        return goal;
    }

    private void Awake()
    {
        GenerateBuyList();
    }

    void Start()
    {
        backpack = GetComponent<BackpackComponent>();
        if (backpack == null)
            backpack = gameObject.AddComponent<BackpackComponent>() as BackpackComponent;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public override HashSet<KeyValuePair<string, object>> getWorldState()
    {

        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("hasFood", (backpack.HasFood())));
        return worldData;
    }

    void GenerateBuyList()
    {
        currentFoodOnList = 0;
        int numFood = (int) Random.Range(minFoodToBuy, maxFoodToBuy);//Numero de articulos que desea comprar.
        listaDelaCompra = new List<Alimento.enAlimentos>();

        while (currentFoodOnList < numFood)
        {
            System.Array values = Alimento.enAlimentos.GetValues(typeof(Alimento.enAlimentos));
            System.Random random = new System.Random();
            Alimento.enAlimentos randomBar = (Alimento.enAlimentos)values.GetValue(Random.Range(0,values.Length));

            int amountToBuy = Mathf.RoundToInt( Random.Range(minFoodToBuyOfCertainProduct, maxFoodToBuyOfCertainProduct));
            for(int i = 0; i< amountToBuy; i++)
            {
                listaDelaCompra.Add(randomBar);
                currentFoodOnList++;
            }
        }

        ToDictionary();
    }

    public void ToDictionary()
    {
        this.listaDelaCompraDictionary = listaDelaCompra.GroupBy(str => str)
           .ToDictionary(group => group.Key, group => group.Count());
    }

    //Actualiza la lista borrando los articulos tomados
    public void updateBuyList(Alimento.enAlimentos a, int amount)
    {
        for(int j = 0; j< amount; j++)
        {
            listaDelaCompra.Remove(a);
        }
        ToDictionary();
    }
}
