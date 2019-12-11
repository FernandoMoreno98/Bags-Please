﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Alimento
{
    public enum enAlimentos
    {
        Kiwi = 1,
        Tomato = 2,
        Banana = 3,
        Apple = 4,
        Grape = 5,
        Yogurt = 6,
        IceCream = 7,
        Wine_Bottle = 8,
        Pumpkin = 9,
        Chips_Bag = 10,
        Cake = 11,
        Eggs = 12,
        Pizza = 13,
        Hamburger = 14,
        Ham = 15,
        Watermelon = 16
    };

    public static Dictionary<enAlimentos, float> Alimentos_MaxAmountEstante = new Dictionary<enAlimentos, float>();

    /*
    static void Main(string[] args)
    {
       
    }
    */
    public static void setAlimentos_MaxAmountEstante()
    {
        foreach (enAlimentos e in Enum.GetValues(typeof(enAlimentos)))
        {
            if(!Alimentos_MaxAmountEstante.ContainsKey(e))
            Alimentos_MaxAmountEstante.Add(e, 3);//DEBUG SE LE PONE MAXIMA CANTIDAD 3 A TODOS , ESTO CAMBIARA A POSTERIORI
        }
    }

    public static GameObject GetAlimentoPrefab(enAlimentos name)
    {
        Debug.Log(name.ToString());
        GameObject alimento = (GameObject) Resources.Load(name.ToString(), typeof(GameObject));

        //AJUSTAMOS LA ESCALA Y ROTACION DEL GAMEOBJECT (ES DECIR COPIAMOS EL TRANSFORM)
        alimento.transform.localScale = Alimento.GetFoodExample(name).lossyScale;
        alimento.transform.rotation = Alimento.GetFoodExample(name).rotation;
        Debug.Log(alimento.ToString());
        return alimento;
    }

    //Devuelve un ejemplo de como debe estar colocado y escalado un alimento 
    public static Transform GetFoodExample(enAlimentos name)
    {
        GameObject Alimento = GameObject.FindGameObjectWithTag("AlimentoExamples");
        Debug.Log(name.ToString() + "s");
        Transform child = Alimento.transform.Find(name.ToString() + "s");
        Transform example = child.GetComponentInChildren<FoodComponent>().transform;
        return example;
    }

}

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }
    }
}