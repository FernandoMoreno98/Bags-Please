using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Alimento
{
    public enum enAlimentos
    {
        Kiwis,
        Tomatoes,
        Bananas,
        Apples,
        Grapes,
        Yogurts,
        IceCreams,
        WineBottles,
        Pumpkins,
        Chips_Bags,
        Cakes,
        Eggs,
        Pizzas,
        Hamburguers,
        Hams,
        WaterMelons
    };

    public static Dictionary<enAlimentos, float> Alimentos_MaxAmountEstante = new Dictionary<enAlimentos, float>();

    static void Main(string[] args)
    {
       foreach(enAlimentos e in Enum.GetValues(typeof(enAlimentos)))
        {
            Alimentos_MaxAmountEstante.Add(e, 3);//DEBUG SE LE PONE MAXIMA CANTIDAD 3 A TODOS , ESTO CAMBIARA A POSTERIORI
        }
    }
}