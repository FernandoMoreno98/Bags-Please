﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Holds resources for the Agent and WareHouse.
 */
public class BackpackComponent : MonoBehaviour
{
    public int maxAmount;
    public List<Alimento.enAlimentos> alimentos = new List<Alimento.enAlimentos>();
    public Dictionary<Alimento.enAlimentos, int> dictionary;

    public void Start()
    {
        ToDictionary();
    }
    public bool HasFood()
    {
        return this.alimentos.Count > 0;
    }

    public bool isFull()
    {
        if (alimentos.Count == maxAmount)
            return true;
        return false;
    }

    public void Vaciar()
    {
        alimentos.Clear();
        ToDictionary();
    }
    public int FoodAmount(Alimento.enAlimentos a)
    {
       return dictionary[a];
    }

    //Devuelve la maxima cantidad que se puede rellenar
    public int canFill(Alimento.enAlimentos a, int amount)
    {
        ToDictionary();
        int aux = amount;
        int aux2 = alimentos.Count;
        while (maxAmount > aux2 && aux>0)
        {
            //alimentos.Add(a);
            aux--;
            aux2++;
        }
        //ToDictionary();
        return amount - aux;
    }

    public int Fill(Alimento.enAlimentos a, int amount)
    {
        ToDictionary();
        int cFill = canFill(a,amount);
        
        for (int i =0; i < cFill;i++)
        {
            alimentos.Add(a);
        }
       //Actualiza el diccionario
        return cFill;
    }
    //Devuelve la maxima cantidad que se puede tomar 
    public int canTake(Alimento.enAlimentos a, int amount)
    {
        ToDictionary();
        int c = dictionary[a];
        int aux = 0;
        while (c > 0 && aux<amount)
        {
            aux++;
            c--;
        }
        //Debug.LogError("Can take: " + aux);
        return aux;
        
    }

    public int Take(Alimento.enAlimentos a, int amount)
    {
        ToDictionary();
        int cTake = canTake(a, amount);
        for (int i = 0; i < cTake; i++)
        {
            alimentos.Remove(a);
        }
        return cTake;
    }

    public void ToDictionary()
    {
       this.dictionary = alimentos.GroupBy(str => str)
          .ToDictionary(group => group.Key, group => group.Count());
    }

    public bool HasFood(Alimento.enAlimentos a)
    {
        return dictionary.ContainsKey(a);
    }

    public List<Alimento.enAlimentos> KindsOfFoodAvailable()
    {
        ToDictionary();
        return dictionary.Keys.ToList();
    }

    public void Add(Alimento.enAlimentos a,int amount)
    {
        if (maxAmount > alimentos.Count)
        {
            alimentos.Add(a);
            ToDictionary();
        }
      
    }
}

